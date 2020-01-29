using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{
    class Employees
    {
        string FirtstName { get; set; }
        string LastName { get; set; }
        bool Gender { get; set; }
        double Salary { get; set; }
    }    

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        List<Employees> employees = new List<Employees>();

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return employees;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return employees[id];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //add a value to the strings
            strings.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            strings[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            strings.RemoveAt(id);
        }
    }
}
