using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    public partial class DriversForm : Form
    {
        driverConn driver = new driverConn();
        public DriversForm()
        {
            InitializeComponent();
        }
        bool verify()
        {
            if ((guna2TextBox_fname.Text == "") || (guna2TextBox_lname.Text == "") || (guna2TextBox_add.Text == "")
               || (guna2TextBox_pno.Text == "") || (guna2NumericUpDown_exp.Text == "0") ||
               (guna2TextBox_email.Text == "") ||
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
        // To show driver list in DatagridView
        public void showTable()
        {
            guna2DataGridView_driver.DataSource = driver.getdriverlist(new MySqlCommand("SELECT * FROM `driver`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)guna2DataGridView_driver.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void Guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
           

        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            //print
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            //search
           // guna2DataGridView_driver.DataSource = driver.Searchdriver(textBox_search.Text);
            //DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            //imageColumn = (DataGridViewImageColumn)guna2DataGridView_driver.Columns[9];
            //imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Guna2GradientButton4_Click(object sender, EventArgs e)
        {
                
        }

        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Display driver data from driver to textbox
           /*
                guna2TextBox_fname.Text = guna2DataGridView_driver.CurrentRow.Cells[1].Value.ToString();
                guna2TextBox_lname.Text = guna2DataGridView_driver.CurrentRow.Cells[2].Value.ToString();
                guna2TextBox_pno.Text = guna2DataGridView_driver.CurrentRow.Cells[3].Value.ToString();
                guna2TextBox_add.Clear();
                guna2TextBox_email.Clear();
                dateTimePicker_dob.Value = DateTime.Now;
                dateTimePicker_now.Value = DateTime.Now;
                guna2TextBox_pass.Clear();
                guna2CheckBox_Active.Checked = false;
                guna2ComboBox_gen.SelectedItem = true;
                pictureBox1.Image = null;
               

                dateTimePicker1.Value = (DateTime)DataGridView_driver.CurrentRow.Cells[3].Value;
                if (DataGridView_driver.CurrentRow.Cells[4].Value.ToString() == "Male")
                    radioButton_male.Checked = true;

                textBox_phone.Text = DataGridView_driver.CurrentRow.Cells[5].Value.ToString();
                textBox_address.Text = DataGridView_driver.CurrentRow.Cells[6].Value.ToString();
                byte[] img = (byte[])DataGridView_driver.CurrentRow.Cells[7].Value;
                MemoryStream ms = new MemoryStream(img);
                pictureBox_driver.Image = Image.FromStream(ms);
                */
            
        }

        private void Guna2GradientButton2_Click(object sender, EventArgs e)
        {
            //clear
            guna2TextBox_fname.Clear();
            guna2TextBox_lname.Clear();
            guna2TextBox_pno.Clear();
            guna2TextBox_add.Clear();
            guna2TextBox_email.Clear();
            dateTimePicker_dob.Value = DateTime.Now;
            dateTimePicker_now.Value = DateTime.Now;
            guna2TextBox_pass.Clear();
            guna2CheckBox_Active.Checked = false;
            guna2ComboBox_gen.SelectedItem = true;
            pictureBox1.Image = null;
        }

        private void Guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            //browse
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(opf.FileName);
           
        }

        private void Guna2GradientButton3_Click(object sender, EventArgs e)
        {
            //browse
            OpenFileDialog opf = new OpenFileDialog();
            opf.InitialDirectory = @"c:\";
            opf.Filter = "All files(*.*)|*.*|All files(*.*)|*.*";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                // pictureBox1.Image = File.Move(opf.FileName);
            }
        }

        private void Guna2GradientButton8_Click_1(object sender, EventArgs e)
        {
            //delete
            // remove the selected driver

            int id = Convert.ToInt32(textBox_id.Text);
            //Show a confirmation message before delete the driver
            if (MessageBox.Show("Are you sure you want to remove this driver", "Remove driver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (driver.deletedriver(id))
                {
                    showTable();
                    MessageBox.Show("driver Removed", "Remove driver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2GradientButton2.PerformClick();
                }
            }
        }

        private void Guna2GradientButton7_Click_1(object sender, EventArgs e)
        {
            //update
            // add new driver

            string fname = guna2TextBox_fname.Text;
            string lname = guna2TextBox_lname.Text;
            int phone = int.Parse(guna2TextBox_pno.Text);
            string address = guna2TextBox_add.Text;
            string email = guna2TextBox_email.Text;
            string gender = guna2ComboBox_gen.SelectedItem.ToString();
            DateTime bdate = dateTimePicker_dob.Value;
            DateTime jdate = dateTimePicker_now.Value;
            string pass = guna2TextBox_pass.Text;
            int expi = int.Parse(guna2NumericUpDown_exp.Text);
           // string Blocked = guna2CheckBox_Active.Checked ? "Active" : "Dormant";

            //we need to check driver age between 10 and 100

            int born_year = dateTimePicker_dob.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 18 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The driver age must be between 18 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           // else if (driver.Searchdriver(email))
            //{
              //  MessageBox.Show("the email you indicated is already in registered", "Invalid palate", MessageBoxButtons.OK, MessageBoxIcon.Error);
           // }
            else if (verify())

            {
                try
                {
                    string license = "";
                    // to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (driver.insertdriver(fname, lname, gender, bdate, phone, pass, address, email, jdate, expi, img))
                    {
                        showTable();
                        MessageBox.Show("driver data update", "Update driver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update driver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Guna2GradientButton6_Click_1(object sender, EventArgs e)
        {

            //(int PId,string FName,string LName,string Gender,int PhoNo,string SCare,string City,string Kebele, string work_Area, string Blocked, byte[] img)
            // add new driver

            string fname = guna2TextBox_fname.Text;
            string lname = guna2TextBox_lname.Text;
            int phone = int.Parse(guna2TextBox_pno.Text);
            string address = guna2TextBox_add.Text;
            string email = guna2TextBox_email.Text;
            string gender = guna2ComboBox_gen.SelectedItem.ToString();
            DateTime bdate = dateTimePicker_dob.Value;
            DateTime jdate = dateTimePicker_now.Value;
            string pass = guna2TextBox_pass.Text;
            int expi = int.Parse(guna2NumericUpDown_exp.Text);
            //string Blocked = guna2CheckBox_Active.Checked ? "Active" : "Dormant";

            //we need to check driver age between 10 and 100

            int born_year = dateTimePicker_dob.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 18 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The driver age must be between 18 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if (driver.Searchdriver(email))
            //{
              //  MessageBox.Show("the email you indicated is already in registered", "Invalid palate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else if (verify())

            {
                try
                {
                    
                    // to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (driver.insertdriver(fname, lname, gender, bdate, phone, pass, address, email, jdate, expi, img))
                    {
                        showTable();
                        MessageBox.Show("New driver Added", "Add driver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add driver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DriversForm_Load(object sender, EventArgs e)
        {

        }
    }
}
