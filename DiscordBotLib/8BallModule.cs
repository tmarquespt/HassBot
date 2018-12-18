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
                "As If","Ask Me If I Care","Dumb Question Ask Another","Forget About It","Get A Clue","In Your Dreams",
                "Not A Chance","Obviously","Oh Please","Sure","That's Ridiculous!","Well Maybe","What Do You Think?",
                "Whatever","Who Cares?","Yeah And I'm The Pope","Yeah Right","You Wish","You've Got To Be Kidding...",
                "At Least I Love You","Nice Try!","Pure Genius!","That's O.K.","The Sky's The Limit","You're 100% Fun!",
                "As I See It Yes","Ask Again Later","Better Not Tell You Now","Cannot Predict Now",
                "Concentrate and Ask Again","Don't Count On It","It Is Certain","Most Likely","My Reply Is No",
                "My Sources Say No","Outlook Good","Outlook Not So Good","Reply Hazy Try Again","Signs Point to Yes",
                "Very Doubtful","Without A Doubt","Yes","Yes - Definitely","You May Rely On It","Absolutely",
                "Answer Unclear Ask Later","Cannot Foretell Now","Can't Say Now","Chances Aren't Good",
                "Consult Me Later","Don't Bet On It","Focus And Ask Again","Indications Say Yes","Looks Like Yes",
                "No","No Doubt About It","Positively","Prospect Good","So It Shall Be","The Stars Say No",
                "Unlikely","Very Likely","Yes","You Can Count On It" };

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