using System.Drawing;

namespace ReportsAndLabels.Models
{
	public class Patient
	{
		public int CustomerCode { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Address Address { get; set; }
		public Bitmap Image { get; set; }
	}
}
