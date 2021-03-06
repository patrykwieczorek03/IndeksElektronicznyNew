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
        int choosenStudentIndexNumber;
        int choosenStudentGroupID;
        LecturerStudentsDataView choosenStudent;
        string currentGradeStatus;


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
            DataGridUpdateLecturerCourses();
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
            DataGridUpdateLecturerGroups();
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
            textBoxSearch.IsEnabled = false;
            contentGrid.Children.Add(textBoxSearch);

            comboBoxSearch = new ComboBox();
            comboBoxSearch.Margin = margin;
            Grid.SetColumn(comboBoxSearch, 2);
            Grid.SetColumnSpan(comboBoxSearch, columns - 3);
            Grid.SetRow(comboBoxSearch, 0);
            comboBoxSearch.SelectedIndex = 0;
            ComboBoxItem c1 = new ComboBoxItem();
            c1.FontSize = 32;
            c1.Background = Brushes.White;
            c1.Content = "Wszystkie";
            comboBoxSearch.Items.Add(c1);
            ComboBoxItem c2 = new ComboBoxItem();
            c2.FontSize = 32;
            c2.Background = Brushes.White;
            c2.Content = "Numer indeksu";
            comboBoxSearch.Items.Add(c2);
            comboBoxSearch.SelectionChanged += new SelectionChangedEventHandler(this.Wybierz_SelectedChange);
            contentGrid.Children.Add(comboBoxSearch);

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 0);
            Grid.SetColumnSpan(contentDataGrid, columns);
            Grid.SetRow(contentDataGrid, 1);
            Grid.SetRowSpan(contentDataGrid, rows - 1);
            DataGridUpdateLecturerStudents();
            contentGrid.Children.Add(contentDataGrid);

            contentDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.WybierzStudenta_SelectionChanged);

            UpdateLecturerStudents();
        }

        public void CreateOcenianieOcena()
        {
            Clear_Content();

            ManageMainButtons(4);

            int all_rows = 7;
            int rows = all_rows - 1;
            int columns = 6;

            

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

            UpdateStudentGradeData();

            titleTextBlock.Text = "Student: " + tableRowContentList[0].Text;

            if (tableRowContentList[5].Text == "E" || tableRowContentList[5].Text == "n")
            {
                editDataButton = new Button();
                editDataButton.Margin = margin;
                Grid.SetColumn(editDataButton, columns - 1);
                Grid.SetRow(editDataButton, all_rows - 1);
                contentGrid.Children.Add(editDataButton);
                editDataButton.Content = "Edytuj";
                editDataButton.Click += new RoutedEventHandler(this.EdytujOcene_Click);

                dropChangesButton = new Button();
                dropChangesButton.Margin = margin;
                Grid.SetColumn(dropChangesButton, columns - 2);
                Grid.SetRow(dropChangesButton, all_rows - 1);
                contentGrid.Children.Add(dropChangesButton);

                dropChangesButton.Content = "Powrót";

                dropChangesButton.Click += new RoutedEventHandler(this.Ocenianie_Click);
            }
            else
            {
                dropChangesButton = new Button();
                dropChangesButton.Margin = margin;
                Grid.SetColumn(dropChangesButton, columns - 1);
                Grid.SetRow(dropChangesButton, all_rows - 1);
                contentGrid.Children.Add(dropChangesButton);
                dropChangesButton.Content = "Powrót";
                dropChangesButton.Click += new RoutedEventHandler(this.Ocenianie_Click);
            }
        }

        public void CreateOcenianieOcenaEdycja()
        {
            Clear_Content();

            ManageMainButtons(4);

            int all_rows = 7;
            int rows = all_rows - 1;
            int columns = 6;

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

            UpdateStudentGradeData();
            titleTextBlock.Text = "Student: " + tableRowContentList[0].Text;
            currentGradeStatus = tableRowContentList[5].Text;


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
            if (currentGradeStatus == "E")
            {
                AddGrade();
            }
            else
            {
                ChangeGrade();
            }
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
            if (comboBoxSearch.Text != "Wszystkie")
            {
                string errorMessage = "";
                if (DataValidation.DataValidation.ValidIndexNumber(textBoxSearch.Text, out errorMessage))
                {
                    lecturerStudentsDatas.Clear();
                    contentDataGrid.Items.Clear();
                    contentDataGrid.Items.Refresh();
                    UpdateLecturerStudentsByIndexNumber();
                }
                else
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                }
            }
            else
            {
                lecturerStudentsDatas.Clear();
                contentDataGrid.Items.Clear();
                contentDataGrid.Items.Refresh();
                UpdateLecturerStudents();
            }
        }

        private void TwojeDane_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
        }

        private void Zajecia_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaProwadzonekursy();
        }

        private void ZajeciaDataGrid_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPrzegladanieGrup();
        }

        private void Wybierz_SelectedChange(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSearch.Text == "Wszystkie")
            {
                textBoxSearch.IsEnabled = true;
            }
            else
            {
                textBoxSearch.IsEnabled = false;
                textBoxSearch.Text = "";
            }
        }

        private void WybierzStudenta_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (this.contentDataGrid.SelectedIndex >= 0 && this.contentDataGrid.AlternationCount >= 0)
            {
                choosenStudent = (LecturerStudentsDataView)this.contentDataGrid.SelectedItems[0];
                choosenStudentIndexNumber = choosenStudent.IndexNumber;
                choosenStudentGroupID = choosenStudent.GroupID;
                CreateOcenianieOcena();
            }
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
            foreach (var item in lecturerCursesDatas)
            {
                contentDataGrid.Items.Add(item);
            }
        }

        private void DataGridUpdateLecturerCourses()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "ID Kursu";
            c1.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c1.Binding = new Binding("IndexCourse");
            contentDataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Nazwa kursu";
            c2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c2.Binding = new Binding("NameOfCourse");
            contentDataGrid.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Ects";
            c3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c3.Binding = new Binding("Ects");
            contentDataGrid.Columns.Add(c3);
        }

        private void UpdateLecturerGroups()
        {
            lecturerGroupsDatas = DbService.DataBaseShowLecturerGroups(signInUser, choosenCourseID);
            foreach (var item in lecturerGroupsDatas)
            {
                contentDataGrid.Items.Add(item);
            }
        }

        private void DataGridUpdateLecturerGroups()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "ID Grupy";
            c1.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c1.Binding = new Binding("GroupID");
            contentDataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Budynek";
            c2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c2.Binding = new Binding("Building");
            contentDataGrid.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Sala";
            c3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c3.Binding = new Binding("Room");
            contentDataGrid.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Dzień tygodnia";
            c4.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c4.Binding = new Binding("DayOfWeek");
            contentDataGrid.Columns.Add(c4);

            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "Typ zajęć";
            c5.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c5.Binding = new Binding("TypeOfClasses");
            contentDataGrid.Columns.Add(c5);

            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "Godz. rozp.";
            c6.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c6.Binding = new Binding("StartTime");
            contentDataGrid.Columns.Add(c6);

            DataGridTextColumn c7 = new DataGridTextColumn();
            c7.Header = "Godz. zak.";
            c7.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c7.Binding = new Binding("FinishTime");
            contentDataGrid.Columns.Add(c7);
        }

        private void UpdateLecturerStudents()
        {
            lecturerStudentsDatas = DbService.DataBaseShowLecturerStudents(signInUser);
            foreach (var item in lecturerStudentsDatas)
            {
                contentDataGrid.Items.Add(item);
            }
        }

        private void UpdateLecturerStudentsByIndexNumber()
        {
            lecturerStudentsDatas = DbService.DataBaseShowLecturerStudentsByIndexNumber(signInUser, textBoxSearch.Text);
            foreach (var item in lecturerStudentsDatas)
            {
                contentDataGrid.Items.Add(item);
            }
        }

        private void DataGridUpdateLecturerStudents()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Numer indeksu";
            c1.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c1.Binding = new Binding("IndexNumber");
            contentDataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Nazwa kursu";
            c2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c2.Binding = new Binding("NameOfCourse");
            contentDataGrid.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "ID Grupy";
            c3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c3.Binding = new Binding("GroupID");
            contentDataGrid.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Typ zajęć";
            c4.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c4.Binding = new Binding("TypeOfClasses");
            contentDataGrid.Columns.Add(c4);

            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "Ocena";
            c5.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c5.Binding = new Binding("Grade");
            contentDataGrid.Columns.Add(c5);

            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "Status oceny";
            c6.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c6.Binding = new Binding("GradeStatus");
            contentDataGrid.Columns.Add(c6);
        }

        private void UpdateStudentGradeData()
        {
            tableRowContentList[0].Text = choosenStudent.IndexNumber.ToString();
            tableRowContentList[1].Text = choosenStudent.NameOfCourse;
            tableRowContentList[2].Text = choosenStudent.GroupID.ToString();
            tableRowContentList[3].Text = choosenStudent.TypeOfClasses.ToString();
            tableRowContentList[4].Text = choosenStudent.Grade.ToString();
            tableRowContentList[5].Text = choosenStudent.GradeStatus.ToString();
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

        private void AddGrade()
        {
            string errorMessage = "";
            if (!DataValidation.DataValidation.ValidLecturerGradeStatus(tableRowContentList[5].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (tableRowContentList[5].Text != "w")
            {
                AlertWindow alertWindow = new AlertWindow("Student nie ma jeszcze oceny. Możesz więc ją wprowadzić tylko używając statusu 'w'.");
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidGrade(tableRowContentList[4].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            AddGradeProcedure grade = new AddGradeProcedure();
            grade.StudentID = int.Parse(tableRowContentList[0].Text);
            grade.GroupID = int.Parse(tableRowContentList[2].Text);
            grade.NewGrade = tableRowContentList[4].Text;
            grade.currentUser = signInUser.UserID;

            DbService.DataBaseAddGrade(grade);
            AlertWindow alertWindow2 = new AlertWindow("Wprowadzenie oceny przebiegło pomyślnie.");
            alertWindow2.ShowDialog();
            CreateOcenianie();
        }

        private void ChangeGrade()
        {
            string errorMessage = "";
            if (!DataValidation.DataValidation.ValidLecturerGradeStatus(tableRowContentList[5].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (tableRowContentList[5].Text == "w")
            {
                AlertWindow alertWindow = new AlertWindow("Student ma już wprowadzoną ocenę. Możesz więc ją zmienić tylko używając statusu 'o' lub 'p'.");
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidGrade(tableRowContentList[4].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            ChangeGradeProcedure grade = new ChangeGradeProcedure();
            grade.StudentID = int.Parse(tableRowContentList[0].Text);
            grade.GroupID = int.Parse(tableRowContentList[2].Text);
            grade.NewGrade = tableRowContentList[4].Text;
            grade.GradeStatus = tableRowContentList[5].Text;
            grade.currentUser = signInUser.UserID;

            DbService.DataBaseChangeGrade(grade);
            AlertWindow alertWindow2 = new AlertWindow("Zmiana oceny przebiegła pomyślnie.");
            alertWindow2.ShowDialog();
            CreateOcenianie();
        }
    }
}
