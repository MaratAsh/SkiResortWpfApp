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

namespace SkiResortWPF.Windows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        Codes.BarCodeEAN13 code;
        Models.Order order;
        AppContext db;
        public OrderWindow(AppContext db, Models.Order order)
        {
            InitializeComponent();
            this.DataContext = order;
            this.db = db;
            this.order = order;
            statusOrderTB.Text = order.Status == 1 ? "Started" : order.Id == 2 ? "In Progress" : "Complete";
            code = new Codes.BarCodeEAN13(460, 210, order.Id);
            code.ConstructOn(CanvasBarCode);
        }
    }
}
