///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : FormatModule.cs
//  DESCRIPTION     : A class that implements ~format command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using System;

namespace DiscordBotLib
{
    public class FormatModule : BaseModule {
        [Command("format")]
        public async Task FormatAsync() {
            await FormatCommand();
        }

        [Command("format")]
        public async Task FormatAsync([Remainder]string cmd) {
            await FormatCommand();
        }

        private async Task FormatCommand() {
            StringBuilder sb = new StringBuilder();

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            if (mentionedUsers.Trim() != string.Empty )
                sb.Append(mentionedUsers + " ");

            sb.Append("Para formatar o teu texto como código, insira três backticks na primeira linha, pressione Enter para uma nova linha, cole seu código, pressione Enter novamente para outra nova linha e, por último, três outros backticks. Aqui está um exemplo:\n\n");
            sb.Append("\\`\\`\\`yaml\n");
            sb.Append("código aqui\n");
            sb.Append("\\`\\`\\`\n");
            sb.Append("Veja o gif animado aqui: <https://bit.ly/2GbfRJE>\n");
            sb.Append("** NÃO ** repita as mensagens. Por favor, edite a mensagem postada anteriormente, aqui está como -> <https://bit.ly/2qOOf1G>");

            await ReplyAsync(sb.ToString(), false, null);
        }
    }
}
