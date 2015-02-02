using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Loggers
{
    public interface IJobLogger
    {
        void LogJobToBeExecuted(Quartz.IJobExecutionContext jobContext);
        void LogJobWasExecuted(Quartz.IJobExecutionContext jobContext, Quartz.JobExecutionException exception);
        void LogJobExecutionVetoed(Quartz.IJobExecutionContext jobContext);
    }
}
