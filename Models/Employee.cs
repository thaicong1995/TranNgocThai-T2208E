using System.ComponentModel.DataAnnotations;

namespace Exercise.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Employeecode { get; set; }

        public string? Department { get; set; }
        [Required]
        public string Rank { get; set; }

        public int DepartmentId { get; set; }
       


    }
}
