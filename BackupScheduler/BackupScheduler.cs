using Core.Managers;
using Core.Schedulers;
using Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackupScheduler
{
    public class BackupScheduler : IBackupScheduler
    {
        private Timer timer;
        private IBackupManager backupManager;
        private IBackupSchedulerSettings backupSchedulerSettings;

        public BackupScheduler(IBackupSchedulerSettings backupSchedulerSettings, IBackupManager backupManager)
        {
            this.backupSchedulerSettings = backupSchedulerSettings;
            this.backupManager = backupManager;
            this.timer = new Timer(backupSchedulerSettings.BackupSequence * 1000);
            this.timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.backupManager.Backup();
        }

        public void Start()
        {
            if (this.backupSchedulerSettings.InstantBackup)
            {
                this.backupManager.Backup();
            }

            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
