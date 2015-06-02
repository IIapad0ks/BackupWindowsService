using Core.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupManager
{
    public class AmazonS3Settings : IAmazonS3Settings
    {
        public string AccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AmazonS3AccessKey"];
            }
        }

        public string SecretAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AmazonS3SecretAccessKey"];
            }
        }

        public string BucketName
        {
            get
            {
                return ConfigurationManager.AppSettings["AmazonS3BucketName"];
            }
        }
    }
}
