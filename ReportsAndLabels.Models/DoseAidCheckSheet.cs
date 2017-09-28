using System;
using System.Collections.Generic;


namespace ReportsAndLabels.Models
{
	public class DoseAidCheckSheet
	{
		public string PharmacyName { get; set; }
		public string FacilityName { get; set; }
		public string Ward { get; set; }
		public int TrayNumber { get; set; }
		public DateTime Date { get; set; }
		public string PurchaseOrderId { get; set; }
		public int PatientCount { get; set; }

		public List<Patient> Consumers { get; set; }
	}
}
