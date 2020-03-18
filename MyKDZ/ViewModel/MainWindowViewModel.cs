using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MyKDZ.Model;
using GameDataLibrary;

namespace MyKDZ.ViewModel
{
    partial class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            CheckResourcesPath();

            _startMenu = new View.StartMenu();
            _gameSettingsUser1 = new View.GameSettings();
            _gameSettingsUser2 = new View.GameSettings();
            _startGame = new View.GameProcessView();
            _prevPage = null;
            #region Initialize photo and video wallpapers 
            VideoWallpaper = new MediaElement();
            try
            {
                _heroesList = HeroesParser.GetHeroes();
                _heroesListNotFilter = HeroesParser.GetHeroes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "*Добавьте файл Dota2.csv в папку Resources, а папку Resources положите рядом с MyKDZ.csproj и перезапустите программу",
                    "Ошибка парсинга персонажей", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _lightImage = new Image();
            _darkImage = new Image();
            _darkImage.Stretch = Stretch.UniformToFill;
            _lightImage.Stretch = Stretch.UniformToFill;
            _darkImage.Source = new BitmapImage(new Uri(@"\..\..\Resources\Images\backgroundImageBlack.jpg", UriKind.Relative));
            _lightImage.Source = new BitmapImage(new Uri(@"\..\..\Resources\Images\Background4.jpg", UriKind.Relative));
            _photoWallpaper = _darkImage;
            LoadUserTheme();
            #endregion
        }


        /// <summary>
        /// Method for resetting game parameters
        /// </summary>
        public void SetDeafaultsValue()
        {
            SelectedHeroUser1 = null;
            SelectedHeroUser2 = null;
            SelectedHero = null;
            IsCheckMoveSpeedFilter = false;
            IsCheckRegenerationFilter = false;
            IsCheckTypeFilter = false;
            //EnemyTypeIsHuman = false;
            _user1Move = null;
            _user2Move = null;
            IsUser1MoveNot = Visibility.Visible;
            IsUser2MoveNot = Visibility.Visible;
            IsUser1MoveYes = Visibility.Collapsed;
            IsUser2MoveYes = Visibility.Collapsed;
            Logs = new System.Collections.ObjectModel.ObservableCollection<string>();
            _logs = new System.Collections.ObjectModel.ObservableCollection<string>();
            _prevPage = _startMenu;
            _gameSettingsUser1 = new View.GameSettings();
            _gameSettingsUser2 = new View.GameSettings();
            _selectedHeroUser1 = null;
            _selectedHeroUser2 = null;
            UserName = UserName1;
            _startGame = new View.GameProcessView();
        }


