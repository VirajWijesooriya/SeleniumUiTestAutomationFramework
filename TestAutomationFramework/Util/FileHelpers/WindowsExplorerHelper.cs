using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.FileHelpers
{
    class WindowsExplorerHelper
    {
        internal static string GetExecutionFolderPath()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        internal static void CreateFolder(string folderName)
        {
            var currentPath = GetExecutionFolderPath();
            if (!Directory.Exists(Path.GetDirectoryName(currentPath + $"\\{folderName}\\")))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(currentPath) + $"\\{folderName}\\");
            }
        }
    }
}
