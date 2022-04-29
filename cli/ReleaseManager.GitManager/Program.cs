using ReleaseManager.GitManager.Handlers;
using System.CommandLine;


var from = new Argument<string>("from", "starting point: tag or branch");
var to = new Argument<string>("to", "ending point: tag or branch");

var changeLogCommand = new Command("--change-log", "show change log")
{
    from,
    to,
};

changeLogCommand.AddAlias("-cl");

changeLogCommand.SetHandler<string, string>(ChangeLogHandler.Exec, from, to);

var listReleaseBranchesCommand = new Command("--list-release-branches", "list branch release");
listReleaseBranchesCommand.AddAlias("-rb");

listReleaseBranchesCommand.SetHandler(ListReleaseBranchesHandler.Exec);

var nameOption = new Option<string?>(new string[] { "-n", "--name" }, "tag name");

//var autoIncrement = new Option<bool>(new string[] { "-i", "--increment" }, () => false, "increment previous tag");

var addTagCommand = new Command("--add-tag", "add a tag")
{
    nameOption,
    from,
    //autoIncrement,
};

addTagCommand.AddAlias("-at");

addTagCommand.SetHandler<string?, string>(AddTagHandler.Exec, nameOption, from);

var rootCommand = new RootCommand() { listReleaseBranchesCommand, changeLogCommand, addTagCommand };

return rootCommand.Invoke(args);