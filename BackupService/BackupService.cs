using CastleRegistration = Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Clients;
using Core.Managers;
using Core.Schedulers;
using Core.Services;
using Core.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public partial class BackupService : ServiceBase, IBackupService
    {
        private WindsorContainer iocCotainer;
        private IBackupScheduler BackupScheduler { get; set; }

        public BackupService()
        {
            InitializeComponent();
            this.InitializeIoC();

            this.BackupScheduler = this.iocCotainer.Resolve<IBackupScheduler>();
        }

        protected override void OnStart(string[] args)
        {
            this.BackupScheduler.Start();
        }

        protected override void OnStop()
        {
            this.BackupScheduler.Stop();
        }

        protected void InitializeIoC()
        {
            this.iocCotainer = new WindsorContainer();
            this.iocCotainer.Register(CastleRegistration.Component.For<IBackupScheduler>().ImplementedBy<BackupScheduler.BackupScheduler>());
            this.iocCotainer.Register(CastleRegistration.Component.For<IBackupSchedulerSettings>().ImplementedBy<BackupScheduler.BackupSchedulerSettings>());
            this.iocCotainer.Register(CastleRegistration.Component.For<IBackupManager>().ImplementedBy<BackupManager.BackupManager>());
            this.iocCotainer.Register(CastleRegistration.Component.For<IBackupSettings>().ImplementedBy<BackupManager.BackupSettings>());
            this.iocCotainer.Register(CastleRegistration.Component.For<IAmazonS3Client>().ImplementedBy<AmazonClient.AmazonS3Client>());
            this.iocCotainer.Register(CastleRegistration.Component.For<IAmazonS3Settings>().ImplementedBy<BackupManager.AmazonS3Settings>());
        }
    }
}
