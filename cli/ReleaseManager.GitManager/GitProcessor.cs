using System.Diagnostics;
using System.Text;

namespace ReleaseManager.GitManager;

public class GitProccessor
{
    private ProcessStartInfo GetGitInfo()
    {
        var gitInfo = new ProcessStartInfo
        {
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            FileName = @"C:\Program Files\Git" + @"\bin\git.exe",
            WorkingDirectory = @"C:\Projects\GEODP\GEODPV2"
        };

        return gitInfo;
    }

    public string RunCommand(string command)
    {
        var errors = new StringBuilder();
        var output = new StringBuilder();

        var gitProcess = new Process();

        var gitInfo = GetGitInfo();
        gitInfo.Arguments = command;

        gitProcess.StartInfo = gitInfo;
        Console.WriteLine($"Running command : {command}");

        gitProcess.ErrorDataReceived += (s, d) =>
        {
            if (!string.IsNullOrEmpty(d.Data))
            {
                errors.AppendLine(d.Data);
            }
        };

        gitProcess.OutputDataReceived += (s, d) => output.AppendLine(d.Data);

        gitProcess.Start();

        gitProcess.BeginErrorReadLine();
        gitProcess.BeginOutputReadLine();

        gitProcess.WaitForExit();

        var exitCode = gitProcess.ExitCode;

        Console.WriteLine($"Command terminated with code {exitCode}");

        gitProcess.Close();

        if(exitCode > 0)
        {
            Console.WriteLine(errors.ToString());
            Console.WriteLine(output.ToString());
            throw new Exception("Process failed, see log ahead");
        }

        if (errors.Length > 0)
        {
            Console.WriteLine(errors.ToString());
        }

        return output.ToString();
    }
}

