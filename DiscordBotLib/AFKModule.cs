///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AwayFromKeyboard.cs
//  DESCRIPTION     : A class that implements ~away command
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

namespace DiscordBotLib
{
    public class AFKModule : BaseModule {

        [Command("afk"), Alias("away")]
        public async Task AFKAsync() {
            await base.DisplayUsage(Constants.USAGE_AFK);
        }

        [Command("seen")]
        public async Task SeenAsync() {
            await base.DisplayUsage(Constants.USAGE_SEEN);
        }

        [Command("afk"), Alias("away")]
        public async Task AFKAsync([Remainder]string afkMessage) {
            string userName = Context.User.Username;
            AFKDTO afkDTO = AFKManager.TheAFKManager.GetAFKByName(userName);
            if (afkDTO == null) {
                afkDTO = new AFKDTO() {
                    Id = Context.User.Id,
                    AwayMessage = afkMessage,
                    AwayTime = DateTime.Now,
                    AwayUser = userName
                };
            }
            AFKManager.TheAFKManager.UpdateAFK(afkDTO);
            await ReplyAsync(string.Format(Constants.AWAY_MESSAGE_FORMAT, 
                             userName, 
                             afkMessage));
        }

        [Command("seen")]
        public async Task SeenAsync(string afkMessage) {
            AFKDTO afkDTO = AFKManager.TheAFKManager.GetAFKByName(afkMessage);
            if (afkDTO == null)
                return;
            
            string msg = Constants.SEEN_MESSAGE_FORMAT;
            string awayFor = string.Empty;
            if ((DateTime.Now - afkDTO.AwayTime).Days > 0)
                awayFor += (DateTime.Now - afkDTO.AwayTime).Days.ToString() + "d ";
            if ((DateTime.Now - afkDTO.AwayTime).Hours > 0)
                awayFor += (DateTime.Now - afkDTO.AwayTime).Hours.ToString() + "h ";
            if ((DateTime.Now - afkDTO.AwayTime).Minutes > 0)
                awayFor += (DateTime.Now - afkDTO.AwayTime).Minutes.ToString() + "m ";
            if ((DateTime.Now - afkDTO.AwayTime).Seconds > 0)
                awayFor += (DateTime.Now - afkDTO.AwayTime).Seconds.ToString() + "s ";

            string message = string.Format(msg, afkDTO.AwayUser, awayFor, afkDTO.AwayMessage);
            await ReplyAsync(message);
        }
    }
}