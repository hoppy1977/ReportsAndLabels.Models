using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using combit.ListLabel22;
using combit.ListLabel22.DataProviders;

namespace ReportsAndLabels.Models
{
	public class ListLabelManager
	{
		private const string ListLabelLicenseString = "vAlqEQ";

		public static string Design(string labelDefinition, IEnumerable<Object> data, string parentEntity)
		{
			return Design(labelDefinition, data, parentEntity, null);
		}

		public static string Design(string labelDefinition, IEnumerable<Object> data, string parentEntity, string printerName)
		{
			using (var listLabel = new ListLabel())
			{
				listLabel.LicensingInfo = ListLabelLicenseString;
				listLabel.Core.LlSetOption(LlOption.Metric, 1);
				listLabel.Core.LlSetOption(LlOption.Wizard_FileNew, 0);
				listLabel.Core.LlSetOption(LlOption.RibbonDefaultEnabledState, 1);
				listLabel.Core.LlSetOption(LlOption.NoFileVersionUpgradeWarning, 1);

				// Set up the dataset
				listLabel.DataSource = new ObjectDataProvider(data)
				{
					FlattenStructure = true,
				};
				listLabel.DataMember = parentEntity;
				listLabel.AutoMasterMode = LlAutoMasterMode.AsVariables;


				// If we have a default printer then set it here
				if (printerName != null)
				{
					listLabel.Core.LlSetOptionString(71, printerName);
				}

				// Launch the designer
				listLabel.Design(LlProject.List);

				return String.Empty;
			}
		}

		public static bool Print(string labelDefinition, IEnumerable<Object> data, string parentEntity)
		{
			return Print(labelDefinition, data, parentEntity, null);
		}

		public static bool Print(string labelDefinition, IEnumerable<Object> data, string parentEntity, string printerName)
		{
			try
			{
				using (var listLabel = new ListLabel())
				{
					listLabel.LicensingInfo = ListLabelLicenseString;

					// Set up the dataset
					listLabel.DataSource = new ObjectDataProvider(data)
					{
						FlattenStructure = true,
					};
					listLabel.DataMember = parentEntity;
					listLabel.AutoMasterMode = LlAutoMasterMode.AsVariables;

					// Configure the designer to read the report design from a memoryStream
					var memoryStream = new MemoryStream();
					if (labelDefinition != null)
					{
						var stringBytes = Encoding.Unicode.GetBytes(labelDefinition);
						memoryStream.Write(stringBytes, 0, stringBytes.Length);
					}

					// If we have a default printer then set it here
					if (printerName != null)
					{
						listLabel.Core.LlSetOptionString(71, printerName);
					}

					// Launch the designer
					listLabel.Print(LlProject.List, memoryStream);

					return true;
				}
			}
			catch (LL_User_Aborted_Exception)
			{
				// Ignore user abort
			}

			return false;
		}

		public static void ExportAsPdf(string labelDefinition, IEnumerable<Object> data, string parentEntity, string targetFilePath)
		{
			using (var listLabel = new ListLabel())
			{
				listLabel.LicensingInfo = ListLabelLicenseString;

				// Set up the dataset
				listLabel.DataSource = new ObjectDataProvider(data)
				{
					FlattenStructure = true,
				};
				listLabel.DataMember = parentEntity;
				listLabel.AutoMasterMode = LlAutoMasterMode.AsVariables;

				// Configure the designer to read the report design from a memoryStream
				var memoryStream = new MemoryStream();
				if (labelDefinition != null)
				{
					var stringBytes = Encoding.Unicode.GetBytes(labelDefinition);
					memoryStream.Write(stringBytes, 0, stringBytes.Length);
				}

				// Launch the designer
				var exportConfiguration = new ExportConfiguration(
					LlExportTarget.Pdf,
					targetFilePath,
					memoryStream)
				{
					ShowResult = false,
				};
				listLabel.Export(exportConfiguration);
			}
		}
	}
}
