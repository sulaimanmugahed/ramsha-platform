using Asp.Versioning;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Features.Common.Commands.AddNewOption;
using Ramsha.Application.Features.Common.Commands.RemoveOption;
using Ramsha.Application.Features.Common.Queries.GetOption;
using Ramsha.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Ramsha.Api.Controllers.v1;

/// <summary>
/// Manages options-related operations.
/// </summary>
[ApiVersion("1.0")]
public class OptionsController : BaseApiController
{
    /// <summary>
    /// Retrieves all options.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of all available options in the system.
    /// </remarks>
    [HttpGet]
    public async Task<ActionResult<List<OptionDto?>>> GetOptions()
        => await Query(new GetOptionsQuery());

    /// <summary>
    /// Retrieves a specific option by its ID.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the details of an option identified by its unique ID.
    /// </remarks>
    [HttpGet("{id}", Name = nameof(GetOption))]
    public async Task<ActionResult<OptionDto?>> GetOption(Guid id)
        => await Query(new GetOptionQuery { Id = id });

    /// <summary>
    /// Deletes an option by its ID.
    /// </summary>
    /// <remarks>
    /// This endpoint deletes an option identified by its unique ID.
    /// </remarks>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOption(Guid id)
        => await Command(new RemoveOptionCommand { OptionId = id });

    /// <summary>
    /// Adds a new option.
    /// </summary>
    /// <remarks>
    /// This endpoint creates a new option with the provided details.
    /// Returns the ID of the newly created option and a link to retrieve it.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<ActionResult<string>>> AddNewOption(AddNewOptionCommand command)
    => await Command(command);
}