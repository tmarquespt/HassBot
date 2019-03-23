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
            embed.WithTitle("Bem-vindo! Eu sou @CPHAbot \n");
            embed.AddField ("Se tiverem alguma ideias para o meu desenvolvimento contactem @Tiago \n");
            embed.WithColor(Helper.GetRandomColor());
            embed.AddInlineField("Ligado desde", $"{ GetUptime() }");
            embed.AddInlineField("Total de usuários", $"{Context.Client.Guilds.Sum(g => g.Users.Count)}");
            embed.AddInlineField("Heap Size", $"{GetHeapSize()} MiB");
            embed.AddInlineField("Memoria", $"{ GetMemoryUsage() }");
            embed.AddInlineField("Discord Bibloteca Versão", $"{ GetLibrary() }");
            embed.AddInlineField("Latencia", $" { GetLatency() }");
            embed.AddField ("GitHub", "Você pode encontrar o código fonte aqui https://github.com/skalavala/HassBot");

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
