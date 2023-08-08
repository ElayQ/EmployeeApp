using DAL.Interfaces;
using Domain.Entity;
using Domain.Enum;
using Domain.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Employee>> Get()
    {
        return await _dbContext.Employees.ToListAsync();
    }
    
    public async Task<Employee> GetByName(string name)
    { 
        return await _dbContext.Employees.FirstOrDefaultAsync(n => n.Name == name);
    }
    
    public async Task<Employee> Create(Employee entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Employee> Update(Employee entity)
    {
        _dbContext.Attach(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Employee> Delete(Employee entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}