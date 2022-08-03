using API.DataAccessLayer;
using API.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IConfiguration _configuration;
        EmployeeDAL _EmployeeDAL;
        Employee objEmployee = new Employee();
        public EmployeeController(IConfiguration configuration, EmployeeDAL employeeDAL)
        {
            _EmployeeDAL = employeeDAL;
            _configuration = configuration;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DataTable dataTable = _EmployeeDAL.GetAllEmployee();
                return Ok(dataTable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                objEmployee = _EmployeeDAL.GetEmployeeById(id);
                return Ok(objEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee objEmployee)
        {
            try
            {
                bool result = _EmployeeDAL.InsertUpdate(objEmployee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Employee objEmployee)
        {
            try
            {
                bool result = _EmployeeDAL.UpdateEmployee(objEmployee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _EmployeeDAL.DeleteEmployee(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
