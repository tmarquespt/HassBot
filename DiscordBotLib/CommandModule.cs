///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : CommandModule.cs
//  DESCRIPTION     : A class that implements ~command command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HassBotData;
using HassBotDTOs;
using System;
using Discord.WebSocket;
using HassBotUtils;

namespace DiscordBotLib
{
    public class CommandModule : BaseModule {

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Command("command")]
        public async Task CommandAsync() {
            if (!await VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_COMMAND);
        }

        [Command("command")]
        public async Task CustomCommandAsync([Remainder]string cmd) {
            if (!await VerifyMod(Context))
                return;

            string command = cmd;
            string value = string.Empty;
            if (cmd.Length > 0) {
                int i = cmd.IndexOf(' ');
                if (i != -1) {
                    command = cmd.Substring(0, i);
                    value = cmd.Substring(i, cmd.Length - i);
                }

                value = (value != null) ? value.Trim() : string.Empty;
                if (value == string.Empty) {
                    // passed empty value - remove that command from list
                    CommandManager.TheCommandManager.RemoveCommandByName(command);
                    return;
                }

                CommandDTO cmdDTO = CommandManager.TheCommandManager.GetCommandByName(command);
                if (cmdDTO == null) {
                    cmdDTO = new CommandDTO();
                    cmdDTO.CommandCount = 0;
                    cmdDTO.CommandData = value;
                    cmdDTO.CommandCreatedDate = DateTime.Now;
                    cmdDTO.CommandName = command;
                }
                cmdDTO.CommandCount += 1;
                cmdDTO.CommandAuthor = Context.User.Mention;
                CommandManager.TheCommandManager.UpdateCommand(cmdDTO);

                var embed = new EmbedBuilder();
                embed.WithTitle(Constants.EMOJI_THUMBSUP);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField(Constants.SUCCESS, 
                    string.Format(Constants.COMMAND_SUCCESS_MESSAGE, command));
                await ReplyAsync(string.Empty, false, embed);
            }
        }

        [Command("list")]
        public async Task CommandListAsync() {
            if (!await VerifyMod(Context))
                return;

            StringBuilder sb = new StringBuilder(128);

            string commandTotal = string.Format(Constants.COMMAND_TOTAL,
                                                CommandManager.TheCommandManager.CommandCount.ToString());

            sb.Append(commandTotal);
            GetCommaSeparatedCommandList(sb);

            await ReplyAsync(sb.ToString());
        }

        private static void GetCommaSeparatedCommandList(StringBuilder buffer) {
            List<CommandDTO> cmds = CommandManager.TheCommandManager.Commands();

            if (buffer == null || cmds == null || cmds.Count == 0)
                return;

            for ( int i = 0; i < cmds.Count; i++ ) {
                if (i == 0) buffer.Append("[ ");
                buffer.Append(cmds[i].CommandName);
                if (i + 1 == cmds.Count)
                    buffer.Append(" ]");
                else
                    buffer.Append(", ");
            }
        }
    }
}