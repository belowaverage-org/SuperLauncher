using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Animation;

namespace SuperLauncherCommon
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        string Site = "";
        public Splash()
        {
            InitializeComponent();
            Assembly self = Assembly.GetEntryAssembly();
            Copyright.Content = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyCopyrightAttribute))).Copyright;
            Author.Content = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyCompanyAttribute))).Company;
            Version.Content = "v" + ((AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyInformationalVersionAttribute))).InformationalVersion;
            Site = ((AssemblyMetadataAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyMetadataAttribute))).Value;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            IEasingFunction se = new SineEase();
            ((SineEase)se).EasingMode = EasingMode.EaseInOut;
            ThicknessAnimationUsingKeyFrames horz = new()
            {
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(3, -3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(3, 0, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(-3, -3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(-3, 3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(0, -3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(-3, 0, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(3, 3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(0, -3, 0, 0), KeyTime.Uniform, se));
            horz.KeyFrames.Add(new EasingThicknessKeyFrame(new(0, 0, 0, 0), KeyTime.Uniform, se));
            Ship.BeginAnimation(MarginProperty, horz);
            DoubleAnimationUsingKeyFrames flicker = new()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            flicker.KeyFrames.Add(new EasingDoubleKeyFrame(0.3));
            flicker.KeyFrames.Add(new EasingDoubleKeyFrame(0));
            Flame.BeginAnimation(OpacityProperty, flicker);
        }
        private void Website_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
            Process.Start("OpenWith.exe", Site);
        }
        private void Window_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}