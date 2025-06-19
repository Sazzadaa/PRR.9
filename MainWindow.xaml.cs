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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppWithoutRefactoring
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            CreateSampleOrder();
        }

        private void CreateSampleOrder()
        {
            List<string> items = new List<string>() { "Клавиатура" };
            string customerName = "Иванов И.И.";
            string paymentMethod = "По карте";
            OrderCreater newOrder = new OrderCreater(customerName, items, paymentMethod);
        }
    }
}