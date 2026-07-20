using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramsha.Application.Dtos.Baskets;

public record BasketDto(
	Guid Id,
	List<BasketItemDto> Items,
	string Buyer,
	string? ClientSecret
	);

