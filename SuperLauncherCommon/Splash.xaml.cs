using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SuperLauncherCommon
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();
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
    }
}
