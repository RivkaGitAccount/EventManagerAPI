using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerAPI.Models
{
   
        public class Guest
        {
            public string GuestCode { get; set; }            // Guest Code (Primary Key)
            public string GuestName { get; set; }            // Name of the guest
            public string RelationshipType { get; set; }      // Relationship with the guest
            public string Phone { get; set; }                // Phone number
            public string Email { get; set; }                // Email of the guest
            public bool? InvitationSent { get; set; }        // Invitation status (nullable bit)
            public bool? ArrivalConfirmed { get; set; }      // Arrival status (nullable bit)
            public int? NumPeople { get; set; }              // Number of people (nullable int)
            public string EventCode { get; set; }            // Foreign key to EVENTS table
        }
    
}