using Microsoft.AspNetCore.Mvc;
using RentMotorBike.Domain.Enums;
using RentMotorBike.Domain.Response.Base;
using System.Net.Mime;

namespace RentMotorBike.Api.Controllers.Base;

[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase
{
    public ActionResult HandleError<T>(Response<T> response)
    {
        ObjectResult DefaultError() => StatusCode(StatusCodes.Status500InternalServerError, new Notificacao { DetalheErro = response.DetalheErro });

        return response.MotivoErro switch
        {
            MotivoErro.Conflict => Conflict(new Notificacao { DetalheErro = response.DetalheErro }),
            MotivoErro.BadRequest => BadRequest(new Notificacao { DetalheErro = response.DetalheErro }),
            MotivoErro.NotFound => NotFound(null),
            MotivoErro.NoContent => NoContent(),
            MotivoErro.InternalServerError => DefaultError(),
            _ => DefaultError()
        };
    }
}
