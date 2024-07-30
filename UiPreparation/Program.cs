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
            #if OS_WINDOWS
              var path = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
              var exePath = Path.Combine(path, "UI");
              var deletePath = exePath + @"\node_modules\selenium-webdriver\lib\test\data";
              var bld = new StringBuilder();
              bld.Append("npm install -g @angular/cli@latest&"); // node version -> "v18.18.0"; npm version -> "9.8.1"
              bld.Append("npm install&");
              bld.Append("npm install popper.js --save&");
              // if you get code not found error, you should add path to env
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
            #else
              var path = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin"));
              var exePath = Path.Combine(path, "UI");
              var deletePath = Path.Combine(exePath, "node_modules", "selenium-webdriver", "lib", "test", "data");
              var bld = new StringBuilder();
              
              // it will ask password for permission
              bld.Append("sudo npm install -g @angular/cli@latest && "); // node version -> "v18.18.0"; npm version -> "9.8.1"
              bld.Append("npm install && ");
              bld.Append("npm install popper.js --save && ");
              // if you get code not found error, you should add path to env
              bld.Append("code . && ");
              bld.Append($"sudo rm -rf \"{deletePath}\" && ");
              bld.Append("npm run start");
              var cmd = new Process
              {
                  StartInfo =
                  {
                      UseShellExecute = true,
                      FileName = "/bin/bash",
                      WorkingDirectory = exePath,
                      Arguments = $"-c \"{bld}\""
                  }
              };
              cmd.Start();
              cmd.WaitForExit();
            #endif
        }
    }
}