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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        private MainWindow _mainWindow;
        public Menu(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            UsersList.ItemsSource = _mainWindow.GetUsersKeys();
            Play.IsEnabled = false;
            DeleteUser.IsEnabled = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowNewUserWindow();         
        }
        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersList.SelectedItem != null)
            {
                string avatar = _mainWindow.GetUser(UsersList.SelectedItem.ToString()).AvatarPath;
                Avatar.Source = new BitmapImage(new Uri(avatar, UriKind.Relative));
            }
            UpdateButtonsEnabledState();    
        }
        private void UpdateButtonsEnabledState()
        {
            if (UsersList.SelectedItem != null)
            {
                Play.IsEnabled = true;
                DeleteUser.IsEnabled = true;
            }
            else
            {
                Play.IsEnabled = false;
                DeleteUser.IsEnabled = false;
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowConfirmIdentityWindow(_mainWindow.GetUser(UsersList.SelectedItem.ToString()), ConfirmIdentity.Case.Delete);
        }
    }
}
