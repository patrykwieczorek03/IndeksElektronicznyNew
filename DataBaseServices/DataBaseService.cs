using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;

namespace DataBaseServices
{
    public class DataBaseService : DataBaseConnection
    {
        public DataBaseService() : base() { }

        public void DataBaseRead()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM student", this.conection);
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string Output = "";
            while (reader.Read())
            {
                Output = Output + reader.GetValue(0) + reader.GetValue(1) + reader.GetValue(2) + reader.GetValue(3);
            }
        }

        ~DataBaseService()
        {
            this.conection.Close();
        }
    }
}