using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerAPI.Models
{
    public class Event
    {
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string GMAIL { get; set; } // Foreign key reference to Gmail
    }
   
} 