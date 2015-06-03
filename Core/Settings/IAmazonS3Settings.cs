using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public interface IAmazonS3Settings
    {
        string AccessKey { get; }
        string SecretAccessKey { get; }
        string BucketName { get; }
        string ServiceURL { get; }
    }
}
