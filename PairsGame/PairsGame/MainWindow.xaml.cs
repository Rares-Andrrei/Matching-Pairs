using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FileToUsersData = "Data\\UsersData.bin";
        private Menu _menuWindow;
        private NewUser _newUserWindow;
        private ConfirmIdentity _confirmIdentityWindow;
        private UsersManager _usersManager;
        private UserMenu _userMenu;
        private GameWindow _gameWindow;
        public MainWindow()
        {
            ReadUsersData();
            _newUserWindow = null;
            _gameWindow = null;
            _menuWindow = new Menu(this);
            _confirmIdentityWindow = null;
            _userMenu = null;
            InitializeComponent();
            InsideWindow.Content = _menuWindow;
        }
        ~MainWindow() 
        { 
            SaveUsersData();
        }
        private void SaveUsersData()
        {
            DataSerializaion.BinarySerialization(_usersManager, FileToUsersData);
        }
        private void ReadUsersData()
        {
            _usersManager = null;
            try
            {
                _usersManager = (UsersManager)DataSerializaion.BinaryDeserialization(FileToUsersData);
            }
            catch (SerializationException)
            {
                _usersManager = new UsersManager();
            }
            if (_usersManager == null)
            {
                _usersManager = new UsersManager();
            }
        }

        public void ShowNewUserWindow()
        {
            _newUserWindow = new NewUser(this);
            InsideWindow.Content = _newUserWindow;
        }
        public void ShowMenuWindow()
        {
            InsideWindow.Content = _menuWindow;
        }
        public void ShowConfirmIdentityWindow(User user, ConfirmIdentity.Case usability)
        {
            _confirmIdentityWindow = new ConfirmIdentity(this, user, usability);
            InsideWindow.Content = _confirmIdentityWindow;
        }
        public void ShowUserMenu(User user)
        {
            _userMenu = new UserMenu(this, user);
            InsideWindow.Content = _userMenu;
        }

        public void ShowGameWindow(User user)
        {
            _gameWindow = new GameWindow(this, user);
            InsideWindow.Content = _gameWindow;
        }

        public void RegisterUser(User user)
        {
            _usersManager.RegisterUser(user);
        }
        public void DeleteUser(string userKey)
        {
            _usersManager.DeleteUser(userKey);
        }
        public ObservableCollection<string> GetUsersKeys()
        {
            return _usersManager.UsersKeys;
        }
        public User GetUser(string userKey)
        {
            return _usersManager.GetUser(userKey);
        }
    }
}
