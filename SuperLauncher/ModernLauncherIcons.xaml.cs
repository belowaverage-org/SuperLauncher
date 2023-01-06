using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuperLauncher
{
    public partial class ModernLauncherIcons : UserControl
    {
        private string rFilter;
        private Point MouseDownPoint;
        private ModernLauncherIcon MouseDownIcon;
        private bool IsMouseDown = false;
        private bool IsDragging = false;
        public string Filter
        {
            get
            {
                return rFilter;
            }
            set
            {
                rFilter = value;
                bool first = true;
                int invisibleCount = 0;
                foreach (ModernLauncherIcon icon in IconPanel.Children)
                {
                    if (icon.NameText.Text.ToLower().Contains(value.ToLower()))
                    {
                        icon.FilterFocus = first;
                        if (first) first = false;
                        icon.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        invisibleCount++;
                        icon.Visibility = Visibility.Collapsed;
                    }
                }
                if (invisibleCount == IconPanel.Children.Count)
                {
                    NoResults.Visibility = Visibility.Visible;
                }
                else
                {
                    NoResults.Visibility = Visibility.Collapsed;
                }
            }
        }
        public ModernLauncherIcons()
        {
            InitializeComponent();
            PopulateIcons();
        }
        public void PopulateIcons()
        {
            try
            {
                IconPanel.Children.Clear();
                foreach (string filePath in Settings.Default.FileList)
                {
                    if (!File.Exists(filePath)) continue;
                    ModernLauncherIcon mli = new(filePath);
                    IconPanel.Children.Add(mli);
                }
            } catch
            {
                IconPanel.Children.Add(new ModernLauncherIcon(@"C:\Windows\System32\cmd.exe"));
            }
        }
        private void CommitIconsToFile()
        {
            Settings.Default.FileList.Clear();
            foreach (ModernLauncherIcon mli in IconPanel.Children)
            {
                Settings.Default.FileList.Add(mli.FilePath);
            }
            Settings.Default.Save();
        }
        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsMouseDown = true;
                MouseDownPoint = e.GetPosition(this);
                MouseDownIcon = GetIconWithMouseOver();
            }
        }
        private void UserControl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (IsDragging) EndDrag();
                IsMouseDown = IsDragging = false;
            }
        }
        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point curPoint = e.GetPosition(this);
            if (IsDragging && e.MouseDevice.LeftButton == MouseButtonState.Released)
            {
                EndDrag();
                IsMouseDown = IsDragging = false;
            }
            if (!IsDragging && MouseDownIcon != null && IsMouseDown && OutOfBounds(MouseDownPoint, curPoint, 10))
            {
                BeginDrag();
                IsDragging = true;
            }
            if (IsDragging)
            {
                PerformDrag();
            }
        }
        private static bool OutOfBounds(Point StartPoint, Point EndPoint, int Distance)
        {
            if (Math.Abs(StartPoint.X - EndPoint.X) > Distance) return true;
            if (Math.Abs(StartPoint.Y - EndPoint.Y) > Distance) return true;
            return false;
        }
        private ModernLauncherIcon GetIconWithMouseOver()
        {
            foreach (ModernLauncherIcon icon in IconPanel.Children)
            {
                if (icon.IsMouseOver) return icon;
            }
            return null;
        }
        private void ShowAllIcons()
        {
            foreach (ModernLauncherIcon icon in IconPanel.Children) icon.Visibility = Visibility.Visible;
        }
        private void BeginDrag()
        {
            new ModernLauncherIconDragFloat(MouseDownIcon.LIcon.Source).Show();
            MouseDownIcon.Visibility = Visibility.Hidden;
        }
        private void PerformDrag()
        {
            ModernLauncherIcon mli = GetIconWithMouseOver();
            if (mli != null)
            {
                int index = IconPanel.Children.IndexOf(mli);
                IconPanel.Children.Remove(MouseDownIcon);
                IconPanel.Children.Insert(index, MouseDownIcon);
            }
        }
        private void EndDrag()
        {
            ShowAllIcons();
            CommitIconsToFile();
        }
    }
}