using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCareWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public EmployeeController()
        { }

        public IHttpActionResult GetAllEmployees()
        {
            IList<EmployeeViewModel> Employees = null;

            using (var ctx = new TestDbContext())
            {
                Employees = ctx.Employees.Select(s => new EmployeeViewModel()
                {
                    EmployeeId = s.EmployeeId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EmailAddress = s.EmailAddress,
                    DOB = s.DOB.Value
                }).ToList<EmployeeViewModel>();
            }

            if (Employees == null)
            {
                return NotFound();
            }

            return Ok(Employees);
        }

        public IHttpActionResult GetEmployeeById(int id)
        {
            EmployeeViewModel Employee = null;

            using (var ctx = new TestDbContext())
            {
                Employee = ctx.Employees
                    .Where(s => s.EmployeeId == id)
                    .Select(s => new EmployeeViewModel()
                    {
                        EmployeeId = s.EmployeeId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        EmailAddress = s.EmailAddress,
                        DOB = s.DOB.Value
                    }).FirstOrDefault<EmployeeViewModel>();
            }

            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(Employee);
        }

        public IHttpActionResult GetAllEmployees(string name)
        {
            IList<EmployeeViewModel> Employees = null;

            using (var ctx = new TestDbContext())
            {
                Employees = ctx.Employees
                    .Where(s => s.FirstName.ToLower() == name.ToLower())
                    .Select(s => new EmployeeViewModel()
                    {
                        EmployeeId = s.EmployeeId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        EmailAddress = s.EmailAddress,
                        DOB = s.DOB.Value
                    }).ToList<EmployeeViewModel>();
            }

            if (Employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(Employees);
        }

        public IHttpActionResult PostNewEmployee(EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                ctx.Employees.Add(new Employee()
                {
                    EmployeeId = Employee.EmployeeId,
                    FirstName = Employee.FirstName,
                    LastName = Employee.LastName,
                    EmailAddress = Employee.EmailAddress,
                    DOB = Employee.DOB
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(EmployeeViewModel Employee)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                var existingEmployee = ctx.Employees.Where(s => s.EmployeeId == Employee.EmployeeId)
                                                        .FirstOrDefault<Employee>();

                if (existingEmployee != null)
                {
                    existingEmployee.FirstName = Employee.FirstName;
                    existingEmployee.LastName = Employee.LastName;
                    existingEmployee.EmailAddress = Employee.EmailAddress;
                    existingEmployee.DOB = Employee.DOB;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Employee id");

            using (var ctx = new TestDbContext())
            {
                var Employee = ctx.Employees
                    .Where(s => s.EmployeeId == id)
                    .FirstOrDefault();

                ctx.Entry(Employee).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}

