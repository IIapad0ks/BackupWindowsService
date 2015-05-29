namespace BackupService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackupServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BackupServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BackupServiceProcessInstaller
            // 
            this.BackupServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BackupServiceProcessInstaller.Password = null;
            this.BackupServiceProcessInstaller.Username = null;
            // 
            // BackupServiceInstaller
            // 
            this.BackupServiceInstaller.ServiceName = "BackupService";
            this.BackupServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BackupServiceInstaller,
            this.BackupServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BackupServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BackupServiceInstaller;
    }
}