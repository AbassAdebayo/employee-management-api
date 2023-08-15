using System.ComponentModel.DataAnnotations;
using MassTransit;

public class EployeeDto
{
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public Guid Id { get; set; } = NewId.Next().ToGuid();
    public string PhoneNumber {get; set;}
    public string Address {get; set;}
    public string Gender{get; set;}
    public string Email {get; set;}

}

public class CreateEmployeeRequestModel
{
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string FirstName {get; set;}

    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string LastName {get; set;}

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get; set;}

    public string Address {get; set;}

    [Required]
    public string Gender{get; set;}

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email {get; set;}


}

public class UpdateEmployeeRequestModel
{
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string FirstName {get; set;}

    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string LastName {get; set;}

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber {get; set;}
    
    public string Address {get; set;}

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email {get; set;}

    [Required]
    public string Gender{get; set;}

   

}

