using DataBaseMySqlServices;
using DataBaseMySqlServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndeksElektroniczny
{
    /// <summary>
    /// Logika interakcji dla klasy ProwadzacyWindow.xaml
    /// </summary>
    public partial class ProwadzacyWindow : Window
    {

        public List<TextBox> tableRowContentList { get; set; }
        public List<TextBlock> tableRowTitleList { get; set; }
        public Button buttonSearch { get; set; }
        public Button goBackButton { get; set; }
        public Button saveChangesButton { get; set; }
        public Button dropChangesButton { get; set; }
        public Button editDataButton { get; set; }
        public DataGrid contentDataGrid { get; set; }
        public TextBox textBoxSearch { get; set; }
        public ComboBox comboBoxSearch { get; set; }

        public static System.Windows.Thickness margin = new Thickness(5);

        private User signInUser;
        private DataBaseMySqlService DbService;
        private UserDataView userDate;
        List<LecturerGroupsDataView> lecturerGroupsDatas;
        List<LecturerCursesDataView> lecturerCursesDatas;
        List<LecturerStudentsDataView> lecturerStudentsDatas;
        int choosenCourseID;
        string choosenCourseName;

        public ProwadzacyWindow(Window loginWindow, User signInUser_a, DataBaseMySqlService DbService_a)
        {
            signInUser = signInUser_a;
            DbService = DbService_a;
            loginWindow.Close();
            InitializeComponent();
            CreateDaneOsobowe();
        }

        public void CreateDaneOsobowe()
        {
            Clear_Content();

            ManageMainButtons(1);

            int all_rows = 13;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane osobowe";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < all_rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }


            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 0);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 1);
                Grid.SetColumnSpan(infoBorder, columns - 1);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                TextBlock tableRowTitle = new TextBlock();
                Grid.SetColumn(tableRowTitle, 0);
                Grid.SetRow(tableRowTitle, j);
                tableRowTitleList.Add(tableRowTitle);
                contentGrid.Children.Add(tableRowTitleList[j]);
            }

            tableRowTitleList[0].Text = "Pesel";
            tableRowTitleList[1].Text = "Imie";
            tableRowTitleList[2].Text = "Nazwisko";
            tableRowTitleList[3].Text = "Data urodzenia";
            tableRowTitleList[4].Text = "Płeć";
            tableRowTitleList[5].Text = "Numer kontaktowy";
            tableRowTitleList[6].Text = "Kraj zamieszkania";
            tableRowTitleList[7].Text = "Miasto";
            tableRowTitleList[8].Text = "Ulica";
            tableRowTitleList[9].Text = "Numer domu";
            tableRowTitleList[10].Text = "Numer lokalu";
            tableRowTitleList[11].Text = "Kod pocztowy";

            for (int j = 0; j < rows; j++)
            {
                TextBox tableRowContent = new TextBox();
                tableRowContent.Margin = margin;
                tableRowContent.IsEnabled = false;
                Grid.SetColumn(tableRowContent, 1);
                Grid.SetColumnSpan(tableRowContent, columns - 1);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

            editDataButton = new Button();
            editDataButton.Margin = margin;
            Grid.SetColumn(editDataButton, columns - 1);
            Grid.SetRow(editDataButton, all_rows - 1);
            contentGrid.Children.Add(editDataButton);

            editDataButton.Content = "Edytuj";

            editDataButton.Click += new RoutedEventHandler(this.Edytuj_Click);

            UpdateUserData();
        }

        public void CreateDaneOsoboweEdycja()
        {
            Clear_Content();

            ManageMainButtons(1);

            int all_rows = 13;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane osobowe";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < all_rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }


            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 0);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 1);
                Grid.SetColumnSpan(infoBorder, columns - 1);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                TextBlock tableRowTitle = new TextBlock();
                Grid.SetColumn(tableRowTitle, 0);
                Grid.SetRow(tableRowTitle, j);
                tableRowTitleList.Add(tableRowTitle);
                contentGrid.Children.Add(tableRowTitleList[j]);
            }

            tableRowTitleList[0].Text = "Pesel";
            tableRowTitleList[1].Text = "Imie";
            tableRowTitleList[2].Text = "Nazwisko";
            tableRowTitleList[3].Text = "Data urodzenia";
            tableRowTitleList[4].Text = "Płeć";
            tableRowTitleList[5].Text = "Numer kontaktowy";
            tableRowTitleList[6].Text = "Kraj zamieszkania";
            tableRowTitleList[7].Text = "Miasto";
            tableRowTitleList[8].Text = "Ulica";
            tableRowTitleList[9].Text = "Numer domu";
            tableRowTitleList[10].Text = "Numer lokalu";
            tableRowTitleList[11].Text = "Kod pocztowy";

            for (int j = 0; j < rows; j++)
            {
                TextBox tableRowContent = new TextBox();
                tableRowContent.Margin = margin;
                Grid.SetColumn(tableRowContent, 1);
                Grid.SetColumnSpan(tableRowContent, columns - 1);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

            tableRowContentList[0].IsEnabled = false;
            tableRowContentList[3].IsEnabled = false;

            saveChangesButton = new Button();
            saveChangesButton.Margin = margin;
            Grid.SetColumn(saveChangesButton, columns - 1);
            Grid.SetRow(saveChangesButton, all_rows - 1);
            contentGrid.Children.Add(saveChangesButton);

            saveChangesButton.Content = "Zapisz zmiany";

            saveChangesButton.Click += new RoutedEventHandler(this.ZapiszZmiany_Click);

            dropChangesButton = new Button();
            dropChangesButton.Margin = margin;
            Grid.SetColumn(dropChangesButton, columns - 2);
            Grid.SetRow(dropChangesButton, all_rows - 1);
            contentGrid.Children.Add(dropChangesButton);

            dropChangesButton.Content = "Anuluj";

            dropChangesButton.Click += new RoutedEventHandler(this.Anuluj_Click);

            UpdateUserData();
        }

        public void CreateZajeciaProwadzonekursy()
        {
            Clear_Content();

            ManageMainButtons(2);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Prowadzone kursy";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 0);
            Grid.SetColumnSpan(contentDataGrid, columns);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows);
            contentGrid.Children.Add(contentDataGrid);
            contentDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.WybierzUzytkownika_SelectionChanged);

            UpdateLecturerCourses();
        }

        public void CreateZajeciaPrzegladanieGrup()
        {
            Clear_Content();

            ManageMainButtons(4);

            int rows = 5;
            int columns = 5;

            titleTextBlock.Text = "Grupy kursu: " + choosenCourseName;

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 0);
            Grid.SetColumnSpan(contentDataGrid, columns);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows-1);
            contentGrid.Children.Add(contentDataGrid);

            goBackButton = new Button();
            goBackButton.Margin = margin;
            Grid.SetColumn(goBackButton, columns - 1);
            Grid.SetRow(goBackButton, rows);
            contentGrid.Children.Add(goBackButton);

            goBackButton.Content = "Powrót";

            goBackButton.Click += new RoutedEventHandler(this.Powrot_Click);

            UpdateLecturerGroups();
        }

        public void CreateOcenianie()
        {
            Clear_Content();

            ManageMainButtons(3);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Ocenianie";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            buttonSearch = new Button();
            buttonSearch.Margin = margin;
            buttonSearch.Content = "Szukaj";
            Grid.SetColumn(buttonSearch, 4);
            Grid.SetRow(buttonSearch, 0);
            contentGrid.Children.Add(buttonSearch);
            buttonSearch.Click += new RoutedEventHandler(this.Szukaj_Click);

            textBoxSearch = new TextBox();
            textBoxSearch.Margin = margin;
            Grid.SetColumn(textBoxSearch, 0);
            Grid.SetColumnSpan(textBoxSearch, columns - 3);
            Grid.SetRow(textBoxSearch, 0);
            contentGrid.Children.Add(textBoxSearch);

            comboBoxSearch = new ComboBox();
            comboBoxSearch.Margin = margin;
            Grid.SetColumn(comboBoxSearch, 2);
            Grid.SetColumnSpan(comboBoxSearch, columns - 3);
            Grid.SetRow(comboBoxSearch, 0);
            contentGrid.Children.Add(comboBoxSearch);

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 0);
            Grid.SetColumnSpan(contentDataGrid, columns);
            Grid.SetRow(contentDataGrid, 1);
            Grid.SetRowSpan(contentDataGrid, rows - 1);
            contentGrid.Children.Add(contentDataGrid);

            UpdateLecturerStudents();
        }

        public void CreateOcenianieOcena()
        {
            Clear_Content();

            ManageMainButtons(2);

            int all_rows = 7;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Kurs:... Grupa:...";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < all_rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }


            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 0);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 1);
                Grid.SetColumnSpan(infoBorder, columns - 1);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                TextBlock tableRowTitle = new TextBlock();
                Grid.SetColumn(tableRowTitle, 0);
                Grid.SetRow(tableRowTitle, j);
                tableRowTitleList.Add(tableRowTitle);
                contentGrid.Children.Add(tableRowTitleList[j]);
            }

            tableRowTitleList[0].Text = "Numer Indeksu";
            tableRowTitleList[1].Text = "Kurs";
            tableRowTitleList[2].Text = "Grupa";
            tableRowTitleList[3].Text = "Typ zajęć";
            tableRowTitleList[4].Text = "Ocena";
            tableRowTitleList[5].Text = "Status oceny";

            for (int j = 0; j < rows; j++)
            {
                TextBox tableRowContent = new TextBox();
                tableRowContent.Margin = margin;
                tableRowContent.IsEnabled = false;
                Grid.SetColumn(tableRowContent, 1);
                Grid.SetColumnSpan(tableRowContent, columns - 1);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

            editDataButton = new Button();
            editDataButton.Margin = margin;
            Grid.SetColumn(editDataButton, columns - 1);
            Grid.SetRow(editDataButton, all_rows - 1);
            contentGrid.Children.Add(editDataButton);

            editDataButton.Content = "Edytuj";

            editDataButton.Click += new RoutedEventHandler(this.EdytujOcene_Click);
        }

        public void CreateOcenianieOcenaEdycja()
        {
            Clear_Content();

            ManageMainButtons(2);

            int all_rows = 7;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Kurs:... Grupa:...";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < all_rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }


            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 0);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 1);
                Grid.SetColumnSpan(infoBorder, columns - 1);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                TextBlock tableRowTitle = new TextBlock();
                Grid.SetColumn(tableRowTitle, 0);
                Grid.SetRow(tableRowTitle, j);
                tableRowTitleList.Add(tableRowTitle);
                contentGrid.Children.Add(tableRowTitleList[j]);
            }

            tableRowTitleList[0].Text = "Numer Indeksu";
            tableRowTitleList[1].Text = "Kurs";
            tableRowTitleList[2].Text = "Grupa";
            tableRowTitleList[3].Text = "Typ zajęć";
            tableRowTitleList[4].Text = "Ocena";
            tableRowTitleList[5].Text = "Status oceny";

            for (int j = 0; j < rows; j++)
            {
                TextBox tableRowContent = new TextBox();
                tableRowContent.Margin = margin;
                tableRowContent.IsEnabled = false;
                Grid.SetColumn(tableRowContent, 1);
                Grid.SetColumnSpan(tableRowContent, columns - 1);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

            tableRowContentList[4].IsEnabled = true;
            tableRowContentList[5].IsEnabled = true;

            saveChangesButton = new Button();
            saveChangesButton.Margin = margin;
            Grid.SetColumn(saveChangesButton, columns - 1);
            Grid.SetRow(saveChangesButton, all_rows - 1);
            contentGrid.Children.Add(saveChangesButton);
            saveChangesButton.Content = "Zapisz";
            saveChangesButton.Click += new RoutedEventHandler(this.EdytujStatusOceny_Click);

            dropChangesButton = new Button();
            dropChangesButton.Margin = margin;
            Grid.SetColumn(dropChangesButton, columns - 2);
            Grid.SetRow(dropChangesButton, all_rows - 1);
            contentGrid.Children.Add(dropChangesButton);
            dropChangesButton.Content = "Anuluj";
            dropChangesButton.Click += new RoutedEventHandler(this.AnulujEdycjeOceny_Click);
        }

        private void Clear_Content()
        {
            contentGrid.Children.Clear();
            contentGrid.RowDefinitions.Clear();
            contentGrid.ColumnDefinitions.Clear();
        }

        private void ManageMainButtons(int activeButton)
        {
            if (activeButton == 1)
            {
                TwojeDane.IsEnabled = false;
                Zajecia.IsEnabled = true;
                Ocenianie.IsEnabled = true;
            }
            else if (activeButton == 2)
            {
                TwojeDane.IsEnabled = true;
                Zajecia.IsEnabled = false;
                Ocenianie.IsEnabled = true;
            }
            else if (activeButton == 3)
            {
                TwojeDane.IsEnabled = true;
                Zajecia.IsEnabled = true;
                Ocenianie.IsEnabled = false;
            }
            else if (activeButton == 4)
            {
                TwojeDane.IsEnabled = true;
                Zajecia.IsEnabled = true;
                Ocenianie.IsEnabled = true;
            }
            else throw new ArgumentException("Parameter must be in range 1 - 4", nameof(activeButton));
        }

        // The metod close the window after click on button
        /// <summary>
        /// The metod close the window after click on button
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ocenianie_Click(object sender, RoutedEventArgs e)
        {
            CreateOcenianie();
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow(this);
            login.ShowDialog();
        }

        private void ZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserData();
        }

        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsoboweEdycja();
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
        }

        private void EdytujOcene_Click(object sender, RoutedEventArgs e)
        {
            CreateOcenianieOcenaEdycja();
        }

        private void EdytujStatusOceny_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AnulujEdycjeOceny_Click(object sender, RoutedEventArgs e)
        {
            CreateOcenianieOcena();
        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaProwadzonekursy();
        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TwojeDane_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
            //CreateOcenianieOcena();
        }

        private void Zajecia_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaProwadzonekursy();
        }

        private void ZajeciaDataGrid_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPrzegladanieGrup();
        }

        private void WybierzUzytkownika_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (this.contentDataGrid.SelectedIndex >= 0 && this.contentDataGrid.AlternationCount >= 0)
            {
                LecturerCursesDataView course = (LecturerCursesDataView)this.contentDataGrid.SelectedItems[0];

                choosenCourseID = course.IndexCourse;
                choosenCourseName = course.NameOfCourse;
                CreateZajeciaPrzegladanieGrup();
            }
        }

        private void UpdateUserData()
        {
            userDate = DbService.DataBaseShowUserData(signInUser);
            tableRowContentList[0].Text = userDate.Pesel;
            tableRowContentList[1].Text = userDate.Name;
            tableRowContentList[2].Text = userDate.Surname;
            tableRowContentList[3].Text = userDate.DateOfBirth.ToString("MM/dd/yyyy");
            tableRowContentList[4].Text = userDate.Sex.ToString();
            tableRowContentList[5].Text = userDate.ContactNumber;
            tableRowContentList[6].Text = userDate.Country;
            tableRowContentList[7].Text = userDate.City;
            tableRowContentList[8].Text = userDate.Street;
            tableRowContentList[9].Text = userDate.HouseNumber;
            tableRowContentList[10].Text = userDate.ApartmentNumber;
            tableRowContentList[11].Text = userDate.PostalCode;
        }

        private void UpdateLecturerCourses()
        {
            lecturerCursesDatas = DbService.DataBaseShowLecturerCurses(signInUser);
            contentDataGrid.ItemsSource = lecturerCursesDatas.ToList();
        }

        private void UpdateLecturerGroups()
        {
            lecturerGroupsDatas = DbService.DataBaseShowLecturerGroups(signInUser, choosenCourseID);
            contentDataGrid.ItemsSource = lecturerGroupsDatas.ToList();
        }

        private void UpdateLecturerStudents()
        {
            lecturerStudentsDatas = DbService.DataBaseShowLecturerStudents(signInUser);
            contentDataGrid.ItemsSource = lecturerStudentsDatas.ToList();
        }

        private void ChangeUserData()
        {
            ChangeDataProcedure user = new ChangeDataProcedure();
            string errorMessage = "";

            if (!DataValidation.DataValidation.ValidName(tableRowContentList[1].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidSurname(tableRowContentList[2].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidSex(tableRowContentList[4].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidContactNumber(tableRowContentList[5].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidCountry(tableRowContentList[6].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidCity(tableRowContentList[7].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidStreet(tableRowContentList[8].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidHouseNumber(tableRowContentList[9].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidApartmentNumber(tableRowContentList[10].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidPostalCode(tableRowContentList[11].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            user.Name = tableRowContentList[1].Text;
            user.Surname = tableRowContentList[2].Text;
            user.Sex = tableRowContentList[4].Text;
            user.ContactNumber = tableRowContentList[5].Text;
            user.Country = tableRowContentList[6].Text;
            user.City = tableRowContentList[7].Text;
            user.Street = tableRowContentList[8].Text;
            user.HouseNumber = tableRowContentList[9].Text;
            user.ApartmentNumber = tableRowContentList[10].Text;
            user.PostalCode = tableRowContentList[11].Text;
            user.CurrentUser = signInUser.UserID;
            DbService.DataBaseChangeUserData(user);
            AlertWindow alertWindow2 = new AlertWindow("Zmiana danych przebiegła pomyślnie.");
            alertWindow2.ShowDialog();
            CreateDaneOsobowe();
        }
    }
}
