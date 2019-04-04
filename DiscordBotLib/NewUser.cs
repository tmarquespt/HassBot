using Discord.WebSocket;
using HassBotUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class NewUser
    {

        private static readonly string FUNFACT_URL =
            "http://api.icndb.com/jokes/random?firstName={0}&lastName=&limitTo=[nerdy]";

        private static readonly string POST_DATA =
            @"{""object"":{""name"":""Name""}}";

        public static async Task NewUserJoined(SocketGuildUser user)
        {
            StringBuilder sb = new StringBuilder();

            GetWelcomeMessage(sb, user);

            // Send a Direct Message to the new Users with instructions
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(sb.ToString());
        }

        private static void GetWelcomeMessage(StringBuilder sb, SocketGuildUser user)
        {
            sb.Append($"Olá, {user.Mention}! Bem vindo ao servidor de Discord da Comunidade Portuguesa de Home Assistant.\n\n");
            sb.Append("Por favor, lê com atenção o canal **#regras-de-utilização** antes de começar a utilizar os nossos canais e obter ajuda.\n\n");

            sb.Append(string.Format("Mais uma vez, bem vindo ao canal {0}!\n\n", user.Guild.Name));
        }

        private static string GetRandomFunFact(string userHandle)
        {
            string url = string.Format(FUNFACT_URL, userHandle);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = POST_DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(POST_DATA);
            requestWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                responseReader.Close();
                dynamic stuff = JObject.Parse(response);
                return stuff.value.joke;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
