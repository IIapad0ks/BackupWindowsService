using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public interface IBackupSchedulerSettings
    {
        bool InstantBackup { get; }
        ulong BackupSequence { get; }
    }
}
