///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 03/15/2018
//  FILE            : Base64Module.cs
//  DESCRIPTION     : A class that implements ~base64_encode and ~base64_decode 
//                    commands
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBotLib
{
    public class Base64Module : BaseModule {

        [Command("base64_encode")]
        public async Task Base64EncodeAsync() {
            await base.DisplayUsage(Constants.USAGE_BASE64);
        }

        [Command("base64_decode")]
        public async Task Base64DecodeAsync() {
            await base.DisplayUsage(Constants.USAGE_BASE64);
        }

        [Command("base64_encode")]
        public async Task Base64EncodeAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":rosette:");
            embed.WithColor(Helper.GetRandomColor());
            string data = HassBotUtils.Utils.Base64Encode(cmd);
            embed.AddField("Base64 Encoded Value:", data);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("base64_decode")]
        public async Task Base64DecodeAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":rosette:");
            embed.WithColor(Helper.GetRandomColor());
            string data = HassBotUtils.Utils.Base64Decode(cmd);
            embed.AddField("Base64 Decoded Value:", data);

            await ReplyAsync(string.Empty, false, embed);
        }
    }
}