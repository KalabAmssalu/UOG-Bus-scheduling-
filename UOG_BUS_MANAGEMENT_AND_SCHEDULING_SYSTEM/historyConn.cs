using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    class historyConn
    {
        DbConnection connect = new DbConnection();
        //create a function to add a new drivers to the database

        public bool inserthistory(string type, string fname)
        {
            string sql = "INSERT INTO `history`(`user_type`, `name`) VALUES ( @typ, @fn)";
            MySqlCommand command = new MySqlCommand(sql, connect.getconnection);
            
           
            command.Parameters.Add("@typ", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@fn", MySqlDbType.Text).Value = fname;

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
    
    }
}
