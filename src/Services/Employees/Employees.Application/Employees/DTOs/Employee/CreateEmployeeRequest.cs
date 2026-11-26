using Employees.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Employees.Application.Employees.DTOs.Employee;

public class CreateEmployeeRequest
{
    // Personuppgifter
    [Required(ErrorMessage = "Förnamn måste fyllas i")]
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")] 
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Efternamn måste fyllas i")]
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")] 
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Personnummer måste fyllas i")]
    [StringLength(20, ErrorMessage = "Får inte vara längre än 20 tecken")]
    [RegularExpression(@"^\d{8}-\d{4}$", ErrorMessage = "Ange personnummer som ÅÅÅÅMMDD-XXXX")]
    public string PersonalNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email måste fyllas i")]
    [EmailAddress(ErrorMessage = "Ange en giltig e-postadress")]
    [StringLength(100, ErrorMessage = "Får inte vara längre än 100 tecken")] 
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Telefon måste fyllas i")]
    [StringLength(20, ErrorMessage = "Får inte vara längre än 20 tecken")] 
    public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Adress måste fyllas i")]
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Postnummer måste fyllas i")]
    [StringLength(6, ErrorMessage = "Får inte vara längre än 6 tecken")]
    [RegularExpression(@"^\d{3} ?\d{2}$", ErrorMessage = "Ange postnummer som 123 45")]
    public string Zipcode { get; set; } = string.Empty;
    [Required(ErrorMessage = "Stad måste fyllas i")] 
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")] 
    public string City { get; set; } = string.Empty;

    // Nödkontaktuppgifter
    [Required(ErrorMessage = "Kontaktnamn måste fyllas i")]
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")]
    public string EmergencyContactName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Telefonnummer måste fyllas i")]
    [StringLength(50, ErrorMessage = "Får inte vara längre än 50 tecken")] 
    public string EmergencyContactNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Relation måste fyllas i")]
    [StringLength(20, ErrorMessage = "Får inte vara längre än 20 tecken")] 
    public string Relationship { get; set; } = string.Empty;

    // Anställningsuppgifter
    [EnumDataType(typeof(JobTitle), ErrorMessage ="Måste ange titel")]
    public JobTitle Title { get; set; } = JobTitle.Other;
    [EnumDataType(typeof(Department), ErrorMessage = "Måste ange avdelning")]
    public Department Department { get; set; } = Department.Operations;
    [EnumDataType(typeof(EmployeeType), ErrorMessage = "Måste ange anställningsavtal")]
    public EmployeeType EmployeeType { get; set; } = EmployeeType.Permanent;
    public DateOnly EmploymentStartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly? EmploymentEndDate { get; set; }
    [Range(1, 100, ErrorMessage = "Sysselsättningsgrad måste vara mellan 1 och 100")]
    public decimal EmployeeRate { get; set; } = 100;
    
}