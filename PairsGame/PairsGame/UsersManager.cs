using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame
{
    [Serializable]
    internal class UsersManager
    {
        private Dictionary<string, User> _users;
        ObservableCollection<string> _usersKeys;

        public UsersManager() 
        { 
            _users = new Dictionary<string, User>();
            _usersKeys = new ObservableCollection<string>();
        }
        public void RegisterUser(User user)
        {
            if (_users.ContainsKey(user.UserName))
            { throw new UserException("Acest username este deja folosit!"); }
            else 
            { 
                _users.Add(user.UserName, user);
                _usersKeys.Add(user.UserName);
            }
        }

        public bool LoginUser(string username, string password) 
        {
            if (!_users.ContainsKey(username))
            { throw new UserException("Userul nu a fost gasit!"); }

            if (password == _users[username].UserPassword)
            { return true; }
            else 
            { return false; }
        }
        public void DeleteUser(string userKey)
        {
            if (_users.ContainsKey(userKey))
            {
                _users.Remove(userKey);
                _usersKeys.Remove(userKey);
            }
        }
        public ObservableCollection<string> UsersKeys
        {
            get { return _usersKeys; }
        }
        public User GetUser(string userKey)
        {
            return _users[userKey];
        }
    }
}
