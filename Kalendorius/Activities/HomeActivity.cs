using System.Collections.Generic;
using Java.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using MV = Com.Applandeo.Materialcalendarview;

namespace Kalendorius.Activities
{
    [Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Kalendorius";
            SetSupportActionBar(toolbar);


            MV.CalendarView calendarView = (MV.CalendarView)FindViewById(Resource.Id.calendarView);

            List<MV.EventDay> events = new List<MV.EventDay>();

            Calendar calendarToday = Calendar.Instance;
            events.Add(new MV.EventDay(calendarToday, Resource.Drawable.sample_icon_1));
            calendarView.SetEvents(events);


            calendarView.DayClick += CalendarView_DayClick;

        }

        private void CalendarView_DayClick(object sender, MV.Listeners.DayClickEventArgs e)
        {
            Intent i = new Intent(ApplicationContext, typeof(ViewDayActivity));
            var val = e.P0.Calendar.Time.ToString();
            i.PutExtra("DATE", e.P0.Calendar.Time.ToString());
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
                case Resource.Id.kalendoriusMenu:                  
                    return true;
                case Resource.Id.srautaiMenu:
                    StartActivity(typeof(SourcesActivity));
                    return true;
                case Resource.Id.settingsMenu:
                    //do something
                    return true;
                case Resource.Id.logoutMenu:
                    
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}