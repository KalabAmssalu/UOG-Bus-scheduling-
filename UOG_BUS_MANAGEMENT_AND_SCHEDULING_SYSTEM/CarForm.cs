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
    public partial class CarForm : Form
    {
        vehicleConn vehicle = new vehicleConn();
        
        public CarForm()
        {
            InitializeComponent();
        }

        private void Guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //create a function to verify
        bool verify()
        {
            if ((guna2TextBox_palate.Text == "") || (guna2TextBox_mark.Text == "") ||
                (guna2TextBox_model.Text == "") || (guna2ComboBox_engin.SelectedItem.ToString() == "") ||
                (pictureBox_vehicle.Image == null))
            {
                return false;
            }
            else
                return true;
        }
        private void Guna2GradientButton6_Click(object sender, EventArgs e)
        {
            //string palate, string enginType, string mark, string name, int capacity, DateTime jdate, string driver, string status, byte[] img
            //Add data on database 
            string palate = guna2TextBox_palate.Text;
            string enginType = guna2ComboBox_engin.SelectedItem.ToString();
            string mark = guna2TextBox_mark.Text;
            string name = guna2TextBox_model.Text;
            int capacity = int.Parse(guna2TextBox_capacity.Text);
            DateTime jdate = guna2DateTimePicker_added.Value;
            string driver = guna2ComboBox_driver.SelectedItem.ToString();
            string status = guna2RadioButton1.Checked ? "Active" : "inActive";
      

            //we need to check student age between 10 and 100

            int jdate_select = guna2DateTimePicker_added.Value.Day;
            int this_day = DateTime.Now.Day;
            //if (this_day == jdate_select)
            //{
              //  MessageBox.Show("your selected date is not the same as the current day please select the day ", "Invalid Day", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            if (vehicle.Searchpalate(palate))
            {
                MessageBox.Show("the palate you indicated is already in the registered", "Invalid palate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    // to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_vehicle.Image.Save(ms, pictureBox_vehicle.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (vehicle.insertvehicles(palate, enginType, mark, name, capacity, jdate, driver, status, img))
                    {
                        showTable();
                        MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //add vehicles picture 

            // browse photo from computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_vehicle.Image = Image.FromFile(opf.FileName);
        }

        private void CarForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        // To show student list in DatagridView
        public void showTable()
        {
            guna2DataGridView_vehicles.DataSource = vehicle.getVehiclelist(new MySqlCommand("SELECT * FROM `vehicles`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)guna2DataGridView_vehicles.Columns[9];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void Guna2GradientButton2_Click(object sender, EventArgs e)
        {
            guna2TextBox_palate.Clear();
            guna2ComboBox_engin.SelectedIndex = -1;
            guna2TextBox_mark.Clear();
            guna2TextBox_model.Clear();
            guna2TextBox_capacity.Clear();
            guna2ComboBox_driver.SelectedIndex = -1;
            guna2RadioButton1.Checked = false;
            guna2DateTimePicker_added.Value = DateTime.Now;
            pictureBox_vehicle.Image = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
