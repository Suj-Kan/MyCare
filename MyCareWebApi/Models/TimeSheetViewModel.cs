using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyCareWebApi.Models
{
    [DataContract]
    [KnownType(typeof(Client))]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Task))]
    public class TimeSheetViewModel
    {
        [DataMember]
        public int TimeSheetId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        
        [ForeignKey("ClientId")]
        public virtual Client client { get; set; }
        [DataMember]
        public int EmployeeId { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee employee { get; set; }
        [DataMember]
        public int TaskId { get; set; }
        
        [ForeignKey("TaskId")]
        public virtual Task task  { get; set; }
        [DataMember]
        public int HoursDone { get; set; }
        [DataMember]
        public DateTime DateWorked { get; set; }

    }
}