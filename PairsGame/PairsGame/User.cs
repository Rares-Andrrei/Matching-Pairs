using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;

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
        private List<GameLogic> _gameSaves;

        public User(string username, string password, string avatarPath)
        {
            _gamesWon = 0;
            _gamesPlayed = 0;
            UserName = username;
            UserPassword = password;
            AvatarPath = avatarPath;
            _gameSaves = new List<GameLogic>();
        }
        public List<GameLogic> GameSaves
        {
            get { return _gameSaves; }
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
        public void SaveGame(GameLogic game, string name)
        {
            game.SaveName = name;
            _gameSaves.Add(game);
        }
        public void DeleteSave(GameLogic save)
        {
            _gameSaves.Remove(save);
        }
    }
}
