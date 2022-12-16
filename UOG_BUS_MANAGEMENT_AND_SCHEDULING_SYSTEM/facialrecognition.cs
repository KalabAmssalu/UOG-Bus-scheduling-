using Guna.UI2.WinForms;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            loadform(new DashBoard());
        }

        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 131, b.Location.Y - 43);
            imgSlide.SendToBack();
        }
        private void Guna2Button1_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void UC_TripScheduling2_Load(object sender, EventArgs e)
        {
           
        }

     

        private void Guna2Button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       public void loadform(object Form)
        {
            if(this.mainpanel.Controls.Count > 0)
               this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
            
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            loadform(new DashBoard());
        }

        private void Guna2Button2_Click(object sender, EventArgs e)
        {
            loadform(new PassengerForm());
        }

        private void Guna2Button3_Click(object sender, EventArgs e)
        {
            loadform(new DriversForm());
        }

        private void Guna2Button4_Click(object sender, EventArgs e)
        {
            loadform(new CarForm());
        }

        private void Guna2Button5_Click(object sender, EventArgs e)
        {
            loadform(new TripForm());
        }

        private void Guna2Button6_Click(object sender, EventArgs e)
        {
            loadform(new SettingForm());
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ImgSlide_Click(object sender, EventArgs e)
        {

        }

        private void Guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void Guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
