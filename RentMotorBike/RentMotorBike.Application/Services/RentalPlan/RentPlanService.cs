using Microsoft.Extensions.Logging;
using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanService(ILogger<RentPlanService> logger) : IRentPlanService
{
    private readonly ILogger _logger = logger;
    public async Task CalcPlanCostAsync(Rent rent) =>
        await CalcService(rent).CalcPlanCostAsync(rent);

    private IRentPlanCalcService CalcService(Rent rent)
    {
        _logger.LogInformation("Chosing the calcule strategy:{0}", rent.RentPlan.ToString());

        var calc = new Dictionary<RentPlan, IRentPlanCalcService>
        {
            { RentPlan.SEVEN, new RentPlanServiceSevenDays() },
            { RentPlan.FIFTEEN, new RentPlanServiceFifteenDays() },
            { RentPlan.THIRTY, new RentPlanServiceThirtyDays() }
        };

        return calc.GetValueOrDefault(rent.RentPlan)
            ?? throw new ArgumentNullException(nameof(IRentPlanCalcService), "Service not found.");
    }
}
