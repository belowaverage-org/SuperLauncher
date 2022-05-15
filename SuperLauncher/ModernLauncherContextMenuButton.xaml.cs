using System.Windows;
using System.Windows.Controls;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenuButton.xaml
    /// </summary>
    public partial class ModernLauncherContextMenuButton : UserControl
    {
        public string Text
        {
            get { return (string)TextLabel.Content; }
            set { TextLabel.Content = value; }
        }
        public string Icon
        {
            get { return (string)IconLabel.Content; }
            set { IconLabel.Content = value; }
        }
        public ModernLauncherContextMenuButton()
        {
            InitializeComponent();
        }
    }
}
