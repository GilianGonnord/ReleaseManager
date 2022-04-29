using System.Diagnostics;
using System.Text;

namespace ReleaseManager.Api.GitPilot;

public class GitProccessor
{
    private ProcessStartInfo GetGitInfo(string gitExePath, string workingDir)
    {
        var gitInfo = new ProcessStartInfo
        {
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            FileName = gitExePath,
            WorkingDirectory = workingDir,
        };

        return gitInfo;
    }

    public delegate void HandlerEvent(string data);

    public string RunCommand(string gitExePath, string workingDir, string command, HandlerEvent handlerEvent)
    {
        var errors = new StringBuilder();
        var output = new StringBuilder();

        var gitProcess = new Process();

        var gitInfo = GetGitInfo(gitExePath, workingDir);
        gitInfo.Arguments = command;

        gitProcess.StartInfo = gitInfo;
        handlerEvent.Invoke($"Running command : {command}");

        gitProcess.ErrorDataReceived += (object d, DataReceivedEventArgs a) => { if (!string.IsNullOrEmpty(a.Data)) handlerEvent.Invoke(a.Data); };

        gitProcess.OutputDataReceived += (object d, DataReceivedEventArgs a) => { if (!string.IsNullOrEmpty(a.Data)) handlerEvent.Invoke(a.Data); };

        gitProcess.Start();

        gitProcess.BeginErrorReadLine();
        gitProcess.BeginOutputReadLine();

        gitProcess.WaitForExit();

        var exitCode = gitProcess.ExitCode;

        handlerEvent.Invoke($"Command terminated with code {exitCode}");

        gitProcess.Close();

        if (exitCode > 0)
        {
            throw new Exception("Process failed, see log ahead");
        }

        if (errors.Length > 0)
        {
            handlerEvent.Invoke(errors.ToString());
        }

        return output.ToString();
    }

    public string RunCommand(string gitExePath, string workingDir, string command)
    {
        var errors = new StringBuilder();
        var output = new StringBuilder();

        var gitProcess = new Process();

        var gitInfo = GetGitInfo(gitExePath, workingDir);
        gitInfo.Arguments = command;

        gitProcess.StartInfo = gitInfo;

        gitProcess.OutputDataReceived += (s, d) => output.AppendLine(d.Data);
        gitProcess.ErrorDataReceived += (s, d) => errors.AppendLine(d.Data);

        gitProcess.Start();

        gitProcess.BeginErrorReadLine();
        gitProcess.BeginOutputReadLine();

        gitProcess.WaitForExit();

        var exitCode = gitProcess.ExitCode;

        gitProcess.Close();

        if (exitCode > 0)
        {
            Console.WriteLine(errors.ToString());
            throw new Exception("Process failed, see log ahead");
        }

        return output.ToString();
    }
}