        /// <summary>
        /// Checking the existence of a folder with resources
        /// </summary>
        private void CheckResourcesPath()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\..\..\Resources\"))
            {
                switch (MessageBox.Show("Привет. Эта ошибка появилась в связи с тем, что программа не смогла обнаружить папку \"Resources\" рядом с файлом MyKDZ.csproj" + Environment.NewLine + Environment.NewLine +
                    " *(Папку Resources закинь рядом с файлом MyKDZ.csproj)" + Environment.NewLine +
                    Environment.NewLine + "Yes - Закрыть программу и закинуть ресурысы, No - запустить программу без ресурсов(очень нестабильно)",
                        "Не найдены ресуры", MessageBoxButton.YesNo, MessageBoxImage.Error))
                {
                    case MessageBoxResult.Yes:
                        Environment.Exit(-1);
                        break;
                    case MessageBoxResult.No:
                        break;
                }

            }

        }


        #region Change Theme Logic
        private RelayCommand _themeToggle;
        private Image _darkImage;
        private Image _lightImage;

        /// <summary>
        /// Command property for changing the current resource dictionary
        /// </summary>
        public RelayCommand ThemeToggle
        {
            get
            {
                if (_themeToggle == null)
                {
                    _themeToggle = new RelayCommand(obj =>
                    {
                        //_timer = DateTime.Now;
                        var isChecked = (bool)obj;
                        if (isChecked)
                        {
                            VideoWallpaper.Source = new Uri(@"..\..\Resources\Images\LiveWallpaperLight.mp4", UriKind.Relative);
                            PhotoWallpaper = _lightImage;
                            ResourceDictionary dictionary = new ResourceDictionary();
                            dictionary.Source = new Uri(@"Resources\LightTheme.xaml", UriKind.Relative);
                            Application.Current.Resources.MergedDictionaries[0] = dictionary;
                        }
                        else
                        {
                            VideoWallpaper.Source = new Uri(@"..\..\Resources\Images\LiveWallpaper2.mp4", UriKind.Relative);
                            PhotoWallpaper = _darkImage;
                            ResourceDictionary dictionary = new ResourceDictionary();
                            dictionary.Source = new Uri(@"Resources\DarkTheme.xaml", UriKind.Relative);
                            Application.Current.Resources.MergedDictionaries[0] = dictionary;
                        }
                    });
                }
                return _themeToggle;
            }

        }

        /// <summary>
        /// Подгружает последнюю установленную пользователем тему
        /// </summary>
        private void LoadUserTheme()
        {
            try
            {
                string oldTheme = File.ReadAllText(Environment.CurrentDirectory + @"\..\..\UserTheme.t");

                if (oldTheme == "light")
                {
                    VideoWallpaper.Source = new Uri(@"..\..\Resources\Images\LiveWallpaperLight.mp4", UriKind.Relative);
                    PhotoWallpaper = _lightImage;
                    ResourceDictionary dictionary = new ResourceDictionary();
                    dictionary.Source = new Uri(@"Resources\LightTheme.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries[0] = dictionary;
                    Theme = true;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Сохраняет тему, которой пользуется игрок
        /// </summary>
        private void SaveUserTheme()
        {
            try
            {
                if (PhotoWallpaper == _lightImage)
                {
                    File.WriteAllText(@"..\..\UserTheme.t", "light");
                }
                else
                {
                    File.WriteAllText(@"..\..\UserTheme.t", "dark");
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion


        #region Current Page Settings
        private Page _page;
        private Page _gameSettingsUser1;
        private Page _gameSettingsUser2;
        private Page _startMenu;
        private Page _startGame;
        private Page _prevPage;
        private Visibility _isNameButtonOn = Visibility.Hidden;

        public Visibility IsNameButtonOn
        {
            get
            {
                return _isNameButtonOn;
            }
            set
            {
                _isNameButtonOn = value;
                OnPropertyChanged(nameof(IsNameButtonOn));
            }
        }

        /// <summary>
        /// Current page
        /// </summary>
        public Page Page
        {
            get
            {
                if (_page == null)
                {
                    _page = _startMenu;
                    _page.DataContext = this;
                }
                return _page;
            }
            set
            {
                _page = value;
                _page.DataContext = this;
                OnPropertyChanged(nameof(Page));
            }
        }


        private bool _isCloseDialogOpen;

        /// <summary>
        /// Is the exit menu open
        /// </summary>
        public bool IsCloseDialogOpen
        {
            get
            {
                return _isCloseDialogOpen;
            }
            set
            {
                _isCloseDialogOpen = value;
                OnPropertyChanged(nameof(IsCloseDialogOpen));
            }
        }

        /// <summary>
        /// Exit command with saving data
        /// </summary>
        public override RelayCommand CloseWindow
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        if (SelectedHeroUser1 != null && SelectedHeroUser2 != null && Page == _startGame)
                        {
                            SaveGame.StartSaveGame(SelectedHeroUser1, SelectedHeroUser2, UserName1, UserName2, false, Logs, EnemyTypeIsHuman);
                        }

                        SaveUserTheme();
                        window.Close();
                    });
                }
                return _close;
            }
        }


        private RelayCommand _closeWindowWithoutSaving;

        /// <summary>
        /// Exit command without saving data
        /// </summary>
        public RelayCommand CloseWindowWithoutSaving
        {
            get
            {
                if (_closeWindowWithoutSaving == null)
                {
                    _closeWindowWithoutSaving = new RelayCommand(obj =>
                    {
                        var window = obj as Window;
                        SaveUserTheme();
                        window.Close();
                    });
                }
                return _closeWindowWithoutSaving;
            }
        }



        private RelayCommand _closeWindowDialog;

        /// <summary>
        /// Return to the main menu with saving game data and close the exit panel
        /// </summary>
        public RelayCommand CloseWindowDialog
        {
            get
            {
                if (_closeWindowDialog == null)
                {
                    _closeWindowDialog = new RelayCommand(obj =>
                    {
                        if (SelectedHeroUser1 != null && SelectedHeroUser2 != null && Page == _startGame)
                        {
                            SaveGame.StartSaveGame(SelectedHeroUser1, SelectedHeroUser2, UserName1, UserName2, false, Logs, EnemyTypeIsHuman);
                        }
                        IsCloseDialogOpen = false;
                        Page = _startMenu;
                        SetDeafaultsValue();

                    }, obj => Page != _startMenu);
                }
                return _closeWindowDialog;
            }
        }



        private RelayCommand _closeWindowDialogWithoutSave;

        /// <summary>
        /// Return to the main menu without saving game data and close the exit panel
        /// </summary>
        public RelayCommand CloseWindowDialogWithoutSave
        {
            get
            {
                if (_closeWindowDialogWithoutSave == null)
                {
                    _closeWindowDialogWithoutSave = new RelayCommand(obj =>
                    {

                        IsCloseDialogOpen = false;
                        Page = _startMenu;
                        SetDeafaultsValue();


                    }, obj => Page != _startMenu);
                }
                return _closeWindowDialogWithoutSave;
            }
        }



        private RelayCommand _closeDialog;

        /// <summary>
        /// Close the exit menu (Return button)
        /// </summary>
        public RelayCommand CloseDialog
        {
            get
            {
                if (_closeDialog == null)
                {
                    _closeDialog = new RelayCommand(obj =>
                    {
                        IsCloseDialogOpen = true;
                    });
                }
                return _closeDialog;
            }
        }


        private RelayCommand _closeWindowDialogSaveAs;

        /// <summary>
        /// Open a dialog for manually saving game data
        /// </summary>
        public RelayCommand CloseWindowDialogSaveAs
        {
            get
            {
                if (_closeWindowDialogSaveAs == null)
                {
                    _closeWindowDialogSaveAs = new RelayCommand(obj =>
                    {
                        SaveGame.StartSaveGame(SelectedHeroUser1, SelectedHeroUser2, UserName1, UserName2, true, Logs, EnemyTypeIsHuman);

                    }, obj => SelectedHeroUser1 != null && SelectedHeroUser2 != null && Page == _startGame);
                }
                return _closeWindowDialogSaveAs;
            }
        }


        private RelayCommand _rePlayCommand;
        /// <summary>
        /// Replay round
        /// </summary>
        public RelayCommand RePlayCommand
        {
            get
            {
                if (_rePlayCommand == null)
                {
                    _rePlayCommand = new RelayCommand(obj =>
                      {
                          SaveGame.StartSaveGame(SelectedHeroUser1, SelectedHeroUser2, UserName1, UserName2, false, Logs, EnemyTypeIsHuman);
                          SelectedHeroUser1.MaxHealth = SelectedHeroUser1.DefaultHealth;
                          SelectedHeroUser1.Health = SelectedHeroUser1.DefaultHealth;

                          SelectedHeroUser2.MaxHealth = SelectedHeroUser2.DefaultHealth;
                          SelectedHeroUser2.Health = SelectedHeroUser2.DefaultHealth;

                          Logs.Clear();
                          _startGame = new View.GameProcessView();
                          Page = _startGame;
                          IsCloseDialogOpen = false;
                      }, obj => Page == _startGame && Logs.Count > 0 && SelectedHeroUser2.DefaultHealth != 0 && SelectedHeroUser1.DefaultHealth != 0);
                }
                return _rePlayCommand;
            }
        }


        /// <summary>
        /// Open dialog for loading saved games
        /// </summary>
        public override RelayCommand OpenExplorer
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (!Directory.Exists(@"Saves"))
                    {
                        Directory.CreateDirectory(@"Saves");
                    }
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Filter = "Text documents (*.xml)|*.xml|All files (*.*)|*.*";
                    dialog.Title = "Загрузить игру";
                    dialog.FilterIndex = 1;
                    dialog.InitialDirectory = Environment.CurrentDirectory+@"\Saves\";
                    dialog.FileName = "LastSave.xml";
                    Nullable<bool> result = dialog.ShowDialog();

                    if (result == true)
                    {
                        _pathToLoad = dialog.FileName;
                        SetDeafaultsValue();
                        if (LoadGame.StartLoadGame(_pathToLoad, ref _userName1, out _selectedHeroUser1, ref _userName2, out _selectedHeroUser2, ref _logs, ref _enemyTypeIsHuman))
                        {
                            UserName1 = _userName1;
                            UserName2 = _userName2;
                            SelectedHeroUser1 = _selectedHeroUser1;
                            SelectedHeroUser2 = _selectedHeroUser2;
                            Logs = _logs;
                            EnemyTypeIsHuman = _enemyTypeIsHuman;
                            Page = _startGame;
                        }
                    }
                });
            }
        }


        #endregion


        #region Video and Photo wallpapers settings
        private MediaElement _videoWallpaper;


        /// <summary>
        /// Returns video wallpaper and loops the video
        /// </summary>
        public MediaElement VideoWallpaper
        {
            get => _videoWallpaper;
            set
            {
                if (_videoWallpaper == null)
                {
                    _videoWallpaper = value;
                    _videoWallpaper.Source = new Uri(@"..\..\Resources\Images\LiveWallpaper2.mp4", UriKind.Relative);
                }
                _videoWallpaper.MediaEnded += OnVideoEnded;
                _videoWallpaper.UnloadedBehavior = MediaState.Manual;
                _videoWallpaper.IsMuted = true;
                _videoWallpaper.Stretch = System.Windows.Media.Stretch.UniformToFill;
                OnPropertyChanged(nameof(VideoWallpaper));
            }
        }

        /// <summary>
        /// Video End Event Handler
        /// Zero video position
        /// </summary>
        /// <param name="param"></param>
        /// <param name="e"></param>
        private void OnVideoEnded(object param, RoutedEventArgs e)
        {
            _videoWallpaper.Position = TimeSpan.Zero;
            _videoWallpaper.Play();
        }

        private Image _photoWallpaper;

        /// <summary>
        /// Returns photo wallpaper
        /// </summary>
        public Image PhotoWallpaper
        {
            get => _photoWallpaper;

            set
            {
                _photoWallpaper = value;
                OnPropertyChanged(nameof(PhotoWallpaper));
            }
        }

        #endregion



    }
}
