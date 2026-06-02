using Ramsha.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Ramsha.AspNetCore.Mvc;

namespace Ramsha.Api.Controllers;



[ControllerRoutePrefix("api/v{version:apiVersion}/")]
public class BaseApiController : RamshaApiController
{

}
