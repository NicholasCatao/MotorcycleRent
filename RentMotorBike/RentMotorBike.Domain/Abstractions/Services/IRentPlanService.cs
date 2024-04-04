using RentMotorBike.Domain.Enums;

namespace RentMotorBike.Domain.Abstractions.Services;

public interface IRentPlanService
{
    Task<(decimal cost, decimal? fee)> GetPlanCost(RentPlan rentPlan);
}
