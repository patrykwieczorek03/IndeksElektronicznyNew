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

            UpdateTheData();
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
        }

        public void CreateStudenciStudent()
        {
            Clear_Content();

            ManageMainButtons(3);

            int rows = 12;
            int columns = 5;

            titleTextBlock.Text = "Dane studenta";

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
        }


        public void CreateNowyStudent()
        {
            Clear_Content();

            ManageMainButtons(3);

            int all_rows = 18;
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

        }

        private void ZapiszZmianyStudenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZapiszStudenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UsunStudenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateTheData()
        {
            userDate = DbService.DataBaseShowUserDate(signInUser);
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
    }
}

