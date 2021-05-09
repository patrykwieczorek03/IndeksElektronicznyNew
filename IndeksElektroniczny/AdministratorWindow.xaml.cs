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
    /// Logika interakcji dla klasy AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public List<TextBox> tableRowContentList { get; set; }
        public List<TextBlock> tableRowTitleList { get; set; }
        public Button buttonSearch { get; set; }
        public Button saveChangesButton { get; set; }
        public Button dropChangesButton { get; set; }
        public Button editDataButton { get; set; }
        public Button saveUserChangesButton { get; set; }
        public Button addUserButton { get; set; }
        public Button deleteUserButton { get; set; }
        public Button saveUserButton { get; set; }
        public DataGrid contentDataGrid { get; set; }
        public TextBox textBoxSearch { get; set; }
        public ComboBox comboBoxSearch { get; set; }

        public static System.Windows.Thickness margin = new Thickness(5);

        private User signInUser;
        private DataBaseMySqlService DbService;
        private UserDataView userDate;
        List<UsersList> userListDatas;
        UserPreviewDataView userPreviewData;
        private int choosenUserID;


        public AdministratorWindow(Window loginWindow, User signInUser_a, DataBaseMySqlService DbService_a)
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

        public void CreateUzytkownicyLista()
        {
            Clear_Content();

            ManageMainButtons(2);

            int rows = 12;
            int columns = 5;

            titleTextBlock.Text = "Lista Użytkowników";

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
            DataGridUpdateUsersList();
            contentGrid.Children.Add(contentDataGrid);

            addUserButton = new Button();
            addUserButton.Margin = margin;
            Grid.SetColumn(addUserButton, columns - 1);
            Grid.SetRow(addUserButton, rows);
            contentGrid.Children.Add(addUserButton);
            addUserButton.Content = "Dodaj Użytkownika";
            addUserButton.Click += new RoutedEventHandler(this.DodajUzytkownika_Click);
            contentDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.WybierzUzytkownika_SelectionChanged);

            UpdateUsersList();
        }

        public void CreateUzytkownicyUser()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 16;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane użytkownika";

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
            tableRowContentList[14].IsEnabled = false;

            saveUserChangesButton = new Button();
            saveUserChangesButton.Margin = margin;
            Grid.SetColumn(saveUserChangesButton, columns - 1);
            Grid.SetRow(saveUserChangesButton, all_rows - 1);
            contentGrid.Children.Add(saveUserChangesButton);
            saveUserChangesButton.Content = "Zapisz Zmiany";
            saveUserChangesButton.Click += new RoutedEventHandler(this.ZapiszZmianyUzytkownika_Click);

            deleteUserButton = new Button();
            deleteUserButton.Margin = margin;
            Grid.SetColumn(deleteUserButton, columns - 2);
            Grid.SetRow(deleteUserButton, all_rows - 1);
            contentGrid.Children.Add(deleteUserButton);
            deleteUserButton.Content = "Usuń użytkownika";
            deleteUserButton.Click += new RoutedEventHandler(this.UsunUzytkownika_Click);

            dropChangesButton = new Button();
            dropChangesButton.Margin = margin;
            Grid.SetColumn(dropChangesButton, columns - 3);
            Grid.SetRow(dropChangesButton, all_rows - 1);
            contentGrid.Children.Add(dropChangesButton);
            dropChangesButton.Content = "Anuluj";
            dropChangesButton.Click += new RoutedEventHandler(this.AnulujDaneUzytkownika_Click);

            ShowChoosenUserData();
        }

        public void CreateNowyUzytkownik()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 19;
            int rows = all_rows - 1;
            int columns = 6;

            titleTextBlock.Text = "Dane nowego użytkownika";

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

            saveUserButton = new Button();
            saveUserButton.Margin = margin;
            Grid.SetColumn(saveUserButton, columns - 1);
            Grid.SetRow(saveUserButton, all_rows - 1);
            contentGrid.Children.Add(saveUserButton);
            saveUserButton.Content = "Zapisz Użytkownika";
            saveUserButton.Click += new RoutedEventHandler(this.ZapiszUzytkownika_Click);

            dropChangesButton = new Button();
            dropChangesButton.Margin = margin;
            Grid.SetColumn(dropChangesButton, columns - 2);
            Grid.SetRow(dropChangesButton, all_rows - 1);
            contentGrid.Children.Add(dropChangesButton);
            dropChangesButton.Content = "Anuluj";
            dropChangesButton.Click += new RoutedEventHandler(this.Uzytkownicy_Click);
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
                Uzytkownicy.IsEnabled = true;
            }
            else if (activeButton == 2)
            {
                TwojeDane.IsEnabled = true;
                Uzytkownicy.IsEnabled = false;
            }
            else if (activeButton == 3)
            {
                TwojeDane.IsEnabled = true;
                Uzytkownicy.IsEnabled = true;
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

        private void Uzytkownicy_Click(object sender, RoutedEventArgs e)
        {
            CreateUzytkownicyLista();
        }

        private void DodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            CreateNowyUzytkownik();
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

        private void AnulujDaneUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            CreateUzytkownicyLista();
        }

        private void ZapiszZmianyUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            ChangeOtherUserData();
        }

        private void ZapiszUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser();
        }

        private void UsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            bool answer = false;
            ZapytanieWindow question = new ZapytanieWindow("Czy na pewno chcesz usunąć uzytkownika?");
            answer = question.ShowDialog(true);
            if (answer)
            {
                DeleteUser();
                CreateUzytkownicyLista();
            }
        }

        private void WybierzUzytkownika_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (this.contentDataGrid.SelectedIndex >= 0 && this.contentDataGrid.AlternationCount >= 0)
            {
                UsersList user = (UsersList)this.contentDataGrid.SelectedItems[0];
                choosenUserID = user.UserID;
                CreateUzytkownicyUser();
            }
        }

        private void UpdateUserData()
        {
            userDate = DbService.DataBaseShowUserData(signInUser);
            tableRowContentList[0].Text = userDate.Pesel;
            tableRowContentList[1].Text = userDate.Name;
            tableRowContentList[2].Text = userDate.Surname;
            tableRowContentList[3].Text = userDate.DateOfBirth.ToString();
            tableRowContentList[4].Text = userDate.Sex.ToString();
            tableRowContentList[5].Text = userDate.ContactNumber;
            tableRowContentList[6].Text = userDate.Country;
            tableRowContentList[7].Text = userDate.City;
            tableRowContentList[8].Text = userDate.Street;
            tableRowContentList[9].Text = userDate.HouseNumber;
            tableRowContentList[10].Text = userDate.ApartmentNumber;
            tableRowContentList[11].Text = userDate.PostalCode;
        }

        private void UpdateUsersList()
        {
            userListDatas = DbService.DataBaseShowUsersList();
            foreach (var item in userListDatas)
            {
                contentDataGrid.Items.Add(item);
            }
        }

        private void DataGridUpdateUsersList()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "ID użytkownika";
            c1.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c1.Binding = new Binding("UserID");
            contentDataGrid.Columns.Add(c1);

            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Imie";
            c2.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c2.Binding = new Binding("Name");
            contentDataGrid.Columns.Add(c2);

            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "Nazwisko";
            c3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c3.Binding = new Binding("Surname");
            contentDataGrid.Columns.Add(c3);

            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "Rola";
            c4.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            c4.Binding = new Binding("Role");
            contentDataGrid.Columns.Add(c4);
        }

        private void ShowChoosenUserData()
        {
            userPreviewData = DbService.DataBaseShowUserPreview(choosenUserID);
            tableRowContentList[0].Text = userPreviewData.Pesel;
            tableRowContentList[1].Text = userPreviewData.Name;
            tableRowContentList[2].Text = userPreviewData.Surname;
            tableRowContentList[3].Text = userPreviewData.DateOfBirth.ToString("MM/dd/yyyy");
            tableRowContentList[4].Text = userPreviewData.Sex.ToString();
            tableRowContentList[5].Text = userPreviewData.ContactNumber;
            tableRowContentList[6].Text = userPreviewData.Country;
            tableRowContentList[7].Text = userPreviewData.City;
            tableRowContentList[8].Text = userPreviewData.Street;
            tableRowContentList[9].Text = userPreviewData.HouseNumber;
            tableRowContentList[10].Text = userPreviewData.ApartmentNumber;
            tableRowContentList[11].Text = userPreviewData.PostalCode;
            tableRowContentList[12].Text = userPreviewData.Login;
            tableRowContentList[13].Text = userPreviewData.Password;
            tableRowContentList[14].Text = userPreviewData.Role.ToString();
        }

        private void AddNewUser()
        {
            AddUserProcedure newUser = new AddUserProcedure();
            FieldOfStudyCheckProcedure field = new FieldOfStudyCheckProcedure();
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

                if (!DataValidation.DataValidation.ValidSemestr(tableRowContentList[16].Text, out errorMessage))
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }

                if (!DataValidation.DataValidation.ValidDegree(tableRowContentList[17].Text, out errorMessage))
                {
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }

                field.StudyField = tableRowContentList[15].Text;
                field.Semestr = int.Parse(tableRowContentList[16].Text);
                field.Degree = int.Parse(tableRowContentList[17].Text);

                if (!DbService.DataBaseCheckFieldOfStudy(field))
                {
                    errorMessage = "Nie istnieje kierunek lub nie ma takiego semestru na danym stopniu studiów.";
                    AlertWindow alertWindow = new AlertWindow(errorMessage);
                    alertWindow.ShowDialog();
                    return;
                }

                newUser.StudyField = tableRowContentList[15].Text;
                newUser.Semestr = int.Parse(tableRowContentList[16].Text);
                newUser.Degree = int.Parse(tableRowContentList[17].Text);
            }
            else
            {
                newUser.StudyField = "";
                newUser.Degree = 0;
                newUser.Semestr = 0;
            }

            if (DbService.DataBaseCheckPesel(tableRowContentList[0].Text))
            {
                errorMessage = "Taki pesel już istnieje w bazie danych.";
                AlertWindow alertWindow = new AlertWindow(errorMessage);
                alertWindow.ShowDialog();
                return;
            }

            if (DbService.DataBaseCheckLogin(tableRowContentList[12].Text))
            {
                errorMessage = "Taki login już istnieje w bazie danych.";
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

            errorMessage = "Dodano użytkownika.";
            AlertWindow alertWindow2 = new AlertWindow(errorMessage);
            alertWindow2.ShowDialog();
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

        private void ChangeOtherUserData()
        {
            ChangeDataProcedure user = new ChangeDataProcedure();
            ChangeLoginPasswordProcedure loginPassword = new ChangeLoginPasswordProcedure();
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

            if (DbService.DataBaseCheckLogin(tableRowContentList[12].Text))
            {
                errorMessage = "Taki login już istnieje w bazie danych.";
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
            user.CurrentUser = choosenUserID;

            loginPassword.UserToChangeLoginPassword = choosenUserID;
            loginPassword.Login = tableRowContentList[12].Text;
            loginPassword.Password = tableRowContentList[13].Text;
            loginPassword.CurrentUser = signInUser.UserID;

            DbService.DataBaseChangeUserData(user);
            DbService.DataBaseChangeLoginPassword(loginPassword);
            AlertWindow alertWindow2 = new AlertWindow("Zmiana danych przebiegła pomyślnie.");
            alertWindow2.ShowDialog();
            CreateUzytkownicyLista();
        }

        private void DeleteUser()
        {
            DeleteUserProcedure user = new DeleteUserProcedure();
            user.UserToDrop = choosenUserID;
            user.CurrentUser = signInUser.UserID;
            DbService.DataBaseDeleteUser(user);
            AlertWindow alertWindow2 = new AlertWindow("Użytkownik został usunięty.");
            alertWindow2.ShowDialog();
        }
    }
}
