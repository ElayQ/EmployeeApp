using Domain.Entity;
using Domain.Enum;
using Service.Interfaces;

namespace CLI.Controllers;

public class EmployeeController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    private void GetMenu()
    {
        Console.WriteLine("=========================================\n" +
                          "1. Get all employees\n" +
                          "2. Find employee by name\n" +
                          "3. Create new employee\n" +
                          "4. Update employee by name\n" +
                          "5. Delete employee by name\n" +
                          "6. Calculate employee salary\n" +
                          "0. Exit\n" +
                          "=========================================\n" +
                          "Enter command:"
        );
    }

    private void GetReturnMenu()
    {
        Console.WriteLine("Enter 'r' to return to menu");
    }
    
    private async Task GetAllEmployees()
    {
        var response = await _employeeService.GetAllEmployees();
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }

        int i = 1;
        foreach (var employee in response.Data)
        {
            Console.WriteLine($"=========================================\n" +
                              $"Employee number {i++}\n" +
                              $"=========================================\n" +
                              $"Name: {employee.Name}\n" +
                              $"Age: {employee.Age}\n" +
                              $"Appointment: {employee.Appointment}\n" +
                              $"Salary: {employee.Salary}\n" +
                              $"EmploymentDate: {employee.EmploymentDate:dd.MM.yyyy}\n" +
                              $"=========================================");
        }
    }
    private async Task GetEmployeeByName()
    {
        Console.WriteLine("Name: ");
        var name = "";
        while (true)
        {
            name = Console.ReadLine();
            if (name != "") break;
            Console.WriteLine("Name can't be empty");
        }

        var response = await _employeeService.GetEmployeeByName(name);
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }
        Console.WriteLine($"=========================================\n" +
                          $"Finded by name {response.Data.Name} employee:\n" +
                          $"=========================================\n" +
                          $"Name: {response.Data.Name}\n" +
                          $"Age: {response.Data.Age}\n" +
                          $"Appointment: {response.Data.Appointment}\n" +
                          $"Salary: {response.Data.Salary}\n" +
                          $"EmploymentDate: {response.Data.EmploymentDate:dd.MM.yyyy}" +
                          $"=========================================");
        
    }

    private async Task CreateNewEmployee()
    {
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Age:");
        var age = int.Parse(Console.ReadLine());
        Console.WriteLine("Appointment:");
        Appointments appointment = Enum.Parse<Appointments>(Console.ReadLine());
        Console.WriteLine("Salary:");
        var salary = int.Parse(Console.ReadLine());
        Console.WriteLine("EmploymentDate:");
        DateTime employmentDate = Convert.ToDateTime(Console.ReadLine());

        var response = await _employeeService.CreateNewEmployee(new Employee()
        {
            Name = name,
            Age = age,
            Appointment = appointment,
            Salary = salary,
            EmploymentDate = employmentDate
        });
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }
        Console.WriteLine($"=========================================\n" +
                          $"Created employee:\n" +
                          $"=========================================\n" +
                          $"Name: {response.Data.Name}\n" +
                          $"Age: {response.Data.Age}\n" +
                          $"Appointment: {response.Data.Appointment}\n" +
                          $"Salary: {response.Data.Salary}\n" +
                          $"EmploymentDate: {response.Data.EmploymentDate:dd.MM.yyyy}" +
                          $"=========================================");
    }
    
    
    private async Task UpdateExistingEmployee()
    {
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("New data:");
        Console.WriteLine("Age:");
        var updAge = int.Parse(Console.ReadLine());
        Console.WriteLine("Appointment:");
        Appointments updAppointment = Enum.Parse<Appointments>(Console.ReadLine());
        Console.WriteLine("Salary:");
        var updSalary = int.Parse(Console.ReadLine());
        Console.WriteLine("EmploymentDate:");
        DateTime updEmploymentDate = Convert.ToDateTime(Console.ReadLine());

        var response = await _employeeService.UpdateExistingEmployee(name, new Employee()
        {
            Name = name,
            Age = updAge,
            Appointment = updAppointment,
            Salary = updSalary,
            EmploymentDate = updEmploymentDate
        });
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }
        Console.WriteLine($"=========================================\n" +
                          $"Updated employee:\n" +
                          $"=========================================\n" +
                          $"Name: {response.Data.Name}\n" +
                          $"Age: {response.Data.Age}\n" +
                          $"Appointment: {response.Data.Appointment}\n" +
                          $"Salary: {response.Data.Salary}\n" +
                          $"EmploymentDate: {response.Data.EmploymentDate:dd.MM.yyyy}" +
                          $"=========================================");
    }
    
    private async Task DeleteEmployeeByName()
    {
        Console.WriteLine("Name: ");
        var firstResponse = await _employeeService.GetEmployeeByName(Console.ReadLine());
        if (firstResponse.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(firstResponse.Description);
            return;
        }
        var response = await _employeeService.DeleteEmployeeByName(firstResponse.Data.Name);
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }
        Console.WriteLine($"=========================================\n" +
                          $"Deleted employee:\n" +
                          $"=========================================\n" +
                          $"Name: {response.Data.Name}\n" +
                          $"Age: {response.Data.Age}\n" +
                          $"Appointment: {response.Data.Appointment}\n" +
                          $"Salary: {response.Data.Salary}\n" +
                          $"EmploymentDate: {response.Data.EmploymentDate:dd.MM.yyyy}" +
                          $"=========================================");
    }

    private async Task SalaryPeriodEmployeeCountByName()
    {
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Start date: ");
        var startDate = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("End date: ");
        var endDate = Convert.ToDateTime(Console.ReadLine());
        var response = await _employeeService.SalaryPeriodCount(name, startDate, endDate);
        if (response.StatusCode != StatusCodes.Ok)
        {
            Console.WriteLine(response.Description);
            return;
        }
        Console.WriteLine($"Employee name: {response.Data.Name}.\n" +
                          $"Current employee salary: {response.Data.Salary}.\n" +
                          $"Period: {startDate:dd.MM.yyyy}-{endDate:dd.MM.yyyy}.\n" +
                          $"Salary in this period: {response.SalaryPeriodCount}");
    }
    
    public void EmployeeMenu()
    {
        GetMenu();
        while (true)
        {
            var menuCase = Console.ReadLine().ToCharArray()[0];
            switch (menuCase)
            {
                case '1':
                    GetAllEmployees().Wait();
                    GetReturnMenu();
                    break;
                case '2':
                    GetEmployeeByName().Wait();
                    GetReturnMenu();
                    break;
                case '3':
                    CreateNewEmployee().Wait();
                    GetReturnMenu();
                    break;
                case '4':
                    UpdateExistingEmployee().Wait();
                    GetReturnMenu();
                    break;
                case '5':
                    DeleteEmployeeByName().Wait();
                    GetReturnMenu();
                    break;
                case '6':
                    SalaryPeriodEmployeeCountByName().Wait();
                    GetReturnMenu();
                    break;
                case 'r':
                    GetMenu();
                    break;
                default:
                    Console.WriteLine("Incorrect input. Try again.");
                    break;
            }
        }
    }
}