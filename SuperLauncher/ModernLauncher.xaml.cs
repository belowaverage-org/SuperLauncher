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
using System.Windows.Shapes;
using System.Windows.Interop;
using PInvoke;
using System.Windows.Media.Animation;
//using Image = System.Drawing.Image;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        public ModernLauncher()
        {
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Win32Interop.EnableBlur(new WindowInteropHelper(this).Handle, 200, 0);
        }
    }
}
