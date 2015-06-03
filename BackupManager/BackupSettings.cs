using Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupSettings : IBackupSettings
    {
        public string ServerName
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupServerName"];
            }
        }

        public string DatabaseName
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupDatabaseName"];
            }
        }

        public string BackupDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupDirectory"];
            }
        }

        public string BackupName
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupName"];
            }
        }

        public bool LoginSecure
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupServerLoginSecure"].ToLower() == "true";
            }
        }

        public string Login
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupServerLogin"];
            }
        }

        public string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["BackupServerPassword"];
            }
        }


        public bool RestoreLocalBackup
        {
            get 
            {
                return ConfigurationManager.AppSettings["RestoreLocalBackup"].ToLower() == "true";
            }
        }

        public ulong BackupLifetime
        {
            get
            {
                return ulong.Parse(ConfigurationManager.AppSettings["BackupLifetime"]);
            }
        }

        public bool UploadBackup
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadBackup"].ToLower() == "true";
            }
        }
    }
}
