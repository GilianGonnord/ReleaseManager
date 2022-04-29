using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReleaseManager.GitManager.Handlers;

internal class AddTagHandler
{
    public static void Exec(string? name, string from)
    {
        var gitProcessor = new GitProccessor();

        gitProcessor.RunCommand("fetch");

        if (string.IsNullOrEmpty(name))
        {
            var autoName = GetAutomaticName(gitProcessor, from);
            Console.WriteLine($"Enter a tag name (default: {autoName})");
            var manualName = Console.ReadLine();
            name = !string.IsNullOrEmpty(manualName) ? manualName : autoName;
        }

        Console.WriteLine($"Add tag {name} from {from}");

        var currentBranch = gitProcessor.RunCommand("rev-parse --abbrev-ref HEAD");
        Console.WriteLine($"Current branch : {currentBranch}");

        if (StrEqual(currentBranch, from))
        {
            Console.WriteLine($"Already on branch {from}");
        } else
        {
            gitProcessor.RunCommand($"checkout {from}");
        }

        gitProcessor.RunCommand($"pull");

        gitProcessor.RunCommand($"tag {name}");

        gitProcessor.RunCommand($"push origin {name}");
    }

    private static string GetAutomaticName(GitProccessor gitProcessor, string from)
    {
        Console.WriteLine($"no name provided, calculated a new one");

        var releaseRegex = new Regex(@"release-(\d+.\d+.\d+)");
        var match = releaseRegex.Match(from);
        if (!match.Success)
        {
            throw new Exception("eacfea");
        }

        var baseTag = match.Groups[1].Value;

        Console.WriteLine($"base tag : {baseTag}");

        var existingTags = gitProcessor.RunCommand($"tag --merge {from} --sort=-creatordate")
            .Split('\n')
            .Select(s => s.Trim())
            .Where(t => t.Contains(baseTag))
            .OrderByDescending(t => t);

        var mostRecentTag = existingTags.FirstOrDefault();

        if (mostRecentTag == null)
        {
            var newTag = $"{baseTag}a";
            Console.WriteLine($"no already existing tag");
            return newTag;
        }

        Console.WriteLine($"last tag was : {mostRecentTag}");

        var lastChar = mostRecentTag[mostRecentTag.Length - 1];

        var newLetter = char.ConvertFromUtf32(lastChar + 1);

        var incrementedTag = baseTag + newLetter;

        return incrementedTag;
    }

    private static bool StrEqual(string a, string b)
    {
        string normalizedA = Regex.Replace(a, @"\s", "");
        string normalizedB = Regex.Replace(b, @"\s", "");

        return string.Equals(normalizedA, normalizedB, StringComparison.OrdinalIgnoreCase);
    }
}
