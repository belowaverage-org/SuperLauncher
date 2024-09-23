using System.Windows;
using System.Windows.Input;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherRenameUI.xaml
    /// </summary>
    public partial class ModernLauncherRenameUI : Window
    {
        private ModernLauncherIcon MLI;
        public ModernLauncherRenameUI(ModernLauncherIcon Icon)
        {
            MLI = Icon;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Shared.EnableAcrylic(this);
            Shared.SetWindowColor(this);
            TBName.Focus();
            TBName.Text = MLI.Title;
            TBName.SelectAll();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            MLI.Title = TBName.Text;
            ((ModernLauncher)Program.ModernApplication.MainWindow).MLI.CommitIconsToFile();
            Close();
        }
        private void TBName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnOK_Click(null, null);
        }
    }
}