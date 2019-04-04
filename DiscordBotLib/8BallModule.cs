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
                "Sim... Não... Hummm... Talvez... Estou baralhado", "Não quero saber disso para nada", "Que pergunta parva!", "Esqueçe lá isso", "Só nos teus sonhos",
                "Querias que eu responde-se, não era?", "Nem sim nem não, antes pelo contrário", "Nem penses nisso!", "O que é que tu achas?", "Quero lá saber disso...",
                "Isso é só parvo", "Pois, pois. E eu sou o Pápa...", "Deves estar a brincar", "Pergunta-me depois, agora estou ocupado",
                "Tens muita piada! Já pensaste em fazer sitdown comedy? É que se fizeres standup comedy podes cansar as pernas à espera que te batam palmas...",
                "Sim!", "Claro, que sim!", "Sem duvida que sim!", "Sim e olha que eu nunca me engano e raramente tenho dúvidas", "Yep",
                "Não!", "Claro que não!", "Sem divida que não!", "Não e olha que eu nunca me engano e raramente tenho dúvidas", "Nope",
                "Só se não chover", "Não contes com isso", "Não te sei responder a isso", "É isso e couves...", "Não são horas para fazer perguntas dessas! Já viste as horas?!",
                "Hummm... Depois de consultar a Maya, o prof. Karamba e o Nuno Rogeiro posso afirmar com toda a certeza que a resposta à tua pergunta é... talvez",
                "Duvido muito" };

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
