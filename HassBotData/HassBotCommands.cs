using HassBotDTOs;
using HassBotUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HassBotData
{
    public class HassBotCommands
    {
        private static readonly Lazy<HassBotCommands> lazy = new Lazy<HassBotCommands>(() => new HassBotCommands());
        private static List<CommandDTO> _commands = null;
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private HassBotCommands()
        {
            // private .ctor
        }

        static HassBotCommands()
        {
            ReloadData();
        }

        public static void ReloadData()
        {
            try
            {
                if (null != _commands)
                    _commands.Clear();

                string localPath = AppSettingsUtil.AppSettingsString("hassbotCommandPath", true, string.Empty);
                string remoteurl = AppSettingsUtil.AppSettingsString("hassbotCommandUrl", true, string.Empty);

                if (File.Exists(localPath))
                    File.Delete(localPath);

                Helper.DownloadFile(remoteurl, localPath);

                _commands = Persistence.LoadCommands(localPath);
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_COMMANDS_FILE, e);
            }
        }

        public static HassBotCommands Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string Lookup(string cmd)
        {
            CommandDTO retCommand = _commands.Find(delegate (CommandDTO dto)
            {
                return dto.CommandName.Trim().ToLower() == cmd.Trim().ToLower();
            });

            if (null != retCommand)
                return retCommand.CommandData;
            else
                return string.Empty;
        }

        public List<CommandDTO> Commands()
        {
            if (_commands == null)
                return null;
            return _commands;
        }

        public int CommandCount
        {
            get
            {
                if (_commands != null)
                    return _commands.Count;
                else
                    return 0;
            }
        }

        private CommandDTO GetCommandByName(string name)
        {
            if (_commands == null)
                return null;

            CommandDTO retCommand = _commands.Find(delegate (CommandDTO dto) {
                return dto.CommandName.Trim().ToLower() == name.Trim().ToLower();
            }
            );
            return retCommand;
        }
    }
}