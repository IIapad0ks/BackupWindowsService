using Amazon;
using S3 = Amazon.S3;
using Amazon.S3.Model;
using Core.Clients;
using Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonClient
{
    public class AmazonS3Client : IAmazonS3Client
    {
        private S3.AmazonS3Client client;
        private IAmazonS3Settings amazonSettings;

        public AmazonS3Client(IAmazonS3Settings amazonSettings)
        {
            this.amazonSettings = amazonSettings;
            this.client = new S3.AmazonS3Client(this.amazonSettings.AccessKey, this.amazonSettings.SecretAccessKey, new S3.AmazonS3Config { ServiceURL = this.amazonSettings.ServiceURL });
        }

        public void PutFile(string filePath, string key)
        {
            var request = new PutObjectRequest
            {
                BucketName = this.amazonSettings.BucketName,
                Key = key,
                FilePath = filePath
            };

            this.client.PutObject(request);
        }

        public List<S3Object> GetFiles()
        {
            return this.client.ListObjects(this.amazonSettings.BucketName).S3Objects;
        }

        public void DeleteFile(string key)
        {
            this.client.DeleteObject(this.amazonSettings.BucketName, key);
        }
    }
}
