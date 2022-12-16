using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    class driverConn
    {
        DbConnection connect = new DbConnection();
        //create a function to add a new drivers to the database

        public bool insertdriver(string fname, string lname, string gender, DateTime bdate, int phone, string pass, string address, string email, DateTime jdate, int expi, byte[] img)
        {
            //fname, lname, gender, bdate, phone, pass, address, email, jdate, expi, img
            string sql = "INSERT INTO `driver`(`FirstName`, `LastName`, `Gender`, `DoB`, `PhoNumber`, `Driver_Password`, `Address`, `Email`, `JoinDate`, `WorkExpirence`, `Image`) VALUES (@fn, @ln, @gn, @dob, @pho, @dpas, @add, @ema, @jd, @we, @img)";
            MySqlCommand command = new MySqlCommand(sql, connect.getconnection);

            //(@fn, @ln, @gn, @dob, @pho, @dpas, @add, @ema, @jd, @we, @img
       
            //command.Parameters.Add("@did", MySqlDbType.Int32).Value = did;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@dob", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@pho", MySqlDbType.Int32).Value = phone;
            command.Parameters.Add("@dpas", MySqlDbType.LongText).Value = pass;
            command.Parameters.Add("@add", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@ema", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@jd", MySqlDbType.Date).Value = jdate;
            command.Parameters.Add("@we", MySqlDbType.Int32).Value = expi;
           // command.Parameters.Add("@li", MySqlDbType.VarChar).Value = license;
            command.Parameters.Add("@img", MySqlDbType.LongBlob).Value = img;

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
        // to get driver table
        public DataTable getdriverlist(MySqlCommand command)
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
        //to get the total driver
        public string totaldriver()
        {
            return exeCount("SELECT COUNT(*) FROM driver");
        }
        // to get the male driver count
        public string maledriver()
        {
            return exeCount("SELECT COUNT(*) FROM driver WHERE `Gender`='Male'");
        }
        // to get the female driver count
        public string femaledriver()
        {
            return exeCount("SELECT COUNT(*) FROM driver WHERE `Gender`='Female'");
        }
        //create a function search for driver (first name, last name, address)
        public DataTable searchdriver(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `driver` WHERE CONCAT(`FirstName`,`LastName`,`Address`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //create a function edit for driver
        public bool updatedriver(int did, string fname, string lname, string gender, DateTime bdate, string phone, string pass, string address, string email, DateTime jdate, string expi, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `driver` SET 'FirstName'=@fn, 'LastName'=@ln,'Gender'=@gd,'DoB'=@dob,'PhoNumber'=@pho,'Driver_Password'=@dpas,'Address'=@add, 'Email'= @ema,'JoinDate'=@jd,'WorkExpirence'=@we,'Image'=@img", connect.getconnection);

            //@did, @fn, @ln, @gd, @dob, @pho, @dpas, @add, @ema, @jd, @we, @li, @img
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@gn", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@dob", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@pho", MySqlDbType.Int32).Value = phone;
            command.Parameters.Add("@dpas", MySqlDbType.LongText).Value = pass;
            command.Parameters.Add("@add", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@ema", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@jd", MySqlDbType.Date).Value = jdate;
            command.Parameters.Add("@we", MySqlDbType.Int32).Value = expi;
            // command.Parameters.Add("@li", MySqlDbType.VarChar).Value = license;
            command.Parameters.Add("@img", MySqlDbType.LongBlob).Value = img;

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
        public bool deletedriver(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `driver` WHERE `DriverId`=@id", connect.getconnection);

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
        // create a function for any command in driverDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
       // public bool Searchdriver(string email)
        //{
           
            /*
            bool result;
            MySqlCommand command = new MySqlCommand("SELECT * FROM `Vehicles` WHERE `Email` ='%" + email + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count >= 1)
            {
                result = true;
                return result;
            }
            else
            {
                return false;
            }*/
           
        //}
    }
}
