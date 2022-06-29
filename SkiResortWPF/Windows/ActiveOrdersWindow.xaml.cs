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
    /// Interaction logic for ActiveOrdersWindow.xaml
    /// </summary>
    public partial class ActiveOrdersWindow : Window
    {
        AppContext db;
        OrderWindow window;
        public ActiveOrdersWindow(AppContext db)
        {
            InitializeComponent();
            this.db = db;
            this.db.Orders.Load();
            ordersIC.ItemsSource = this.db.Orders.Where(o => o.Status != 1).ToList();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Order order;
            Button b = (Button)sender;
            order = (Models.Order)b.DataContext;
            if (window != null)
            {
                MessageBox.Show("Close current order window");
                return;
            }
            window = new OrderWindow(db, order);
            window.Closed += (s, o) => {
                if (this != null)
                {
                    this.Visibility = Visibility.Visible;
                }
            };
            this.Visibility = Visibility.Hidden;
            window.Show();
        }
    }
}
