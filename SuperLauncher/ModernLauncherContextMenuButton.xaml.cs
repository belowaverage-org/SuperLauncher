using System.Windows;
using System.Windows.Controls;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenuButton.xaml
    /// </summary>
    public partial class ModernLauncherContextMenuButton : UserControl
    {
        private bool useSLIcons = false;
        public string Text
        {
            get { return (string)TextLabel.Content; }
            set { TextLabel.Content = value; }
        }
        public string Icon
        {
            get { return (string)IconLabel.Content; }
            set 
            { 
                IconLabel.Content = value;
                SLIconLabel.Content = value;
            }
        }
        public bool UserSLIcons
        {
            get { return useSLIcons; }
            set 
            {
                useSLIcons = value;
                if (useSLIcons)
                {
                    IconLabel.Visibility = Visibility.Hidden;
                    SLIconLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    IconLabel.Visibility = Visibility.Visible;
                    SLIconLabel.Visibility = Visibility.Hidden;
                }
            }
        }
        public ModernLauncherContextMenuButton()
        {
            InitializeComponent();
        }
        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                Opacity = 0.5;
            }
            else
            {
                Opacity = 1;
            }
        }
    }
}