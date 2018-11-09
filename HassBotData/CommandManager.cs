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
    public sealed class CommandManager
    {

        private static List<CommandDTO> _commands = null;
        private static string _commandsFile = string.Empty;
        public static readonly CommandManager TheCommandManager = new CommandManager();
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private CommandManager()
        {
            if (null == _commands)
                LoadCommands();
        }

        private void LoadCommands()
        {
            try
            {

                _commandsFile = AppSettingsUtil.AppSettingsString("commandsFile", true, string.Empty);

                if (Persistence.FileExists(_commandsFile))
                {
                    _commands = Persistence.LoadCommands(_commandsFile);
                }
                else
                {
                    _commands = new List<CommandDTO>();
                    SaveCommands();
                }
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_COMMANDS_FILE, e);
            }
        }

        private void SaveCommands()
        {
            Persistence.SaveCommands(_commands, _commandsFile);
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

        public void RemoveCommandByName(string name)
        {
            if (_commands == null)
                return;

            CommandDTO retCommand = _commands.Find(delegate (CommandDTO dto) {
                return dto.CommandName.Trim().ToLower() == name.Trim().ToLower();
            }
            );
            _commands.Remove(retCommand);
            SaveCommands();
        }

        public CommandDTO GetCommandByName(string name)
        {
            if (_commands == null)
                return null;

            CommandDTO retCommand = _commands.Find(delegate (CommandDTO dto) {
                return dto.CommandName.Trim().ToLower() == name.Trim().ToLower();
            }
            );
            return retCommand;
        }

        public void UpdateCommand(CommandDTO cmd)
        {
            if (null != cmd)
                _commands.Remove(cmd);

            CommandDTO newCmd = new CommandDTO();
            newCmd.CommandAuthor = cmd.CommandAuthor;
            newCmd.CommandCreatedDate = DateTime.Now;
            newCmd.CommandData = cmd.CommandData;
            newCmd.CommandName = cmd.CommandName;
            newCmd.CommandCount = cmd.CommandCount;

            _commands.Add(newCmd);
            SaveCommands();
        }
    }
}