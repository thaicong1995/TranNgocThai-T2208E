using Exercise.Dto;
using Exercise.Models;

namespace Exercise.Service.Interface
{
    public interface IDepartmentsService
    {
        string AddNewDepartments(DepartmentDto departmentDto);

        DepartmentWithEmployeesDTO GetDepartmentById(int departmentId);
        List<DepartmentWithEmployeesDTO> GetAll();
        string DeleteDepartment(int departmentId);
        Departments Update(int id, Departments updatedDepartment);
    }
}
