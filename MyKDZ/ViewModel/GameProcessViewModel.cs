using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GameDataLibrary;
using MyKDZ.Model;

namespace MyKDZ.ViewModel
{
    enum AttackTypes
    {
        Attack,
        Run,
        Block
    }

    partial class MainWindowViewModel : ViewModelBase
    {
        static Random rnd = new Random();
        ObservableCollection<string> _logs = new ObservableCollection<string>();

        /// <summary>
        /// All information about the current round
        /// </summary>
        public ObservableCollection<string> Logs
        {
            get
            {
                return _logs;
            }
            set
            {
                _logs = value;
                OnPropertyChanged(nameof(Logs));
            }
        }

        private Visibility _isUser1Move;

        /// <summary>
        /// Switch for a button informing that the user has not made a move
        /// </summary>
        public Visibility IsUser1MoveNot
        {
            get
            {
                if (_isUser1Move == Visibility.Visible)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            set
            {
                _isUser1Move = value;
                OnPropertyChanged(nameof(IsUser1MoveNot));
                OnPropertyChanged(nameof(IsUser1MoveYes));
            }
        }

        /// <summary>
        /// Switch for a button informing that the user has already made a move
        /// </summary>
        public Visibility IsUser1MoveYes
        {
            get
            {
                if (_isUser1Move == Visibility.Visible)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
            set
            {
                OnPropertyChanged(nameof(IsUser1MoveYes));
            }
        }


        private Visibility _isUser2Move;

        /// <summary>
        /// Switch for a button informing that the user has not made a move
        /// </summary>
        public Visibility IsUser2MoveNot
        {
            get
            {
                if (!EnemyTypeIsHuman)
                {
                    return Visibility.Collapsed;
                }
                if (_isUser2Move == Visibility.Visible)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            set
            {
                _isUser2Move = value;
                OnPropertyChanged(nameof(IsUser2MoveNot));
                OnPropertyChanged(nameof(IsUser2MoveYes));
            }
        }

        /// <summary>
        /// Switch for a button informing that the user has already made a move
        /// </summary>
        public Visibility IsUser2MoveYes
        {
            get
            {
                if (!EnemyTypeIsHuman)
                {
                    return Visibility.Visible;
                }
                if (_isUser2Move == Visibility.Visible)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
            set
            {
                OnPropertyChanged(nameof(IsUser2MoveYes));
            }
        }



        private RelayCommand _user1Move;
        private string _user1MoveInformation;

        /// <summary>
        /// Describes the logic after a first player makes a move
        /// </summary>
        public RelayCommand User1Move
        {
            get
            {
                if (_user1Move == null)
                {
                    _user1Move = new RelayCommand(obj =>
                      {
                          IsUser1MoveNot = Visibility.Collapsed;
                          var param = obj.ToString();
                          if (!EnemyTypeIsHuman)
                          {
                              string res = Fighting.Fight(SelectedHeroUser1, obj.ToString(), SelectedHeroUser2,
                                  (Enum.GetValues(typeof(AttackTypes)).GetValue(rnd.Next(0, 3))).ToString());
                              if (res != null)
                              {
                                  //Logs.Add(res);
                                  CultureInfo.CurrentCulture = new CultureInfo("ru-RU", false);
                                  Logs.Insert(0, res + "      (" + DateTime.Now.ToString("T") + ")");
                                  CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

                              }

                              IsUser1MoveNot = Visibility.Visible;
                          }
                          else
                          {
                              _user1MoveInformation = param;
                          }
                          if (SelectedHeroUser1.Health == 0)
                          {
                              string result = "\t************************************************************************" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += $"\t         {UserName2} ПОБЕДИЛ используя пресонажа {SelectedHeroUser2.Name}          " + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t************************************************************************";
                              Logs.Insert(0, result);
                          }
                          else if (SelectedHeroUser2.Health == 0)
                          {
                              string result = "\t************************************************************************" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += $"\t         {UserName1} ПОБЕДИЛ используя пресонажа {SelectedHeroUser1.Name}          " + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t************************************************************************";
                              Logs.Insert(0, result);
                          }
                      }, obj => Page==_startGame && IsUser1MoveNot == Visibility.Visible && SelectedHeroUser1?.Health > 0 && SelectedHeroUser2?.Health > 0);
                }
                return _user1Move;
            }

        }


        private RelayCommand _cancelUser1Move;

        /// <summary>
        /// Player Cancellation Logic
        /// </summary>
        public RelayCommand CancelUser1Move
        {
            get
            {
                if (_cancelUser1Move == null)
                {
                    _cancelUser1Move = new RelayCommand(obj =>
                      {
                          IsUser1MoveYes = Visibility.Collapsed;
                          IsUser1MoveNot = Visibility.Visible;
                      });
                }
                return _cancelUser1Move;
            }
        }


        private RelayCommand _user2Move;
        private string _user2MoveInformation;

        /// <summary>
        /// Describes the logic after a second player makes a move
        /// </summary>
        public RelayCommand User2Move
        {
            get
            {
                if (_user2Move == null)
                {
                    _user2Move = new RelayCommand(obj =>
                    {
                        IsUser2MoveNot = Visibility.Collapsed;
                        _user2MoveInformation = obj.ToString();
                    }, obj => Page == _startGame && IsUser2MoveNot == Visibility.Visible && SelectedHeroUser1?.Health > 0 && SelectedHeroUser2?.Health > 0);
                }
                return _user2Move;
            }

        }

        private RelayCommand _cancelUser2Move;

        /// <summary>
        /// Player Cancellation Logic
        /// </summary>
        public RelayCommand CancelUser2Move
        {
            get
            {
                if (_cancelUser2Move == null)
                {
                    _cancelUser2Move = new RelayCommand(obj =>
                    {
                        IsUser2MoveYes = Visibility.Collapsed;
                        IsUser2MoveNot = Visibility.Visible;
                    });
                }
                return _cancelUser2Move;
            }
        }



        private RelayCommand _nextMove;

        /// <summary>
        /// Describes actions after both players have selected an action
        /// </summary>
        public RelayCommand NextMove
        {
            get
            {
                if (_nextMove == null)
                {
                    _nextMove = new RelayCommand(obj =>
                      {
                          string res = Fighting.Fight(SelectedHeroUser1, _user1MoveInformation, SelectedHeroUser2, _user2MoveInformation);
                          if (res != null)
                          {
                              //Logs.Add(res);
                              CultureInfo.CurrentCulture = new CultureInfo("ru-RU", false);
                              Logs.Insert(0, res + "      (" + DateTime.Now.ToString("T") + ")");
                              CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
                          }
                          IsUser2MoveYes = Visibility.Collapsed;
                          IsUser2MoveNot = Visibility.Visible;
                          IsUser1MoveYes = Visibility.Collapsed;
                          IsUser1MoveNot = Visibility.Visible;
                          if (SelectedHeroUser1.Health == 0)
                          {
                              string result = "\t************************************************************************" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += $"\t      {UserName2} ПОБЕДИЛ используя пресонажа {SelectedHeroUser2.Name}          " + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t************************************************************************";
                              Logs.Insert(0, result);
                          }
                          else if(SelectedHeroUser2.Health == 0)
                          {
                              string result = "\t************************************************************************" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += $"\t      {UserName1} ПОБЕДИЛ используя пресонажа {SelectedHeroUser1.Name}          " + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t*                                                                                                           *" + Environment.NewLine;
                              result += "\t************************************************************************";
                              Logs.Insert(0, result);
                          }
                      }, obj => Page == _startGame && IsUser2MoveYes == Visibility.Visible && IsUser1MoveYes == Visibility.Visible
                                                                        && SelectedHeroUser1?.Health > 0 && SelectedHeroUser2?.Health > 0);
                }
                return _nextMove;
            }
        }

    }
}
