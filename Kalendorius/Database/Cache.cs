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

namespace Kalendorius.Database
{
    public static class Cache
    {
        public static List<Source> Sources = new List<Source>();
        public static List<DayEvent> Events = new List<DayEvent>();
        public static List<User> Users = new List<User>();
        public static List<SourceEvent> SourceEvents = new List<SourceEvent>();
        public static List<SourceUsers> SourceUsers = new List<SourceUsers>();
    }
}