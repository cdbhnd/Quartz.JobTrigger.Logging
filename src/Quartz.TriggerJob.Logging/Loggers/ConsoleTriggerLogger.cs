using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Loggers
{
    public class ConsoleTriggerLogger : ITriggerLogger
    {
        private string triggerMisfiredMessage = "Trigger {1}.{0} misfired job {6}.{5}  at: {4:HH:mm:ss MM/dd/yyyy}.  Should have fired at: {3:HH:mm:ss MM/dd/yyyy}";

        public void LogTriggerCompleted(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            string info = string.Format(
                "**TriggerListener** Trigger '{2}' Completed: {0} {1}",
                context.JobDetail.Key.Name,
                context.JobDetail.Key.Group,
                trigger.Key.Name);

            Console.WriteLine(info);
        }

        public void LogTriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            string info = string.Format(
                 "**TriggerListener** Trigger '{2}' Fired: {0} {1}",
                 context.JobDetail.Key.Name,
                 context.JobDetail.Key.Group,
                 trigger.Key.Name);

            Console.WriteLine(info);
        }

        public void LogTriggerMisfired(ITrigger trigger)
        {
            object[] args =
               new object[]
                    {
                        trigger.Key.Name, trigger.Key.Group, trigger.GetPreviousFireTimeUtc(), trigger.GetNextFireTimeUtc(), DateTime.Now,
                        trigger.JobKey.Name, trigger.JobKey.Group
                    };

            string info = String.Format(CultureInfo.InvariantCulture, triggerMisfiredMessage, args);

            Console.WriteLine(info);
        }
    }
}
