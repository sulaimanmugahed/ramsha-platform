using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ramsha.Application.Contracts;

namespace Ramsha.FileStorage;

public class SupabaseStorageService : IStorageService
{
    private readonly Supabase.Client _supabaseClient;
    private readonly string _bucketName;

    public SupabaseStorageService(IOptions<SupabaseStorageSettings> settings)
    {
        _supabaseClient = new Supabase.Client(
            settings.Value.Url,
            settings.Value.ApiKey,
            new Supabase.SupabaseOptions
            {
                AutoRefreshToken = true
            });
        _bucketName = settings.Value.BucketName;
    }

    // Sanitize filename to only allow valid Supabase Storage characters
    private string SanitizeFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return Guid.NewGuid().ToString();

        // Get the file extension
        var extension = Path.GetExtension(fileName);
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

        // Remove any non-ASCII characters and replace with underscore
        // This handles Arabic, Chinese, Hebrew, accented characters, etc.
        var sanitizedName = Regex.Replace(nameWithoutExt, @"[^\x00-\x7F]+", "_");

        // Replace other invalid characters with underscore
        // Invalid: ! * ' ( ) ; : @ & = + $ , / ? % # [ ] 
        sanitizedName = Regex.Replace(sanitizedName, @"[!*'();:@&=+$,/?%#\[\]]", "_");

        // Replace spaces with underscores
        sanitizedName = sanitizedName.Replace(' ', '_');

        // Remove consecutive underscores
        sanitizedName = Regex.Replace(sanitizedName, @"_+", "_");

        // Trim leading/trailing underscores and dots
        sanitizedName = sanitizedName.Trim('_', '.');

        // Ensure we don't end up with empty string
        if (string.IsNullOrEmpty(sanitizedName))
            sanitizedName = Guid.NewGuid().ToString();

        // Add a timestamp or GUID to ensure uniqueness
        var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);

        // Combine with extension
        return $"{sanitizedName}_{uniqueId}{extension}";
    }

    public async Task<string?> UploadFile(IFormFile file, string folderPath = null)
    {
        try
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            // Sanitize the original filename
            var sanitizedFileName = SanitizeFileName(file.FileName);

            // Build the object path
            var objectPath = string.IsNullOrEmpty(folderPath)
                ? sanitizedFileName
                : $"{folderPath}/{sanitizedFileName}";

            var bucket = _supabaseClient.Storage.From(_bucketName);

            await bucket.Upload(fileBytes, objectPath, new Supabase.Storage.FileOptions
            {
                ContentType = file.ContentType,
                Upsert = false
            });

            return objectPath;
        }
        catch (Exception ex)
        {
            // Log the exception if you have logging
            return null;
        }
    }

    public async Task<bool> DeleteFile(string objectPath)
    {
        try
        {
            var bucket = _supabaseClient.Storage.From(_bucketName);
            await bucket.Remove(objectPath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string GetImageUrl(string objectPath)
    {
        var bucket = _supabaseClient.Storage.From(_bucketName);
        return bucket.GetPublicUrl(objectPath);
    }
}