using MyCareWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyCareWebApi.Controllers
{
    public class TaskController : ApiController
    {
        public TaskController()
        { }

        public IHttpActionResult GetAllTasks()
        {
            IList<TaskViewModel> Tasks = null;

            using (var ctx = new TestDbContext())
            {
                Tasks = ctx.Tasks.Select(s => new TaskViewModel()
                {
                    TaskId = s.TaskId,
                    TaskName = s.TaskName,
                    TaskDescription = s.TaskDescription                 
                }).ToList<TaskViewModel>();
            }

            if (Tasks == null)
            {
                return NotFound();
            }

            return Ok(Tasks);
        }

        public IHttpActionResult GetTaskById(int id)
        {
            TaskViewModel Task = null;

            using (var ctx = new TestDbContext())
            {
                Task = ctx.Tasks
                    .Where(s => s.TaskId == id)
                    .Select(s => new TaskViewModel()
                    {
                        TaskId = s.TaskId,
                        TaskName = s.TaskName,
                        TaskDescription = s.TaskDescription
                    }).FirstOrDefault<TaskViewModel>();
            }

            if (Task == null)
            {
                return NotFound();
            }

            return Ok(Task);
        }

        public IHttpActionResult GetAllTasks(string name)
        {
            IList<TaskViewModel> Tasks = null;

            using (var ctx = new TestDbContext())
            {
                Tasks = ctx.Tasks
                    .Where(s => s.TaskName.ToLower() == name.ToLower())
                    .Select(s => new TaskViewModel()
                    {
                        TaskId = s.TaskId,
                        TaskName = s.TaskName,
                        TaskDescription = s.TaskDescription,
                    }).ToList<TaskViewModel>();
            }

            if (Tasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(Tasks);
        }

        public IHttpActionResult PostNewTask(TaskViewModel Task)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                ctx.Tasks.Add(new Task()
                {
                    TaskId = Task.TaskId,
                    TaskName = Task.TaskName,
                    TaskDescription = Task.TaskDescription
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(TaskViewModel Task)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TestDbContext())
            {
                var existingTask = ctx.Tasks.Where(s => s.TaskId == Task.TaskId)
                                                        .FirstOrDefault<Task>();

                if (existingTask != null)
                {
                    existingTask.TaskName = Task.TaskName;
                    existingTask.TaskDescription = Task.TaskDescription;
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
                return BadRequest("Not a valid Task id");

            using (var ctx = new TestDbContext())
            {
                var Task = ctx.Tasks
                    .Where(s => s.TaskId == id)
                    .FirstOrDefault();

                ctx.Entry(Task).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
