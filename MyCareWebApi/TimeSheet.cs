//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCareWebApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeSheet
    {
        public int TimeSheetId { get; set; }
        public int TaskId { get; set; }
        public int EmployeeID { get; set; }
        public int ClientId { get; set; }
        public int HoursDone { get; set; }
        public Nullable<System.DateTime> DateWorked { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
    }
}
