using Exercise.DataBase;
using Exercise.Dto;
using Exercise.Models;
using Exercise.Respo.Interface;
using Exercise.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.TokenConfig;

namespace Exercise.Service.Service
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IRepo _irepo;
        private readonly DBContext _dBContext;
        public DepartmentsService(DBContext dbContext, IRepo repo)
        {
            _dBContext = dbContext;
            _irepo = repo;
        }
        public string AddNewDepartments(DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                throw new ArgumentNullException("not null");
            }

            var random = new Random();
            var employeeCode = $"EMP-{random.Next(1000, 9999)}";
            Departments departments = new Departments
            {
                NameDepartments = departmentDto.NameDepartments,
                CodeDepartment = employeeCode,
                Location = departmentDto.Location,
            };

            _dBContext.Add(departments);
            _dBContext.SaveChanges();

            return ("add sucess!! ");
        }

        public List<DepartmentWithEmployeesDTO> GetAll()
        {
            try
            {
                var departmentsWithEmployees = _dBContext.Departments
                    .Select(d => new DepartmentWithEmployeesDTO
                    {
                        Id = d.Id,
                        NameDepartments = d.NameDepartments,
                        Employees = _dBContext.Employees
                            .Where(e => e.DepartmentId == d.Id)
                            .ToList()
                    })
                    .ToList();

                return departmentsWithEmployees;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DepartmentWithEmployeesDTO GetDepartmentById(int departmentId)
        {
            try
            {
                var departmentWithEmployees = _dBContext.Departments
                    .Where(d => d.Id == departmentId)
                    .Select(d => new DepartmentWithEmployeesDTO
                    {
                        Id = d.Id,
                        NameDepartments = d.NameDepartments,
                        Employees = _dBContext.Employees
                            .Where(e => e.DepartmentId == d.Id)
                            .ToList()
                    })
                    .FirstOrDefault();

                if (departmentWithEmployees == null)
                {
                    throw new Exception("Department not found");
                }

                return departmentWithEmployees;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteDepartment(int departmentId)
        {
            try
            {
                var department = _irepo.GetDepartByID(departmentId);

                if (department == null)
                {
                    throw new ArgumentNullException("Department not found");
                }

                List<Employee> employeesToDelete = _dBContext.Employees
                    .Where(e => e.Department == department.NameDepartments)
                    .ToList();

                _dBContext.RemoveRange(employeesToDelete);

                _dBContext.Remove(department);

                _dBContext.SaveChanges();

                return "Department and associated employees deleted!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public Departments Update(int id, Departments updatedDepartment)
        {
            try
            {
                var department = _dBContext.Departments.FirstOrDefault(d => d.Id == id);
                if (department == null)
                {
                    throw new ArgumentNullException("Department not found");
                }

                department.NameDepartments = updatedDepartment.NameDepartments;
                department.Location = updatedDepartment.Location;

                _dBContext.SaveChanges();

                var employeesToUpdate = _dBContext.Employees
                    .Where(e => e.DepartmentId == department.Id)
                    .ToList();

                foreach (var employee in employeesToUpdate)
                {

                    employee.Department = updatedDepartment.NameDepartments;
                }

                _dBContext.SaveChanges();

                return department;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

