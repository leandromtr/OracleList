using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountNetDispatcher.Models
{
    public class NetDispatcherModel
    {
        public DateTime Report_Time { get; set; }
        public string EmpId { get; set; }
        public string Fname { get; set; }
        public string Login_At { get; set; }
    }
}