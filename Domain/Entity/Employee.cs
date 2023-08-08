using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Enum;

namespace Domain.Entity;

public class Employee
{
    [Key]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [Range(18, 65)]
    public int Age { get; set; }
    [Required]
    [EnumDataType(typeof(Appointments))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Appointments Appointment { get; set; }
    [Required]
    
    public int Salary { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime EmploymentDate { get; set; }
}