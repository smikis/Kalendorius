using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Kalendorius.Database;
using Kalendorius.Models;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Kalendorius.Activities
{
    [Activity(Label = "ViewEvent")]
    public class ViewEvent : AppCompatActivity
    {
        private int _id;
        private TextView _titleField;
        private TextView _locationField;
        private TextView _categoryField;
        private TextView _descriptionField;
        private TextView _dateField;

        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewEvent);
            _categoryField = FindViewById<TextView>(Resource.Id.categoryField);
            _titleField = FindViewById<TextView>(Resource.Id.titleField);
            _locationField = FindViewById<TextView>(Resource.Id.locationField);
            _dateField = FindViewById<TextView>(Resource.Id.timeField);
            _descriptionField = FindViewById<TextView>(Resource.Id.descriptionField);

            _id = Intent.GetIntExtra("ID", 1);
            var dayEvent = _databaseService.GetEvent(_id);
            _categoryField.Text = dayEvent.Category;
            _titleField.Text = dayEvent.Title;
            _locationField.Text = dayEvent.Location;
            _dateField.Text = dayEvent.Time.ToString();
            _descriptionField.Text = dayEvent.Description;

            // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = dayEvent.ToString();
            SetSupportActionBar(toolbar);

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