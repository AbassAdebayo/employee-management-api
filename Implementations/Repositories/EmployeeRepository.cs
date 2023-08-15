using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeContext _employeeContext;

    public EmployeeRepository(EmployeeContext employeeContext)
    {
        _employeeContext = employeeContext;
    }

    public async Task<Employee> CreateEmployee(Employee Employee)
    {
        await _employeeContext.Employees.AddAsync(Employee);
        await _employeeContext.SaveChangesAsync();
        return Employee;

    }

    public async Task<bool> DeleteEmployee(Guid EmployeeId)
    {
        var Employee  = await _employeeContext.Employees.FirstOrDefaultAsync(id => id.Id == EmployeeId);
          _employeeContext.Employees.Remove(Employee);
          await _employeeContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeContext.Employees.Select(e => new Employee
        {
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            Id = e.Id,
            Address = e.Address,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Gender = e.Gender,
            PhoneNumber = e.PhoneNumber,

        }).OrderBy(t => t.FirstName)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(Guid EmployeeId)
    {
        return 
        await _employeeContext.Employees.FirstOrDefaultAsync(id => id.Id == EmployeeId);
    }

    public async Task<bool> EmployeeExistsByEmailAsync(string email)
    {
        return await _employeeContext
                .Employees.AnyAsync(emp => emp.Email == email);
    }

    public async Task<Employee> UpdateEmployee(Guid EmployeeId, Employee employee)
    {
        var getId = await _employeeContext.Employees.FirstOrDefaultAsync(id => id.Id == EmployeeId);
        _employeeContext.Employees.Update(getId);
        await _employeeContext.SaveChangesAsync();
        return getId;

    }

    public async Task<int> EmployeeCount()
    {
        return await _employeeContext.Employees.CountAsync();
      
    }
}