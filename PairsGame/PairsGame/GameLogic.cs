using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Ink;
using static System.Net.Mime.MediaTypeNames;

namespace PairsGame
{
    [Serializable]
    public class GameLogic
    {
        private const float Time = 1.5f;

        private string _saveName;
        private User _user;
        private List<List<string>> _images;
        private string _pathToImages;
        private int _level;
        private int _timeLeft;
        private int _rows;
        private int _columns;
        private int _imagesLeft;
        private Tuple<int, int> _firstPick;
        private Tuple<int, int> _secondPick;
        public GameLogic(User user, string pathToImages, int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _pathToImages = pathToImages;
            _user = user;
            _user.GamesPlayed++;
            _level = 1;
            SetGame();
        }
        public int TimeLeft
        {
            get { return _timeLeft; }
        }
        public string SaveName
        {
            get { return _saveName; }
            set { _saveName = value; }
        }
        public void SetGame()
        {
            _timeLeft = (int)((float)(_rows * _columns) * Time);
            _firstPick = null;
            _secondPick = null;
            _images = new List<List<string>>();
            GetRandomImagesFromFile();
        }
        public int Level
        {
            get { return _level; }
        }
        public int Rows
        {
            get { return _rows; }
        }
        public int Columns
        {
            get { return _columns; }
        }
        public List<List<string>> Images
        {
            get { return _images; }
        }
        public void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int itemsNr = list.Count;
            while (itemsNr > 1)
            {
                itemsNr--;
                int k = rng.Next(itemsNr + 1);
                T value = list[k];
                list[k] = list[itemsNr];
                list[itemsNr] = value;
            }
        }
        public void GetRandomImagesFromFile()
        {
            string[] files = Directory.GetFiles(_pathToImages);
            HashSet<string> images = new HashSet<string>();
            int imagesNr = (_rows * _columns) % 2 == 0 ? _rows * _columns / 2 : _rows * _columns /2 + 1;
            _imagesLeft = imagesNr;
            Random random = new Random();
            while (images.Count < imagesNr)
            {
                images.Add(files[random.Next(0, files.Length)]);
            }
            List<string> auxiliary = new List<string>();
            foreach(var item in images)
            {
                if (auxiliary.Count == _rows * _columns - 1)
                {
                    auxiliary.Add(item);
                }
                else
                {
                    auxiliary.Add(item);
                    auxiliary.Add(item);
                }
            }
            Shuffle<string>(auxiliary);
            int cnt = 0;
            _images = new List<List<string>>();
            for (int i = 0; i < _rows; i++)
            {
                _images.Add(new List<string>());
                for (int j = 0; j < _columns; j++)
                {
                    _images[i].Add(auxiliary[cnt++]);
                }
            }
        }
        public bool CardsSelected()
        {
            if (_firstPick != null && _secondPick != null)
            {
                return true;
            }
            return false;
        }
        public void ResetPicks()
        {
            _firstPick = null;
            _secondPick = null;
        }
        public bool IsPairMatching()
        {
            bool matching = String.Equals(_images[_firstPick.Item1][_firstPick.Item2], _images[_secondPick.Item1][_secondPick.Item2]);
            if (matching)
            {
                _images[_firstPick.Item1][_firstPick.Item2] = "";
                _images[_secondPick.Item1][_secondPick.Item2] = "";
                _imagesLeft--;
            }
            return matching;
        }
        public void PickCard(Tuple<int, int> pozition)
        {
            if (_firstPick == null)
            {
                _firstPick = pozition;
            }
            else
            {
                _secondPick = pozition;
            }
        }
        public bool CheckLevelWinningState()
        {
            if (Rows * Columns % 2 != 0)
            {
                if (_imagesLeft <= 1)
                {
                    _level++;
                    return true;
                }
                return false;
            }
            else
            {
                if (_imagesLeft == 0)
                {
                    _level++;
                    return true;
                }
                return false;
            }
        }
        public bool DecrementTime()
        {
            if (_timeLeft == 0)
            {
                return false;
            }
            _timeLeft--;
            return true;
        }
    }
}
