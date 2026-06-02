using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Dtos.Products;

using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.UpdateProductVariant;

public class UpdateProductVariantCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
    public List<VariantValuesCommand> VariantValuesToAdd { get; set; } = [];
    public List<VariantValuesCommand> VariantValuesToRemove { get; set; } = [];

    public List<ImageRequest> VariantImagesToAdd { get; set; } = [];
    public List<string> VariantImagesUrlToRemove { get; set; } = [];

}
