using Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupScheduler
{
    public class BackupSchedulerSettings : IBackupSchedulerSettings
    {
        public ulong BackupSequence
        {
            get
            {
                return ulong.Parse(ConfigurationManager.AppSettings["BackupSequence"]);
            }
        }


        public bool InstantBackup
        {
            get
            {
                return ConfigurationManager.AppSettings["InstantBackup"].ToLower() == "true";
            }
        }
    }
}
