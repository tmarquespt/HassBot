using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public abstract class BaseModule : ModuleBase<SocketCommandContext>
    {

        protected string MentionedUsers()
        {
            string mentionedUsers = string.Empty;
            foreach (var user in Context.Message.MentionedUsers)
            {
                mentionedUsers += $"{user.Mention} ";
            }

            return mentionedUsers;
        }

        protected string MentionedChannels()
        {
            string mentionedChannels = string.Empty;
            foreach (var channel in Context.Message.MentionedChannels)
            {
                mentionedChannels += $"{channel.Id} ";
            }

            return mentionedChannels.TrimEnd();
        }

        protected async Task DisplayUsage(string usageString)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_INFORMATION);
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField(Constants.USAGE_TITLE, usageString);
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}
