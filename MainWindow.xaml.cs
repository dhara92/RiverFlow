/* Final Assignment
 * Group: 3
 * Members:
 * Dhara Narola
 * Kirti
 * Laren
 * Laxen Sapani
 * Mitesh Ghevariya
 * */
using System.Windows;

namespace RiverFlow
{
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            vm.OpenFile();
        }

        private void count_Click(object sender, RoutedEventArgs e)
        {
            vm.CountRiverLength();
        }
    }
}
