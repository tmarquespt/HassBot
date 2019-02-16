///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 10/12/2018
//  FILE            : SubscriptionsModule.cs
//  DESCRIPTION     : A class that implements ~subscribe and ~unsunbscribe 
//                    commands. When subscribed, @HassBot sends a DM everytime
//                    there is a matching tag found in the PRs
//                  
//                    This command is especially useful to keep an eye on 
//                    interested component's development.
//
//                    For ex: the command "~subscribe nest" will notify you
//                    everytime someone mentions "nest" in the Pull Request.
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
    public class SubscriptionsModule : BaseModule {

        [Command("subscribe")]
        public async Task SubscribeAsync() {
            if (! await Helper.VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_SUBSCRIBE);
        }

        [Alias("unsubscribe")]
        public async Task UnSubscribeAsync() {
            if (!await Helper.VerifyMod(Context))
                return;

            await base.DisplayUsage(Constants.USAGE_UNSUBSCRIBE);
        }

        [Command("subscribe")]
        public async Task SubscribeAsync([Remainder]string tag) {
            if (!await Helper.VerifyMod(Context))
                return;

            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_GO);
            embed.WithColor(Helper.GetRandomColor());

            string msg = string.Empty;
            string userName = Context.User.Username;
            SubscribeDTO subDTO = SubscriptionManager.TheSubscriptionManager.GetSubscriptionByUser(userName);
            if (tag.ToLower() == "list") {
                if (subDTO == null) {
                    embed.AddField(Constants.TITLE_SUBSCRIBE, Constants.ERROR_NO_SUBSCRIPTIONS);
                }
                else {
                    string list = GetSubscriptionsList(subDTO.Tags);
                    embed.AddField(Constants.TITLE_SUBSCRIBE, 
                                    string.Format(Constants.INFO_CURRENT_SUBSCRIPTIONS, list));
                }
                await ReplyAsync(string.Empty, false, embed);
                return;
            }

            // if there are no subscriptions for the user, add one!
            if (subDTO == null) {
                List<string> tags = new List<string>();
                tags.Add(tag.ToLower());

                subDTO = new SubscribeDTO() {
                    Id = Context.User.Id,
                    Tags = tags,
                    User = userName
                };
            }
            else {
                // there are already subscriptions, lets not add duplicates
                if (!subDTO.Tags.Contains(tag)) {
                    subDTO.Tags.Add(tag.ToLower());
                    embed.AddField(Constants.TITLE_SUBSCRIBE, 
                                    string.Format(Constants.INFO_SUBSCRIPTION_SUCCESS, tag.ToLower()));
                    await ReplyAsync(string.Empty, false, embed);
                }
                else {
                    // acknowledge that the subscription already exists
                    embed.AddField(Constants.TITLE_SUBSCRIBE, 
                                    string.Format(Constants.INFO_TAG_EXISTS, tag.ToLower()));
                    await ReplyAsync(string.Empty, false, embed);
                }
            }

            // finally, save any changes!
            SubscriptionManager.TheSubscriptionManager.UpdateSubscription(subDTO);
        }

        [Command("unsubscribe")]
        public async Task UnsubscribeAsync([Remainder]string tag) {
            if (!await Helper.VerifyMod(Context))
                return;

            var embed = new EmbedBuilder();
            embed.WithTitle("🚩");
            embed.WithColor(Helper.GetRandomColor());

            string userName = Context.User.Username;
            SubscribeDTO subDTO = SubscriptionManager.TheSubscriptionManager.GetSubscriptionByUser(userName);
            if (null == subDTO) {
                // nothing to unsubscribe... no record of any previous subscription activity
                embed.AddField(Constants.TITLE_UNSUBSCRIBE, Constants.ERROR_NO_SUBSCRIPTIONS);
                await ReplyAsync(string.Empty, false, embed);
            }
            else {
                if (tag.ToLower() == "all") {
                    subDTO.Tags.Clear();
                    embed.AddField(Constants.TITLE_UNSUBSCRIBE, Constants.INFO_UNSUBSCRIBE_ALL_SUCCESS);
                    await ReplyAsync(string.Empty, false, embed);
                }
                else {
                    if (subDTO.Tags.Contains(tag.ToLower())) {
                        subDTO.Tags.Remove(tag.ToLower());
                        embed.AddField(Constants.TITLE_UNSUBSCRIBE, 
                                        string.Format(Constants.INFO_UNSUBSCRIBE_SUCCESS, tag));
                        await ReplyAsync(string.Empty, false, embed);
                    }
                    else {
                        embed.AddField(Constants.TITLE_UNSUBSCRIBE, 
                                        string.Format(Constants.INFO_NOT_SUBSCRIBED, tag.ToLower()));
                        await ReplyAsync(string.Empty, false, embed);
                    }

                    // update subscription preferences
                    SubscriptionManager.TheSubscriptionManager.UpdateSubscription(subDTO);
                }
            }
        }

        private string GetSubscriptionsList(List<string> tags) {
            if (tags == null || tags.Count == 0)
                return "none!";

            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < tags.Count; i++) {
                if (i == 0) buffer.Append("[ ");
                buffer.Append(tags[i]);
                if (i + 1 == tags.Count)
                    buffer.Append(" ]");
                else
                    buffer.Append(", ");
            }

            return buffer.ToString();
        }
    }
}