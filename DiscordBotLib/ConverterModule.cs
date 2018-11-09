///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AboutModule.cs
//  DESCRIPTION     : A class that implements ~yaml2json & ~json2yaml commands
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace DiscordBotLib
{
    public class ConverterModule : BaseModule {

        [Command("yaml2json")]
        public async Task Yaml2Json() {
            await base.DisplayUsage(Constants.USAGE_YAML2JSON);
        }

        [Command("json2yaml")]
        public async Task Json2Yaml() {
            await base.DisplayUsage(Constants.USAGE_JSON2YAML);
        }

        [Command("yaml2json")]
        public async Task Yaml2Json([Remainder]string cmd) {
            string json = HassBotUtils.Utils.Yaml2Json(cmd);
            await ReplyAsync("```json\n" + json + "\n```\n");
        }

        [Command("json2yaml")]
        public async Task Json2Yaml([Remainder]string cmd) {
            string yaml = HassBotUtils.Utils.Json2Yaml(cmd);
            await ReplyAsync("```yaml\n" + yaml + "\n```\n");
        }
    }
}