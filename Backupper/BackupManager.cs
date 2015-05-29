using Amazon.S3;
using Amazon.S3.Model;
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
        private IAmazonS3Settings amazonSettings;
        private IBackupSettings backupSettings;

        public BackupManager(IAmazonS3Settings amazonSettings, IBackupSettings backupSettings)
        {
            this.amazonSettings = amazonSettings;
            this.backupSettings = backupSettings;
        }

        public void Backup()
        {
            Server backupServer = new Server(this.backupSettings.ServerName);
            backupServer.ConnectionContext.LoginSecure = this.backupSettings.LoginSecure;
            backupServer.ConnectionContext.Login = this.backupSettings.Login;
            backupServer.ConnectionContext.Password = this.backupSettings.Password;
            backupServer.ConnectionContext.Connect();

            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.Database = this.backupSettings.DatabaseName;
            backup.Devices.AddDevice(this.backupSettings.BackupPath, DeviceType.File);
            backup.Initialize = false;
            backup.SqlBackup(backupServer);

            if (backupServer.ConnectionContext.IsOpen)
                backupServer.ConnectionContext.Disconnect();
        }

        public void Upload()
        {
            AmazonS3Client amazonClient = new AmazonS3Client(this.amazonSettings.AccessKey, this.amazonSettings.SecretAccessKey);

            var request = new PutObjectRequest
            {
                BucketName = this.amazonSettings.BucketName,
                Key = Guid.NewGuid().ToString(),
                FilePath = this.backupSettings.BackupPath
            };

            amazonClient.PutObject(request);

            if (!this.backupSettings.RestoreLocalBackup)
            {
                File.Delete(this.backupSettings.BackupPath);
            }
        }

        public void BackupAndUpload()
        {
            this.Backup();
            this.Upload();
        }
    }
}
