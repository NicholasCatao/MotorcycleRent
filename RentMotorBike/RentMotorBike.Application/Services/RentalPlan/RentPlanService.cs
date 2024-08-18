using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanService() : IRentPlanService
{
    public async Task CalcPlanCostAsync(Rent rent) =>
        await CalcService(rent).CalcPlanCostAsync(rent);

    private IRentPlanCalcService CalcService(Rent rent)
    {
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
