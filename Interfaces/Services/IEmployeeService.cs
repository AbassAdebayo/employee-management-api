public interface IEmployeeService
{
    public Task<BaseResponse<bool>> CreateEmployee(CreateEmployeeRequestModel model);
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    public Task<BaseResponse<Employee>> GetEmployeeById(Guid employeeId);
    public Task<BaseResponse<bool>> UpdateEmployeeDetails(Guid employeeId, UpdateEmployeeRequestModel model);
    public Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId);
    public Task<BaseResponse<bool>> EmployeeExistsByEmailAsync(string email);
    public Task<BaseResponse<int>> GetEmployeeCountAsync(); 
}