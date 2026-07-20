using Ramsha.Application.Dtos.Account.Responses;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Dtos.Customers;
using Ramsha.Application.DTOs.Account.Responses;
using Ramsha.Domain.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramsha.Application.Extensions;

public static class CustomerExtensions
{



	public static CustomerDto AsDto(this Customer customer) =>
	 new CustomerDto(customer.Id.Value, customer.FirstName, customer.LastName, customer.Username);
}
