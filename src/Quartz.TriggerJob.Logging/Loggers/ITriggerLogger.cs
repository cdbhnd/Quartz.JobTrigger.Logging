using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Loggers
{
    public interface ITriggerLogger
    {
        void LogTriggerCompleted(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context, Quartz.SchedulerInstruction triggerInstructionCode);
        void LogTriggerFired(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context);
        void LogTriggerMisfired(Quartz.ITrigger trigger);
    }
}
