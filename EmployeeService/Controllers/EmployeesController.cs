using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using System.Threading;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender="All")
        {

            string username = Thread.CurrentPrincipal.Identity.Name;


            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    var userLow = username.ToLower();

                    switch (userLow)
                    {
                        case "male":
                            return Request.CreateResponse(HttpStatusCode.OK, 
                                employeeDBEntities.Employees.Where(e => e.Gender == userLow).ToList());
                        case "female":
                            return Request.CreateResponse(HttpStatusCode.OK,
                                employeeDBEntities.Employees.Where(e => e.Gender == userLow).ToList());
                        default:
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage ReadEmployeeById(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with ID " + id + " not found.");
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    employeeDBEntities.Employees.Add(employee);
                    employeeDBEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID.ToString());

                    return message;

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {

                    var entity = employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The employee with ID " + id + " not found");
                    }
                    else
                    {
                        employeeDBEntities.Employees.Remove(entity);
                        employeeDBEntities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromUri]int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    var entity = employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        entity.FisrtName = employee.FisrtName;
                        entity.Gender = employee.Gender;
                        entity.LastName = employee.LastName;
                        entity.Salary = employee.Salary;
                        employeeDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "The employee with id " + id + " not found.");
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
