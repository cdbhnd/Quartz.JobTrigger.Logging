using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Quartz.TriggerJob.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.TriggerJob.Logging.Mongo
{
    public class MongoTriggerLogger : ITriggerLogger
    {
        private MongoDatabase database;
        private string tablePrefix;

        private MongoCollection<TriggerLogEntry> TriggerLogs { get { return this.database.GetCollection<TriggerLogEntry>(tablePrefix + ".TriggerLogs"); } }

        public MongoTriggerLogger(string connectionString, string tablePrefix)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TriggerLogEntry)))
            {
                BsonClassMap.RegisterClassMap<TriggerLogEntry>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField(x => x.Timestamp);
                    cm.MapField(x => x.Name);
                    cm.MapField(x => x.Kind);
                    cm.MapField(x => x.Group);
                });
            }

            var urlBuilder = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(urlBuilder.ToMongoUrl());
            this.tablePrefix = tablePrefix;
            this.database = client.GetServer().GetDatabase(urlBuilder.DatabaseName);
        }

        public MongoTriggerLogger(string connectionString)
            : this(connectionString, "QUARTZ_")
        {
        }

        public MongoTriggerLogger()
            : this(ConfigurationManager.ConnectionStrings["quartznet-mongodb"].ConnectionString)
        { }

        public void LogTriggerCompleted(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context, Quartz.SchedulerInstruction triggerInstructionCode)
        {
            TriggerLogs.Insert(this.CreateEntry(trigger, "completed"));
        }

        public void LogTriggerFired(Quartz.ITrigger trigger, Quartz.IJobExecutionContext context)
        {
            TriggerLogs.Insert(this.CreateEntry(trigger, "fired"));
        }

        public void LogTriggerMisfired(Quartz.ITrigger trigger)
        {
            TriggerLogs.Insert(this.CreateEntry(trigger, "misfired"));
        }

        private TriggerLogEntry CreateEntry(Quartz.ITrigger trigger, string kind)
        {
            TriggerLogEntry entry = new TriggerLogEntry();

            entry.Name = trigger.Key.Name;
            entry.Group = trigger.Key.Group;
            entry.Timestamp = DateTime.UtcNow;
            entry.Kind = kind;

            return entry;
        }
    }
}
