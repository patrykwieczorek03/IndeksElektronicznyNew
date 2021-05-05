using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseMySqlServices.Models;
using MySqlConnector;

namespace DataBaseMySqlServices
{
    public class DataBaseMySqlService : DataBaseMySqlConnection
    {
        private MySqlCommand command;
        private MySqlDataReader reader;
        public DataBaseMySqlService() : base() { }

        public User DataBaseSignIn(string login, string password)  
        {   
            command = new MySqlCommand($"CALL sprawdz_uzytkownika('{login}', '{password}')", this.conection);
            reader = command.ExecuteReader();
            User user = new User();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.UserID = int.Parse(reader.GetValue(0).ToString());
                    user.Role = char.Parse(reader.GetValue(3).ToString());
                }
            }
            else
            {
                user.UserID = -1;
                user.Role = 'n';
            }    
            reader.Close();
            return user;
        }

        public UserDataView DataBaseShowUserDate(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_dane_uzytkownika('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            UserDataView userData = new UserDataView();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userData.Name = reader.GetValue(0).ToString();
                    userData.Surname = reader.GetValue(1).ToString();
                    userData.Pesel = reader.GetValue(2).ToString();
                    userData.DateOfBirth = DateTime.Parse(reader.GetValue(3).ToString());
                    userData.Sex = char.Parse(reader.GetValue(4).ToString());
                    userData.ContactNumber = reader.GetValue(5).ToString();
                    userData.Country = reader.GetValue(6).ToString();
                    userData.City = reader.GetValue(7).ToString();
                    userData.Street = reader.GetValue(8).ToString();
                    userData.HouseNumber = reader.GetValue(9).ToString();
                    userData.ApartmentNumber = reader.GetValue(10).ToString();
                    userData.PostalCode = reader.GetValue(11).ToString();
                }
            }
            reader.Close();
            return userData;
        }


        public void DataBaseRead()
        {
            command = new MySqlCommand("SELECT * FROM student", this.conection);
            reader = command.ExecuteReader();
            string Output = "";
            while (reader.Read())
            {
                Output = Output + reader.GetValue(0) + reader.GetValue(1) + reader.GetValue(2) + reader.GetValue(3);
            }
        }

        ~DataBaseMySqlService()
        {
            this.conection.Close();
        }
    }
}
