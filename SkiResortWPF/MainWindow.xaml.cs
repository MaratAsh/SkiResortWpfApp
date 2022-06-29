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
        int attempts;
        public MainWindow()
        {
            InitializeComponent();
            db = new AppContext();
            db.Clients.Load();
            db.Employees.Load();
            ChangePasswordField();
            // gohufreilagrau-3818@yopmail.com	cl12345
            // (new Windows.MakeOrderWindow(db)).Show();
            attempts = 0;
        }
        private void ChangePasswordField()
        {
            if (PasswordTBField.Visibility == Visibility.Visible)
            {
                PasswordPBField.Visibility = Visibility.Visible;
                PasswordTBField.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordPBField.Visibility = Visibility.Collapsed;
                PasswordTBField.Visibility = Visibility.Visible;
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

        private int CheckIf()
        {
            if (attempts > 2)
                return 1;
            return 0;
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            String password, email;
            Models.Client client;
            int res;
            Models.LogActivityEmployee logActivityEmployee;
            Models.Employee employee;

            password = PasswordPBField.Password;
            email = EmailTBField.Text;
            res = tryLoginAsEmployee(password, email);
            if (CheckIf() == 1)
            {
                Windows.CapchaWindow window;

                window = new Windows.CapchaWindow(CapchaSucceed);
                window.Closed += (s, o) => {
                    this.Visibility = Visibility.Visible;
                };
                window.Show();
                return;
            }
            if (res == Parameters.Authenticated)
            {
                Windows.EmployeeMainWindow e_w;

                employee = db.Employees.Where(c => c.Email == email).FirstOrDefault();
                logActivityEmployee = new Models.LogActivityEmployee(employee.Id, true);
                db.LogActivityEmployees.Add(logActivityEmployee);
                db.SaveChanges();
                e_w = new Windows.EmployeeMainWindow(db, employee);
                e_w.Closed += Window_Child_Closed;
                this.Visibility = Visibility.Hidden;
                e_w.Show();
                return;
            }
            else if (res == Parameters.PasswordNotCorrect)
            {
                employee = db.Employees.Where(c => c.Email == email).FirstOrDefault();
                logActivityEmployee = new Models.LogActivityEmployee(employee.Id, false);
                db.LogActivityEmployees.Add(logActivityEmployee);
                db.SaveChanges();
                attempts++;
            }
            res = tryLoginAsClient(password, email);
            if (res == Parameters.EmailNotCorrect)
            {
                MessageBox.Show("Client Not Exist");
                return;
            }
            if (res == Parameters.PasswordNotCorrect)
            {
                attempts++;
                MessageBox.Show("Password Not Right");
                return;
            }
            client = db.Clients.Where(c => c.Email == email).FirstOrDefault();
            Windows.ClientMainWindow w;
            w = new Windows.ClientMainWindow(db, client);
            w.Closed += Window_Child_Closed;
            this.Visibility = Visibility.Hidden;
            w.setExpirationTime(DateTime.Now.AddMinutes(10));
            w.Show();
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
        private int tryLoginAsEmployee(String password, String email)
        {
            Models.Employee employee;

            employee = db.Employees.Where(c => c.Email == email).FirstOrDefault();
            if (employee == null)
            {
                return Parameters.EmailNotCorrect;
            }
            if (!employee.CheckPassword(password))
            {
                return Parameters.PasswordNotCorrect;
            }
            return Parameters.Authenticated;
        }
        private void CapchaSucceed()
        {
            attempts = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            db.Dispose();
        }
        private void Window_Child_Closed(object sender, EventArgs e)
        {
            PasswordPBField.Password = "";
            this.Visibility = Visibility.Visible;
        }
    }
}
