using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Loggers
{
    public class ConsoleJobLogger : IJobLogger
    {
        public void LogJobToBeExecuted(IJobExecutionContext jobContext)
        {
            string info = string.Format(
                 "**JobListener** Job To Be Executed: {0} {1}",
                 jobContext.JobDetail.Key.Name,
                 jobContext.JobDetail.Key.Group);

            Console.WriteLine(info);
        }

        public void LogJobWasExecuted(IJobExecutionContext jobContext, JobExecutionException exception)
        {
            string info = string.Format(
               "**JobListener** Job Was Executed: {0} {1}",
               jobContext.JobDetail.Key.Name,
               jobContext.JobDetail.Key.Group);

            if (exception != null) {
                info = string.Format("{0}. ERROR: {1} STACKTRACE: {2}", info, exception.Message, exception.StackTrace);
            }

            Console.WriteLine(info);
        }

        public void LogJobExecutionVetoed(IJobExecutionContext jobContext)
        {
            string info = string.Format(
                "**JobListener** Job Vetoed: {0} {1}",
                jobContext.JobDetail.Key.Name,
                jobContext.JobDetail.Key.Group);

            Console.WriteLine(info);
        }
    }
}
