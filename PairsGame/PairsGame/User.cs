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
    internal class User
    {
        private string _username;
        private string _password;
        private string _avatarPath;

        public User(string username, string password, string avatarPath)
        {
            UserName = username;
            UserPassword = password;
            AvatarPath = avatarPath;
        }

        public string UserName { 
            get { return _username;} 
        set {
                if (value.Length < 4)
                { throw new UserException("Username-ul trebuie sa contina minim 4 caractere!"); }
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
                { throw new UserException("Parola trebuie sa contina minim 4 caractere!"); }
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
                { throw new UserException("Avatarul nu a fost gasit"); }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
