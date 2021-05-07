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
    /// Logika interakcji dla klasy StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public List<TextBox> tableRowContentList { get; set; }
        public List<TextBlock> tableRowTitleList { get; set; }
        public Button saveChangesButton { get; set; }
        public Button dropChangesButton { get; set; }
        public Button editDataButton { get; set; }
        public Button buttonSearch { get; set; }
        public Button buttonStudentData { get; set; }
        public Button buttonGrades { get; set; }
        public Button buttonGroups { get; set; }
        public Button buttonTimetable { get; set; }
        public DataGrid contentDataGrid { get; set; }
        public TextBox textBoxSearch { get; set; }
        public ComboBox comboBoxSearch { get; set; }

        public static System.Windows.Thickness margin = new Thickness(5);

        private User signInUser;
        private DataBaseMySqlService DbService;
        private UserDataView userDate;
        private StudentDataView studentDate;
        List<StudentGradesDataView> studentGradesDatas;
        List<StudentTimeTableDataView> studentTimeTableDatas;
        List<BrowseGroupsDataView> browseGroupsDatas;

        public StudentWindow(Window loginWindow, User signInUser_a, DataBaseMySqlService DbService_a)
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
                Grid.SetColumnSpan(infoBorder, columns-1);
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
                Grid.SetColumnSpan(tableRowContent, columns-1);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

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


        public void CreateIndeksDaneStudenta()
        {
            Clear_Content();

            ManageMainButtons(2);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Dane Studenta";

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


            buttonStudentData = new Button();
            buttonStudentData.Margin = margin;
            Grid.SetColumn(buttonStudentData, 0);
            Grid.SetRow(buttonStudentData, 0);
            contentGrid.Children.Add(buttonStudentData);
            buttonStudentData.Content = "Dane Studenta";
            buttonStudentData.Click += new RoutedEventHandler(this.DaneStudenta_Click);

            buttonGrades = new Button();
            buttonGrades.Margin = margin;
            Grid.SetColumn(buttonGrades, 0);
            Grid.SetRow(buttonGrades, 1);
            contentGrid.Children.Add(buttonGrades);
            buttonGrades.Content = "Oceny";
            buttonGrades.Click += new RoutedEventHandler(this.Oceny_Click);

            buttonStudentData.IsEnabled = false;
            buttonGrades.IsEnabled = true;

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 1);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                Border infoBorder = new Border();
                infoBorder.Style = borderStyle;
                Grid.SetColumn(infoBorder, 2);
                Grid.SetColumnSpan(infoBorder, columns - 2);
                Grid.SetRow(infoBorder, j);
                contentGrid.Children.Add(infoBorder);
            }

            for (int j = 0; j < rows; j++)
            {
                TextBlock tableRowTitle = new TextBlock();
                Grid.SetColumn(tableRowTitle, 1);
                Grid.SetRow(tableRowTitle, j);
                tableRowTitleList.Add(tableRowTitle);
                contentGrid.Children.Add(tableRowTitleList[j]);
            }

            tableRowTitleList[0].Text = "Nazwa kierunku";
            tableRowTitleList[1].Text = "Stopień";
            tableRowTitleList[2].Text = "Semestr";
            tableRowTitleList[3].Text = "Numer indeksu";

            for (int j = 0; j < rows; j++)
            {
                TextBox tableRowContent = new TextBox();
                tableRowContent.Margin = margin;
                tableRowContent.IsEnabled = false;
                Grid.SetColumn(tableRowContent, 2);
                Grid.SetColumnSpan(tableRowContent, columns - 2);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }

            UpdateStudentData();
        }

        public void CreateIndeksOceny()
        {
            Clear_Content();

            ManageMainButtons(2);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Oceny";

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

            buttonStudentData = new Button();
            buttonStudentData.Margin = margin;
            Grid.SetColumn(buttonStudentData, 0);
            Grid.SetRow(buttonStudentData, 0);
            contentGrid.Children.Add(buttonStudentData);
            buttonStudentData.Content = "Dane Studenta";
            buttonStudentData.Click += new RoutedEventHandler(this.DaneStudenta_Click);

            buttonGrades = new Button();
            buttonGrades.Margin = margin;
            Grid.SetColumn(buttonGrades, 0);
            Grid.SetRow(buttonGrades, 1);
            contentGrid.Children.Add(buttonGrades);
            buttonGrades.Content = "Oceny";
            buttonGrades.Click += new RoutedEventHandler(this.Oceny_Click);

            buttonStudentData.IsEnabled = true;
            buttonGrades.IsEnabled = false;

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 1);
            Grid.SetColumnSpan(contentDataGrid, columns - 1);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows);
            contentGrid.Children.Add(contentDataGrid);

            ////////////////////////////////////////////////
            //contentDataGrid.Columns.Counts = columns;

            //DataGridTextColumn name = new DataGridTextColumn();
            //name.Header = "ID";
            //contentDataGrid.Columns[0].Header = name.Header;

            //DataGridTemplateColumn button = new DataGridTemplateColumn();
            //button.Header = "Button";
            //button.
            //contentDataGrid.CanUserAddRows = false;
            //contentDataGrid.CanUserDeleteRows = false;

            //contentDataGrid.AlternatingRowBackground = Brushes.Gray;

            //DataGridBu button = new DataGridViewButtonColumn();
            //button.HeaderText = "Podgląd użytkownika";

            //contentDataGrid.FrozenColumnCount = 0;
            //contentDataGrid.Columns[0].Header = "ID";
            //contentDataGrid.AutoGenerateColumns = false;

            UpdateStudentGradesData();
            //DataGridColumn item = new DataGridColumn();
            //contentDataGrid.Columns.Add("Text");
            
            //contentDataGrid.Columns.Count();
            //DataGridColumn kolumna = new DataGrid;
            //kolumna.Header = "ID";
            //DataGridColumn item;
            //item.Header = "AAA";
            //contentDataGrid.AutoGenerateColumns = true;
            //contentDataGrid.Columns.Add(item);
            //contentDataGrid.ItemsSource = "{Binding}";
            //contentDataGrid.Columns[0].Header = "ID";
            //contentDataGrid.Columns[1].ClipboardContentBinding = "ID";
        }

        public void CreateZajeciaPlanZajec()
        {
            Clear_Content();

            ManageMainButtons(3);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Plan zajęć";

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

            buttonGroups = new Button();
            buttonGroups.Margin = margin;
            Grid.SetColumn(buttonGroups, 0);
            Grid.SetRow(buttonGroups, 0);
            contentGrid.Children.Add(buttonGroups);
            buttonGroups.Content = "Przeglądanie grup";
            buttonGroups.Click += new RoutedEventHandler(this.PrzegladanieGrup_Click);

            buttonTimetable = new Button();
            buttonTimetable.Margin = margin;
            Grid.SetColumn(buttonTimetable, 0);
            Grid.SetRow(buttonTimetable, 1);
            contentGrid.Children.Add(buttonTimetable);
            buttonTimetable.Content = "Plan zajęć";
            buttonTimetable.Click += new RoutedEventHandler(this.PlanZajec_Click);

            buttonTimetable.IsEnabled = false;
            buttonGroups.IsEnabled = true;

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 1);
            Grid.SetColumnSpan(contentDataGrid, columns - 1);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows);
            contentGrid.Children.Add(contentDataGrid);

            UpdateStudentTimeTableData();
        }

        public void CreateZajeciaPrzegladanieGrup()
        {
            Clear_Content();

            ManageMainButtons(3);

            int rows = 4;
            int columns = 5;

            titleTextBlock.Text = "Przeglądanie grup";

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

            buttonGroups = new Button();
            buttonGroups.Margin = margin;
            Grid.SetColumn(buttonGroups, 0);
            Grid.SetRow(buttonGroups, 0);
            contentGrid.Children.Add(buttonGroups);
            buttonGroups.Content = "Przeglądanie grup";
            buttonGroups.Click += new RoutedEventHandler(this.PrzegladanieGrup_Click);

            buttonTimetable = new Button();
            buttonTimetable.Margin = margin;
            Grid.SetColumn(buttonTimetable, 0);
            Grid.SetRow(buttonTimetable, 1);
            contentGrid.Children.Add(buttonTimetable);
            buttonTimetable.Content = "Plan zajęć";
            buttonTimetable.Click += new RoutedEventHandler(this.PlanZajec_Click);

            buttonSearch = new Button();
            buttonSearch.Margin = margin;
            buttonSearch.Content = "Szukaj";
            Grid.SetColumn(buttonSearch, 4);
            Grid.SetRow(buttonSearch, 0);
            contentGrid.Children.Add(buttonSearch);
            buttonSearch.Click += new RoutedEventHandler(this.Szukaj_Click);

            buttonTimetable.IsEnabled = true;
            buttonGroups.IsEnabled = false;

            textBoxSearch = new TextBox();
            textBoxSearch.Margin = margin;
            Grid.SetColumn(textBoxSearch, 1);
            Grid.SetColumnSpan(textBoxSearch, columns - 3);
            Grid.SetRow(textBoxSearch, 0);
            contentGrid.Children.Add(textBoxSearch);

            comboBoxSearch = new ComboBox();
            comboBoxSearch.Margin = margin;
            Grid.SetColumn(comboBoxSearch, 3);
            Grid.SetRow(comboBoxSearch, 0);
            contentGrid.Children.Add(comboBoxSearch);

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 1);
            Grid.SetColumnSpan(contentDataGrid, columns - 1);
            Grid.SetRow(contentDataGrid, 1);
            Grid.SetRowSpan(contentDataGrid, rows - 1);
            contentGrid.Children.Add(contentDataGrid);

            UpdateBrowseGroupsData();
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
                Indeks.IsEnabled = true;
                Zajecia.IsEnabled = true;
            }
            else if (activeButton == 2)
            {
                TwojeDane.IsEnabled = true;
                Indeks.IsEnabled = false;
                Zajecia.IsEnabled = true;
            }
            else if (activeButton == 3)
            {
                TwojeDane.IsEnabled = true;
                Indeks.IsEnabled = true;
                Zajecia.IsEnabled = false;
            }
            else if (activeButton == 4)
            {
                TwojeDane.IsEnabled = true;
                Indeks.IsEnabled = true;
                Zajecia.IsEnabled = true;
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

        private void Indeks_Click(object sender, RoutedEventArgs e)
        {
            CreateIndeksDaneStudenta();
        }

        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsoboweEdycja();
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
        }

        private void Zajecia_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPrzegladanieGrup();
        }

        private void ZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserData();
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow(this);
            login.ShowDialog();
        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TwojeDane_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
        }

        private void Oceny_Click(object sender, RoutedEventArgs e)
        {
            CreateIndeksOceny();
        }

        private void DaneStudenta_Click(object sender, RoutedEventArgs e)
        {
            CreateIndeksDaneStudenta();
        }

        private void PrzegladanieGrup_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPrzegladanieGrup();
        }

        private void PlanZajec_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPlanZajec();
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

        private void UpdateStudentData()
        {
            studentDate = DbService.DataBaseShowStudentData(signInUser);
            tableRowContentList[0].Text = studentDate.StudyFiled;
            tableRowContentList[1].Text = studentDate.Degree.ToString();
            tableRowContentList[2].Text = studentDate.Semestr.ToString();
            tableRowContentList[3].Text = studentDate.IndexNumber.ToString();
        }

        private void UpdateStudentGradesData()
        {
            studentGradesDatas = DbService.DataBaseShowStudentGradesData(signInUser);
            contentDataGrid.ItemsSource = studentGradesDatas.ToList();
        }

        private void UpdateStudentTimeTableData()
        {
            studentTimeTableDatas = DbService.DataBaseShowStudentTimeTableData(signInUser);
            contentDataGrid.ItemsSource = studentTimeTableDatas.ToList();
        }

        private void UpdateBrowseGroupsData()
        {
            browseGroupsDatas = DbService.DataBaseShowBrowseGroups();
            contentDataGrid.ItemsSource = browseGroupsDatas.ToList();
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
