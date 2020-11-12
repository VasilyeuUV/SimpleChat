using System.Windows;
using System.Windows.Controls;

namespace Vasilev.SimpleChat.WpfNetCore.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbMain.ScrollIntoView(lbMain.SelectedItem);
        }
    }
}
