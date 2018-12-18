///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Ping.cs
//  DESCRIPTION     : A class that implements ping/pong commands
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class PingModule : BaseModule {

        [Command("ping"), Alias("pong")]
        public async Task PingAsync() {
            string response = string.Empty;
            string request = Context.Message.Content.ToLower();
            request = request.Replace("~", string.Empty).Replace(".", string.Empty);

            if (request == "ping")
                response = "PONG!";
            else if (request == "pong")
                response = "PING!!!";
            if (string.Empty == response)
                return;

            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_PING_PONG);
            embed.WithColor(Color.DarkRed);
            embed.AddField(request + "?", response);
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}