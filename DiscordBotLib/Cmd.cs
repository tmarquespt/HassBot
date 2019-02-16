///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : Magic8BallModule.cs
//  DESCRIPTION     : A class that implements ~Cmd command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

using HassBotData;
namespace DiscordBotLib
{
    public class CmdModule : BaseModule
    {
        [Command("command")]
        public async Task CmdAsync()
        {
            if (!await VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_COMMAND);
        }

        [Command("command")]
        public async Task CmdAsync([Remainder]string cmd)
        {
            if (!await VerifyMod(Context))
                return;

            var embed = new EmbedBuilder();
            if (cmd.ToLower() == "refresh")
            {
                HassBotCommands.ReloadData();
                Sitemap.ReloadData();
                BlockedDomains.ReloadData();

                embed.WithTitle(Constants.EMOJI_INFORMATION);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField("Success!", "Commands, Sitemap and Blocked domains are reloaded and ready to go!");
                await ReplyAsync(string.Empty, false, embed);
                return;
            }
            else if (cmd.ToLower() == "list" || cmd.ToLower() == "add")
            {
                embed.WithTitle(Constants.EMOJI_INFORMATION);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField("Commands", "Manage commands here <https://github.com/awesome-automations/hassbot-data/blob/master/commands.json>");
                await ReplyAsync(string.Empty, false, embed);
                return;
            }
            else
            {
                string data = HassBotCommands.Instance.Lookup(cmd);
                if (string.Empty != data)
                {
                    // mention users if any
                    string mentionedUsers = base.MentionedUsers();
                    await ReplyAsync(mentionedUsers + data);
                }
            }
        }
    }
}