using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportsAndLabels.Models
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var templateDefinition = File.ReadAllText("DoseAidCheckSheet.list");
			var dateTimeText = DateTime.Now.ToString("yyyy_MM_dd - HH_mm_ss");

			var task1 = Task.Run(() =>
			{
				ListLabelManager.ExportAsPdf(
					labelDefinition: templateDefinition,
					data: new List<DoseAidCheckSheet>
					{
						GetTray1Sheet(),
					},
					parentEntity: typeof(DoseAidCheckSheet).Name,
					targetFilePath: dateTimeText + " [" + Thread.CurrentThread.ManagedThreadId + "]" + ".pdf");
			});
			var task2 = Task.Run(() =>
			{
				ListLabelManager.ExportAsPdf(
					labelDefinition: templateDefinition,
					data: new List<DoseAidCheckSheet>
					{
						GetTray2Sheet(),
					},
					parentEntity: typeof(DoseAidCheckSheet).Name,
					targetFilePath: dateTimeText + " [" + Thread.CurrentThread.ManagedThreadId + "]" + ".pdf");
			});

			Task.WaitAll(task1, task2);
		}

		private DoseAidCheckSheet GetTray1Sheet()
		{
			return new DoseAidCheckSheet
			{
				PharmacyName = "Lucy Walker Pharmacy",
				FacilityName = "FLUC",
				Ward = "FLUC",
				TrayNumber = 1,
				Date = DateTime.Parse("2017-09-28T00:00:00+10:00"),
				PurchaseOrderId = "CMLWFLUC170928140604FLUCT1",
				PatientCount = 5,
				Consumers = new List<Patient>
				{
					new Patient
					{
						CustomerCode = 0,
						FirstName = "CLINTON",
						LastName = "BILLSBOROUGH",
					},
					new Patient
					{
						CustomerCode = 0,
						FirstName = "PHILIPPA",
						LastName = "BOWEN",
					},
					new Patient
					{
						CustomerCode = 0,
						FirstName = "MOYA",
						LastName = "CARTER",
					},
					new Patient
					{
						CustomerCode = 0,
						FirstName = "RONALD",
						LastName = "CHANDLER",
					},
					new Patient
					{
						CustomerCode = 0,
						FirstName = "BARRY",
						LastName = "CHAPMAN",
					},
				},
			};
		}

		private DoseAidCheckSheet GetTray2Sheet()
		{
			return new DoseAidCheckSheet
			{
				PharmacyName = "Lucy Walker Pharmacy",
				FacilityName = "FLUC",
				Ward = "FLUC",
				TrayNumber = 1,
				Date = DateTime.Parse("2017-09-28T00:00:00+10:00"),
				PurchaseOrderId = "CMLWFLUC170928140604FLUCT2",
				PatientCount = 1,
				Consumers = new List<Patient>
				{
					new Patient
					{
						CustomerCode = 0,
						FirstName = "PHILIPPA",
						LastName = "BOWEN",
					},
				},
			};
		}
	}
}
