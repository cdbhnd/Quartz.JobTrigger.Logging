using Quartz.Listener;
using Quartz.TriggerJob.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Listeners
{
    public class JobLoggerListener : JobListenerSupport
    {
        private string name;
        private IJobLogger jobLogger;
        private List<string> logEvents;
        public override string Name
        {
            get { return this.name; }
        }

        public JobLoggerListener(string name, IJobLogger jobLogger)
        {
            this.name = name;
            this.jobLogger = jobLogger;

            string configValue = ConfigurationManager.AppSettings["quartz.job.logger.events"];
            if (!string.IsNullOrEmpty(configValue))
            {
                this.logEvents = configValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                this.logEvents = new List<string>() { "JobWasExecuted" };
            }
        }

        public JobLoggerListener(IJobLogger jobLogger)
            : this("JobLoggerListener", jobLogger) { }

        public JobLoggerListener()
            : this(new ConsoleJobLogger()) { }

        public IJobLogger JobLogger
        {
            set
            {
                this.jobLogger = value;
            }
        }

        public override void JobExecutionVetoed(IJobExecutionContext context)
        {
            try
            {
                if (this.logEvents.Contains("JobExecutionVetoed"))
                {
                    this.jobLogger.LogJobExecutionVetoed(context);
                }
            }
            catch { }
        }

        public override void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            try
            {
                if (this.logEvents.Contains("JobWasExecuted"))
                {
                    this.jobLogger.LogJobWasExecuted(context, jobException);
                }
                else if (this.logEvents.Contains("JobWasExecuted-ErrorOnly") && jobException != null)
                {
                    this.jobLogger.LogJobWasExecuted(context, jobException);
                }
            }
            catch { }
        }

        public override void JobToBeExecuted(IJobExecutionContext context)
        {
            try
            {
                if (this.logEvents.Contains("JobToBeExecuted"))
                {
                    this.jobLogger.LogJobToBeExecuted(context);
                }
            }
            catch { }
        }
    }
}
