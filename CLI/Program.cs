using CLI.Controllers;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service.Implementations;
using Service.Interfaces;

var serviceProvider = new ServiceCollection()
    .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString:"server=VGD-DESKTOP;database=Employees;Integrated Security=True;TrustServerCertificate=True"))
    .AddScoped<IEmployeeRepository, EmployeeRepository>()
    .AddScoped<IEmployeeService, EmployeeService>()
    //.AddSingleton<EmployeeController>()
    .BuildServiceProvider();
    
new EmployeeController(serviceProvider.GetService<IEmployeeService>()).EmployeeMenu();