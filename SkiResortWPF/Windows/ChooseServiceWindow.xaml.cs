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
    /// Interaction logic for OrderAddServiceWindow.xaml
    /// </summary>
    public partial class ChooseServiceWindow : Window
    {
        public delegate int ChooseTriggerFunction(Models.Service service);
        Func<List<Models.Service>, int> action;
        List<Models.Service> selectable;
        List<Models.Service> selected;

        public ChooseServiceWindow(List<Models.Service> selectable, List<Models.Service> selected, Func<List<Models.Service>, int> action)
        {
            InitializeComponent();
            this.action = action;
            this.selected = selected;
            this.selectable = selectable;
            servicesItemsControl.ItemsSource = selectable;
            servicesSelectedItemsControl.ItemsSource = selected;
        }
        public ChooseServiceWindow(List<Models.Service> services, Func<List<Models.Service>, int> action)
        {
            InitializeComponent();
            this.action = action;
            selectable = services;
            selected = new List<Models.Service>();
            servicesItemsControl.ItemsSource = services;
            servicesSelectedItemsControl.ItemsSource = selected;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (action(selected) == 0)
            {
                Close();
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Models.Service s = (Models.Service) b.DataContext;

            if (selected.Contains(s))
            {
                selected.Remove(s);
                selectable.Add(s);
                Update();
            }
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Models.Service s = (Models.Service)b.DataContext;

            if (selectable.Contains(s))
            {
                selectable.Remove(s);
                selected.Add(s);
                Update();
            }
        }
        private void Update()
        {
            selected.Sort((s1, s2) => s1.Id.CompareTo(s2.Id));
            selectable.Sort((s1, s2) => s1.Id.CompareTo(s2.Id));
            servicesItemsControl.ItemsSource = null;
            servicesSelectedItemsControl.ItemsSource = null;
            servicesItemsControl.ItemsSource = selectable;
            servicesSelectedItemsControl.ItemsSource = selected;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
