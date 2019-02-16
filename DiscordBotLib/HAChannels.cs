using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotUtils;

namespace DiscordBotLib
{
    public class HAChannels
    {
        public static ITextChannel ModChannel(SocketCommandContext context)
        {
            ulong modChannelId = (ulong)AppSettingsUtil.AppSettingsLong("modChannel", true, 330948240805462026);
            return context.Client.GetChannel(modChannelId) as ITextChannel;
        }

        public static ITextChannel ModLogChannel(SocketCommandContext context)
        {
            ulong modlogChannelId = (ulong)AppSettingsUtil.AppSettingsLong("modlogChannel", true, 473590680103419943);
            return context.Client.GetChannel(modlogChannelId) as ITextChannel;
        }

        public static ITextChannel BotSpamChannel(SocketCommandContext context)
        {
            ulong botspamChannelId = (ulong)AppSettingsUtil.AppSettingsLong("botspamChannel", true, 331106174722113548);
            return context.Client.GetChannel(botspamChannelId) as ITextChannel;
        }
        
        public static SocketGuild ServerGuild(SocketCommandContext context)
        {
            ulong serverGuild = (ulong)AppSettingsUtil.AppSettingsLong("serverGuild", true, 330944238910963714);
            return context.Client.GetGuild(serverGuild);
        }

        public static SocketUser GetUser(SocketCommandContext context, ulong userId)
        {
            var guild = ServerGuild(context);
            SocketUser user = guild.GetUser(userId);
            return user;
        }

        public static async Task<IDMChannel> GetUsersDMChannel(SocketCommandContext context, ulong userId)
        {
            var guild = ServerGuild(context);
            SocketUser user = guild.GetUser(userId);
            return await user.GetOrCreateDMChannelAsync();
        }
    }
}