using Exercise.Models;

namespace Exercise.Respo.Interface
{
    public interface IRepo
    {
        Admin GetByUserName(string userName);
        Departments GetDepartByID(int Id);
    }
}
