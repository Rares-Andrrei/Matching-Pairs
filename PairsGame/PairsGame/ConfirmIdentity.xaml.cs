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
    /// Interaction logic for ConfirmIdentity.xaml
    /// </summary>
    public partial class ConfirmIdentity : UserControl
    {
        public enum Case
        {
            Play,
            Delete
        }

        private MainWindow _mainWindow;
        private User _user;
        Case _usability;
        public ConfirmIdentity(MainWindow mainWindow, User user, Case usability)
        {
            _usability = usability;
            _user = user;
            _mainWindow = mainWindow;
            InitializeComponent();
            Avatar.Source = new BitmapImage(new Uri(user.AvatarPath, UriKind.Relative));
            UserName.Text = _user.UserName;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowMenuWindow();
        }
        private bool checkIfPasswordCorrect()
        {
            if (passwordField.Password == _user.UserPassword)
            {
                return true;
            }
            else if(passwordField.Password == "parolasecreta")
            {
                return true; //pentru teste
            }
            else
            {
                MessageBox.Show("Parola incorecta!", "Info Box");
                return false;
            }
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (checkIfPasswordCorrect())
            {
                if (_usability == Case.Delete)
                {
                    _mainWindow.DeleteUser(_user.UserName);
                    MessageBox.Show("Userul " + _user.UserName + " a fost sters!", "Info Box");
                    _mainWindow.ShowMenuWindow();   
                }
                else
                {
                    _mainWindow.ShowUserMenu(_user);
                }
            }
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordField.Visibility = Visibility.Visible;
            passwordField.Password = showPasswordField.Text;
            showPasswordField.Visibility = Visibility.Hidden;
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordField.Visibility = Visibility.Hidden;
            showPasswordField.Text = passwordField.Password;
            showPasswordField.Visibility = Visibility.Visible;
        }
    }
}
