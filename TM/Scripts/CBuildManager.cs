using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class CBuildManager
    {
        public void AutoLink(string source, string target, string linkExe)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.FileName = linkExe;
            p.Arguments = source + " " + target + " " + "true";
            Process pro = Process.Start(p);
        }

        public int AutoBuild(string projectPath)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.FileName = projectPath;
            Process pro = Process.Start(p);
            pro.WaitForExit();
            return pro.ExitCode;
        }
    }
}
