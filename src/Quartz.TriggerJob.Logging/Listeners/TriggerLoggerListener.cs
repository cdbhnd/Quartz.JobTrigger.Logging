using Quartz.Listener;
using Quartz.TriggerJob.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Listeners
{
    public class TriggerLoggerListener : TriggerListenerSupport
    {
        private string name;
        private ITriggerLogger triggerLogger;
        public TriggerLoggerListener(string name, ITriggerLogger triggerLogger)
        {
            this.name = name;
            this.triggerLogger = triggerLogger;
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
                this.triggerLogger.LogTriggerCompleted(trigger, context, triggerInstructionCode);
            }
            catch
            {

            }
        }

        public override void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            try
            {
                this.triggerLogger.LogTriggerFired(trigger, context);
            }
            catch { }
        }

        public override void TriggerMisfired(ITrigger trigger)
        {
            try
            {
                this.triggerLogger.LogTriggerMisfired(trigger);
            }
            catch { }
        }
    }
}
