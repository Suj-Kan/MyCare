using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCareWebApi.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }
}