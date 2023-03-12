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

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : UserControl
    {
        private User _user;
        private MainWindow _mainWindow;
        public GameWindow(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            _user = user;
            _mainWindow = mainWindow;
            Avatar.Source = new BitmapImage(new Uri(_user.AvatarPath, UriKind.Relative));
            UserName.Text = _user.UserName;
        }
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowUserMenu(_user);
        }
    }
}
