using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kalendorius.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Kalendorius.Database
{
    public class DatabaseService
    {
        private static ISettings AppSettings =>
            CrossSettings.Current;
        public bool CreateSource(string name, string description)
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            int id = 0;
            if (Cache.Sources.Any())
            {
                id = Cache.Sources.Max(x => x.Id) + 1;
            }
            var source = new Source
            {
                Id = id,
                Name = name,
                Description = description,
                User = user
            };
            Cache.Sources.Add(source);
            return true;
        }

        public bool DeleteSource(int id)
        {
            Cache.Sources.RemoveAll(x => x.Id == id);
            return true;
        }

        public List<Source> GetUserCreatedSources()
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            return Cache.Sources.Where(x => x.User == user).ToList();
        }

        public bool SubscribeUserToSource(int sourceId)
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            Cache.SourceUsers.Add(new SourceUsers
            {
             UserId   = user,
             SourceId = sourceId
            });
            return true;
        }

        public bool UnsubscribeUserFromSource(int sourceId)
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            Cache.SourceUsers.RemoveAll(x => x.SourceId == sourceId && x.UserId == user);
            return true;
        }

        public List<Source> GetUserSubscribedSources()
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            return Cache.Sources.Where(x => Cache.SourceUsers.Any(y=> y.UserId == user && y.SourceId == x.Id)).ToList();
        }

        public List<Source> GetAllSources()
        {        
            return Cache.Sources.ToList();
        }

        public bool CreateEvents(string title, string category, DateTime time, string description, string location,
            int sourceId, DateTime fromDate, DateTime toDate, int interval)
        {
            while (fromDate < toDate)
            {
                var eventTime = fromDate.AddHours(time.Hour);
                eventTime = eventTime.AddMinutes(time.Minute);
                CreateEvent(title, category, eventTime, description, location, sourceId);
                fromDate = fromDate.AddDays(interval);
            }
            return true;
        }

        public bool CreateEvent(string title, string category, DateTime time, string description, string location, int sourceId)
        {
            int id = 0;
            if (Cache.Events.Any())
            {
                id = Cache.Events.Max(x => x.Id) + 1;
            }
            var dayEvent = new DayEvent
            {
                Id = id,
                Category = category,
                Description = description,
                Location = location,
                Time = time,
                Title = title
            };

            Cache.Events.Add(dayEvent);
            Cache.SourceEvents.Add(new SourceEvent
            {
                EventId = id,
                SourceId = sourceId
            });
            return true;
        }

        public List<DayEvent> GetUserEvents()
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            var userSubscriptions = Cache.SourceUsers.Where(x => x.UserId == user);
            var events = new List<DayEvent>();
            foreach (var userSubscription in userSubscriptions)
            {
                var sourceEvents = Cache.Events.Where(x =>
                    Cache.SourceEvents.Any(y => y.SourceId == userSubscription.SourceId && y.EventId == x.Id));
                events.AddRange(sourceEvents);
                
            }
            return events.ToList();
        }

        public DayEvent GetEvent(int id)
        {
            return Cache.Events.FirstOrDefault(x => x.Id == id);
        }

        public List<DayEvent> GetUserEventsSpecificDay(DateTime time)
        {
            var user = AppSettings.GetValueOrDefault("user", 1);
            var userSubscriptions = Cache.SourceUsers.Where(x => x.UserId == user);
            var events = new List<DayEvent>();
            foreach (var userSubscription in userSubscriptions)
            {
                var sourceEvents = Cache.Events.Where(x =>
                    Cache.SourceEvents.Any(y => y.SourceId == userSubscription.SourceId && y.EventId == x.Id)).Where(x=> x.Time.Year == time.Year && x.Time.Month == time.Month && x.Time.Day == time.Day);
                events.AddRange(sourceEvents);

            }
            return events.ToList();
        }

        public bool Register(string name, string username, string password)
        {
            int id = 0;
            if (Cache.Events.Any())
            {
                id = Cache.Events.Max(x => x.Id) + 1;
            }
            var user = new User
            {
                Id = id,
                Username = username,
                Name = name,
                Password = password
            };
            Cache.Users.Add(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = Cache.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                AppSettings.AddOrUpdateValue("user", user.Id);
                return true;
            }
            return false;
        }

    }
}