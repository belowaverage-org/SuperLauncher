using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SuperLauncher
{
    public partial class ModernLauncherIcon : UserControl
    {
        public ModernLauncherIcon()
        {
            InitializeComponent();
        }
        private DoubleAnimation To1 = new()
        {
            To = 1,
            Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
        };
        private DoubleAnimation To0 = new()
        {
            To = 0,
            Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
        };
        private DoubleAnimation To0_5 = new()
        {
            To = 0.5,
            Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
        };
        private DoubleAnimation To0_9 = new()
        {
            To = 0.9,
            Duration = new Duration(new TimeSpan(0, 0, 0, 0, 100))
        };
        private void UserControl_FadeInHighlight(object sender, object e)
        {
            Highlight.BeginAnimation(OpacityProperty, To1);
        }
        private void UserControl_FadeOutHightlight(object sender, object e)
        {
            Highlight.BeginAnimation(OpacityProperty, To0);
            IconScale.BeginAnimation(ScaleTransform.ScaleXProperty, To1);
            IconScale.BeginAnimation(ScaleTransform.ScaleYProperty, To1);
        }
        private void UserControl_FadeOutHightlightSlight(object sender, object e)
        {
            Highlight.BeginAnimation(OpacityProperty, To0_5);
            IconScale.BeginAnimation(ScaleTransform.ScaleXProperty, To0_9);
            IconScale.BeginAnimation(ScaleTransform.ScaleYProperty, To0_9);
        }
        private void UserControl_FadeInHightlightSlight(object sender, object e)
        {
            Highlight.BeginAnimation(OpacityProperty, To1);
            IconScale.BeginAnimation(ScaleTransform.ScaleXProperty, To1);
            IconScale.BeginAnimation(ScaleTransform.ScaleYProperty, To1);
        }
    }
}
