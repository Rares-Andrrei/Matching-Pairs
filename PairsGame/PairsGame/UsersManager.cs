using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame
{
    [Serializable]
    internal class UsersManager
    {
        private Dictionary<string, User> _users;
        
        public UsersManager() 
        { 
            _users = new Dictionary<string, User>();
        }
        public void RegisterUser(User user)
        {
            if (_users.ContainsKey(user.UserName))
            { throw new UserException("Acest username este deja folosit!"); }
            else 
            { _users.Add(user.UserName, user); }
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
    }
}
