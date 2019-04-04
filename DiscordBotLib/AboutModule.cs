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
            embed.WithTitle(":cpha: Bem vindo! Eu sou o @CPHAbot \n");
            embed.WithColor(Helper.GetRandomColor());
            embed.AddInlineField("Ligado desde", $"{ GetUptime() }");
            embed.AddInlineField("Total de Utilizadores", $"{Context.Client.Guilds.Sum(g => g.Users.Count)}");
            embed.AddInlineField("Tamanho", $"{GetHeapSize()} MiB");
            embed.AddInlineField("Memória", $"{ GetMemoryUsage() }");
            embed.AddInlineField("Versão do DiscordLib", $"{ GetLibrary() }");
            embed.AddInlineField("Latência", $" { GetLatency() }");

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
