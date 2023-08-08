using Domain.Entity;

namespace DAL.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> Get();
    Task<Employee> GetByName(string name);
    Task<Employee> Create(Employee entity);
    Task<Employee> Update(Employee entity);
    Task<Employee> Delete(Employee entity);
}