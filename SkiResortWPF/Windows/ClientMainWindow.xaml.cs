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
using System.Windows.Threading;

namespace SkiResortWPF.Windows
{
    /// <summary>
    /// Interaction logic for ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : Window
    {
        Models.Client client;
        AppContext db;
        DispatcherTimer timer;
        DateTime expired;

        public ClientMainWindow(AppContext db, Models.Client client = null)
        {
            InitializeComponent();
            this.client = client;
            this.db = db;
            db.Services.Load();
            servicesItemsControl.ItemsSource = db.Services.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            scrollBarPanel.Maximum = scroolViewerPanel.ScrollableHeight;
            //scroolViewerPanel.ScrollableHeight
            scrollBarPanel.Value = 0;
        }
        public void setExpirationTime(DateTime d)
        {
            this.expired = d;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > expired)
                Close();
            TimeSpan t = expired - DateTime.Now;
            TimeSessionCounterTB.Text = t.ToString("h'h 'm'm 's's'");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            scrollBarPanel.Value = scroolViewerPanel.VerticalOffset;
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            scroolViewerPanel.ScrollToVerticalOffset(scrollBarPanel.Value);
        }

        private void EndSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Close Current Session", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Close();
        }
    }
}
