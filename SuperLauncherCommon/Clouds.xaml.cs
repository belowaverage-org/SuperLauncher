using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SuperLauncherCommon
{
    /// <summary>
    /// Interaction logic for Clouds.xaml
    /// </summary>
    public partial class Clouds : Page
    {
        public Clouds()
        {
            InitializeComponent();
            Cloud1.StartCloudAnimation();
            Cloud2.StartCloudAnimation();
            Cloud3.StartCloudAnimation();
            Cloud4.StartCloudAnimation();
            Cloud5.StartCloudAnimation();
            Cloud6.StartCloudAnimation();
            Cloud7.StartCloudAnimation();
        }
    }
    public static class CloudAnimation
    {
        public static void StartCloudAnimation(this Image Cloud)
        {
            double bound_left = -300;
            double bound_right = 440;
            double left = Canvas.GetLeft(Cloud);
            double half = (left - bound_left) / ((bound_left - bound_right) * -1);
            DoubleAnimationUsingKeyFrames animation = new()
            {
                Duration = TimeSpan.FromSeconds(new Random().Next(60, 120)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(left, KeyTime.FromPercent(0)));
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(bound_left, KeyTime.FromPercent(half)));
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(bound_right, KeyTime.FromPercent(half)));
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(left, KeyTime.FromPercent(1)));
            Cloud.BeginAnimation(Canvas.LeftProperty, animation);
        }
    }
}
