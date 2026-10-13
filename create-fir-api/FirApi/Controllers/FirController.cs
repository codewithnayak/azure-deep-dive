using System.Security.Claims;
using FirApi.Application.DTO;
using FirApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirApi.Controllers;

[ApiController]
//[Authorize]
[Route("api/v1/firs")]
public class FirController : ControllerBase
{
    private readonly IFirService _firService;

    public FirController(IFirService firService)
    {
        _firService = firService;
    }

    [HttpPost]
    public async Task<ActionResult<FirResponseDto>> CreateFir(
        [FromBody] CreateFirRequestDto request,
        CancellationToken cancellationToken)
    {
       /* var officerId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(officerId))
        {
            return Forbid();
        }*/

        var result = await _firService.CreateFirAsync(
            request,
            "officerId",
            cancellationToken);

        return CreatedAtAction(
            nameof(GetFirById),
            new { id = result.FirId },
            result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FirResponseDto>> GetFirById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _firService.GetFirByIdAsync(
            id, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}