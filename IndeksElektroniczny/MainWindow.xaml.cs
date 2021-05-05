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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace IndeksElektroniczny
{
    // The MainWindow class containes window mechanics
    /// <summary>
    /// The <c>MainWindow</c> class.
    /// Containes window mechanics.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        DataBaseMySqlService DbService = new DataBaseMySqlService();

        User SignInUser = new User();

        // Initializes the main window
        /// <summary>
        /// Initializes the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Window userWindow)
        {
            userWindow.Close();
            InitializeComponent();
        }

        // Logs the user in
        /// <summary>
        /// Logs the user in 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            SignInUser = DbService.DataBaseSignIn(this.LoginTextBox.Text, this.HasloTextBox.Text);
            if(SignInUser.CheckUser())
            {
                if(SignInUser.Role == 'a')
                {
                    MessageBox.Show("Zalogowałeś się jako admin");
                }
                else if (SignInUser.Role == 'd')
                {
                    MessageBox.Show("Zalogowałeś się jako pracownik dziekanatu");
                }
                else if(SignInUser.Role == 'p')
                {
                    MessageBox.Show("Zalogowałeś się jako prowadzacy");
                }
                else if (SignInUser.Role == 's')
                {
                    MessageBox.Show("Zalogowałeś się jako student");
                    StudentWindow Student = new StudentWindow(this, SignInUser, DbService);
                    Student.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Błąd");
                }
            }
            else
            {
                MessageBox.Show("Nie poprawny login lub hasło");
            }

            //StudentWindow Student = new StudentWindow(this);
            //ProwadzacyWindow Prowadzacy = new ProwadzacyWindow(this);
            //DziekanatWindow PracownikDziekanatu = new DziekanatWindow(this);
            //AdministratorWindow Administrator = new AdministratorWindow(this);
            //Student.ShowDialog();
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

        // A method that allows change the position of the window
        /// <summary>
        /// A method that allows change the position of the window
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
