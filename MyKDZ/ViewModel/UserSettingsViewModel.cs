using System;
using System.Windows;
using GameDataLibrary;
using MaterialDesignThemes.Wpf;
using MyKDZ.Model;


namespace MyKDZ.ViewModel
{
    partial class MainWindowViewModel
    {

        private string _userName1 = "Игрок 1";
        private string _userName2 = "Robot";
        private bool _enemyTypeIsHuman;


        /// <summary>
        /// Determines the type of second player
        /// </summary>
        public bool EnemyTypeIsHuman
        {
            get
            {
                return _enemyTypeIsHuman;
            }
            set
            {
                _enemyTypeIsHuman = value;
                OnPropertyChanged(nameof(EnemyTypeIsHuman));
                if (_enemyTypeIsHuman)
                {
                    UserName2 = "Игрок 2";
                }
                else UserName2 = "Robot";
            }
        }


        /// <summary>
        /// First player name
        /// </summary>
        public string UserName1
        {
            get => _userName1;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _userName1 = value;
                }
                OnPropertyChanged(nameof(UserName1));
            }
        }

        /// <summary>
        /// Second player name
        /// </summary>
        public string UserName2
        {
            get => _userName2;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _userName2 = value;
                }
                OnPropertyChanged(nameof(UserName2));
            }
        }

        private string _userName;


        /// <summary>
        /// The name of the player to configure
        /// </summary>
        public string UserName
        {
            get
            {
                if (Page == _gameSettingsUser1)
                {
                    return UserName1;
                }
                if (Page == _gameSettingsUser2)
                {
                    return UserName2;
                }
                return _userName;
            }
            set
            {
                if (Page == _gameSettingsUser1)
                {
                    UserName1 = value;
                }
                if (Page == _gameSettingsUser2)
                {
                    UserName2 = value;
                }
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        private RelayCommand _saveUserNameAndStartGame;

        /// <summary>
        /// Save player settings and go to the next window
        /// </summary>
        public RelayCommand SaveUserNameAndStartGame
        {
            get
            {
                if (_saveUserNameAndStartGame == null)
                {
                    _saveUserNameAndStartGame = new RelayCommand(obj =>
                      {
                          SetDeafaultsValue();
                          //_prevPage = _startMenu;
                          //_gameSettingsUser1 = new View.GameSettings();
                          //_gameSettingsUser2 = new View.GameSettings();
                          //_selectedHeroUser1 = null;
                          //_selectedHeroUser2 = null;
                          //UserName = UserName1;
                          //_startGame = new View.GameProcessView();
                          try
                          {
                              _heroesListNotFilter = HeroesParser.GetHeroes();
                          }
                          catch (Exception ex)
                          {

                              MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "*Добавьте файл Dota2.csv в папку Resources рядом с MyKDZ.csproj и перезапустите программу",
                                 "Ошибка парсинга персонажей", MessageBoxButton.OK, MessageBoxImage.Error);
                          }
                          HeroesList = _heroesListNotFilter;
                          IsNameButtonOn = Visibility.Visible;
                          DialogHost.CloseDialogCommand.Execute(null, null);
                          Page = _gameSettingsUser1;
                      }, obj => !String.IsNullOrWhiteSpace(UserName1) && !String.IsNullOrWhiteSpace(UserName2));
                }
                return _saveUserNameAndStartGame;
            }

        }

    }
}
