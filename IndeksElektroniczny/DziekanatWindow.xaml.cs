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
    /// Logika interakcji dla klasy DziekanatWindow.xaml
    /// </summary>
    public partial class DziekanatWindow : Window
    {

        public List<TextBox> tableRowContentList { get; set; }
        public List<TextBlock> tableRowTitleList { get; set; }
        public Button buttonSearch { get; set; }
        public Button saveChangesButton { get; set; }
        public Button dropChangesButton { get; set; }
        public Button editDataButton { get; set; }
        public Button saveStudentChangesButton { get; set; }
        public Button addStudentButton { get; set; }
        public Button deleteStudentButton { get; set; }
        public Button saveStudentButton { get; set; }
        public DataGrid contentDataGrid { get; set; }
        public TextBox textBoxSearch { get; set; }
        public ComboBox comboBoxSearch { get; set; }

        public static System.Windows.Thickness margin = new Thickness(5);

        private User signInUser;
        private DataBaseMySqlService DbService;
        private UserDataView userDate;
        List<StudentsList> studentListDatas;
        StudentPreviewDataView studentPreviewData;
        private int choosenIndexNumber;
        private int choosenUserID;

        public DziekanatWindow(Window loginWindow, User signInUser_a, DataBaseMySqlService DbService_a)
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

        public void CreateStudenciLista()
        {
            Clear_Content();

            ManageMainButtons(2);

            int rows = 12;
            int columns = 5;

            titleTextBlock.Text = "Lista studentów";

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
            Grid.SetRowSpan(contentDataGrid, rows - 1);
            contentGrid.Children.Add(contentDataGrid);

            addStudentButton = new Button();
            addStudentButton.Margin = margin;
            Grid.SetColumn(addStudentButton, columns - 1);
            Grid.SetRow(addStudentButton, rows);
            contentGrid.Children.Add(addStudentButton);

            addStudentButton.Content = "Dodaj studenta";
            addStudentButton.Click += new RoutedEventHandler(this.DodajStudenta_Click);
            contentDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.WybierzUzytkownika_SelectionChanged);

            UpdateStudentsList();
        }

        public void CreateStudenciStudent()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 17;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane nowego studenta";

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

            tableRowTitleList[0].Text = "Numer indeksu";
            tableRowTitleList[1].Text = "Pesel";
            tableRowTitleList[2].Text = "Imie";
            tableRowTitleList[3].Text = "Nazwisko";
            tableRowTitleList[4].Text = "Data urodzenia";
            tableRowTitleList[5].Text = "Płeć";
            tableRowTitleList[6].Text = "Numer kontaktowy";
            tableRowTitleList[7].Text = "Kraj zamieszkania";
            tableRowTitleList[8].Text = "Miasto";
            tableRowTitleList[9].Text = "Ulica";
            tableRowTitleList[10].Text = "Numer domu";
            tableRowTitleList[11].Text = "Numer lokalu";
            tableRowTitleList[12].Text = "Kod pocztowy";
            tableRowTitleList[13].Text = "Kierunek";
            tableRowTitleList[14].Text = "Semestr";
            tableRowTitleList[15].Text = "Stopień";

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
            tableRowContentList[1].IsEnabled = false;
            tableRowContentList[4].IsEnabled = false;

            saveStudentButton = new Button();
            saveStudentButton.Margin = margin;
            Grid.SetColumn(saveStudentButton, columns - 1);
            Grid.SetRow(saveStudentButton, all_rows - 1);
            contentGrid.Children.Add(saveStudentButton);

            deleteStudentButton = new Button();
            deleteStudentButton.Margin = margin;
            Grid.SetColumn(deleteStudentButton, columns - 2);
            Grid.SetRow(deleteStudentButton, rows);
            contentGrid.Children.Add(deleteStudentButton);
            deleteStudentButton.Content = "Usuń studenta";
            deleteStudentButton.Click += new RoutedEventHandler(this.UsunStudenta_Click);

            saveStudentChangesButton = new Button();
            saveStudentChangesButton.Margin = margin;
            Grid.SetColumn(saveStudentChangesButton, columns - 1);
            Grid.SetRow(saveStudentChangesButton, rows);
            contentGrid.Children.Add(saveStudentChangesButton);

            saveStudentChangesButton.Content = "Zapisz zmiany";
            saveStudentChangesButton.Click += new RoutedEventHandler(this.ZapiszZmianyStudenta_Click);

            ShowChoosenStudentData();
        }

        public void CreateNowyStudent()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 19;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane nowego studenta";

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
            tableRowTitleList[12].Text = "Login";
            tableRowTitleList[13].Text = "Hasło";
            tableRowTitleList[14].Text = "Rola";
            tableRowTitleList[15].Text = "Kierunek";
            tableRowTitleList[16].Text = "Semestr";
            tableRowTitleList[17].Text = "Stopień";

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

            saveStudentButton = new Button();
            saveStudentButton.Margin = margin;
            Grid.SetColumn(saveStudentButton, columns - 1);
            Grid.SetRow(saveStudentButton, all_rows - 1);
            contentGrid.Children.Add(saveStudentButton);

            saveStudentButton.Content = "Zapisz studenta";
            saveStudentButton.Click += new RoutedEventHandler(this.ZapiszStudenta_Click);
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
                Studenci.IsEnabled = true;
            }
            else if (activeButton == 2)
            {
                TwojeDane.IsEnabled = true;
                Studenci.IsEnabled = false;
            }
            else if (activeButton == 3)
            {
                TwojeDane.IsEnabled = true;
                Studenci.IsEnabled = true;
            }
            else throw new ArgumentException("Parameter must be in range 1 - 3", nameof(activeButton));
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

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow(this);
            login.ShowDialog();
        }

        private void TwojeDane_Click(object sender, RoutedEventArgs e)
        {
            CreateDaneOsobowe();
        }

        private void Studenci_Click(object sender, RoutedEventArgs e)
        {
            CreateStudenciLista();
        }

        private void DodajStudenta_Click(object sender, RoutedEventArgs e)
        {
            CreateNowyStudent();
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

        private void ZapiszZmianyStudenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZapiszStudenta_Click(object sender, RoutedEventArgs e)
        {
            AddNewStudent();
        }

        private void UsunStudenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WybierzUzytkownika_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (this.contentDataGrid.SelectedIndex >= 0 && this.contentDataGrid.AlternationCount >= 0)
            {
                StudentsList student = (StudentsList)this.contentDataGrid.SelectedItems[0];
                choosenIndexNumber = student.IndexNumber;
                choosenUserID = student.UserID;
                CreateStudenciStudent();
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

        private void UpdateStudentsList()
        {
            studentListDatas = DbService.DataBaseShowStudentsList();
            contentDataGrid.ItemsSource = studentListDatas.ToList();
        }

        private void ShowChoosenStudentData()
        {
            studentPreviewData = DbService.DataBaseShowStudentPreview(choosenIndexNumber);
            tableRowContentList[0].Text = studentPreviewData.IndexNumber.ToString();
            tableRowContentList[1].Text = studentPreviewData.Pesel;
            tableRowContentList[2].Text = studentPreviewData.Name;
            tableRowContentList[3].Text = studentPreviewData.Surname;
            tableRowContentList[4].Text = studentPreviewData.DateOfBirth.ToString();
            tableRowContentList[5].Text = studentPreviewData.Sex.ToString();
            tableRowContentList[6].Text = studentPreviewData.ContactNumber;
            tableRowContentList[7].Text = studentPreviewData.Country;
            tableRowContentList[8].Text = studentPreviewData.City;
            tableRowContentList[9].Text = studentPreviewData.Street;
            tableRowContentList[10].Text = studentPreviewData.HouseNumber;
            tableRowContentList[11].Text = studentPreviewData.ApartmentNumber;
            tableRowContentList[12].Text = studentPreviewData.PostalCode;
            tableRowContentList[13].Text = studentPreviewData.StudyFiled;
            tableRowContentList[14].Text = studentPreviewData.Degree.ToString();
            tableRowContentList[15].Text = studentPreviewData.Semestr.ToString();
        }


        private void AddNewStudent()
        {
            AddUserProcedure newUser = new AddUserProcedure();
            string errorMessage = "";

            if (!DataValidation.DataValidation.ValidPesel(tableRowContentList[0].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

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

            if (!DataValidation.DataValidation.ValidDateOfBirth(tableRowContentList[3].Text, out errorMessage))
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

            if (!DataValidation.DataValidation.ValidLogin(tableRowContentList[12].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidPassword(tableRowContentList[13].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (!DataValidation.DataValidation.ValidRole(tableRowContentList[14].Text, out errorMessage))
            {
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (tableRowContentList[14].Text == "s")
            {
                if (!DataValidation.DataValidation.ValidStudyField(tableRowContentList[15].Text, out errorMessage))
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }

                if (!DataValidation.DataValidation.ValidDegree(tableRowContentList[16].Text, out errorMessage))
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }

                if (!DataValidation.DataValidation.ValidSemestr(tableRowContentList[17].Text, out errorMessage))
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }
                newUser.StudyField = tableRowContentList[15].Text;
                newUser.Degree = int.Parse(tableRowContentList[16].Text);
                newUser.Semestr = int.Parse(tableRowContentList[17].Text);
            }
            else
            {
                errorMessage = "Jako pracownik dziekanatu możesz dodać tylko studenta.\n" + "Jako rola użytkownika musisz więc wpisać 's'";
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            newUser.Pesel = tableRowContentList[0].Text;
            newUser.Name = tableRowContentList[1].Text;
            newUser.Surname = tableRowContentList[2].Text;
            newUser.DateOfBirth = tableRowContentList[3].Text;
            newUser.Sex = tableRowContentList[4].Text.ToCharArray()[0];
            newUser.ContactNumber = tableRowContentList[5].Text;
            newUser.Country = tableRowContentList[6].Text;
            newUser.City = tableRowContentList[7].Text;
            newUser.Street = tableRowContentList[8].Text;
            newUser.HouseNumber = tableRowContentList[9].Text;
            newUser.ApartmentNumber = tableRowContentList[10].Text;
            newUser.PostalCode = tableRowContentList[11].Text;
            newUser.Login = tableRowContentList[12].Text;
            newUser.Password = tableRowContentList[13].Text;
            newUser.Role = tableRowContentList[14].Text;
            newUser.CurrentUser = signInUser.UserID;
            DbService.DataBaseAddUser(newUser);
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

