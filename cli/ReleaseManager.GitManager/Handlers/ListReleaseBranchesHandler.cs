using System.Text.RegularExpressions;

namespace ReleaseManager.GitManager.Handlers;

public class ListReleaseBranchesHandler
{
    public static void Exec()
    {
        var gitProcessor = new GitProccessor();

        gitProcessor.RunCommand("fetch");

        Console.WriteLine("List release branches");

        var ouput = gitProcessor.RunCommand("branch -r");
        var branches = ouput.Split('\n');

        var releaseRegex = @"origin/release-\d.\d.\d";

        var releaseBranches = branches.Where(b => Regex.IsMatch(b, releaseRegex));

        foreach (var branch in releaseBranches)
        {
            Console.WriteLine(branch.Trim(' '));
        }
    }
}
