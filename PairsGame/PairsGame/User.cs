using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PairsGame
{
    [Serializable]
    public class User
    {
        private string _username;
        private string _password;
        private string _avatarPath;
        private int _gamesPlayed;
        private int _gamesWon;

        public User(string username, string password, string avatarPath)
        {
            _gamesWon = 0;
            _gamesPlayed = 0;
            UserName = username;
            UserPassword = password;
            AvatarPath = avatarPath;
        }

        public int GamesPlayed
        {
            get { return _gamesPlayed; }
            set { _gamesPlayed = value; }
        }

        public int GamesWon
        {
            get { return _gamesWon; }
            set { _gamesWon = value; }
        }

        public string UserName { 
            get { return _username;} 
        set {
                if (value.Length < 4)
                { throw new UserException("Your username must be at least 4 caracters long!"); }
                else 
                { _username = value; }
            }
        }

        public string UserPassword
        {
            get { return _password;}
            set
            {
                if (value.Length < 4)
                { throw new UserException("Your password must be at least 4 caracters long!"); }
                else 
                { _password = value; }
            }
        }
        
        public string AvatarPath
        {
            get { return _avatarPath;}
            set { 
                if (File.Exists(value)) 
                { _avatarPath = value; }
                else 
                { throw new UserException("Your avatar was not found!"); }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
