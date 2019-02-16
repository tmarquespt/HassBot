///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 08/21/2018
//  FILE            : AsModule.cs
//  DESCRIPTION     : A class that implements ~as command
//                    Use this command to yell at someone "as" @hassbot :)
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;
using Discord.WebSocket;
using HassBotUtils;
using System.Reflection;

namespace DiscordBotLib
{

    public class AsModule : BaseModule {

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Command("as")]
        public async Task AsAsync() {
            if (!await Helper.VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_AS);
        }

        [Command("as")]
        private async Task AsCommand([Remainder]string cmd) {
            if (!await Helper.VerifyMod(Context))
                return;

            // delete the original message
            await Context.Message.DeleteAsync();
            string mentionedChannels = base.MentionedChannels();
            if (mentionedChannels != string.Empty) {
                cmd = cmd.Replace("<#" + mentionedChannels + ">", string.Empty);
                ulong id = ulong.Parse(mentionedChannels); ;
                var chnl = Context.Client.GetChannel(id) as ITextChannel;
                await chnl.SendMessageAsync(cmd, false, null);
                logger.Info(string.Format("From {0} in #{1} ==>: {2}", Context.User, chnl.Name, cmd));
            }
            else {
                logger.Info(string.Format("From {0} ==>: {1}", Context.User, cmd));
                await ReplyAsync(cmd, false, null);
            }
        }
    }
}