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
    public class TriggerLoggerListener : TriggerListenerSupport
    {
        private string name;
        private ITriggerLogger triggerLogger;
        private List<string> logEvents;
        public TriggerLoggerListener(string name, ITriggerLogger triggerLogger)
        {
            this.name = name;
            this.triggerLogger = triggerLogger;

            string configValue = ConfigurationManager.AppSettings["quartz.trigger.logger.events"];
            if (!string.IsNullOrEmpty(configValue))
            {
                this.logEvents = configValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                this.logEvents = new List<string>() { "TriggerFired", "TriggerComplete" };
            }
        }

        public TriggerLoggerListener(ITriggerLogger triggerLogger)
            : this("TriggerLoggerListener", triggerLogger) { }

        public TriggerLoggerListener()
            : this(new ConsoleTriggerLogger()) { }

        public override string Name
        {
            get { return name; }
        }

        public ITriggerLogger TriggerLogger
        {
            set
            {
                this.triggerLogger = value;
            }
        }

        public override void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            try
            {
                if (this.logEvents.Contains("TriggerComplete"))
                {
                    this.triggerLogger.LogTriggerCompleted(trigger, context, triggerInstructionCode);
                }
            }
            catch
            {

            }
        }

        public override void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            try
            {
                if (this.logEvents.Contains("TriggerFired"))
                {
                    this.triggerLogger.LogTriggerFired(trigger, context);
                }
            }
            catch { }
        }

        public override void TriggerMisfired(ITrigger trigger)
        {
            try
            {
                if (this.logEvents.Contains("TriggerFired"))
                {
                    this.triggerLogger.LogTriggerMisfired(trigger);
                }
            }
            catch { }
        }
    }
}
