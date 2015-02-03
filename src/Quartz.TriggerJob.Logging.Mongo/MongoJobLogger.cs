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
    public class MongoJobLogger : IJobLogger
    {
        private string tablePrefix;
        private MongoDatabase database;

        private MongoCollection<JobLogEntry> JobLogs { get { return this.database.GetCollection<JobLogEntry>(tablePrefix + ".JobLogs"); } }

        public MongoJobLogger(string connectionString, string tablePrefix)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(JobLogEntry)))
            {
                BsonClassMap.RegisterClassMap<JobLogEntry>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField(x => x.Timestamp);
                    cm.MapField(x => x.Name);
                    cm.MapField(x => x.Kind);
                    cm.MapField(x => x.Group);
                    cm.MapField(x => x.NextFireTimeUtc);
                    cm.MapField(x => x.PreviousFireTimeUtc);
                    cm.MapField(x => x.JobRunTime);
                    cm.MapField(x => x.ExceptionMessage);
                    cm.MapField(x => x.ExceptionStackTrace);
                });
            }

            this.tablePrefix = tablePrefix;

            var urlBuilder = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(urlBuilder.ToMongoUrl());
            this.tablePrefix = tablePrefix;
            this.database = client.GetServer().GetDatabase(urlBuilder.DatabaseName);
        }

        public MongoJobLogger(string connectionString)
            : this(connectionString, "QUARTZ_") { }

        public MongoJobLogger()
            : this(ConfigurationManager.ConnectionStrings["quartznet-mongodb"].ConnectionString)
        {

        }

        public void LogJobToBeExecuted(Quartz.IJobExecutionContext jobContext)
        {
            var entry = CreateEntry(jobContext, "toBeExecuted");

            this.JobLogs.Insert(entry);
        }

        public void LogJobWasExecuted(Quartz.IJobExecutionContext jobContext, Quartz.JobExecutionException exception)
        {
            var entry = CreateEntry(jobContext, "wasExecuted", exception);

            this.JobLogs.Insert(entry);
        }

        public void LogJobExecutionVetoed(Quartz.IJobExecutionContext jobContext)
        {
            var entry = CreateEntry(jobContext, "executionVetoed");

            this.JobLogs.Insert(entry);
        }

        private JobLogEntry CreateEntry(Quartz.IJobExecutionContext jobContext, string kind, Quartz.JobExecutionException exception = null)
        {
            JobLogEntry entry = new JobLogEntry();
            entry.Group = jobContext.JobDetail.Key.Group;
            entry.Name = jobContext.JobDetail.Key.Name;
            entry.Kind = kind;
            entry.Timestamp = DateTime.UtcNow;
            entry.JobRunTime = jobContext.JobRunTime;
            entry.NextFireTimeUtc = jobContext.NextFireTimeUtc;
            entry.PreviousFireTimeUtc = jobContext.PreviousFireTimeUtc;

            if (exception != null)
            {
                entry.ExceptionMessage = exception.Message;
                entry.ExceptionStackTrace = exception.StackTrace;
            }

            return entry;
        }
    }
}
