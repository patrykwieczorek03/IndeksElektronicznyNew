using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;

namespace DataBaseServices
{
    public class DataBaseConnection
    {
        private string connetionString;
        public MySqlConnection conection;
        public DataBaseConnection()
        {
            this.connetionString = "server=localhost;Uid=root;database=elektroniczny_indeks;Pwd=Prosba1998rok;";
            this.conection = new MySqlConnection(connetionString);
            this.conection.Open();
        }

        ~DataBaseConnection()
        {
            this.conection.Close();
        }
    }
}


