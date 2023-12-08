using Exercise.Dto;
using Exercise.Models;
using Exercise.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpoyleeController : ControllerBase
    {
        private readonly IEmpoylee _empoylee;
        public EmpoyleeController(IEmpoylee empoylee)
        {
            _empoylee = empoylee;
        }

        [Authorize]
        [HttpPost("AddDepart-Empoylee")]
        public ActionResult<string> AddEmpolee(EmpoyleeDto empoylee, [FromQuery] int id)
        {
            try 
            {
                var depart = _empoylee.AddEmpoylee(empoylee, id);
                return Ok(depart);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPut("Update-Empoylee")]
        public ActionResult<string> Update(EmpoyleeUpdateDto empoylee, int userId)
        {
            try
            {
                var depart = _empoylee.UpdatedEmpoylee(empoylee, userId);
                return Ok(depart);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [Authorize]
        [HttpDelete("delete-Empoylee")]
        public ActionResult<string> Delete(int userid)
        {
            try
            {
                var depart = _empoylee.Delete(userid);
                return Ok(depart);
            }catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
           
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Employee>> GetAllEmployees()
        {
            try
            {
                var allEmployees = _empoylee.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            try
            {
                var employee = _empoylee.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
