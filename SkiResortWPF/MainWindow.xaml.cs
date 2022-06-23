using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace SkiResortWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new AppContext();
            db.Clients.Load();
            ChangePasswordField();
        }
        private void ChangePasswordField()
        {
            if (PasswordPBField.Visibility == Visibility.Visible)
            {
                PasswordPBField.Visibility = Visibility.Collapsed;
                PasswordTBField.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPBField.Visibility = Visibility.Visible;
                PasswordTBField.Visibility = Visibility.Collapsed;
            }
        }

        private void PasswordChangeVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordField();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTBField.Visibility == Visibility.Visible)
            {
                PasswordPBField.Password = PasswordTBField.Text;
            }
        }

        private void PasswordPBField_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordTBField.Text = PasswordPBField.Password;
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            String password, email;
            Models.Client client;

            password = PasswordPBField.Password;
            email = EmailTBField.Text;
            client = db.Clients.Where(c => c.Email == email).FirstOrDefault();
            if (client == null)
            {
                MessageBox.Show("Client Not Exist");
                return;
            }
            if (!client.CheckPassword(password))
            {
                MessageBox.Show("Password Not Right");
                return;
            }

        }
        private int tryLoginAsClient(String password, String email)
        {
            Models.Client client;

            client = db.Clients.Where(c => c.Email == email).FirstOrDefault();
            if (client == null)
            {
                return Parameters.EmailNotCorrect;
            }
            if (!client.CheckPassword(password))
            {
                return Parameters.PasswordNotCorrect;
            }
            return Parameters.Authenticated;
        }
    }
}
