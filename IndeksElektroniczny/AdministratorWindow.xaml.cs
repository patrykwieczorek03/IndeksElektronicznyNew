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
                Grid.SetColumn(tableRowContent, 1);
                Grid.SetColumnSpan(tableRowContent, columns - 1);
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
            contentGrid.Children.Add(contentDataGrid);

            addUserButton = new Button();
            addUserButton.Margin = margin;
            Grid.SetColumn(addUserButton, columns - 1);
            Grid.SetRow(addUserButton, rows);
            contentGrid.Children.Add(addUserButton);

            addUserButton.Content = "Dodaj Użytkownika";
            addUserButton.Click += new RoutedEventHandler(this.DodajUzytkownika_Click);

            UpdateUsersList();
        }

        public void CreateUzytkownicyUser()
        {
            Clear_Content();

            ManageMainButtons(3);

            int rows = 12;
            int columns = 5;

            titleTextBlock.Text = "Dane Użytkownika";

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

            deleteUserButton = new Button();
            deleteUserButton.Margin = margin;
            Grid.SetColumn(deleteUserButton, columns - 2);
            Grid.SetRow(deleteUserButton, rows);
            contentGrid.Children.Add(deleteUserButton);

            deleteUserButton.Content = "Usuń Użytkownika";
            deleteUserButton.Click += new RoutedEventHandler(this.UsunUzytkownika_Click);

            saveUserChangesButton = new Button();
            saveUserChangesButton.Margin = margin;
            Grid.SetColumn(saveUserChangesButton, columns - 1);
            Grid.SetRow(saveUserChangesButton, rows);
            contentGrid.Children.Add(saveUserChangesButton);

            saveUserChangesButton.Content = "Zapisz zmiany";
            saveUserChangesButton.Click += new RoutedEventHandler(this.ZapiszZmianyUzytkownika_Click);

            
        }


        public void CreateNowyUzytkownik()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 18;
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
            tableRowTitleList[10].Text = "Kod pocztowy";
            tableRowTitleList[11].Text = "Login";
            tableRowTitleList[12].Text = "Hasło";
            tableRowTitleList[13].Text = "Rola";
            tableRowTitleList[14].Text = "Kierunek";
            tableRowTitleList[15].Text = "Semestr";
            tableRowTitleList[16].Text = "Stopień";

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

        }

        private void ZapiszZmianyUzytkownika_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZapiszUzytkownika_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UsunUzytkownika_Click(object sender, RoutedEventArgs e)
        {

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
            contentDataGrid.ItemsSource = userListDatas.ToList();
        }
    }
}
