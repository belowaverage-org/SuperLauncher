using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperLauncher.Elements
{
    /// <summary>
    /// Interaction logic for SuperLauncherIcon.xaml
    /// </summary>
    public partial class SuperLauncherIcon : UserControl
    {
        private DoubleAnimation AniFadeIn = new DoubleAnimation()
        {
            Duration = new Duration(TimeSpan.FromMilliseconds(100)),
            To = 0.5
        };
        private DoubleAnimation AniFadeOut = new DoubleAnimation()
        {
            Duration = new Duration(TimeSpan.FromMilliseconds(100)),
            To = 1
        };
        public SuperLauncherIcon()
        {
            InitializeComponent();
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ControlBG.BeginAnimation(OpacityProperty, AniFadeIn);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ControlBG.BeginAnimation(OpacityProperty, AniFadeOut);
        }
    }
}
