using MyKDZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyKDZ.ViewModel
{
    class ViewModelBase : NotifyPropertyChanged
    {
        //Path to load save
        protected string _pathToLoad;

        private bool _theme;

        /// <summary>
        /// Property for toggle button
        /// </summary>
        public bool Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }


        public bool PhotoOrVideoWallpaper { get; set; }

        protected RelayCommand _close;

        /// <summary>
        /// Command property to close the window
        /// </summary>
        public virtual RelayCommand CloseWindow
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(obj =>
                     {
                         var window = obj as Window;
                         MessageBox.Show(window.ToString() + " say GoodBye :)");
                         window.Close();
                     });
                }
                return _close;
            }
        }



        /// <summary>
        /// Open dialog window
        /// </summary>
        public virtual RelayCommand OpenExplorer
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Filter = "Text documents (*.xml)|*.xml|All files (*.*)|*.*";
                    dialog.Title = "Загрузить игру";
                    dialog.FilterIndex = 1;

                    Nullable<bool> result = dialog.ShowDialog();

                    if (result == true)
                    {
                        // Open document
                        _pathToLoad = dialog.FileName;
                    }
                });
            }
        }

    }
}
