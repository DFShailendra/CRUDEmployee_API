
using EmployeeAPI.DataAccessLayer;
using EmployeeAPI.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RRFController : ControllerBase
    {
        IConfiguration _configuration;
        RRFDAL _RRFDAL;
        RRF objRRF = new RRF();

        public RRFController(IConfiguration configuration, RRFDAL rRFDAL)
        {
            _configuration = configuration;
            _RRFDAL = rRFDAL;
        }

        //GET: api/<RRFController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DataTable dataTable = _RRFDAL.GetAllRRFRecords();
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
                objRRF = _RRFDAL.GetRRFRecordById(id);
                return Ok(objRRF);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] RRF objRRF)
        {
            try
            {
                bool result = _RRFDAL.InsertUpdateRRFRecord(objRRF);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] RRF objRRF)
        {
            try
            {
                bool result = _RRFDAL.InsertUpdateRRFRecord(objRRF);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                bool result = _RRFDAL.DeleteRRFRecord(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("getDDL")]
        public IActionResult getDDL()
        {
            try
            {
                DataSet dataSet = new DataSet();
                DataTable resource = _RRFDAL.GetResourceDDL();
                DataTable client = _RRFDAL.GetClientDDL();
                DataTable role = _RRFDAL.GetRoleDDL();
                DataTable billable = _RRFDAL.GetBillableDDL();
                DataTable positiontype = _RRFDAL.GetPositionTypeDDL();
                DataTable numberofposition = _RRFDAL.GetNumberOfPositionDDL();
                DataTable isinternalresource = _RRFDAL.GetIsInternalResourceDDL();
                DataTable payroletype = _RRFDAL.GetPayroleTypeDDL();
                DataTable isremotely = _RRFDAL.GetIsRemotelyDDL();
                DataTable minimumyearsofexperience = _RRFDAL.GetMinimumYearsOfExperienceDDL();






                dataSet.Tables.Add(resource);
                dataSet.Tables.Add(client);
                dataSet.Tables.Add(role);
                dataSet.Tables.Add(billable);
                dataSet.Tables.Add(positiontype);
                dataSet.Tables.Add(numberofposition);
                dataSet.Tables.Add(isinternalresource);
                dataSet.Tables.Add(payroletype);
                dataSet.Tables.Add(minimumyearsofexperience);
                dataSet.Tables.Add(isremotely);






                return Ok(dataSet);
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
