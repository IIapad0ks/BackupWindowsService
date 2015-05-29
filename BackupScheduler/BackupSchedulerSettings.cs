using Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    class BackupSchedulerSettings : IBackupSchedulerSettings
    {
        public int BackupSequence
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["BackupSequence"]);
            }
        }
    }
}
