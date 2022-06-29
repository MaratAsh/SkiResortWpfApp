﻿using System;
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
    /// Interaction logic for EmployeeMainWindow.xaml
    /// </summary>
    public partial class EmployeeMainWindow : Window
    {
        AppContext db;
        Models.Employee employee;
        EmployeeTrackerWindow etw;
        public EmployeeMainWindow(AppContext db, Models.Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            this.db = db;
            this.etw = null;
        }

        private void logEmployeesShow_Click(object sender, RoutedEventArgs e)
        {
            if (this.etw == null)
            {
                this.etw = new EmployeeTrackerWindow(this.db);
                this.etw.Closed += EmployeeTrackerWindow_Closed;
            }
            this.etw.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        private void EmployeeTrackerWindow_Closed(object sender, EventArgs e)
        {
            this.etw = null;
        }
    }
}
