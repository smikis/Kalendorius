using System.Collections.Generic;
using Java.Util;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
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
            
        }
    }
}