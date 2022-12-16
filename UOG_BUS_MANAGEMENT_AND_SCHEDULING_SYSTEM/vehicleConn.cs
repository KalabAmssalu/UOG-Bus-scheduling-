using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    class vehicleConn
    {
        DbConnection connect = new DbConnection();
        //create a function to add a new vehicles to the database

        public bool insertvehicles(string palate, string enginType, string mark, string name, int capacity, DateTime jdate, string driver, string status, byte[] img)
        {
            string sql = "INSERT INTO `vehicles`(`VehiclesPalate`, `VehiclesMark`, `VehiclesName`, `EngineType`, `Capacity`, `JoinDate`, `Driverid`, `Active`, `VImage`) VALUES (@pa, @vm, @et, @vn, @ca, @jd, @di, @ac, @img)";
            MySqlCommand command = new MySqlCommand(sql, connect.getconnection);

            //@id, @pa, @vm, @et, @vn, @ca, @jd, @di, @ac, @img
          //  command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@pa", MySqlDbType.Text).Value = palate;
            command.Parameters.Add("@vm", MySqlDbType.Text).Value = mark;
            command.Parameters.Add("@et", MySqlDbType.VarChar).Value = enginType;
            command.Parameters.Add("@ca", MySqlDbType.Int32).Value = capacity;
            command.Parameters.Add("@vn", MySqlDbType.Text).Value = name;
            command.Parameters.Add("@jd", MySqlDbType.Date).Value = jdate;
            command.Parameters.Add("@di", MySqlDbType.VarChar).Value = driver;
            command.Parameters.Add("@ac", MySqlDbType.VarChar).Value = status;
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
        // to get vehicle table
        public DataTable getVehiclelist(MySqlCommand command)
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
        //to get the total vehicle
        public string totalVehicle()
        {
            return exeCount("SELECT COUNT(*) FROM Vehicles");
        }
        // to get the male vehicle count
        public string DieselVehicle()
        {
            return exeCount("SELECT COUNT(*) FROM Vehicles WHERE `EngineType`='Diesel'");
        }
        // to get the female vehicle count
        public string PetrolVehicle()
        {
            return exeCount("SELECT COUNT(*) FROM Vehicles WHERE `EngineType`='Petrol'");
        }
        //create a function search for vehicle (first name, last name, address)
        public DataTable SearchVehicle(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `Vehicles` WHERE CONCAT(`VehiclesName`,`VehicleID`,`VehiclesPalate`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //create a function edit for vehicle
        public bool UpdateVehicle(string palate, string enginType, string mark, string name,int capacity, DateTime jdate, string driver,string status, byte[] img)
        {
            string query = "UPDATE `Vehicles` SET  `VehiclesPalate`=@pa, `VehiclesMark `= @vm, `EngineType`= @et, `VehiclesName`= @vn,`Capacity`= @ca, `JoinDate`=@jd, `Driverid`= @di ,`Active` = @ac,`VImage` = @img";
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);

            //@id, @pa, @vm, @et, @vn, @ca, @jd, @di, @ac, @img
           //command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@pa", MySqlDbType.Text).Value = palate;
            command.Parameters.Add("@vm", MySqlDbType.Text).Value = mark;
            command.Parameters.Add("@et", MySqlDbType.VarChar).Value = enginType;
            command.Parameters.Add("@ca", MySqlDbType.Int32).Value = capacity;
            command.Parameters.Add("@vn", MySqlDbType.Text).Value = name;
            command.Parameters.Add("@jd", MySqlDbType.Date).Value = jdate;
            command.Parameters.Add("@di", MySqlDbType.Int32).Value = driver;
            command.Parameters.Add("@ac", MySqlDbType.VarChar).Value = status;
            command.Parameters.Add("@img", MySqlDbType.Byte).Value = img;

                                                             
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
        public bool deletevehicle(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `Vehicles` WHERE `VehicleID`=@id", connect.getconnection);

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
        // create a function for any command in vehicleDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool Searchpalate(string palate)
        {
            bool result;
            MySqlCommand command = new MySqlCommand("SELECT * FROM `Vehicles` WHERE `VehiclesPalate` ='%" + palate + "%'", connect.getconnection);
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
            }
        }
    }
}
