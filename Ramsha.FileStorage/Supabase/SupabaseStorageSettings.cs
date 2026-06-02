using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ramsha.FileStorage
{
    public class SupabaseStorageSettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string BucketName { get; set; }
    }
}