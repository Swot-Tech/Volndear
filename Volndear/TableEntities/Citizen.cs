using System;
using SQLite;

namespace Volndear
{
	[Table("Citizen")]
	public class Citizen
	{
		public int CitizenID { get; set; }
		public string CitizenName { get; set; }
		public string Password { get; set; }
		public string CitizenAddress { get; set; }
		public string CitizenMobileNumber { get; set; }
		public string CitizenDistrict { get; set; }
		public string CitizenPincode { get; set; }
		public string CitizenEmailId { get; set; }
		public string CitizenDOB { get; set; }
		public string CitizenGender { get; set; }
		public int Status { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime CreatedOn { get; set; }
		public int LastModifiedBy { get; set; }
		public System.DateTime LastModifiedOn { get; set; }
	}
}
