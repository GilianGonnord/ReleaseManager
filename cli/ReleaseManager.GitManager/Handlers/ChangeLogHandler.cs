using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReleaseManager.GitManager.Handlers;

internal class ChangeLogHandler
{
    public static void Exec(string from, string to)
    {
        var gitProcessor = new GitProccessor();

        gitProcessor.RunCommand("fetch");

        Console.WriteLine("Change log from " + from + ", to " + to);

        var ouput = gitProcessor.RunCommand($"log --pretty=oneline --grep=Merge {from}..{to}");

        var logs = ouput.Split('\n').Where(s => !string.IsNullOrEmpty(s));

        var extractNumberRegex = @"[a-z0-9]{40} Merge branch '(hotfix|bug|feature|technical)?-?(\d{1,4})[a-z0-9\-\.]+'.*";

        var extractedNumbers = logs.Select(l =>
        {
            var match = Regex.Match(l, extractNumberRegex);

            if (match.Success)
                return match.Groups[2].Value;
            return l;
        }).Distinct();

        foreach (var extractedNumber in extractedNumbers)
        {
            Console.WriteLine(extractedNumber.Trim(' '));
        }

        Console.WriteLine("end");

        //var branches = ouput.Split('\n');

        //var releaseRegex = @"origin/release-\d.\d.\d";

        //var releaseBranches = branches.Where(b => Regex.IsMatch(b, releaseRegex));

        //foreach (var branch in releaseBranches)
        //{
        //    Console.WriteLine(branch.Trim(' '));
        //}
    }
}
