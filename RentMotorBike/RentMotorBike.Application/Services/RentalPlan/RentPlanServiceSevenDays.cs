﻿using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanServiceSevenDays : IRentPlanCalcService
{
    private const int DayCost = 28;
    private const double Taxfee = 0.4;
    public async Task CalcPlanCostAsync(Rent rent)
    {
        var rentEnd = rent.InitialDate.AddDays(15);
        var daysRemaining = (rentEnd - rent.FinalDate).TotalDays;

        rent.Cost = Convert.ToDecimal(DayCost * (int)RentPlan.FIFTEEN);
        rent.Fee = Convert.ToDecimal((int)RentPlan.FIFTEEN * Taxfee);

        if (daysRemaining < (int)RentPlan.FIFTEEN)
            rent.Cost = Convert.ToDecimal((DayCost * (int)RentPlan.FIFTEEN) * Taxfee);
    }
}
