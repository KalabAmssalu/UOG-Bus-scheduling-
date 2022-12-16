using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {

        }

        private void Guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void GoToTripForm(object sender, EventArgs e)
        {
            

        }

        private void GoToNotePadForm(object sender, EventArgs e)
        {
            NotePad np = new NotePad();
            np.Show();
        }

        private void Guna2GradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void Guna2GradientPanel2_Paint_1(object sender, PaintEventArgs e)
        {
           

        }

        private void Guna2GradientPanel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Guna2GradientPanel1_Click(object sender, EventArgs e)
        {

            Scheduling sc = new Scheduling();
            sc.Show();
        }

        private void Guna2GradientPanel6_Paint(object sender, PaintEventArgs e)
        {
           /**
            TripForm tri = new TripForm();
            tri.Show();**/
        }
    }
}
