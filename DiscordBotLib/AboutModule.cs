///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AboutModule.cs
//  DESCRIPTION     : A class that implements ~about command
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace DiscordBotLib
{
    public class AboutModule : BaseModule {
        private Process _process;

        public AboutModule() {
            _process = Process.GetCurrentProcess();
        }

        [Command("about")]
        public async Task About() {
            await AboutCommand();
        }

        [Command("about")]
        public async Task About([Remainder]string cmd) {
            await AboutCommand();
        }

        private async Task AboutCommand() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Hello! This is @HassBot, written by @skalavala \n");
            embed.WithColor(Helper.GetRandomColor());
            embed.AddInlineField("Up Since", $"{ GetUptime() }");
            embed.AddInlineField("Total Users", $"{Context.Client.Guilds.Sum(g => g.Users.Count)}");
            embed.AddInlineField("Heap Size", $"{GetHeapSize()} MiB");
            embed.AddInlineField("Memory", $"{ GetMemoryUsage() }");
            embed.AddInlineField("Discord Lib Version", $"{ GetLibrary() }");
            embed.AddInlineField("Latency", $" { GetLatency() }");
            embed.AddField("GitHub", "You can find the source code here https://github.com/skalavala/HassBot");

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            if (string.Empty != mentionedUsers)
                embed.AddInlineField("FYI", mentionedUsers);            

            await ReplyAsync(string.Empty, false, embed);
        }

        public string GetUptime() {
            var uptime = (DateTime.Now - _process.StartTime);
            return $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
        }

        private static string GetHeapSize()
            => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();

        public string GetLibrary()
            => $"Discord.Net ({DiscordConfig.Version})";

        public string GetMemoryUsage()
            => $"{Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2)}mb";

        public string GetLatency()
            => $"{Context.Client.Latency}ms";
    }
}