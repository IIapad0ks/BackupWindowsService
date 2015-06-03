using Amazon.S3.Model;
using Core.Clients;
using Core.Managers;
using Core.Settings;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class BackupManager : IBackupManager
    {
        private string backupName;
        private string backupFullName;
        private IBackupSettings backupSettings;
        private IAmazonS3Client amazonS3Client;

        public BackupManager(IBackupSettings backupSettings, IAmazonS3Client amazonS3Client)
        {
            this.backupSettings = backupSettings;
            this.amazonS3Client = amazonS3Client;
        }

        public void Backup()
        {
            Server backupServer = new Server(this.backupSettings.ServerName);
            backupServer.ConnectionContext.LoginSecure = this.backupSettings.LoginSecure;
            if (!this.backupSettings.LoginSecure)
            {
                backupServer.ConnectionContext.Login = this.backupSettings.Login;
                backupServer.ConnectionContext.Password = this.backupSettings.Password;
            }
            backupServer.ConnectionContext.Connect();

            this.SetBackupName();

            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.Database = this.backupSettings.DatabaseName;
            backup.Devices.AddDevice(this.backupFullName, DeviceType.File);
            backup.Initialize = true;
            backup.SqlBackup(backupServer);

            if (backupServer.ConnectionContext.IsOpen)
                backupServer.ConnectionContext.Disconnect();

            if(this.backupSettings.BackupLifetime != 0) {
                this.RemoveExpiredLocal();
            }

            if (this.backupSettings.UploadBackup)
            {
                this.Upload();
            }
        }

        private void Upload()
        {
            amazonS3Client.PutFile(this.backupFullName, this.backupName);

            if (!this.backupSettings.RestoreLocalBackup)
            {
                File.Delete(this.backupFullName);
            }

            if(this.backupSettings.BackupLifetime != 0) {
                this.RemoveExpiredAmazon();
            }
        }

        private void SetBackupName()
        {
            this.backupName = string.Format(this.backupSettings.BackupName, DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.'));
            this.backupFullName = string.Concat(this.backupSettings.BackupDirectory.TrimEnd('/', '\\'), "\\", this.backupName);
        }

        private void RemoveExpiredLocal()
        {
            var localBackups = Directory.GetFiles(this.backupSettings.BackupDirectory, this.backupSettings.BackupName.Replace("{0}", "*"));
            foreach (var localBackup in localBackups)
            {
                if (DateTime.Now > File.GetLastWriteTime(localBackup).AddSeconds(this.backupSettings.BackupLifetime))
                {
                    File.Delete(localBackup);
                }
            }
        }

        private void RemoveExpiredAmazon()
        {
            var amazonBackups = this.amazonS3Client.GetFiles();
            foreach (var amazonBackup in amazonBackups)
            {
                if (DateTime.Now > amazonBackup.LastModified.AddSeconds(this.backupSettings.BackupLifetime))
                {
                    this.amazonS3Client.DeleteFile(amazonBackup.Key);
                }
            }
        }
    }
}
