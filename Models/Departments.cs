using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Exercise.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        public string NameDepartments { get; set; }
        public string? CodeDepartment { get; set; }
        public string Location { get; set; }
        [JsonIgnore]
        public int? NumberOfPersonal { get; set; } = 0;
       
    }
}
