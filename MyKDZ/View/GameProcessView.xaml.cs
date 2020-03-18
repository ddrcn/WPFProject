using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyKDZ.View
{
    /// <summary>
    /// Логика взаимодействия для GameProcessView.xaml
    /// </summary>
    public partial class GameProcessView : Page
    {

        public GameProcessView()
        {
            InitializeComponent();

            
        }


        private static double DpiWidthFactor;
        private static double DpiHeightFactor;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Page visual = sender as Page;
            double curheight = this.Height;
            double curwidth = this.Width;
            PresentationSource presentationsource = PresentationSource.FromVisual(visual);
            Matrix m = presentationsource.CompositionTarget.TransformToDevice;
            DpiWidthFactor = m.M11;
            DpiHeightFactor = m.M22;

            double screenHeight = Screen.PrimaryScreen.Bounds.Height;
            if (screenHeight < curheight)
            {
                DpiHeightFactor = DpiHeightFactor * curheight * 1.08 / screenHeight;
                DpiWidthFactor = DpiWidthFactor * curheight * 1.08 / screenHeight;
            }

            curheight = curheight / DpiHeightFactor + (DpiHeightFactor - 1) * 24;
            curwidth = curwidth / DpiWidthFactor + (DpiWidthFactor - 1) * 8;
            visual.Height = curheight;
            visual.Width = curwidth;
            visual.LayoutTransform = new ScaleTransform(1 / DpiWidthFactor, 1 / DpiHeightFactor);
        }
    }
}
