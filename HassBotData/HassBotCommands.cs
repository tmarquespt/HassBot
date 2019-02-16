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
        public static HassBotCommands Instance
        {
            get { return lazy.Value; }
        }

        public static void ReloadData()
        {
            try
            {
                if (null != _commands)
                    _commands.Clear();

                _commands = Persistence.LoadCommands();
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_COMMANDS_FILE, e);
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