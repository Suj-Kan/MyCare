using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace MyCareWebApi.Controllers
{
    [KnownType(typeof(Client))]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Task))]
    public class TimeSheetController : ApiController
    {
        public TimeSheetController()
        { }

        public IHttpActionResult GetAllTimeSheets()
        {
            IList<TimeSheetViewModel> TimeSheets = null;

            using (var ctx = new TestDbContext())
            {
                TimeSheets = ctx.TimeSheets.Include("Client")
                    .Include("Employee")
                    .Include("Task")
                    .Select(s => new TimeSheetViewModel()
                {
                    TimeSheetId = s.TimeSheetId,
                    ClientId = s.Client.ClientId,
                    EmployeeId = s.Employee.EmployeeId,
                    TaskId = s.Task.TaskId,            
                    client = s.Client,
                    task  = s.Task,
                    employee = s.Employee,
                    DateWorked = s.DateWorked.Value
                }).ToList<TimeSheetViewModel>();
            }

            if (TimeSheets == null)
            {
                return NotFound();
            }

            return Ok(TimeSheets);
        }

        public IHttpActionResult GetTimeSheetById(int id)
        {
            TimeSheetViewModel TimeSheet = null;

            using (var ctx = new TestDbContext())
            {
                TimeSheet = ctx.TimeSheets.Include("Client")
                    .Include("Employee")
                    .Include("Task")
                    .Where(s => s.TimeSheetId == id)
                    .Select(s => new TimeSheetViewModel()
                    {
                        TimeSheetId = s.TimeSheetId,
                        ClientId = s.Client.ClientId,
                        EmployeeId = s.Employee.EmployeeId,
                        TaskId = s.Task.TaskId,
                        HoursDone = s.HoursDone,
                        DateWorked = s.DateWorked.Value
                    }).FirstOrDefault<TimeSheetViewModel>();
            }

            if (TimeSheet == null)
            {
                return NotFound();
            }

            return Ok(TimeSheet);
        }

        public IHttpActionResult GetAllTimeSheets(DateTime FromDate, DateTime ToDate)
        {
            IList<TimeSheetViewModel> TimeSheets = null;

            using (var ctx = new TestDbContext())
            {
                TimeSheets = ctx.TimeSheets.Include("Client")
                    .Include("Employee")
                    .Include("Task")
                    .Where(s => s.DateWorked >= FromDate && s.DateWorked <= ToDate)
                    .Select(s => new TimeSheetViewModel()
                    {
                        TimeSheetId = s.TimeSheetId,
                        ClientId = s.Client.ClientId,
                        EmployeeId = s.Employee.EmployeeId,                
                        TaskId = s.Task.TaskId,                       
                        HoursDone = s.HoursDone,
                        DateWorked = s.DateWorked.Value
                    }).ToList<TimeSheetViewModel>();
            }

            if (TimeSheets.Count == 0)
            {
                return NotFound();
            }

            return Ok(TimeSheets);
        }

        public IHttpActionResult PostNewTimeSheet(TimeSheetViewModel TimeSheet)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                ctx.TimeSheets.Add(new TimeSheet()
                {
                    TimeSheetId = TimeSheet.TimeSheetId,
                    ClientId = TimeSheet.ClientId,
                    EmployeeID = TimeSheet.EmployeeId,
                    TaskId = TimeSheet.TaskId,
                    HoursDone = TimeSheet.HoursDone,
                    DateWorked = TimeSheet.DateWorked
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(TimeSheetViewModel TimeSheet)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                var existingTimeSheet = ctx.TimeSheets.Where(s => s.TimeSheetId == TimeSheet.TimeSheetId)
                                                        .FirstOrDefault<TimeSheet>();

                if (existingTimeSheet != null)
                {
                    existingTimeSheet.ClientId = TimeSheet.ClientId;
                    existingTimeSheet.EmployeeID = TimeSheet.EmployeeId;
                    existingTimeSheet.TaskId = TimeSheet.TaskId;
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
                return BadRequest("Not a valid TimeSheet id");

            using (var ctx = new TestDbContext())
            {
                var TimeSheet = ctx.TimeSheets
                    .Where(s => s.TimeSheetId == id)
                    .FirstOrDefault();

                ctx.Entry(TimeSheet).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}

