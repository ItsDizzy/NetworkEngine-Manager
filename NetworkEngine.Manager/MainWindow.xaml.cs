using System.Windows;
using NetworkEngine.Manager.Core.ViewModels;

namespace NetworkEngine.Manager
{
    /// <summary>
    /// Interaction logic for SessionWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SessionListViewModel();
        }
    }
}
