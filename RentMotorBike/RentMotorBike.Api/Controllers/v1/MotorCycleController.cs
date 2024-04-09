using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentMotorBike.Api.Controllers.Base;
using RentMotorBike.Application.Request;
using RentMotorBike.Application.UseCases.MotorCycle.Queries;
using RentMotorBike.Domain.Response.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace RentMotorBike.Api.Controllers.v1;

[Route("[controller]")]
[ApiController]
public class MotorCycleController : BaseController
{
    private readonly IMediator _mediator;

    public MotorCycleController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Creates an Motorcycle
    /// </summary>		
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Notificacao))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(Notificacao))]
    public async Task<IActionResult> CreateMotorCycle([FromBody] MotorBikeCommandRequest  request)
    {
        var response = await _mediator.Send(request);

        return response.PossuiErro ? HandleError(response) : Ok(response.Dados);
    }
    /// <summary>
    /// Gets an Motorcycle
    /// </summary
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Notificacao))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(Notificacao))]
    public async Task<IActionResult> GetMotorCycle([FromQuery] int id)
    {
        var query = new GetMotorCycleByIdQuery { Id = id };

        var response = await _mediator.Send(query);

        return response.PossuiErro ? HandleError(response) : Ok(response.Dados);
    }




}

