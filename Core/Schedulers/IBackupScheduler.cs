using Core.Managers;
using Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Schedulers
{
    public interface IBackupScheduler
    {
        void Start();
        void Stop();
    }
}
