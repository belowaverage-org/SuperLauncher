using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SuperLauncherBootstrap
{
    public static class Bootstrap
    {
        public static string CopyPath = "C:\\Users\\Public\\below average\\Super Launcher\\";
        [STAThread]
        public static void Main(string[] args)
        {
            new SuperLauncherCommon.Splash().ShowDialog();
            return;
            Assembly assemblySelf = Assembly.GetExecutingAssembly();
            string rootPath = Path.GetDirectoryName(assemblySelf.Location);
            string superLauncherPath = Path.GetFullPath(Path.Combine(rootPath, "..\\SuperLauncher"));
            CopyFolder(superLauncherPath, CopyPath);
            Process.Start(Path.Combine(CopyPath, "SuperLauncher.exe"));
        }
        public static void CopyFolder(string Source, string Destination)
        {
            DirectoryInfo source = new DirectoryInfo(Source);
            FileInfo[] sourceFiles = source.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in sourceFiles)
            {
                string strippedFullPath = file.FullName.Replace(source.FullName, "");
                string destinationFullPath = Destination + strippedFullPath;
                string relativeDirectoryPath = file.DirectoryName.Replace(source.FullName, "");
                string destinationDirectoryPath = Destination + relativeDirectoryPath;
                if (!Directory.Exists(destinationDirectoryPath)) Directory.CreateDirectory(destinationDirectoryPath);
                file.CopyTo(destinationFullPath, true);
            }
        }
    }
}
