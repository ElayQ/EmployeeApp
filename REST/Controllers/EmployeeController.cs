using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using StatusCodes = Domain.Enum.StatusCodes;

namespace REST.Controllers;

[Route("api/[controller]")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _employeeService.GetAllEmployees();
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(response.Data);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetEmployeeByName(string name)
    {
        var response = await _employeeService.GetEmployeeByName(name);
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewEmployee(Employee employee)
    {
        var response = await _employeeService.CreateNewEmployee(employee);
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExistingEmployee(Employee employee)
    {
        var response = await _employeeService.UpdateExistingEmployee(employee.Name, employee);
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(response.Data);
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteEmployeeByName(string name)
    {
        var response = await _employeeService.DeleteEmployeeByName(name);
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(response.Data);
    }

    [HttpGet("{name},{startDate},{endDate}")]
    public async Task<IActionResult> SalaryPeriodEmployeeCountByName(string name, DateTime startDate,
        DateTime endDate)
    {
        var response = await _employeeService.SalaryPeriodCount(name, startDate, endDate);
        if (response.StatusCode != StatusCodes.Ok)
        {
            return BadRequest(new { HttpResponseMessage = response.Description });
        }
        return Ok(new {HttpResponseMessage = $"Employee name: {response.Data.Name}. " +
                                             $"Current employee salary: {response.Data.Salary}. " +
                                             $"Period: {startDate:dd:MM:yyyy}-{endDate:dd:MM:yyyy}. " +
                                             $"Salary in this period: {response.SalaryPeriodCount:0.00}"});
    }

}