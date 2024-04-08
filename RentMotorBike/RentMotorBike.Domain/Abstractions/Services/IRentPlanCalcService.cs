using RentMotorBike.Domain.Entities;

namespace RentMotorBike.Domain.Abstractions.Services;

public interface IRentPlanCalcService
{
    Task CalcPlanCostAsync(Rent rent);
}
