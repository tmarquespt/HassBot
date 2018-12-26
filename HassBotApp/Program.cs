using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotLib;

namespace HassBotApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize the log4net.
            log4net.Config.XmlConfigurator.Configure();

            // start the bot
            new DiscordBot().StartBotAsync().GetAwaiter().GetResult();
        }
    }
}