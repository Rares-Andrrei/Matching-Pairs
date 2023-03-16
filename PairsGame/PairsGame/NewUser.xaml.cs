using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : UserControl
    {
        private const string _pathToAvatars = "Avatars\\";

        private MainWindow _mainWindow;
        private int _avatarIndexSelect;
        private List<string> _avatarsPaths;
        public NewUser(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _avatarsPaths = new List<string>();
            _avatarIndexSelect = 0;
            InitializeComponent();
            getAvatarsPaths();
            UpdateImage(_avatarsPaths[_avatarIndexSelect]);       
        }
        private void UpdateImage(string path)
        {
            SelectAvatar.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }
        private void getAvatarsPaths()
        {
            string[] files = Directory.GetFiles(_pathToAvatars);
            foreach (string file in files)
            {
                _avatarsPaths.Add(_pathToAvatars + System.IO.Path.GetFileName(file));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowMenuWindow();   
        }

        private void PreviousAvatar_Click(object sender, RoutedEventArgs e)
        {
            if (_avatarIndexSelect == 0)
            {
                _avatarIndexSelect = _avatarsPaths.Count - 1;
            }
            else
            {
                _avatarIndexSelect--;
            }
            UpdateImage(_avatarsPaths[_avatarIndexSelect]);
        }

        private void NextAvatar_Click(object sender, RoutedEventArgs e)
        {
            if (_avatarIndexSelect == _avatarsPaths.Count - 1)
            {
                _avatarIndexSelect = 0;
            }
            else
            {
                _avatarIndexSelect++;
            }
            UpdateImage(_avatarsPaths[_avatarIndexSelect]);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User newUser = new User(usernameField.Text, passwordField.Password, _avatarsPaths[_avatarIndexSelect]);
                _mainWindow.RegisterUser(newUser);
                MessageBox.Show("You have been successfully registered!", "Info Box");
                _mainWindow.ShowMenuWindow();
            }
            catch (UserException exception)
            {
                MessageBox.Show(exception.Message, "Info Box");
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordField.Visibility = Visibility.Hidden;
            showPassField.Text = passwordField.Password;
            showPassField.Visibility = Visibility.Visible; 
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordField.Visibility = Visibility.Visible;
            passwordField.Password = showPassField.Text;
            showPassField.Visibility = Visibility.Hidden;
        }
    }
}
