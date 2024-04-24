using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CV6WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>()
        {
            new Customer()
            {
                Id = 1, FirstName = "Jan", LastName = "Novak", Age=20
            }
        };
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void mojeTlacitko_Click(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer();
            CustomerForm form = new CustomerForm(c);
            form.ShowDialog();
            this.Customers.Add(c);
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Customer c = btn.DataContext as Customer;
            this.Customers.Remove(c);
        }

        private void AnonymizeCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Customer c = btn.DataContext as Customer;
            c.FirstName = "****";
            c.LastName = "***";
        }

        private void EditCustomer(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;//Možná se to bude muset dát (Button)sender
            Customer c = btn.DataContext as Customer;
            CustomerForm form = new CustomerForm(c);
            form.ShowDialog();
        }
    }
}