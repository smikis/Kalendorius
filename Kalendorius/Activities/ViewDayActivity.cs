using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Kalendorius.Adapters;
using Kalendorius.Database;
using Kalendorius.Models;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Kalendorius.Activities
{
    [Activity(Label = "ViewDay")]
    public class ViewDayActivity : AppCompatActivity
    {

        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewDay);

            var ms =  Intent.GetStringExtra("DATE");
            double ticks = double.Parse(ms);
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);
            DateTime parsedTime = new DateTime(1970, 1, 1) + time;
            // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = parsedTime.ToString("yyyy-MM-dd");
            SetSupportActionBar(toolbar);

           RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            DayEventsAdapter adapter = new DayEventsAdapter(_databaseService.GetUserEventsSpecificDay(parsedTime));
            adapter.ItemClick += Adapter_ItemClick; ;
            recyclerView.SetAdapter(adapter);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new LinearLayoutManager(ApplicationContext));

        }

        private void Adapter_ItemClick(object sender, DayEvent e)
        {
            Intent i = new Intent(ApplicationContext, typeof(ViewEvent));
            i.PutExtra("ID", e.Id);
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

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}