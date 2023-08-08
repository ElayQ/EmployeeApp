using Domain.Enum;

namespace Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCodes StatusCode { get; set; }
    public double SalaryPeriodCount { get; set; }
    public T Data { get; set; }
}

public interface IBaseResponse<T>
{
    public string Description { get; }
    public StatusCodes StatusCode { get; }
    T Data { get; }
}