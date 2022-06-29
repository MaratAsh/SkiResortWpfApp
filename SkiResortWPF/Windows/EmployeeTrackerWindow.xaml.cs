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
    /// Interaction logic for EmployeeTrackerWindow.xaml
    /// </summary>
    public partial class EmployeeTrackerWindow : Window
    {
        AppContext db;
        public EmployeeTrackerWindow(AppContext db)
        {
            InitializeComponent();
            this.db = db;
            db.LogActivityEmployees.Load();
            table.ItemsSource = db.LogActivityEmployees.ToArray();
        }

    }
}
