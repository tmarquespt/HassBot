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
    public class ViolationsManager
    {
        private static List<ViolatorDTO> _violations = null;
        private static string _violationsFile = string.Empty;

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly ViolationsManager TheViolationsManager = new ViolationsManager();

        private ViolationsManager()
        {
            if (null == _violations)
                LoadViolations();
        }

        private void LoadViolations()
        {
            try
            {
                _violationsFile = AppSettingsUtil.AppSettingsString("violationsFile", true, string.Empty);

                if (Persistence.FileExists(_violationsFile))
                {
                    _violations = Persistence.LoadViolations(_violationsFile);
                }
                else
                {
                    _violations = new List<ViolatorDTO>();
                    SaveViolations();
                }
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_VIOLATIONS_FILE, e);
            }
        }

        private void SaveViolations()
        {
            Persistence.SaveViolations(_violations, _violationsFile);
        }

        public List<Violation> GetIncidentsByUser(ulong id)
        {
            foreach (ViolatorDTO violation in _violations)
            {
                if (violation.ViolatorId == id)
                    return violation.Violations;
            }
            return null;
        }

        public void AddIncident(ulong id, string user, string violationDesc, string channel)
        {
            // it is not a violation if the message is a DM to @HassBot
            if (channel.Contains(user))
                return;

            Violation newIncident = new Violation();
            newIncident.ViolationChannel = channel;
            newIncident.ViolationDateTime = DateTime.Now;
            newIncident.ViolationDescription = violationDesc;

            // get current list of incidents for that user
            List<Violation> incidents = GetIncidentsByUser(id);
            if ( null == incidents)
            {
                // first time violator
                ViolatorDTO violation = new ViolatorDTO();
                violation.ViolatorId = id;
                violation.ViolatorName = user;
                violation.Violations.Add(newIncident);

                _violations.Add(violation);
            }
            else
            {
                incidents.Add(newIncident);
            }

            SaveViolations();
        }

        public bool ClearViolationsForUser(ulong id)
        {
            foreach (ViolatorDTO violation in _violations)
            {
                if (violation.ViolatorId == id)
                {
                    violation.Violations.Clear();
                    SaveViolations();
                    return true;
                }
            }
            return false;
        }
    }
}