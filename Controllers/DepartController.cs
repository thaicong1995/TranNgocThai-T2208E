using Exercise.Dto;
using Exercise.Models;
using Exercise.Service.Interface;
using Exercise.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartController : ControllerBase
    {
        private readonly IDepartmentsService _idepart;
        public DepartController(IDepartmentsService idepart)
        {
            _idepart = idepart;
        }

        [Authorize]
        [HttpPost("AddDepart-Departmen")]
        public ActionResult<string> AddDepart(DepartmentDto department)
        {
            try
            {
                var depart = _idepart.AddNewDepartments(department);
                return Ok(depart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<DepartmentWithEmployeesDTO>> GetAllDepartments()
        {
            try
            {
                var departments = _idepart.GetAll();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteDepartment(int id)
        {
            try
            {
                var result = _idepart.DeleteDepartment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Departments> UpdateDepartment(int id, Departments updatedDepartment)
        {
            try
            {
                var department = _idepart.Update(id, updatedDepartment);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<DepartmentWithEmployeesDTO> GetDepartmentWithEmployeesById(int id)
        {
            try
            {
                var department = _idepart.GetDepartmentById(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
