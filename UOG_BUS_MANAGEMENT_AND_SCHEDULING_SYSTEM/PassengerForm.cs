using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    public partial class PassengerForm : Form
    {
        passangerConn passenger = new passangerConn();
        public PassengerForm()
        {
            InitializeComponent();
        }
        bool verify()
        {
            if ((guna2TextBox_fname.Text == "") || (guna2TextBox_lname.Text == "") || (guna2TextBox_scare.Text == "")
               || (guna2TextBox_pno.Text == "") || (guna2TextBox_city.Text == "") || (guna2TextBox_keble.Text == "") ||
               (guna2ComboBox_wa.Text == "") ||
                (pictureBox1.Image == null))
            {
                return false;
            }
            else
                return true;
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        // To show student list in DatagridView
        public void showTable()
        {
            guna2DataGridView1.DataSource = passenger.getpassengerlist(new MySqlCommand("SELECT * FROM `Passenger`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)guna2DataGridView1.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void Guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            //(int PId,string FName,string LName,string Gender,int PhoNo,string SCare,string City,string Kebele, string work_Area, string Blocked, byte[] img)
            //add

            string FName = guna2TextBox_fname.Text;
            string LName = guna2TextBox_lname.Text;
            string Gender = guna2ComboBox_gen.SelectedItem.ToString();
            int PhoNo = int.Parse(guna2TextBox_pno.Text);
            string SCare = guna2TextBox_scare.Text;
            string City = guna2TextBox_city.Text;
            string Kebele = guna2TextBox_keble.Text;
            string work_Area = guna2ComboBox_wa.SelectedItem.ToString();
            string Blocked = guna2CheckBox_bl.Checked ? "Active" : "Dormant";

            if (verify())
            {
                try
                {
                    // to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (passenger.insertpassenger(FName, LName, Gender, PhoNo, SCare, City, Kebele, work_Area, Blocked, img))
                    {
                        showTable();
                        MessageBox.Show("New passenger Added", "Add passenger", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add passenger", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            //update
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            //delete

        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            //print
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //browse
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(opf.FileName);
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            //clear
            guna2TextBox_fname.Clear();
            guna2TextBox_lname.Clear();
            guna2TextBox_pno.Clear();
            guna2TextBox_scare.Clear();
            guna2TextBox_city.Clear();
            guna2TextBox_keble.Clear();
            guna2TextBox_city.Clear();
            guna2TextBox_keble.Clear();
            guna2ComboBox_wa.SelectedItem = true;
            guna2ComboBox_gen.SelectedItem = true;
            pictureBox1.Image = null;
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            //search
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
