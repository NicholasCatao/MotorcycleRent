using RentMotorBike.Domain.Entities;

namespace RentMotorBike.Domain.Abstractions.Services;

public interface IRentPlanService
{
    Task CalcPlanCost(Rent rent);
}
