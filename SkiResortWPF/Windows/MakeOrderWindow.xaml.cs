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
using System.Windows.Shapes;

namespace SkiResortWPF.Windows
{
    /// <summary>
    /// Interaction logic for MakeOrderWindow.xaml
    /// </summary>
    public partial class MakeOrderWindow : Window
    {
        AppContext db;
        Models.Order order;
        List<Models.OrderService> orderServices;
        Windows.ChooseServiceWindow csw;
        public MakeOrderWindow(AppContext db)
        {
            InitializeComponent();
            this.db = db;
            db.Clients.Load();
            db.Services.Load();
            db.Orders.Load();
            db.OrderServices.Load();
            order = new Models.Order(DateTime.Now);
            order.Id = db.Orders.Max(o => o.Id) + 1;
            order.Status = 1;
            orderServices = new List<Models.OrderService>();
            this.DataContext = order;
            clientCB.ItemsSource = db.Clients.ToList();
        }


        private void clientCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientCB.IsEditable = false;
        }

        private void addService_Click(object sender, RoutedEventArgs e)
        {
            if (csw == null)
            {
                csw = new ChooseServiceWindow(db.Services.ToList(), addServiceFunc);
                csw.Closed += (s, o) =>
                {
                    this.Visibility = Visibility.Visible;
                    this.csw = null;
                };
            }
            csw.Show();
            this.Visibility = Visibility.Hidden;
        }
        private int addServiceFunc(List<Models.Service> services)
        {
            Models.OrderService os;

            orderServices = new List<Models.OrderService>();
            foreach (Models.Service service in services)
            {
                os = new Models.OrderService();
                os.ServiceId = service.Id;
                os.Service = service;
                os.OrderId = order.Id;
                os.Order = order;
                orderServices.Add(os);
            }
            this.services.ItemsSource = orderServices;
            return 0;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            db.Orders.Add(order);
            foreach (Models.OrderService oe in orderServices)
                db.OrderServices.Add(oe);
            db.SaveChanges();
        }
        private void saveAndOrderWindowButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow;

            orderWindow = new OrderWindow(db, order);
            orderWindow.Closed += (s, o) => {
                Close();
            };
            this.Visibility = Visibility.Hidden;
            orderWindow.Show();
        }
    }
}
