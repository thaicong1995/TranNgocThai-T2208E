using Exercise.Models;

namespace Exercise.Dto
{
    public class DepartmentWithEmployeesDTO
    {
        public int Id { get; set; }
        public string NameDepartments { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
