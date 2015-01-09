//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCP_Nalashaa_WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trip
    {
        public Trip()
        {
            this.Commuters = new HashSet<Commuter>();
            this.CommuterRequests = new HashSet<CommuterRequest>();
        }
    
        public int TripId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Origin_LatLn { get; set; }
        public string Destination_LatLn { get; set; }
        public Nullable<int> CommutersCapacity { get; set; }
        public Nullable<System.DateTime> TripDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> TripStatus { get; set; }
    
        public virtual ICollection<Commuter> Commuters { get; set; }
        public virtual ICollection<CommuterRequest> CommuterRequests { get; set; }
        public virtual TripStatusMaster TripStatusMaster { get; set; }
        public virtual User User { get; set; }
    }
}