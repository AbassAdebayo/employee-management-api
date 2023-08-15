public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    public async Task<BaseResponse<bool>> CreateEmployee(CreateEmployeeRequestModel model)
    {
        // Check if employee already exist
        var checkIFEmployeeExist = await _employeeRepository.EmployeeExistsByEmailAsync(model.Email);
        if(checkIFEmployeeExist) return new BaseResponse<bool> {Message = $"Employee with email {model.Email} already exist!", 
            Status = false

        };

        // Check if model (input) is not null
        if (model == null)
        {
                throw new ArgumentNullException(nameof(model));
        }

        // If the employee does not exist and the model is not null, create new employee
        var employee = new Employee
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            Gender  = model.Gender,
            CreatedAt = DateTime.UtcNow
        };
       var newEmployee = await _employeeRepository.CreateEmployee(employee);

       // Check if employee was created successfully
       if(newEmployee is null) return new BaseResponse<bool> {Message = "Employee creation unsuccessful", Status = false};

       // Log a message for the event
       _logger.LogWarning($"Employee created successfully");
       return new BaseResponse<bool> 
       {
            Message = $"Employee created successfully",
            Status = true,
       };
    }

     public async Task<BaseResponse<bool>> DeleteEmployee(Guid employeeId)
    {
       var getEmployee = await _employeeRepository.GetEmployeeById(employeeId);
        if(getEmployee is null) return new BaseResponse<bool> {Message = $"Employee with Id {employeeId} doesn't exist", Status = false};

         await _employeeRepository.DeleteEmployee(employeeId);
         _logger.LogWarning($"The employee with Id {employeeId} has been deleted successfully");
        return new BaseResponse<bool> 
       {
            Message = $"The employee with Id {employeeId} has been deleted successfully",
            Status = true,
       };
    }

    public async Task<BaseResponse<int>> GetEmployeeCountAsync()
    {
        var checkCount = await _employeeRepository.EmployeeCount();
        return checkCount != 0 
            ? new BaseResponse<int> {Message = "Employee count retrieved", Status = true, Data = checkCount} 
            : new BaseResponse<int> {Message = "Count cannot be empty!", Status = false, Data = checkCount};

    }

    public async Task<BaseResponse<bool>> EmployeeExistsByEmailAsync(string email)
    {
       var checkIFEmployeeExist = await _employeeRepository.EmployeeExistsByEmailAsync(email);
       if(checkIFEmployeeExist is false)  return new BaseResponse<bool> {Message = $"Employee with email {email} does not exist.", 
            Status = false

        };
       return new BaseResponse<bool> {Message = $"Employee with email {email} already exist!", Status = true};
    }


    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    { 
      var employees = await _employeeRepository.GetAllEmployeesAsync();
      
      return employees.Select(employee => new Employee
      {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        PhoneNumber = employee.PhoneNumber,
        Address = employee.Address,
        Gender  = employee.Gender,
        CreatedAt = employee.CreatedAt,
        UpdatedAt  = employee.UpdatedAt
      });
    }

    public async Task<BaseResponse<Employee>> GetEmployeeById(Guid employeeId)
    {
        // Fetch employee
        var getEmployee = await _employeeRepository.GetEmployeeById(employeeId);
        if(getEmployee is null)
        {
               
            return new BaseResponse<Employee>
            {
                Message = $"Employee with Id {employeeId} doesn't exist",
                Status  = false
            };
        }
         _logger.LogWarning($"Employee with Id '{employeeId}' fetched successfully");
        return new BaseResponse<Employee>
        {
            Status = true,
            Data = new Employee
            {
                Id = getEmployee.Id,
                FirstName = getEmployee.FirstName,
                LastName = getEmployee.LastName,
                Email = getEmployee.Email,
                PhoneNumber = getEmployee.PhoneNumber,
                Address = getEmployee.Address,
                Gender  = getEmployee.Gender,
                CreatedAt = getEmployee.CreatedAt,
                UpdatedAt  = getEmployee.UpdatedAt

            },
            Message = $"Employee with Id '{employeeId}' fetched successfully.",
        };
    }

    public async Task<BaseResponse<bool>> UpdateEmployeeDetails(Guid employeeId, UpdateEmployeeRequestModel model)
    {
        // Fetch employee
        var fetchEmployee = await _employeeRepository.GetEmployeeById(employeeId);

        // Check if employee with the provided is not null
        if(fetchEmployee is null) return new BaseResponse<bool> {Message = $"Employee with Id {employeeId} doesn't exist", Status = false};

        // Check if input is not null
        if(model is null) return new BaseResponse<bool> {Message = $"Field cannot be left empty", Status = false};


        // Update task status
        fetchEmployee.FirstName = model.FirstName;
        fetchEmployee.LastName = model.LastName;
        fetchEmployee.Email = model.Email;
        fetchEmployee.Address = model.Address;
        fetchEmployee.PhoneNumber = model.PhoneNumber;
        fetchEmployee.Gender = model.Gender;
        fetchEmployee.UpdatedAt = DateTime.UtcNow;
        
        var update = await _employeeRepository.UpdateEmployee(fetchEmployee.Id, fetchEmployee);

        // Check if task status was updated successfully
       if(update is null) return new BaseResponse<bool> {Message = "Employee update unsuccessful", Status = false};

       // Log a message for the event
       _logger.LogWarning($"The employee with Id {employeeId} has been successfully updated");
       return new BaseResponse<bool> 
       {
            Message = $"The employee with Id {employeeId} has been successfully updated",
            Status = true,
       };
    }
}