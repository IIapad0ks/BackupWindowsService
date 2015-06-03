using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Clients
{
    public interface IAmazonS3Client
    {
        void PutFile(string filePath, string key);
        List<S3Object> GetFiles();
        void DeleteFile(string key);
    }
}
