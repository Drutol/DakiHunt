using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

namespace DakiHunt.Api.BackgroundTasks
{
    public class SchedulerManager
    {
        private const string HuntUpdateJobName = "HuntUpdate";
        private const string HuntUpdateGroupName = "HuntUpdate";

        private IScheduler _scheduler;


        public async Task Initialize(IUserService userService)
        {
            var props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };

            _scheduler = await new StdSchedulerFactory(props).GetScheduler();

            userService.ConfigureIncludes()
                .WithChain(query => query
                .Include(user => user.Hunts)
                    .ThenInclude(hunt => hunt.TimeTrigger))        
                .Commit();

            foreach (var user in await userService.GetAllWhereAsync(user => user.Hunts.Count > 0))
            {
                foreach (var huntGroup in user.Hunts.GroupBy(hunt => hunt.TimeTrigger.Interval))
                {
                    var job = JobBuilder.Create<HuntUpdateJob>()
                        .WithIdentity($"{HuntUpdateJobName}{user.Id}", HuntUpdateGroupName)
                        .SetJobData(new JobDataMap
                        {
                            ["HuntIds"] = huntGroup.Select(hunt => hunt.Id).ToArray()
                        })
                        .Build();

                    var trigger = TriggerBuilder.Create()
                        .WithIdentity($"{HuntUpdateJobName}{user.Id}", HuntUpdateGroupName)
                        .WithSimpleSchedule(builder => 
                            builder.WithIntervalInMinutes(huntGroup.Key).RepeatForever())
                        .Build();

                    await _scheduler.ScheduleJob(job, trigger);
                }            
            }      
        }
    }
}
