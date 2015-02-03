using Quartz.TriggerJob.Logging.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Mongo
{
    public class MongoTriggerLoggerListener : TriggerLoggerListener
    {
        public MongoTriggerLoggerListener(string name)
            : base(name, new MongoTriggerLogger())
        {

        }

        public MongoTriggerLoggerListener(string name, string mongoConnectionString)
            : base(name, new MongoTriggerLogger(mongoConnectionString))
        {

        }

        public MongoTriggerLoggerListener()
            : this("MongoTriggerLoggerListener")
        { }

    }
}
