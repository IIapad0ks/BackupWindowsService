using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public interface IBackupSettings
    {
        string ServerName { get; }
        string DatabaseName { get; }
        string BackupDirectory { get; }
        string BackupName { get; }
        ulong BackupLifetime { get; }
        bool UploadBackup { get; }
        bool RestoreLocalBackup { get; }

        bool LoginSecure { get; }
        string Login { get; }
        string Password { get; }
    }
}
