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
using Kalendorius.Models;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Kalendorius.Activities
{
    [Activity(Label = "ViewDay")]
    public class ViewDayActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewDay);

            var time =  Intent.GetStringExtra("DATE");

            // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = time;
            SetSupportActionBar(toolbar);

           RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            DayEventsAdapter adapter = new DayEventsAdapter(new List<DayEvent>());
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
                case Resource.Id.kalendoriusMenu:
                    //do something
                    return true;
                case Resource.Id.srautaiMenu:
                    //do something
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