using RentMotorBike.Domain.Abstractions.Services;
using RentMotorBike.Domain.Entities;
using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Application.Services.RentalPlan;

public class RentPlanService : IRentPlanService
{
    private readonly IEnumerable<IRentPlanCalcService> _rentPlanCalcService;

    public RentPlanService(IEnumerable<IRentPlanCalcService> rentPlanCalcService) => _rentPlanCalcService = rentPlanCalcService;

    private IRentPlanCalcService GetService(Rent rent)// TODO Use Dictionary
    => rent.RentPlan switch
    {
        RentPlan.SEVEN => _rentPlanCalcService.FirstOrDefault(x => x.GetType() == typeof(RentPlanServiceSevenDays)),
        RentPlan.FIFTEEN => _rentPlanCalcService.FirstOrDefault(x => x.GetType() == typeof(RentPlanServiceFifteenDays)),
        RentPlan.THIRTY => _rentPlanCalcService.FirstOrDefault(x => x.GetType() == typeof(RentPlanServiceThirtyDays)),
        _ => throw new ArgumentException("Service Type undefined")  // TODO ADD Custom Exception
    } ?? throw new ArgumentNullException(nameof(IRentPlanCalcService), "Service not found."); // TODO ADD Custom Exception

    public async Task CalcPlanCostAsync(Rent rent) => await GetService(rent).CalcPlanCostAsync(rent);
}
