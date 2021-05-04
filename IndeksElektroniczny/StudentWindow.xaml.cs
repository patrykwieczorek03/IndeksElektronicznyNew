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
        public List<Button> buttonList { get; set; }
        public Button buttonSearch { get; set; }
        public DataGrid contentDataGrid { get; set; }
        public TextBox textBoxSearch { get; set; }
        public ComboBox comboBoxSearch { get; set; }

        public static System.Windows.Thickness margin = new Thickness(5);

        public StudentWindow()
        {
            InitializeComponent();
            CreateDaneOsobowe();
        }

        public void CreateDaneOsobowe()
        {
            Clear_Content();

            int rows = 11;
            int columns = 3;

            titleTextBlock.Text = "Dane osobowe";

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
        }


        public void CreateIndeksDaneStudenta()
        {
            Clear_Content();

            int rows = 4;
            int columns = 5;
            int buttons = 2;

            titleTextBlock.Text = "Dane Studenta";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();
            buttonList = new List<Button>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;
            
            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < buttons; i++)
            {
                Button button = new Button();
                button.Margin = margin;
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i);
                buttonList.Add(button);
                contentGrid.Children.Add(buttonList[i]);
            }

            buttonList[0].Content = "Dane Studenta";
            buttonList[1].Content = "Oceny";

            buttonList[0].Click += new RoutedEventHandler(this.DaneStudenta_Click);
            buttonList[1].Click += new RoutedEventHandler(this.Oceny_Click);

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
                Grid.SetColumn(tableRowContent, 2);
                Grid.SetColumnSpan(tableRowContent, columns - 2);
                Grid.SetRow(tableRowContent, j);
                tableRowContentList.Add(tableRowContent);
                contentGrid.Children.Add(tableRowContentList[j]);
            }
        }

        public void CreateIndeksOceny()
        {
            Clear_Content();

            int rows = 4;
            int columns = 5;
            int buttons = 2;

            titleTextBlock.Text = "Oceny";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();
            buttonList = new List<Button>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < buttons; i++)
            {
                Button button = new Button();
                button.Margin = margin;
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i);
                buttonList.Add(button);
                contentGrid.Children.Add(buttonList[i]);
            }

            buttonList[0].Content = "Twoje Dane";
            buttonList[1].Content = "Oceny";

            buttonList[0].Click += new RoutedEventHandler(this.DaneStudenta_Click);
            buttonList[1].Click += new RoutedEventHandler(this.Oceny_Click);

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 1);
            Grid.SetColumnSpan(contentDataGrid, columns - 1);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows);
            contentGrid.Children.Add(contentDataGrid);
        }

        public void CreateZajeciaPlanZajec()
        {
            Clear_Content();

            int rows = 4;
            int columns = 5;
            int buttons = 2;

            titleTextBlock.Text = "Plan zajęć";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();
            buttonList = new List<Button>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < buttons; i++)
            {
                Button button = new Button();
                button.Margin = margin;
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i);
                buttonList.Add(button);
                contentGrid.Children.Add(buttonList[i]);
            }

            buttonList[0].Content = "Przeglądanie grup";
            buttonList[1].Content = "Plan zajęć";

            buttonList[0].Click += new RoutedEventHandler(this.PrzegladanieGrup_Click);
            buttonList[1].Click += new RoutedEventHandler(this.PlanZajec_Click);

            contentDataGrid = new DataGrid();
            Grid.SetColumn(contentDataGrid, 1);
            Grid.SetColumnSpan(contentDataGrid, columns - 1);
            Grid.SetRow(contentDataGrid, 0);
            Grid.SetRowSpan(contentDataGrid, rows);
            contentGrid.Children.Add(contentDataGrid);
        }

        public void CreateZajeciaPrzegladanieGrup()
        {
            Clear_Content();

            int rows = 4;
            int columns = 5;
            int buttons = 2;

            titleTextBlock.Text = "Przeglądanie grup";

            tableRowContentList = new List<TextBox>();
            tableRowTitleList = new List<TextBlock>();
            buttonList = new List<Button>();

            Style borderStyle = Application.Current.FindResource("TableBorder") as Style;

            for (int i = 0; i < columns; i++)
            {
                contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                contentGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < buttons; i++)
            {
                Button button = new Button();
                button.Margin = margin;
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i);
                buttonList.Add(button);
                contentGrid.Children.Add(buttonList[i]);
            }

            buttonList[0].Content = "Przeglądanie grup";
            buttonList[1].Content = "Plan zajęć";

            buttonList[0].Click += new RoutedEventHandler(this.PrzegladanieGrup_Click);
            buttonList[1].Click += new RoutedEventHandler(this.PlanZajec_Click);

            buttonSearch = new Button();
            buttonSearch.Margin = margin;
            buttonSearch.Content = "Szukaj";
            Grid.SetColumn(buttonSearch, 4);
            Grid.SetRow(buttonSearch, 0);
            contentGrid.Children.Add(buttonSearch);

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

        private void Zajecia_Click(object sender, RoutedEventArgs e)
        {
            CreateZajeciaPrzegladanieGrup();
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
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

        private void Clear_Content()
        {
            contentGrid.Children.Clear();
            contentGrid.RowDefinitions.Clear();
            contentGrid.ColumnDefinitions.Clear();
            //tableRowContentList.Clear();
            //tableRowTitleList.Clear();
        }
    }
}
