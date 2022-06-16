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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherCredentialUI.xaml
    /// </summary>
    public partial class ModernLauncherCredentialUI : Window
    {
        public ModernLauncherCredentialUI()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper WIH = new(this);
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
