using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : UserControl
    {
        private User _user;
        private MainWindow _mainWindow;
        public UserMenu(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            _user = user;
            LoadGame.IsEnabled = false;
            _mainWindow = mainWindow;
            UserName.Text = user.UserName;
            Avatar.Source = new BitmapImage(new Uri(user.AvatarPath, UriKind.Relative));
            GamesPlayed.Text = "Games Played: " + _user.GamesPlayed;
            GamesWon.Text = "Games Won: " + _user.GamesWon;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowMenuWindow();
        }


        private void UpdateButtonEnabled()
        {
            if (SavesList.SelectedItem != null)
            {
                LoadGame.IsEnabled = true;
            }
            else
            {
                LoadGame.IsEnabled = false;
            }
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SavesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonEnabled();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowNewGameWindow(_user);
        }
    }
}
