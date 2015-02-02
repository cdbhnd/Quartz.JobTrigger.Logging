using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Mongo
{
    public class JobLogEntry
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime Timestamp { get; set; }
        public string Kind { get; set; }
        public DateTimeOffset? PreviousFireTimeUtc { get; set; }
        public DateTimeOffset? NextFireTimeUtc { get; set; }
        public TimeSpan JobRunTime { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
