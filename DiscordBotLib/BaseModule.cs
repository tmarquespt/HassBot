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

        protected async Task<bool> VerifyMod(SocketCommandContext ctx)
        {
            if (!IsMod(ctx.User))
            {
                var embed = new EmbedBuilder()
                {
                    Title = Constants.EMOJI_STOPSIGN,
                    Color = Color.DarkRed,
                };
                embed.AddInlineField(Constants.ACCESS_DENIED,
                                     Constants.ACCESS_DENIED_MESSAGE);

                await ReplyAsync(string.Empty, false, embed);
                return false;
            }
            return true;
        }

        protected static bool IsMod(SocketUser user)
        {
            // get the list of mods from config file
            string mods = AppSettingsUtil.AppSettingsString("mods",
                                                             true,
                                                             string.Empty);
            string[] moderators = mods.Split(',');
            var results = Array.FindAll(moderators,
                                        s => s.Trim().Equals(user.Username,
                                        StringComparison.OrdinalIgnoreCase));
            if (results.Length == 1)
                return true;
            else
                return false;
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
