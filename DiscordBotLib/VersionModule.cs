///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AboutModule.cs
//  DESCRIPTION     : A class that implements ~ha and ~hassio commands
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;
using HassBotDTOs;
using Newtonsoft.Json;
using System.Reflection;

namespace DiscordBotLib
{
    public class VersionModule : BaseModule {

        private static readonly string HASSIO_BETA =
            "https://s3.amazonaws.com/hassio-version/beta.json";
        private static readonly string HASSIO_STABLE =
            "https://s3.amazonaws.com/hassio-version/stable.json";
        private static readonly string HA_STABLE =
            "https://api.github.com/repos/home-assistant/home-assistant/releases";
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Command("hassio")]
        public async Task About() {
            await GetHASSIOVersion();
        }

        [Command("hassio")]
        public async Task About([Remainder]string cmd) {
            await GetHASSIOVersion();
        }

        [Command("ha")]
        public async Task HAVersion() {
            await GetHAVersion();
        }

        [Command("ha")]
        public async Task HAVersion([Remainder]string cmd) {
            await GetHAVersion();
        }

        private async Task GetHAVersion() {
            HomeAssistantVersion ha = GetHAVersions();

            var embed = new EmbedBuilder();
            embed.WithTitle("Here are the current Home Assistant software versions.\n");
            embed.WithColor(Helper.GetRandomColor());

            if (null != ha) {
                // embed.AddInlineField("As of", DateTime.Now.ToShortDateString());
                embed.AddInlineField("Stable", ha.Stable);
                embed.AddInlineField("Beta", ha.Beta);
            }

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            if (string.Empty != mentionedUsers)
                embed.AddInlineField("FYI", mentionedUsers);

            await ReplyAsync(string.Empty, false, embed);
        }

        private async Task GetHASSIOVersion() {
            HassIOVersion beta = GetHassIOVersion(HassioRelease.Beta);
            HassIOVersion stable = GetHassIOVersion(HassioRelease.Stable);

            var embed = new EmbedBuilder();
            embed.WithTitle("Aqui estão as versões atuais do software Home Assistant.\n");
            embed.WithColor(Helper.GetRandomColor());

            if (null != stable) {
                embed.AddInlineField("Estável", stable.HassOS);
                embed.AddInlineField("Estável Supervisor", stable.Supervisor);
                embed.AddInlineField("Estável Home Assistant", stable.HomeAssistant);
            }

            if (null != beta) {
                embed.AddInlineField("Beta", beta.HassOS);
                embed.AddInlineField("Beta Supervisor", beta.Supervisor);
                embed.AddInlineField("Beta Home Assistant", beta.HomeAssistant);
            }

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            if (string.Empty != mentionedUsers)
                embed.AddInlineField("FYI", mentionedUsers);

            await ReplyAsync(string.Empty, false, embed);
        }

        public static HassIOVersion GetHassIOVersion(HassioRelease release) {
            string url = (release == HassioRelease.Beta) ? HASSIO_BETA : HASSIO_STABLE;

            string json = HassBotUtils.Utils.DownloadURLString(url);
            if (json == string.Empty) {
                logger.Error("Empty data received when downloading " + url);
                return null;
            }

            HassIOVersion version = new HassIOVersion();
            try {
                dynamic entries = JsonConvert.DeserializeObject(json);
                version.HomeAssistant = entries.homeassistant["default"];
                version.HassOS = entries.supervisor;
                version.Supervisor = entries.hassos.rpi3;
            }
            catch (Exception e) {
                logger.Error(e);
            }
            return version;
        }

        public static HomeAssistantVersion GetHAVersions() {
            string json = HassBotUtils.Utils.DownloadURLString(HA_STABLE);
            if (json == string.Empty) {
                logger.Error("Empty data received when downloading " + HA_STABLE);
                return null;
            }

            HomeAssistantVersion ha = new HomeAssistantVersion();

            try {
                dynamic entries = JsonConvert.DeserializeObject(json);
                foreach (dynamic item in entries) {
                    if (item.prerelease == false && item.draft == false) {
                        ha.Stable = item.name;
                        break;
                    }
                }
                foreach (dynamic item in entries) {
                    if (item.prerelease == true && item.draft == false) {
                        ha.Beta = item.name;
                        break;
                    }
                }
            }
            catch (Exception e) {
                logger.Error(e);
            }

            return ha;
        }
    }
}
