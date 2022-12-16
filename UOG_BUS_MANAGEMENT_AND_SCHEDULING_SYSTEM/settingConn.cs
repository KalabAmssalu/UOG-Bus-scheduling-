using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    class settingConn
    {
        DbConnection connect = new DbConnection();
        //create a function to add a new admins to the database

        public bool insertadmin(string FName, string LName, string Admin_pass, string c_pass,
            string Email, string Rol, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `admin`(`FirstName`, `LastName`, `Admin_Password`, `confirm_password`, `Email`, `Role`, `Image`)VALUES(@Aid,@fn, @ln, @adp, @Cdp, @em, @rol, @img)", connect.getconnection);

            //Insert into Admin values('AdminId', 'FirstName', 'LastName','Admin_Password','confirm_password', 'Email','Role','Image');
            //@Aid,@fn, @ln, @adp, @Cdp, @em, @rol, @img
            // command.Parameters.Add("@Aid", MySqlDbType.VarChar).Value = FName;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = FName;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = LName;
            command.Parameters.Add("@adp", MySqlDbType.VarChar).Value = Admin_pass;
            command.Parameters.Add("@cpd", MySqlDbType.VarChar).Value = c_pass;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = Email;
            command.Parameters.Add("@ro", MySqlDbType.LongBlob).Value = Rol;
            command.Parameters.Add("@im", MySqlDbType.LongBlob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        // to get admin table
        public DataTable getadminlist(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        // Create a function to execute the count query(total, male , female)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }
        //to get the total admin
        public string totaladmin()
        {
            return exeCount("SELECT COUNT(*) FROM admin");
        }
        // to get the male admin count
        public string maleadmin()
        {
            return exeCount("SELECT COUNT(*) FROM admin WHERE `Gender`='Male'");
        }
        // to get the female admin count
        public string femaleadmin()
        {
            return exeCount("SELECT COUNT(*) FROM admin WHERE `Gender`='Female'");
        }
        //create a function search for admin (first name, last name, address)
        public DataTable searchadmin(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `admin` WHERE CONCAT(`StdFirstName`,`StdLastName`,`Address`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //create a function edit for admin
        public bool updateadmin(int Aid, string FName, string LName, string Admin_pass, string c_pass, string Email, string Rol, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `admin` SET `admin_id', `FirstName`=@fn, `LastName`=@ln, `Admin_Password`=@adp, `confirm_password`=@cdp, `Email`=@em,`Role`=@rol, `Image`=@img", connect.getconnection);

            //@Aid,@fn, @ln, @adp, @Cdp, @em, @rol, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = FName;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = LName;
            command.Parameters.Add("@adp", MySqlDbType.VarChar).Value = Admin_pass;
            command.Parameters.Add("@cpd", MySqlDbType.VarChar).Value = c_pass;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = Email;
            command.Parameters.Add("@ro", MySqlDbType.LongBlob).Value = Rol;
            command.Parameters.Add("@im", MySqlDbType.LongBlob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //Create a function to delete data
        //we need only id 
        public bool deleteadmin(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `admin` WHERE `PassangerId`=@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        // create a function for any command in adminDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
