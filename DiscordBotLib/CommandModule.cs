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
    public class CommandModule : BaseModule
    {
        [Command("command")]
        public async Task CmdAsync()
        {
            if (!await Helper.VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_COMMAND);
        }

        [Command("update"), Alias("refresh")]
        public async Task UpdateAsync()
        {
            // refresh all data at once
            await Helper.RefreshData(Context);
        }

        [Command("command")]
        public async Task CmdAsync([Remainder]string cmd)
        {
            if (!await Helper.VerifyMod(Context))
                return;

            var embed = new EmbedBuilder();
            if (cmd.ToLower() == "refresh")
            {
                await Helper.RefreshData(Context);
            }
            else if (cmd.ToLower() == "list" || cmd.ToLower() == "add")
            {
                embed.WithTitle(Constants.EMOJI_INFORMATION);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField(Constants.TITLE_HASSBOT, Constants.COMMAND_MESSAGE);
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