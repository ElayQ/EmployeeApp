using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Response;
using Service.Interfaces;

namespace Service.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IBaseResponse<List<Employee>>> GetAllEmployees()
    {
        var baseResponse = new BaseResponse<List<Employee>>();
        try
        {
            var employees = await _employeeRepository.Get();
            if (employees.Count == 0)
            {
                baseResponse.StatusCode = StatusCodes.NoContent;
                baseResponse.Description = "Employees database is empty";
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCodes.Ok;
            baseResponse.Data = employees;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Employee>>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[GetAllEmployees] : {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Employee>> GetEmployeeByName(string name)
    {
        var baseResponse = new BaseResponse<Employee>();
        try
        {
            var existingEmployee = await _employeeRepository.GetByName(name);
            if (existingEmployee == null)
            {
                baseResponse.StatusCode = StatusCodes.NoContent;
                baseResponse.Description = $"Employee with name '{name}' not found";
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCodes.Ok;
            baseResponse.Data = existingEmployee;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Employee>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[GetEmployeeByName] : {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Employee>> CreateNewEmployee(Employee employee)
    {
        var baseResponse = new BaseResponse<Employee>();
        try
        {
            var checkForExistingEmployee = await _employeeRepository.GetByName(employee.Name);
            if (checkForExistingEmployee != null)
            {
                baseResponse.StatusCode = StatusCodes.Error;
                baseResponse.Description = $"Employee with name {employee.Name} already exist";
                return baseResponse;
            }
            var newEmployee = await _employeeRepository.Create(new Employee()
            {
                Name = employee.Name, Age = employee.Age, Appointment = employee.Appointment,
                Salary = employee.Salary, EmploymentDate = employee.EmploymentDate
            });
            baseResponse.Data = newEmployee;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Employee>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[CreateNewEmployee] : {ex.Message}"
            };
        }
    }
    
    
    public async Task<IBaseResponse<Employee>> UpdateExistingEmployee(string name, Employee employee)
    {
        var baseResponse = new BaseResponse<Employee>();
        try
        {
            var existingEmployee = await _employeeRepository.GetByName(name);
            if (existingEmployee == null)
            {
                baseResponse.StatusCode = StatusCodes.NoContent;
                baseResponse.Description = $"Employee with name '{name}' not found";
                return baseResponse;
            }
            existingEmployee.Age = employee.Age;
            existingEmployee.Appointment = employee.Appointment;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.EmploymentDate = employee.EmploymentDate;
            var updEmployee = await _employeeRepository.Update(existingEmployee);
            baseResponse.Data = updEmployee;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Employee>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[UpdateExistingEmployee] : {ex.Message}"
            };
        }
    }
    
    
    public async Task<IBaseResponse<Employee>> DeleteEmployeeByName(string name)
    {
        var baseResponse = new BaseResponse<Employee>();
        try
        {
            var existingEmployee = await _employeeRepository.GetByName(name);
            if (existingEmployee == null)
            {
                baseResponse.StatusCode = StatusCodes.NoContent;
                baseResponse.Description = $"Employee with name '{name}' not found";
                return baseResponse;
            }
            var deletedEmployee = await _employeeRepository.Delete(existingEmployee);
            baseResponse.Data = deletedEmployee;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Employee>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[DeleteEmployeeByName] : {ex.Message}"
            };
        }
    }
    
    public async Task<BaseResponse<Employee>> SalaryPeriodCount(string name, DateTime start, DateTime end)
    {
        var baseResponse = new BaseResponse<Employee>();
        try
        {
            var existingEmployee = await _employeeRepository.GetByName(name);
            if (existingEmployee == null)
            {
                baseResponse.StatusCode = StatusCodes.NoContent;
                baseResponse.Description = $"Employee with name '{name}' not found";
                return baseResponse;
            }
            if (start < existingEmployee.EmploymentDate)
            {
                baseResponse.StatusCode = StatusCodes.Error;
                baseResponse.Description = $"Start date '{start:dd.MM.yyyy}' can't be less than employment date '{existingEmployee.EmploymentDate:dd.MM.yyyy}'";
                return baseResponse;
            }
            if (end > DateTime.Today)
            {
                baseResponse.StatusCode = StatusCodes.Error;
                baseResponse.Description = $"End date '{start:dd.MM.yyyy}' can't be greater than current date '{DateTime.Today:dd.MM.yyyy}'";
                return baseResponse;
            }
            baseResponse.Data = existingEmployee;
            var salaryPerDay = existingEmployee.Salary / 30.0;
            var workDaysCount = (end - start).TotalDays;
            baseResponse.SalaryPeriodCount = workDaysCount * salaryPerDay;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Employee>
            {
                StatusCode = StatusCodes.Error,
                Description = $"[SalaryPeriodCount] : {ex.Message}"
            };
        }
    }
}