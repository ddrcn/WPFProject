using System;
using System.Collections.ObjectModel;
using System.Linq;
using GameDataLibrary;
using MyKDZ.Model;
using System.Windows;

namespace MyKDZ.ViewModel
{
    partial class MainWindowViewModel
    {

        ObservableCollection<Hero> _heroesList;
        ObservableCollection<Hero> _heroesListNotFilter;

        /// <summary>
        /// List of heroes
        /// </summary>
        public ObservableCollection<Hero> HeroesList
        {
            get
            {
                if (Page == _gameSettingsUser2 && _selectedHeroUser1 != null)
                {
                    _heroesList = new ObservableCollection<Hero>(from items in _heroesList where items != _selectedHeroUser1 select items);
                }
                if (Page == _gameSettingsUser1 && _selectedHeroUser2 != null)
                {
                    _heroesList = new ObservableCollection<Hero>(from items in _heroesList where items != _selectedHeroUser2 select items);
                }
                return _heroesList;
            }
            set
            {

                _heroesList = value;

                OnPropertyChanged(nameof(HeroesList));
            }
        }

        private Hero _selectedHeroUser1;
        private Hero _selectedHeroUser2;


        /// <summary>
        /// The hero who is currently selected
        /// </summary>
        public Hero SelectedHero
        {
            get
            {
                if (Page == _gameSettingsUser1)
                {
                    return _selectedHeroUser1;
                }
                else
                {
                    return _selectedHeroUser2;
                }
            }
            set
            {
                if (Page == _gameSettingsUser1)
                {
                    _selectedHeroUser1 = value;

                }
                else
                {
                    _selectedHeroUser2 = value;
                }
                OnPropertyChanged(nameof(SelectedHero));
            }
        }

        /// <summary>
        /// Hero of the first player
        /// </summary>
        public Hero SelectedHeroUser1
        {
            get
            {
                return _selectedHeroUser1;

            }
            set
            {
                _selectedHeroUser1 = value;
                OnPropertyChanged(nameof(SelectedHeroUser1));

            }
        }

        /// <summary>
        /// Hero of the second player
        /// </summary>
        public Hero SelectedHeroUser2
        {
            get
            {
                return _selectedHeroUser2;
            }
            set
            {
                _selectedHeroUser2 = value;
                OnPropertyChanged(nameof(SelectedHeroUser2));
            }
        }


        private bool _isCheckTypeFilter;
        private bool _isCheckRegenerationFilter;
        private bool _isCheckMoveSpeedFilter;

        public bool IsCheckTypeFilter
        {
            get => _isCheckTypeFilter;
            set
            {
                _isCheckTypeFilter = value;
                TypeFilter = null;
                OnPropertyChanged(nameof(IsCheckTypeFilter));

            }
        }
        public bool IsCheckRegenerationFilter
        {
            get => _isCheckRegenerationFilter;
            set
            {
                _isCheckRegenerationFilter = value;
                RegenerationFilter = null;
                OnPropertyChanged(nameof(IsCheckRegenerationFilter));
            }
        }
        public bool IsCheckMoveSpeedFilter
        {
            get => _isCheckMoveSpeedFilter;
            set
            {
                _isCheckMoveSpeedFilter = value;
                MoveSpeed = null;
                OnPropertyChanged(nameof(IsCheckMoveSpeedFilter));

            }
        }



        private string _typeFilter;

