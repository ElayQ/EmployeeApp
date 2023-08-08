using Domain.Entity;
using Domain.Enum;
using Domain.Response;

namespace Service.Interfaces;

public interface IEmployeeService
{
    Task<IBaseResponse<List<Employee>>> GetAllEmployees();
    Task<IBaseResponse<Employee>> GetEmployeeByName(string name);
    Task<IBaseResponse<Employee>> CreateNewEmployee(Employee employee);
    Task<IBaseResponse<Employee>> UpdateExistingEmployee(string name, Employee employee);
    Task<IBaseResponse<Employee>> DeleteEmployeeByName(string name);
    Task<BaseResponse<Employee>> SalaryPeriodCount(string name, DateTime startDate, DateTime endDate);
}