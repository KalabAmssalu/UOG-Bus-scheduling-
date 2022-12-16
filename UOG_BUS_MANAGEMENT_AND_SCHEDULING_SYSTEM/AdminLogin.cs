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
using System.Data;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    public partial class AdminLogin : Form
    {
        settingConn admin = new settingConn();
        historyConn history = new historyConn();
        int num = 0; int counter = 0;
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void Guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialoge = MessageBox.Show("Do you Want to change To Driver Portal.", "Port choose", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            {
                if (dialoge == DialogResult.Yes)
                {
                    LoginDriver log = new LoginDriver();
                    log.Show();
                    this.Hide();
                }

            }
           
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

            this.Activate();
            timer1.Stop();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            num++;
            label4.Text = "To Many attempt wait 20 seconds " + num.ToString();
            if (num == 30)
            {
                timer1.Stop();
                guna2GradientButton2.Enabled = true;
                label4.Text = "";
                num = 0;
                counter = 0;
                label4.Visible = false;
            }
        }

        private void Guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Guna2GradientButton2_Click(object sender, EventArgs e)
        {
            //private void btnencrpyt_Click(object sender, EventArgs e)
            //{
            //  txt2.Text = Eramake.eCryptography.Encrypt(txt1.Text);
            // }
            if (guna2TextBox_username.Text == "" || passwordTextBox.Text == "")
            {
                MessageBox.Show("Need login data", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string uname = guna2TextBox_username.Text;
                //   string pass = Eramake.eCryptography.Decrypt(passwordTextBox.Text);
                string pass = passwordTextBox.Text;
                string type = "Admin";
                DataTable table = admin.getList(new MySqlCommand("SELECT * FROM `admin` WHERE `FirstName`= '" + uname + "' AND `Admin_Password`='" + pass + "'"));
                if (table.Rows.Count > 0)
                {
                    if (history.inserthistory(type, uname))
                    {
                        DialogResult di = MessageBox.Show("You have successfully logged in to Admin ", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (di == DialogResult.OK)
                        {
                            Menu me = new Menu();
                            this.Hide();
                            me.Show();
                        }

                    }


                }
                else
                {
                    MessageBox.Show("Your username and password are not exists", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    counter++;
                    if (counter > 5)
                    {
                        timer1.Start();
                        guna2GradientButton2.Enabled = false;
                    }
                }
            }
        }
     

        private void Button3_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.PasswordChar == '\0') 
            {
                showbutton1.BringToFront();
                passwordTextBox.PasswordChar = '*';
            }
        }

        private void Guna2GradientButton1_Click(object sender, EventArgs e)
        {
            guna2TextBox_username.Text = "";
            passwordTextBox.Text = "";
        }

        private void Guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are On the same Login Port", "Port choose", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            showbutton1.Visible = true;
        }

        private void Guna2TextBox_username_TextChanged(object sender, EventArgs e)
        {

        }

         

        private void Showbutton1_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.PasswordChar == '*')
            {
                passwordTextBox.PasswordChar = '\0';
            }
            if (passwordTextBox.PasswordChar == '\0')
            { 
                passwordTextBox.PasswordChar = '*';
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
