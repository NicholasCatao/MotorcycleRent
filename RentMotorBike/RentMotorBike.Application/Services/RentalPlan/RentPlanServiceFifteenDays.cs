using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanServiceFifteenDays : IRentPlanCalcService
{
    private const int DayCost = 30;
    private const double Taxfee = 0.2;

    public Task CalcPlanCostAsync(Rent rent)
    {
        var rentEnd = rent.InitialDate.AddDays(15);
        var daysRemaining = (rentEnd - rent.FinalDate).TotalDays;

        rent.Cost = Convert.ToDecimal(DayCost * (int)RentPlan.FIFTEEN);
        rent.Fee = Convert.ToDecimal((int)RentPlan.FIFTEEN * Taxfee);

        if (daysRemaining < (int)RentPlan.FIFTEEN)
            rent.Cost = Convert.ToDecimal((DayCost * (int)RentPlan.FIFTEEN) * Taxfee);

        return Task.CompletedTask;
    }
}
