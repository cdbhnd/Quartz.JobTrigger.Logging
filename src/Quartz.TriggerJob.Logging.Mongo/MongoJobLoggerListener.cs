using Quartz.TriggerJob.Logging.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Mongo
{
    public class MongoJobLoggerListener : JobLoggerListener
    {
        public MongoJobLoggerListener(string name)
            : base(name, new MongoJobLogger())
        {
        }

        public MongoJobLoggerListener(string name, string mongoConnectionString)
            : base(name, new MongoJobLogger(mongoConnectionString))
        {

        }

        public MongoJobLoggerListener()
            : this("MongoJobLoggerListener")
        { }


    }
}
