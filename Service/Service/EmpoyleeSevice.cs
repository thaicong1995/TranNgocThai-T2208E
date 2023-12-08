using Exercise.DataBase;
using Exercise.Dto;
using Exercise.Models;
using Exercise.Respo.Interface;
using Exercise.Service.Interface;

namespace Exercise.Service.Service
{
    public class EmpoyleeSevice : IEmpoylee
    {
        private readonly IRepo _irepo;
        private readonly DBContext _dBContext;
        public EmpoyleeSevice(DBContext dbContext, IRepo repo)
        {
            _dBContext = dbContext;
            _irepo = repo;
        }
        public string AddEmpoylee(EmpoyleeDto dto, int idDepart)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto), "Not null");
                var random = new Random();
                var employeeCode = $"EMP-{random.Next(1000, 9999)}";

                var department = _dBContext.Departments.SingleOrDefault(d => d.Id == idDepart);
                if (department == null)
                {
                    throw new ArgumentException("Invalid DepartmentId", nameof(idDepart));
                }

                department.NumberOfPersonal++;

                Employee employee = new Employee
                {
                    EmployeeName = dto.EmployeeName,
                    Employeecode = employeeCode,
                    Department = department.NameDepartments,
                    DepartmentId = department.Id,
                    Rank = dto.Rank,
                };

                _dBContext.Employees.Add(employee);
                _dBContext.SaveChanges();

                return "Add success!!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string UpdatedEmpoylee(EmpoyleeUpdateDto dto, int userid)
        {
            try
            {
                if (dto == null) throw new ArgumentNullException(nameof(dto), "Not null");

                var existingEmpolyee = _dBContext.Employees.FirstOrDefault(c => c.Id == userid);
                if (existingEmpolyee == null)
                {
                    throw new Exception("Not found Empoyle");
                }
               

                existingEmpolyee.EmployeeName = dto.name;
                existingEmpolyee.Rank = dto.rank;


                _dBContext.SaveChanges();

                return "Update success!!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public string Delete(int userId)
        {
            try
            {
                var existingEmployee = _dBContext.Employees.FirstOrDefault(c => c.Id == userId);

                if (existingEmployee == null)
                {
                    throw new ArgumentNullException("Employee not found");
                }

                
                var departmentName = existingEmployee.Department;

                if (!string.IsNullOrEmpty(departmentName))
                {
                   
                    var associatedDepartment = _dBContext.Departments.FirstOrDefault(d => d.NameDepartments == departmentName);

                    if (associatedDepartment != null)
                    {
                        associatedDepartment.NumberOfPersonal --;
                    }
                }

                _dBContext.Remove(existingEmployee);
                _dBContext.SaveChanges();

                return "Delete success!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Employee> GetAll()
        {
            try
            {
                List<Employee> all = _dBContext.Employees.ToList();
                return all;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Employee GetById(int EId)
        {
            try
            {
                var employee = _dBContext.Employees.FirstOrDefault(e => e.Id == EId);
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
