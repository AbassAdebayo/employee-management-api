public interface IEmployeeRepository
{
    public Task<Employee> CreateEmployee(Employee Employee);
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    public Task<Employee> GetEmployeeById(Guid EmployeeId);
    public Task<Employee> UpdateEmployee(Guid EmployeeId, Employee employee);
    public Task<bool> EmployeeExistsByEmailAsync(string email);
    public Task<bool> DeleteEmployee(Guid EmployeeId);
    public Task<int> EmployeeCount(); 
     
}