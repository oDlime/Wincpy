using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wincpy.Helpers;
public class CMDHelper
{
    public static string CMDRunner(string str)
    {
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        p.StandardInput.WriteLine(str + "&exit");
        p.StandardInput.AutoFlush = true;
        p.StandardInput.WriteLine("exit");
        string output = p.StandardOutput.ReadToEnd();
        StreamReader reader = p.StandardOutput;
        string line = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            str += line + "  ";
            line = reader.ReadLine();
        }
        p.WaitForExit();
        p.Close();
        return output;
    }
    public static string CMDRunner2(string strInput)
    {
        Process CmdProcess = new Process();
        CmdProcess.StartInfo.FileName = "cmd.exe";
        CmdProcess.StartInfo.CreateNoWindow=true;
        CmdProcess.StartInfo.UseShellExecute=false;
        CmdProcess.StartInfo.RedirectStandardInput = true;
        CmdProcess.StartInfo.RedirectStandardOutput = true;
        CmdProcess.StartInfo.RedirectStandardError = true;
        CmdProcess.StartInfo.Arguments = "/c"+strInput;
        CmdProcess.Start();
        CmdProcess.WaitForExit();
        string output = CmdProcess.StandardOutput.ReadToEnd();
        CmdProcess.Close();
        return output.Trim();
    }
}
