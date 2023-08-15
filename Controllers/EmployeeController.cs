
using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[Microsoft.AspNetCore.Mvc.Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpPost("createemployee")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(CreateEmployeeRequestModel))]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestModel model)
    {
        var response = await _employeeService.CreateEmployee(model);
        return Ok(response);
    }

    [HttpGet("{employeeId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<Employee>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromRoute] Guid employeeId)
    {
        var response = await _employeeService.GetEmployeeById(employeeId);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }
    

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<Employee>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> List()
    {
        var response = await _employeeService.GetAllEmployeesAsync();
        if(response.ToList().Count < 1) return BadRequest("Employees cannot be empty!");
        return Ok(response);
    }

    [HttpGet("employee-count")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<int>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Count()
    {
        var response = await _employeeService.GetEmployeeCountAsync();
        return response.Status  ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPut("{employeeId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateEmployeeDetails([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeRequestModel model)
    {
        var response = await _employeeService.UpdateEmployeeDetails(employeeId, model);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{employeeId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
    {
        var response = await _employeeService.DeleteEmployee(employeeId);
        return response.Status.Equals(true) ? Ok(response) : BadRequest(response);
    }

    [HttpGet("checkemailexists")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse<bool>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> EmployeeExistsByEmailAsync(string email)
    {
        var response = await _employeeService.EmployeeExistsByEmailAsync(email);
        return Ok(response);
    }
}