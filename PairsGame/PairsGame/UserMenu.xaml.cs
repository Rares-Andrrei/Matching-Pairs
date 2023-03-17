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
            DeleteGame.IsEnabled = false;
            _mainWindow = mainWindow;
            UserName.Text = user.UserName;
            Avatar.Source = new BitmapImage(new Uri(user.AvatarPath, UriKind.Relative));
            GamesPlayed.Text = "Games Played: " + _user.GamesPlayed;
            GamesWon.Text = "Games Won: " + _user.GamesWon;
            SavesList.ItemsSource = _user.GameSaves;
            LeaderBoard.ItemsSource = _mainWindow.GetUsersList();
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
                DeleteGame.IsEnabled = true;
            }
            else
            {
                LoadGame.IsEnabled = false;
                DeleteGame.IsEnabled = false;
            }
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowLoadedGameWindow(_user, SavesList.SelectedItem as GameLogic);
        }

        private void SavesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonEnabled();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowNewGameWindow(_user);
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            _user.DeleteSave(SavesList.SelectedItem as GameLogic);
            SavesList.ItemsSource = null;
            SavesList.ItemsSource = _user.GameSaves;
        }
    }
}
