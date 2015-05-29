using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Schedulers;
using Core.Services;
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

        public IBackupScheduler BackupScheduler { get; set; }

        public BackupService()
        {
            InitializeComponent();

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
            this.iocCotainer.Install(FromAssembly.InThisApplication());
        }
    }
}
