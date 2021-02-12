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
        public SuperLauncherIcon()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ControlBG.Opacity = 0.5;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ControlBG.Opacity = 1;
        }
    }
}
