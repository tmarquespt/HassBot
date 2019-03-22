///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : Magic8BallModule.cs
//  DESCRIPTION     : A class that implements ~8ball command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class Magic8BallModule : BaseModule {

        private string previousPrediction = string.Empty;
        
        [Command("8ball")]
        public async Task Magic8BallAsync() {
            await base.DisplayUsage(Constants.USAGE_EIGHTBALL);
        }

        [Command("8ball")]
        public async Task Magic8BallAsync([Remainder]string cmd) {
            Random rnd = new Random();
            string[] predictions = {
                "Como se", "pergunte-me se eu me importo", "pergunta muda peça a outro", "esqueça-o", "obtenha uma pista", "nos teus sonhos",
                "Não é uma chance", "Obviamente", "Oh, por favor", "Claro", "Isso é ridículo!", "Bem, talvez", "O que você acha?",
                "Seja qual for", "Quem se importa?", "Sim e eu sou o Papa", "Yeah Right", "Você deseja", "Você so pode estar brincando ...",
                "Pelo menos eu te amo", "boa tentativa!", "Pure Genius!", "Isso é O.K.", "O limite do céu", "Você é 100% divertido!",
                "Como eu vejo sim", "Perguntar novamente depois", "Melhor não dizer agora", "Não é possível prever agora",
                "Concentre-se e pergunte novamente", "Não conte com isso", "É certo", "mais provável", "Minha resposta é não",
                "Minhas fontes dizem que não", "Outlook bom", "Outlook não é tão bom", "Resposta Hazy tente novamente", "sinais apontam para Sim",
                "Muito Duvidoso", "Sem Dúvida", "Sim", "Sim - Definitivamente", "Você Pode Confiar Nele", "Absolutamente",
                "Resposta não clara perguntar depois", "Não é possível prever agora", "Não posso dizer agora", "As chances não são boas",
                "Consultar-me depois", "Não apostar nele", "Focar e perguntar novamente", "Indicações dizer sim", "parece sim",
                "No", "No Doubt About It", "Positivamente", "Prospect Good", "So It Shall Be", "As estrelas dizem não",
                "Improvável", "muito provável", "sim", "você pode contar com isso" };

            var embed = new EmbedBuilder();
            embed.WithTitle(":8ball:");
            embed.WithColor(Helper.GetRandomColor());
            string prediction = string.Empty;

            // additional logic, so that the 8Ball prediction doesn't repeat
            while (true) {
                int r = rnd.Next(predictions.Count());
                prediction = predictions[r];
                if (previousPrediction != prediction)
                    break;
            }

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            // update previous prediction
            previousPrediction = prediction;
            if ( string.Empty == mentionedUsers)
                embed.AddField("8Ball Prediction:", prediction);
            else
                embed.AddField("8Ball Prediction:", mentionedUsers + prediction);
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}
