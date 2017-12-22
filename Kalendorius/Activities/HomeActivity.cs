using System;
using System.Collections.Generic;
using Java.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Kalendorius.Database;
using MV = Com.Applandeo.Materialcalendarview;

namespace Kalendorius.Activities
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : AppCompatActivity
    {
        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Kalendorius";
            SetSupportActionBar(toolbar);


            MV.CalendarView calendarView = (MV.CalendarView)FindViewById(Resource.Id.calendarView);

            List<MV.EventDay> events = new List<MV.EventDay>();

            foreach (var dayEvent in _databaseService.GetUserEvents())
            {
                Calendar calendar = Calendar.Instance;
                calendar.Set(dayEvent.Time.Year, dayEvent.Time.Month-1, dayEvent.Time.Day);
                switch (dayEvent.Category)
                {
                    case "Egzaminas":
                        events.Add(new MV.EventDay(calendar, Resource.Drawable.egzas));
                        break;
                    case "Teorija":
                        events.Add(new MV.EventDay(calendar, Resource.Drawable.teorija));
                        break;
                    case "Testas":
                        events.Add(new MV.EventDay(calendar, Resource.Drawable.laborai));
                        break;
                    case "Praktika":
                        events.Add(new MV.EventDay(calendar, Resource.Drawable.laborai));
                        break;
                    case "Laboratoriniai":
                        events.Add(new MV.EventDay(calendar, Resource.Drawable.laborai));
                        break;
                }
            }

            calendarView.SetEvents(events);
            calendarView.DayClick += CalendarView_DayClick;

        }

        private void CalendarView_DayClick(object sender, MV.Listeners.DayClickEventArgs e)
        {
            Intent i = new Intent(ApplicationContext, typeof(ViewDayActivity));
            var date = new DateTime(1900 + e.P0.Calendar.Time.Year, e.P0.Calendar.Time.Month+1, e.P0.Calendar.Time.GetDate());
            i.PutExtra("DATE", date.ToString());
            StartActivity(i);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.createEventMenu:
                    StartActivity(typeof(CreateEvent));
                    return true;
                case Resource.Id.kalendoriusMenu:
                    StartActivity(typeof(HomeActivity));
                    return true;
                case Resource.Id.srautaiMenu:
                    StartActivity(typeof(SourcesActivity));
                    return true;
                case Resource.Id.settingsMenu:
                    //do something
                    return true;
                case Resource.Id.logoutMenu:
                    StartActivity(typeof(LoginActivity));
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}