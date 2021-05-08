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
                    studentGradesData.MembershipID = int.Parse(reader.GetValue(0).ToString());
                    studentGradesData.Name = reader.GetValue(1).ToString();
                    studentGradesData.Surname = reader.GetValue(2).ToString();
                    studentGradesData.NameOfCoure = reader.GetValue(3).ToString();
                    studentGradesData.Ects = int.Parse(reader.GetValue(4).ToString());
                    studentGradesData.GroupID = int.Parse(reader.GetValue(5).ToString());
                    if (!DBNull.Value.Equals(reader.GetValue(6))) studentGradesData.Grade = float.Parse(reader.GetValue(6).ToString());
                    else studentGradesData.Grade = 0.0F;
                    if (!DBNull.Value.Equals(reader.GetValue(7))) studentGradesData.GradeStatus = char.Parse(reader.GetValue(7).ToString());
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
                    DateTime arg1 = DateTime.Parse(reader.GetValue(9).ToString());
                    DateTime arg2 = DateTime.Parse(reader.GetValue(10).ToString());
                    studentTimeTableData.StartTime = TimeSpan.Parse(arg1.Hour.ToString() + ":" + arg1.Minute.ToString());
                    studentTimeTableData.FinishTime = TimeSpan.Parse(arg2.Hour.ToString() + ":" + arg2.Minute.ToString());
                    studentTimeTableDatas.Add(studentTimeTableData);
                }
            }
            reader.Close();
            return studentTimeTableDatas;
        }

        public List<LecturerGroupsDataView> DataBaseShowLecturerGroups(User user, int IndexCourse)
        {
            command = new MySqlCommand($"CALL wyswietl_grupy_kursu_prowadzacego('{user.UserID}', {IndexCourse})", this.conection);
            reader = command.ExecuteReader();
            List<LecturerGroupsDataView> lecturerGroupsDatas = new List<LecturerGroupsDataView>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LecturerGroupsDataView lecturerGroupsData = new LecturerGroupsDataView();
                    lecturerGroupsData.GroupID = int.Parse(reader.GetValue(0).ToString());
                    lecturerGroupsData.Building = reader.GetValue(1).ToString();
                    lecturerGroupsData.Room = reader.GetValue(2).ToString();
                    lecturerGroupsData.DayOfWeek = int.Parse(reader.GetValue(3).ToString());
                    lecturerGroupsData.TypeOfClasses = char.Parse(reader.GetValue(4).ToString());
                    lecturerGroupsData.StartTime = DateTime.Parse(reader.GetValue(5).ToString());
                    lecturerGroupsData.FinishTime = DateTime.Parse(reader.GetValue(6).ToString());
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
                    lecturerCursesData.IndexCourse = int.Parse(reader.GetValue(0).ToString());
                    lecturerCursesData.NameOfCourse = reader.GetValue(1).ToString();
                    lecturerCursesData.Ects = int.Parse(reader.GetValue(2).ToString());
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

        public StudentPreviewDataView DataBaseShowStudentPreview(int indexNumber)
        {
            command = new MySqlCommand($"CALL wyswietl_dane_studenta_podglad('{indexNumber}')", this.conection);
            reader = command.ExecuteReader();
            StudentPreviewDataView studentPreviewData = new StudentPreviewDataView();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentPreviewData.IndexNumber = int.Parse(reader.GetValue(0).ToString());
                    studentPreviewData.Pesel = reader.GetValue(1).ToString();
                    studentPreviewData.Name = reader.GetValue(2).ToString();
                    studentPreviewData.Surname = reader.GetValue(3).ToString();
                    studentPreviewData.DateOfBirth = DateTime.Parse(reader.GetValue(4).ToString());
                    studentPreviewData.Sex = char.Parse(reader.GetValue(5).ToString());
                    studentPreviewData.ContactNumber = reader.GetValue(6).ToString();
                    studentPreviewData.Country = reader.GetValue(7).ToString();
                    studentPreviewData.City = reader.GetValue(8).ToString();
                    studentPreviewData.Street = reader.GetValue(9).ToString();
                    studentPreviewData.HouseNumber = reader.GetValue(10).ToString();
                    studentPreviewData.ApartmentNumber = reader.GetValue(11).ToString();
                    studentPreviewData.PostalCode = reader.GetValue(12).ToString();
                    studentPreviewData.StudyFiled = reader.GetValue(13).ToString();
                    studentPreviewData.Degree = int.Parse(reader.GetValue(14).ToString());
                    studentPreviewData.Semestr = int.Parse(reader.GetValue(15).ToString());
                }
            }
            reader.Close();
            return studentPreviewData;
        }

        public UserPreviewDataView DataBaseShowUserPreview(int userID)
        {
            command = new MySqlCommand($"CALL wyswietl_dane_uzytkownika_podglad('{userID}')", this.conection);
            reader = command.ExecuteReader();
            UserPreviewDataView userPreviewData = new UserPreviewDataView();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userPreviewData.Pesel = reader.GetValue(0).ToString();
                    userPreviewData.Name = reader.GetValue(1).ToString();
                    userPreviewData.Surname = reader.GetValue(2).ToString();
                    userPreviewData.DateOfBirth = DateTime.Parse(reader.GetValue(3).ToString());
                    userPreviewData.Sex = char.Parse(reader.GetValue(4).ToString());
                    userPreviewData.ContactNumber = reader.GetValue(5).ToString();
                    userPreviewData.Country = reader.GetValue(6).ToString();
                    userPreviewData.City = reader.GetValue(7).ToString();
                    userPreviewData.Street = reader.GetValue(8).ToString();
                    userPreviewData.HouseNumber = reader.GetValue(9).ToString();
                    userPreviewData.ApartmentNumber = reader.GetValue(10).ToString();
                    userPreviewData.PostalCode = reader.GetValue(11).ToString();
                    userPreviewData.Login = reader.GetValue(12).ToString();
                    userPreviewData.Password = reader.GetValue(13).ToString();
                    userPreviewData.Role = char.Parse(reader.GetValue(14).ToString());
                }
            }
            reader.Close();
            return userPreviewData;
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

        public List<UsersList> DataBaseShowUsersList()
        {
            command = new MySqlCommand("SELECT * FROM lista_uzytkownikow_view", this.conection);
            reader = command.ExecuteReader();
            List<UsersList> userListDatas = new List<UsersList>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UsersList userListData = new UsersList();
                    userListData.UserID = int.Parse(reader.GetValue(0).ToString());
                    userListData.Name = reader.GetValue(1).ToString();
                    userListData.Surname = reader.GetValue(2).ToString();
                    userListData.Role = char.Parse(reader.GetValue(3).ToString());
                    userListDatas.Add(userListData);
                }
            }
            reader.Close();
            return userListDatas;
        }

        public List<StudentsList> DataBaseShowStudentsList()
        {
            command = new MySqlCommand("SELECT * FROM dane_studentow_view", this.conection);
            reader = command.ExecuteReader();
            List<StudentsList> studentListDatas = new List<StudentsList>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentsList studentListData = new StudentsList();
                    studentListData.UserID = int.Parse(reader.GetValue(0).ToString());
                    studentListData.IndexNumber = int.Parse(reader.GetValue(1).ToString());
                    studentListData.Name = reader.GetValue(2).ToString();
                    studentListData.Surname = reader.GetValue(3).ToString();
                    studentListData.Pesel = reader.GetValue(4).ToString();
                    studentListData.StudyFiled = reader.GetValue(5).ToString();
                    studentListData.Degree = int.Parse(reader.GetValue(6).ToString());
                    studentListData.Semestr = int.Parse(reader.GetValue(7).ToString());
                    studentListData.ContactNumber = reader.GetValue(8).ToString();
                    studentListDatas.Add(studentListData);
                }
            }
            reader.Close();
            return studentListDatas;
        }

        public void DataBaseAddUser(AddUserProcedure newUser)
        {
            command = new MySqlCommand($"call dodaj_uzytkownika('{newUser.Pesel}', '{newUser.Name}', '{newUser.Surname}', '{newUser.DateOfBirth}', '{newUser.Sex}', '{newUser.ContactNumber}', '{newUser.Country}', '{newUser.City}', '{newUser.Street}', '{newUser.HouseNumber}', '{newUser.ApartmentNumber}', '{newUser.PostalCode}', '{newUser.Login}', '{newUser.Password}', '{newUser.Role}', '{newUser.StudyField}', {newUser.Degree}, {newUser.Semestr}, {newUser.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        public void DataBaseChangeUserData(ChangeDataProcedure user)
        {
            command = new MySqlCommand($"call zmien_imie('{user.Name}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_kod_pocztowy('{user.PostalCode}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_kraj_zamieszkania('{user.Country}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_miasto('{user.City}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_nazwisko('{user.Surname}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_numer_domu('{user.HouseNumber}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_numer_kontaktowy('{user.ContactNumber}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_numer_lokalu('{user.ApartmentNumber}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_plec('{user.Sex}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();

            command = new MySqlCommand($"call zmien_ulice('{user.Street}', {user.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        public void DataBaseDeleteUser(DeleteUserProcedure userToDrop)
        {
            command = new MySqlCommand($"call usun_uzytkownika({userToDrop.UserToDrop}, {userToDrop.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        public void DataBaseChangeFieldOfStudyStudent(ChangeStudentDataProcedure studentFieldOfStudy)
        {
            command = new MySqlCommand($"call zmien_kierunek_semestr_stopien({studentFieldOfStudy.StudentID}, '{studentFieldOfStudy.StudyField}', {studentFieldOfStudy.Semestr}, {studentFieldOfStudy.Degree}, {studentFieldOfStudy.CurrentUser});", this.conection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        public void DataBaseChangeLoginPassword(ChangeLoginPasswordProcedure user)
        {
            command = new MySqlCommand($"call zmien_login_haslo({user.UserToChangeLoginPassword}, '{user.Login}', '{user.Password}', {user.CurrentUser});", this.conection);
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
