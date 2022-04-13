using System.IO;
using System.Windows.Controls;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherIcons.xaml
    /// </summary>
    public partial class ModernLauncherIcons : UserControl
    {
        public ModernLauncherIcons()
        {
            InitializeComponent();
            foreach (string filePath in Settings.Default.FileList)
            {
                if (!File.Exists(filePath)) continue;
                ModernLauncherIcon mli = new(filePath);
                IconPanel.Children.Add(mli);
            }
        }
    }
}
