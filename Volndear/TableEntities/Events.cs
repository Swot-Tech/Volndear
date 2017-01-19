using System;
using SQLite;


namespace Volndear
{
	[Table("Events")]
	public class Events
	{
		public int EventID { get; set; }
		public string EventName { get; set; }
		public string EventDescription { get; set; }
		public string EventOrganizersProfileImageLink { get; set; }
		public string EventImageLink { get; set; }
		public DateTime EventDateTime { get; set; }
		public string EventAddress { get; set; }
		public string EventOrganizerName { get; set; }
		public string EventOrganizerContactNumber{ get; set; }
		public string EventLocation { get; set; }
		public double EventLocationLatitude { get; set; }
		public double EventLocationLongitude { get; set; }
		public int NumberofPeopleJoined { get; set; }
		public int MaximumNumberOfParticipants { get; set;}
		public DateTime EventPostedTime { get; set; }
		public bool Isjoined { get; set; }
		public int Status { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime CreatedOn { get; set; }
		public int LastModifiedBy { get; set; }
		public System.DateTime LastModifiedOn { get; set; }
	}
}
