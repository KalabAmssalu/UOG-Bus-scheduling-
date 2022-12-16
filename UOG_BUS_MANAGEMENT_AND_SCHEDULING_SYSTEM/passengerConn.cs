using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    class passangerConn
    {
        DbConnection connect = new DbConnection();
        //create a function to add a new passengers to the database

        public bool insertpassenger(string FName, string LName, string Gender, int PhoNo, string SCare, string City, string Kebele, string work_Area, string Blocked, byte[] img)
        {
            string sql = "INSERT INTO `passenger`(`FirstName`, `LastName`, `Gender`, `PhoNumber`, `SpecialCare`, `Address_City`, `Address_Kebele`, `work_Area`, `Blocked`, `Image`) VALUES (@fn, @ln, @Gd, @Pno, @Sc, @Cit, @keb, @WA, @BL, @img)";
            MySqlCommand command = new MySqlCommand(sql, connect.getconnection);
            //Insert into Passenger values('PassangerId', 'LastName','FirstName','Gender','PhoNumber','SpecialCare','Address_City', 'Address_Kebele', 'work_Area', 'Blocked','Image');

            //(@Pid,@fn, @ln, @Gd, @Pno, @Sc, @Cit,@keb,@WA,@BL, @img
            // command.Parameters.Add("@Pid", MySqlDbType.Int32).Value = PId;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = FName;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = LName;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = Gender;
            command.Parameters.Add("@pno", MySqlDbType.Int32).Value = PhoNo;
            command.Parameters.Add("@Sc", MySqlDbType.Text).Value = SCare;
            command.Parameters.Add("@Cit", MySqlDbType.Text).Value = City;
            command.Parameters.Add("@Keb", MySqlDbType.Text).Value = Kebele;
            command.Parameters.Add("@WA", MySqlDbType.Text).Value = work_Area;
            command.Parameters.Add("@BL", MySqlDbType.VarChar).Value = Blocked;
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
        // to get passenger table
        public DataTable getpassengerlist(MySqlCommand command)
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
        //to get the total passenger
        public string totalpassenger()
        {
            return exeCount("SELECT COUNT(*) FROM passenger");
        }
        // to get the male passenger count
        public string malepassenger()
        {
            return exeCount("SELECT COUNT(*) FROM passenger WHERE `Gender`='Male'");
        }
        // to get the female passenger count
        public string femalepassenger()
        {
            return exeCount("SELECT COUNT(*) FROM passenger WHERE `Gender`='Female'");
        }
        //create a function search for passenger (first name, last name, address)
        public DataTable searchpassenger(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `passenger` WHERE CONCAT(`StdFirstName`,`StdLastName`,`Address`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //create a function edit for passenger
        public bool updatepassenger(int PId, string FName, string LName, string Gender, int PhoNo, string SCare, string City, string Kebele, string work_Area, string Blocked, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `passenger` SET `PassangerId=@PID`, `FirstName`=@fn, `LastName`=@ln, `Gender`=@gd, `Phone`=@pno, `SpecialCare`=@Sc, `Address_City`=@Cit,`Address_Kebele`=@Keb, `work_Area`=@WA, `Blocked`=@BL, `Image`=@img", connect.getconnection);

            //(@Pid,@fn, @ln, @Gd, @Pno, @Sc, @Cit,@keb,@WA,@BL, @img
            command.Parameters.Add("@Pid", MySqlDbType.Int32).Value = PId;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = FName;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = LName;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = Gender;
            command.Parameters.Add("@pno", MySqlDbType.Int32).Value = PhoNo;
            command.Parameters.Add("@Sc", MySqlDbType.VarChar).Value = SCare;
            command.Parameters.Add("@Cit", MySqlDbType.VarChar).Value = City;
            command.Parameters.Add("@Keb", MySqlDbType.VarChar).Value = Kebele;
            command.Parameters.Add("@WA", MySqlDbType.VarChar).Value = work_Area;
            command.Parameters.Add("@BL", MySqlDbType.VarChar).Value = Blocked;
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
        public bool deletepassenger(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `passenger` WHERE `PassangerId`=@id", connect.getconnection);

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
        // create a function for any command in passengerDb
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
