using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UiPreparation
{
    internal static class Program
    {
        private static void Main()
        {
            var path = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
            var exePath = Path.Combine(path, "UI");
            var deletePath = exePath + @"\node_modules\selenium-webdriver\lib\test\data";
            var bld = new StringBuilder();

            bld.Append("npm install -g @angular/cli@latest&");
            bld.Append("npm install&");
            bld.Append("npm install popper.js --save&");
            bld.Append("code .&");
            bld.Append($"RD /S /Q {deletePath} &");
            bld.Append("npm run start&");

            var cmd = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    WorkingDirectory = exePath,
                    Arguments = @"/c " + bld
                }
            };
            cmd.Start();
            Console.ReadLine();
            cmd.WaitForExit();
        }
    }
}