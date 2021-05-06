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
        private MySqlDataAdapter adapter = new MySqlDataAdapter();
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

        public UserDataView DataBaseShowUserData(User user)
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

        public StudentDataView DataBaseShowStudentData(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_dane_studenta('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            StudentDataView studentData = new StudentDataView();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentData.StudyFiled = reader.GetValue(0).ToString();
                    studentData.Degree = int.Parse(reader.GetValue(1).ToString());
                    studentData.Semestr = int.Parse(reader.GetValue(2).ToString());
                    studentData.IndexNumber = int.Parse(reader.GetValue(3).ToString());
                }
            }
            reader.Close();
            return studentData;

        }

        public List<StudentGradesDataView> DataBaseShowStudentGradesData(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_oceny_studenta('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            List<StudentGradesDataView> studentGradesDatas = new List<StudentGradesDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentGradesDataView studentGradesData = new StudentGradesDataView();
                    studentGradesData.Name = reader.GetValue(0).ToString();
                    studentGradesData.Surname = reader.GetValue(1).ToString();
                    studentGradesData.NameOfCoure = reader.GetValue(2).ToString();
                    studentGradesData.Ects = int.Parse(reader.GetValue(3).ToString());
                    studentGradesData.GroupID = int.Parse(reader.GetValue(4).ToString());
                    if (!DBNull.Value.Equals(reader.GetValue(5))) studentGradesData.Grade = float.Parse(reader.GetValue(5).ToString());
                    else studentGradesData.Grade = 0.0F;
                    if (!DBNull.Value.Equals(reader.GetValue(6))) studentGradesData.GradeStatus = char.Parse(reader.GetValue(6).ToString());
                    else studentGradesData.GradeStatus = 'E';
                    studentGradesDatas.Add(studentGradesData);
                }
            }
            reader.Close();
            return studentGradesDatas;
        }

        public List<StudentTimeTableDataView> DataBaseShowStudentTimeTableData(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_plan_zajec_studenta('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            List<StudentTimeTableDataView> studentTimeTableDatas = new List<StudentTimeTableDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentTimeTableDataView studentTimeTableData = new StudentTimeTableDataView();
                    studentTimeTableData.Name = reader.GetValue(0).ToString();
                    studentTimeTableData.Surname = reader.GetValue(1).ToString();
                    studentTimeTableData.NameOfCoure = reader.GetValue(2).ToString();
                    studentTimeTableData.Ects = int.Parse(reader.GetValue(3).ToString());
                    studentTimeTableData.GroupID = int.Parse(reader.GetValue(4).ToString());
                    studentTimeTableData.Building = reader.GetValue(5).ToString();
                    studentTimeTableData.Room = int.Parse(reader.GetValue(6).ToString());
                    studentTimeTableData.DayOfWeek = int.Parse(reader.GetValue(7).ToString());
                    studentTimeTableData.TypeOfClasses = char.Parse(reader.GetValue(8).ToString());
                    studentTimeTableData.StartTime = DateTime.Parse(reader.GetValue(9).ToString());
                    studentTimeTableData.FinishTime = DateTime.Parse(reader.GetValue(10).ToString());
                    studentTimeTableDatas.Add(studentTimeTableData);
                }
            }
            reader.Close();
            return studentTimeTableDatas;
        }

        public List<LecturerGroupsDataView> DataBaseShowLecturerGroups(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_grupy_kursu_prowadzacego('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            List<LecturerGroupsDataView> lecturerGroupsDatas = new List<LecturerGroupsDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LecturerGroupsDataView lecturerGroupsData = new LecturerGroupsDataView();
                    lecturerGroupsData.GroupID = int.Parse(reader.GetValue(0).ToString());
                    lecturerGroupsData.Building = reader.GetValue(1).ToString();
                    lecturerGroupsData.DayOfWeek = int.Parse(reader.GetValue(2).ToString());
                    lecturerGroupsData.TypeOfClasses = char.Parse(reader.GetValue(3).ToString());
                    lecturerGroupsData.StartTime = DateTime.Parse(reader.GetValue(4).ToString());
                    lecturerGroupsData.FinishTime = DateTime.Parse(reader.GetValue(5).ToString());
                    lecturerGroupsDatas.Add(lecturerGroupsData);
                }
            }
            reader.Close();
            return lecturerGroupsDatas;
        }

        public List<LecturerCursesDataView> DataBaseShowLecturerCurses(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_prowadzone_kursy_prowadzacego('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            List<LecturerCursesDataView> lecturerCursesDatas = new List<LecturerCursesDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LecturerCursesDataView lecturerCursesData = new LecturerCursesDataView();
                    lecturerCursesData.NameOfCoure = reader.GetValue(0).ToString();
                    lecturerCursesData.Ects = int.Parse(reader.GetValue(1).ToString());
                    lecturerCursesDatas.Add(lecturerCursesData);
                }
            }
            reader.Close();
            return lecturerCursesDatas;
        }

        public List<LecturerStudentsDataView> DataBaseShowLecturerStudents(User user)
        {
            command = new MySqlCommand($"CALL wyswietl_studentow_prowadzacego('{user.UserID}')", this.conection);
            reader = command.ExecuteReader();
            List<LecturerStudentsDataView> lecturerStudentsDatas = new List<LecturerStudentsDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LecturerStudentsDataView lecturerStudentsData = new LecturerStudentsDataView();
                    lecturerStudentsData.IndexNumber = int.Parse(reader.GetValue(0).ToString());
                    lecturerStudentsData.NameOfCoure = reader.GetValue(1).ToString();
                    lecturerStudentsData.GroupID = int.Parse(reader.GetValue(2).ToString());
                    lecturerStudentsData.TypeOfClasses = char.Parse(reader.GetValue(3).ToString());
                    if (!DBNull.Value.Equals(reader.GetValue(4))) lecturerStudentsData.Grade = float.Parse(reader.GetValue(5).ToString());
                    else lecturerStudentsData.Grade = 0.0F;
                    if (!DBNull.Value.Equals(reader.GetValue(5))) lecturerStudentsData.GradeStatus = char.Parse(reader.GetValue(6).ToString());
                    else lecturerStudentsData.GradeStatus = 'E';
                    lecturerStudentsDatas.Add(lecturerStudentsData);
                }
            }
            reader.Close();
            return lecturerStudentsDatas;
        }

        public List<BrowseGroupsDataView> DataBaseShowBrowseGroups()
        {
            command = new MySqlCommand("SELECT * FROM przegladanie_grup_view", this.conection);
            reader = command.ExecuteReader();
            List<BrowseGroupsDataView> browseGroupsDatas = new List<BrowseGroupsDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BrowseGroupsDataView browseGroupsData = new BrowseGroupsDataView();
                    browseGroupsData.Name = reader.GetValue(0).ToString();
                    browseGroupsData.Surname = reader.GetValue(1).ToString();
                    browseGroupsData.NameOfCoure = reader.GetValue(2).ToString();
                    browseGroupsData.Ects = int.Parse(reader.GetValue(3).ToString());
                    browseGroupsData.GroupID = int.Parse(reader.GetValue(4).ToString());
                    browseGroupsData.Building = reader.GetValue(5).ToString();
                    browseGroupsData.Room = int.Parse(reader.GetValue(6).ToString());
                    browseGroupsData.DayOfWeek = int.Parse(reader.GetValue(7).ToString());
                    browseGroupsData.TypeOfClasses = char.Parse(reader.GetValue(8).ToString());
                    browseGroupsData.StartTime = DateTime.Parse(reader.GetValue(9).ToString());
                    browseGroupsData.FinishTime = DateTime.Parse(reader.GetValue(10).ToString());
                    browseGroupsDatas.Add(browseGroupsData);
                }
            }
            reader.Close();
            return browseGroupsDatas;
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

        public void DataBaseAddUser(AddUserProcedure newUser)
        {
            command = new MySqlCommand($"call dodaj_uzytkownika('{newUser.Pesel}', '{newUser.Name}', '{newUser.Surname}', '{newUser.DateOfBirth}', '{newUser.Sex}', '{newUser.ContactNumber}', '{newUser.Country}', '{newUser.City}', '{newUser.Street}', '{newUser.HouseNumber}', '{newUser.ApartmentNumber}', '{newUser.PostalCode}', '{newUser.Login}', '{newUser.Password}', '{newUser.Role}', '{newUser.StudyField}', {newUser.Degree}, {newUser.Semestr}, {newUser.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        ~DataBaseMySqlService()
        {
            this.conection.Close();
        }
    }
}
