namespace RentMotorBike.Domain.Response.Base;

public class Paginacao<TDados>
{
    public int? TotalDeRegistros { get; set; }
    public TDados Dados { get; set; }
}
