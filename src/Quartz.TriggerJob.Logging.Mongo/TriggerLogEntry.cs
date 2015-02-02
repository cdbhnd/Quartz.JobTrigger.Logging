using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Mongo
{
    public class TriggerLogEntry
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Kind { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
