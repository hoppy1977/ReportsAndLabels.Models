using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            List<Patient> newPatients = new List<Patient>();
            Patient pt = new Patient();
            pt.Address = new Address() { Line1 = "Strasse1", Line2 = "strasse 2", Postcode = "78467", State = "BW", Town = "Konstanz" };
            pt.CustomerCode = 2;
            pt.FirstName = "Hans";
            pt.LastName = "Wurst";
            newPatients.Add(pt);


            Patient pt1 = new Patient();
            pt1.Address = new Address() { Line1 = "Strasse1", Line2 = "strasse 2", Postcode = "78467", State = "BW", Town = "Konstanz" };
            pt1.CustomerCode = 2;
            pt1.FirstName = "Hans";
            pt1.LastName = "Wurst";
            newPatients.Add(pt1);

            DoseAidCheckSheet doseAid = new DoseAidCheckSheet()
            {
                Consumers = newPatients,
                Date = DateTime.Now,
                TrayNumber = 11,

            };

            DoseAidCheckSheet doseAid2 = new DoseAidCheckSheet()
            {
                Consumers = newPatients,
                Date = DateTime.Now,
                TrayNumber = 11,

            };
            IEnumerable<DoseAidCheckSheet> liste = new List<DoseAidCheckSheet>() { doseAid, doseAid2 };
                     
            ListLabelManager.Design("h", liste, "DoseAidCheckSheet");

            



        }
    }
}
