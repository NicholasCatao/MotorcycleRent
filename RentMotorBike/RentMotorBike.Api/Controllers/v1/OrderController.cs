﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentMotorBike.Api.Controllers.Base;
using RentMotorBike.Application.Request;
using RentMotorBike.Domain.Response.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace RentMotorBike.Api.Controllers.v1;

public class OrderController : BaseController
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Notificacao))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(Notificacao))]
    public async Task<IActionResult> CreateMotorCycle([FromBody] OrderCommandRequest request)
    {
        var response = await _mediator.Send(request);

        return response.PossuiErro ? HandleError(response) : Ok(response.Dados);
    }
}
