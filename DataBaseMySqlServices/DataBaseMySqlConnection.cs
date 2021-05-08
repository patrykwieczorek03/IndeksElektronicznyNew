using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace DataBaseMySqlServices
{
    public class DataBaseMySqlConnection
    {
        private string connetionString;
        public MySqlConnection conection;
        public DataBaseMySqlConnection()
        {
            this.connetionString = @"server=localhost;Uid=root;database=elektroniczny_indeks;Pwd=Prosba1998rok;";
            this.conection = new MySqlConnection(this.connetionString);
            this.conection.Open();
        }

        ~DataBaseMySqlConnection()
        {
            this.conection.Close();
        }
    }
}
