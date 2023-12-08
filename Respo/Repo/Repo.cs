using Exercise.DataBase;
using Exercise.Models;
using Exercise.Respo.Interface;

namespace Exercise.Respo.Repo
{
    public class Repo : IRepo
    {
        private readonly DBContext _dBContext;
        public Repo(DBContext dbContext)
        {
            _dBContext = dbContext;
        }
        public Admin GetByUserName(string userName)
        {
            return _dBContext.Admin.FirstOrDefault(x => x.UserName == userName);
        }

        public Departments GetDepartByID(int Id)
        {
            return _dBContext.Departments.FirstOrDefault(x => x.Id == Id);
        }
    }
}
