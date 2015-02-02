using Quartz.Listener;
using Quartz.TriggerJob.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Listeners
{
    public class JobLoggerListener : JobListenerSupport
    {
        private string name;
        private IJobLogger jobLogger;
        public override string Name
        {
            get { return this.name; }
        }

        public JobLoggerListener(string name, IJobLogger jobLogger) {
            this.name = name;
            this.jobLogger = jobLogger;
        }

        public JobLoggerListener(IJobLogger jobLogger)
            : this("JobLoggerListener", jobLogger) { }

        public JobLoggerListener()
            : this(new ConsoleJobLogger()) { }

        public override void JobExecutionVetoed(IJobExecutionContext context)
        {
            try
            {
                this.jobLogger.LogJobExecutionVetoed(context);
            }
            catch { }
        }

        public override void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            try
            {
                this.jobLogger.LogJobWasExecuted(context, jobException);
            }
            catch{}
        }

        public override void JobToBeExecuted(IJobExecutionContext context)
        {
            try
            {
                this.jobLogger.LogJobToBeExecuted(context);
            }
            catch { }
        }
    }
}
