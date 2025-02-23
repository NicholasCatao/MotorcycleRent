using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanService : IRentPlanService
{
    public async Task CalcPlanCostAsync(Rent rent) => await GetService(rent).CalcPlanCostAsync(rent);

    private IRentPlanCalcService GetService(Rent rentPlan)
    {
        var services = new Dictionary<RentPlan, IRentPlanCalcService>
        {
            { RentPlan.SEVEN, new RentPlanServiceSevenDays() },
            { RentPlan.FIFTEEN, new RentPlanServiceFifteenDays() },
            { RentPlan.THIRTY, new RentPlanServiceThirtyDays() },
        };

       var service =  services.GetValueOrDefault(rentPlan.RentPlan);

       if (service is null)
           throw new InvalidOperationException(nameof(rentPlan.RentPlan));

        return service;

        // return services[rentPlan];
    }
}

