///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 12/22/2018
//  FILE            : ViolationModule.cs
//  DESCRIPTION     : A class that implements ~violation command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using HassBotData;

namespace DiscordBotLib
{
    public class ViolationModule : BaseModule
    {

        [Command("violation")]
        public async Task ViolationAsync()
        {
            if (!await VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_VIOLATION);
        }

        [Command("violation")]
        public async Task ViolationAsync([Remainder]string cmd)
        {
            if (!await VerifyMod(Context))
                return;

            string mentionedUsers = base.MentionedUsers();
            string[] users = mentionedUsers.Split(' ');
            foreach ( string usr in users)
            {
                var charsToRemove = new string[] { "@", "!", "<", ">" };
                string u = usr.Trim();
                if (string.Empty == u)
                    continue;
                else
                {
                    foreach (var c in charsToRemove)
                    {
                        u = u.Replace(c, string.Empty);
                    }
                }

                bool result = ViolationsManager.TheViolationsManager.ClearViolationsForUser(ulong.Parse(u));
                if (result)
                {
                    await ReplyAsync("The user " + usr + " has been taken out of the naughty list!", false, null);
                }
                else
                {
                    await ReplyAsync("The user " + usr + " is not in the naughty list!", false, null);
                }
            }
        }
    }
}