using Exercise.Dto;
using Exercise.Models;

namespace Exercise.Service.Interface
{
    public interface IEmpoylee
    {
        string AddEmpoylee(EmpoyleeDto dto, int idDepart);

        string UpdatedEmpoylee(EmpoyleeUpdateDto dto, int id);
        string Delete(int userid);
        List<Employee> GetAll();
        Employee GetById(int EId);
    }
}
