using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HassBotDTOs;
using HassBotUtils;

namespace HassBotData
{
    public sealed class AFKManager
    {

        private static List<AFKDTO> _afk = null;
        private static string _afkUsersFile = string.Empty;
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly AFKManager TheAFKManager = new AFKManager();

        private AFKManager()
        {
            if (null == _afk)
                LoadAFKUsers();
        }

        private void LoadAFKUsers()
        {
            try
            {
                _afkUsersFile = AppSettingsUtil.AppSettingsString("afkUsersFile", true, string.Empty);

                if (Persistence.FileExists(_afkUsersFile))
                {
                    _afk = Persistence.LoadAFKUsers(_afkUsersFile);
                }
                else
                {
                    _afk = new List<AFKDTO>();
                    SaveAFKUsers();
                }
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_AFK_FILE, e);
            }
        }

        private void SaveAFKUsers()
        {
            Persistence.SaveAFKUsers(_afk, _afkUsersFile);
        }

        public List<AFKDTO> AKFUsers()
        {
            if (_afk == null)
                return null;
            return _afk;
        }

        public void RemoveAFKUserById(ulong id)
        {
            if (_afk == null)
                return;

            AFKDTO retAFK = _afk.Find(delegate (AFKDTO dto) {
                return dto.Id == id;
            });

            if (null != retAFK)
            {
                _afk.Remove(retAFK);
                SaveAFKUsers();
            }
        }

        public AFKDTO GetAFKById(ulong Id)
        {
            if (_afk == null)
                return null;

            AFKDTO retAFK = _afk.Find(delegate (AFKDTO dto) {
                return dto.Id == Id;
            });
            return retAFK;
        }

        public AFKDTO GetAFKByName(string name)
        {
            if (_afk == null)
                return null;

            AFKDTO retAFK = _afk.Find(delegate (AFKDTO dto) {
                return dto.AwayUser.Trim().ToLower() == name.Trim().ToLower();
            }
            );
            return retAFK;
        }

        public void UpdateAFK(AFKDTO afk)
        {
            if (null != afk)
                _afk.Remove(afk);

            AFKDTO newAFK = new AFKDTO();
            newAFK.Id = afk.Id;
            newAFK.AwayUser = afk.AwayUser;
            newAFK.AwayTime = afk.AwayTime;
            newAFK.AwayMessage = afk.AwayMessage;

            _afk.Add(newAFK);
            SaveAFKUsers();
        }
    }
}