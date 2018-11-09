using HassBotDTOs;
using HassBotUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HassBotData
{
    public class SubscriptionManager
    {

        private static List<SubscribeDTO> _subscriptions = null;
        private static string _subscriptionsUsersFile = string.Empty;
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly SubscriptionManager TheSubscriptionManager = new SubscriptionManager();

        private SubscriptionManager()
        {
            if (null == _subscriptions)
                LoadSubscriptions();
        }

        private void LoadSubscriptions()
        {
            try
            {
                _subscriptionsUsersFile = AppSettingsUtil.AppSettingsString("subscriptionsFile", true, string.Empty);

                if (Persistence.FileExists(_subscriptionsUsersFile))
                {
                    _subscriptions = Persistence.LoadSubscriptions(_subscriptionsUsersFile);
                }
                else
                {
                    _subscriptions = new List<SubscribeDTO>();
                    SaveSubscriptions();
                }
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_SUBSCRIPTION_FILE, e);
            }
        }

        private void SaveSubscriptions()
        {
            Persistence.SaveSubscriptions(_subscriptions, _subscriptionsUsersFile);
        }

        public List<SubscribeDTO> AllSubscriptions()
        {
            if (_subscriptions == null)
                return null;
            return _subscriptions;
        }

        public void RemoveSubscriptionsByUser(string user)
        {
            if (_subscriptions == null)
                return;

            SubscribeDTO retSub = GetSubscriptionByUser(user);

            if (null != retSub)
            {
                _subscriptions.Remove(retSub);
                SaveSubscriptions();
            }
        }

        public SubscribeDTO GetSubscriptionByUser(string user)
        {
            if (_subscriptions == null)
                return null;

            SubscribeDTO retSub = _subscriptions.Find(delegate (SubscribeDTO dto) {
                return dto.User == user;
            });
            return retSub;
        }

        public List<string> GetSubscriptionTagsByUser(string user)
        {
            if (_subscriptions == null)
                return null;

            SubscribeDTO retSub = _subscriptions.Find(delegate (SubscribeDTO dto) {
                return dto.User == user;
            });
            return retSub.Tags;
        }

        public void UpdateSubscription(SubscribeDTO sub)
        {
            if (null != sub)
                _subscriptions.Remove(sub);

            SubscribeDTO newSub = new SubscribeDTO();
            newSub.Id = sub.Id;
            newSub.User = sub.User;
            newSub.Tags = sub.Tags;

            _subscriptions.Add(newSub);
            SaveSubscriptions();
        }

        public List<string> GetDistinctTags()
        {
            List<string> allTags = new List<string>(64);

            foreach (SubscribeDTO dto in _subscriptions)
                allTags.AddRange(dto.Tags);

            return allTags.Distinct().ToList();
        }

        public List<SubscribeDTO> GetSubscribersByTag(string tag)
        {
            List<SubscribeDTO> subscribers = new List<SubscribeDTO>(64);

            foreach (SubscribeDTO dto in _subscriptions)
                if (dto.Tags.Contains(tag))
                    subscribers.Add(dto);

            return subscribers;
        }
    }
}