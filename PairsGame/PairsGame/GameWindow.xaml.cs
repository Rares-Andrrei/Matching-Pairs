using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
using System.Runtime.InteropServices;

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : UserControl
    {
        private const string FileToGameImages = "GameImages\\";
        private const string HidingCard = "Images\\black-jack.png";
        private const int TransitionTime = 2500;
        private const int LevelsToWin = 3;

        private ImageSource _maskSource;
        private List<List<ImageSource>> _sourceImages;
        private User _user;
        private Image _firstPick;
        private Image _secondPick;
        private MainWindow _mainWindow;
        private GameLogic _gameLogic;
        public GameWindow(MainWindow mainWindow, User user, GameLogic gameLogic)
        {
            InitializeComponent();
            _gameLogic = gameLogic;
            if (gameLogic == null)
            {
                SaveGame.IsEnabled = false;
            }
            _user = user;
            _mainWindow = mainWindow;
            Avatar.Source = new BitmapImage(new Uri(_user.AvatarPath, UriKind.Relative));
            UserName.Text = _user.UserName;
            _maskSource = new BitmapImage(new Uri(HidingCard, UriKind.Relative));
            Level.Text = "1" + "/" + LevelsToWin;
        }
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowUserMenu(_user);
        }

        private void rows_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int caracters;
            if (!int.TryParse(e.Text, out caracters))
            {
                e.Handled = true;
            }
            else if(e.Text == "1" || e.Text == "0")
            {
                e.Handled = true;
            }
            else if (rows.Text.Length >= 1)
            {
                e.Handled = true;
                rows.Text = "7";
            }
        }
        private void columns_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int caracters;
            if (!int.TryParse(e.Text, out caracters))
            {
                e.Handled = true;
            }
            else if (e.Text == "1" || e.Text == "0")
            {
                e.Handled = true;
            }
            else if(columns.Text.Length >= 1 && e.Text != "0")
            {
                e.Handled = true;
                columns.Text = "7";
            }
             
        }
        private async void StartCustom_Click(object sender, RoutedEventArgs e)
        {
            int rowsNr, colsNr;
            if (int.TryParse(rows.Text, out rowsNr) && int.TryParse(columns.Text, out colsNr))
            {
                if (rowsNr > 10 || rowsNr < 2 || colsNr > 10 || colsNr < 2)
                {
                    MessageBox.Show("Input must be a number between 2 and 10", "Input error");
                }
                else
                {
                    gameOptions.Visibility = Visibility.Hidden;
                    _gameLogic = new GameLogic(_user, FileToGameImages, rowsNr, colsNr);
                    CreateImagesList();
                    DisplayGameImages();

                    GameState.Visibility = Visibility.Visible;
                    Message.Text = "Level " + _gameLogic.Level.ToString();
                    await Task.Delay(TransitionTime);
                    GameState.Visibility = Visibility.Hidden;

                    Game.Visibility = Visibility.Visible;
                    SaveGame.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Input must be a number", "Input error");
            }
        }
        private async void StartStandard_Click(object sender, RoutedEventArgs e)
        {
            gameOptions.Visibility = Visibility.Hidden;
            _gameLogic = new GameLogic(_user, FileToGameImages, 5, 5);
            CreateImagesList();
            DisplayGameImages();

            GameState.Visibility = Visibility.Visible;
            Message.Text = "Level " + _gameLogic.Level.ToString();
            await Task.Delay(TransitionTime);
            GameState.Visibility = Visibility.Hidden;

            Game.Visibility = Visibility.Visible;
            SaveGame.IsEnabled = true;
        }
        private async void DisplayNextLevel()
        {
            Game.Visibility = Visibility.Hidden;
            GameState.Visibility = Visibility.Visible;
            Message.Text = "Level " + _gameLogic.Level.ToString();
            await Task.Delay(TransitionTime);
            GameState.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Visible;

            _gameLogic.SetGame();
            Level.Text = _gameLogic.Level.ToString() + "/" + LevelsToWin;
            CreateImagesList();
            PopulateGrid();
        }
        private void CreateImagesList()
        {
            List<List<string>> paths = _gameLogic.Images;
            _sourceImages = new List<List<ImageSource>>();
            for (int i = 0; i < paths.Count; i++)
            {
                _sourceImages.Add(new List<ImageSource>());
                for (int j = 0; j < paths[i].Count; j++)
                {
                    _sourceImages[i].Add(new BitmapImage(new Uri(paths[i][j], UriKind.Relative)));
                }
            }
        }
        private void CreateImagesGrid()
        {
            for (int i = 0; i < _gameLogic.Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                Game.RowDefinitions.Add(row);
            }
            for (int i = 0; i < _gameLogic.Columns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                Game.ColumnDefinitions.Add(column);
            }
        }
        private void PopulateGrid()
        {
            for (int i = 0; i < _gameLogic.Rows; i++)
            {
                for (int j = 0; j < _gameLogic.Columns; j++)
                {
                    Image image = new Image();
                    image.Source = _maskSource;
                    image.Margin = new Thickness(0, 4, 0, 4);
                    image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    Game.Children.Add(image);
                }
            }
        }
        private void DisplayGameImages()
        { 
            CreateImagesGrid();
            PopulateGrid();
        }
        public void ResetPicks()
        {
            _firstPick = null;
            _secondPick = null;
        }
        private async void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Tuple<int, int> pozition = new Tuple<int, int>(0, 0);
            if (image != null)
            {
                pozition = new Tuple<int, int>(Grid.GetRow(image), Grid.GetColumn(image));
            }
            if (!_gameLogic.CardsSelected())
            {
                _gameLogic.PickCard(pozition);
                if (_firstPick == null)
                {
                    _firstPick = image;
                    _firstPick.Source = _sourceImages[pozition.Item1][pozition.Item2];
                    _firstPick.MouseLeftButtonDown -= Image_MouseLeftButtonDown;
                }
                else
                {
                    _secondPick = image;
                    _secondPick.Source = _sourceImages[pozition.Item1][pozition.Item2];
                    _secondPick.MouseLeftButtonDown -= Image_MouseLeftButtonDown;
                    if (_gameLogic.IsPairMatching())
                    {
                        Game.IsEnabled = false;
                        await Task.Delay(300);
                        Game.IsEnabled = true;
                        _firstPick.Visibility = Visibility.Hidden;
                        _secondPick.Visibility = Visibility.Hidden;
                        _gameLogic.ResetPicks();
                        ResetPicks();
                        if (_gameLogic.CheckLevelWinningState())
                        {
                            if (_gameLogic.Level > LevelsToWin)
                            {
                                _user.GamesWon++;
                                Message.Text = "You Won!";
                                Game.Visibility = Visibility.Hidden;
                                GameState.Visibility = Visibility.Visible;
                                await Task.Delay(TransitionTime);
                                GameState.Visibility = Visibility.Hidden;
                                _mainWindow.ShowUserMenu(_user);                               
                            }
                            else 
                            {
                                DisplayNextLevel();
                            }
                        }
                    }
                }
            }
            else
            {
                _firstPick.Source = _maskSource;
                _firstPick.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                _secondPick.Source = _maskSource;
                _secondPick.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                _gameLogic.ResetPicks();
                ResetPicks();
                Image_MouseLeftButtonDown(sender, e);
            }
        }
    }
}
