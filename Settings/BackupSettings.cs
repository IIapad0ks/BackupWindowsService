using Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    class BackupSettings : IBackupSettings
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string BackupPath { get; set; }
        public bool LoginSecure { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