        /// <summary>
        /// Description of logic for filter by type
        /// </summary>
        public string TypeFilter
        {
            get
            {
                return _typeFilter;
            }
            set
            {
                _typeFilter = value ?? _typeFilter;
                OnPropertyChanged(nameof(TypeFilter));
                if (IsCheckTypeFilter && _typeFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(from item in _heroesListNotFilter where item.Type.ToString().Contains(_typeFilter) select item);
                    if (IsCheckRegenerationFilter && _regenerationFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Regeneration.ToString().Contains(_regenerationFilter) select item);
                    }
                    if (IsCheckMoveSpeedFilter && _moveSpeedFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.MoveSpeed.ToString().Contains(_moveSpeedFilter) select item);
                    }
                }
                else if (_typeFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(_heroesListNotFilter);
                    if (IsCheckRegenerationFilter && _regenerationFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Regeneration.ToString().Contains(_regenerationFilter) select item);
                    }
                    if (IsCheckMoveSpeedFilter && _moveSpeedFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.MoveSpeed.ToString().Contains(_moveSpeedFilter) select item);
                    }
                }


            }
        }


        private string _moveSpeedFilter;

        /// <summary>
        /// Description of logic for filter by moveSpeed
        /// </summary>
        public string MoveSpeed
        {
            get
            {
                return _moveSpeedFilter;
            }
            set
            {
                _moveSpeedFilter = value ?? _moveSpeedFilter;
                OnPropertyChanged(nameof(MoveSpeed));
                if (IsCheckMoveSpeedFilter && _moveSpeedFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(from item in _heroesListNotFilter where item.MoveSpeed.ToString().Contains(_moveSpeedFilter) select item);
                    if (IsCheckRegenerationFilter && _regenerationFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Regeneration.ToString().Contains(_regenerationFilter) select item);
                    }
                    if (IsCheckTypeFilter && _isCheckTypeFilter)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Type.ToString().Contains(_typeFilter) select item);
                    }
                }
                else if (_moveSpeedFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(_heroesListNotFilter);
                    if (IsCheckRegenerationFilter && _regenerationFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Regeneration.ToString().Contains(_regenerationFilter) select item);
                    }
                    if (IsCheckTypeFilter && _typeFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Type.ToString().Contains(_typeFilter) select item);
                    }
                }
            }
        }


        private string _regenerationFilter;

        /// <summary>
        /// Description of logic for filter by regeneration
        /// </summary>
        public string RegenerationFilter
        {
            get
            {
                return _regenerationFilter;
            }
            set
            {
                _regenerationFilter = value ?? _regenerationFilter;
                OnPropertyChanged(nameof(RegenerationFilter));
                if (IsCheckRegenerationFilter && _regenerationFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(from item in _heroesListNotFilter where item.Regeneration.ToString().Contains(_regenerationFilter) select item);
                    if (IsCheckMoveSpeedFilter && _moveSpeedFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.MoveSpeed.ToString().Contains(_moveSpeedFilter) select item);
                    }
                    if (IsCheckTypeFilter && _typeFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Type.ToString().Contains(_typeFilter) select item);
                    }
                }
                else if (_regenerationFilter != null)
                {
                    HeroesList = new ObservableCollection<Hero>(_heroesListNotFilter);
                    if (IsCheckMoveSpeedFilter && _moveSpeedFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.MoveSpeed.ToString().Contains(_moveSpeedFilter) select item);
                    }
                    if (IsCheckTypeFilter && _typeFilter != null)
                    {
                        HeroesList = new ObservableCollection<Hero>(from item in _heroesList where item.Type.ToString().Contains(_typeFilter) select item);
                    }
                }
            }
        }



        private RelayCommand _startNewGameCommand;

        /// <summary>
        /// The beginning of a new game command
        /// </summary>
        public RelayCommand StartNewGameCommand
        {
            get
            {
                if (_startNewGameCommand == null)
                {
                    _startNewGameCommand = new RelayCommand(obj =>
                     {
                         if (Page == _gameSettingsUser1)
                         {

                             if (!EnemyTypeIsHuman)
                             {
                                 Random rnd = new Random();
                                 Page = _startGame;
                                 HeroesList = new ObservableCollection<Hero>(from items in _heroesListNotFilter where items != _selectedHeroUser1 select items);
                                 _selectedHeroUser2 = HeroesList[rnd.Next(0, HeroesList.Count)];
                                 SelectedHero = _selectedHeroUser2;
                                 UserName2 = "Robot";
                                 IsNameButtonOn = Visibility.Hidden;

                                 return;
                             }

                             Page = _gameSettingsUser2;

                             if (_selectedHeroUser2 != null)
                             {
                                 SelectedHero = _selectedHeroUser2;
                             }
                             IsCheckMoveSpeedFilter = false;
                             IsCheckRegenerationFilter = false;
                             IsCheckTypeFilter = false;
                             TypeFilter = string.Empty;
                             MoveSpeed = string.Empty;
                             RegenerationFilter = string.Empty;
                             HeroesList = new ObservableCollection<Hero>(from items in _heroesListNotFilter where items != _selectedHeroUser1 select items);
                             OnPropertyChanged(nameof(UserName));

                             return;
                         }
                         if (Page == _gameSettingsUser2)
                         {
                             IsNameButtonOn = Visibility.Hidden;
                             Page = _startGame;

                             return;
                         }

                     }, obj => SelectedHero != null && _selectedHeroUser1 != _selectedHeroUser2 && SelectedHero.Health > 0);
                }
                
                return _startNewGameCommand;
            }
        }



        private RelayCommand _backPage;

        /// <summary>
        /// Return to previous page
        /// </summary>
        public RelayCommand BackPage
        {
            get
            {
                if (_backPage == null)
                {
                    _backPage = new RelayCommand(obj =>
                      {

                          if (Page == _gameSettingsUser1)
                          {
                              Page = _startMenu;
                              IsNameButtonOn = Visibility.Hidden;
                              return;
                          }
                          if (Page == _gameSettingsUser2)
                          {
                              Page = _gameSettingsUser1;
                              SelectedHero = _selectedHeroUser1;
                              IsCheckMoveSpeedFilter = false;
                              IsCheckRegenerationFilter = false;
                              IsCheckTypeFilter = false;
                              TypeFilter = string.Empty;
                              MoveSpeed = string.Empty;
                              RegenerationFilter = string.Empty;
                              HeroesList = new ObservableCollection<Hero>(from items in _heroesListNotFilter where items != _selectedHeroUser2 select items);
                              OnPropertyChanged(nameof(UserName));
                              return;
                          }
                          if (Page == _startGame)
                          {
                              Page = _startMenu;
                              return;
                          }
                      });
                }
                return _backPage;
            }

        }



    }
}
