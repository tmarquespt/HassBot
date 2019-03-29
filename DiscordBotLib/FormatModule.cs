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

            sb.Append("Para formatares o teu texto como código insere três acentos graves na primeira linha, seguido de 'yaml', pressiona Enter para uma nova linha, cola o teu código, pressiona Enter novamente para outra nova linha e, por último, outros três acentos graves. Aqui está um exemplo:\n\n");
            sb.Append("\\`\\`\\`yaml \n");
            sb.Append("código aqui\n");
            sb.Append("\\`\\`\\`\n");
            sb.Append("Vê o gif animado aqui: <https://bit.ly/2GbfRJE>\n");
            sb.Append(" ");
            sb.Append("** NÃO ** repitas as mensagens. Por favor, edita a mensagem publicada anteriormente. Para saberes como o fazer vê aqui: <http://tiny.cc/kk3k4y>");

            await ReplyAsync(sb.ToString(), false, null);
        }
    }
}
