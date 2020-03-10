using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCareWebApi.Models
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}