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
            StudentWindow Student = new StudentWindow(this);
            //ProwadzacyWindow Student = new ProwadzacyWindow(this);
            //DziekanatWindow Student = new DziekanatWindow(this);
            //AdministratorWindow Student = new AdministratorWindow(this);
            Student.ShowDialog();
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
