using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCareWebApi.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DOB { get; set; }
    }
}