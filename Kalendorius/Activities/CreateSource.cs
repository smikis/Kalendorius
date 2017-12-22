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
using Kalendorius.Database;

namespace Kalendorius.Activities
{
    [Activity(Label = "CreateSource")]
    public class CreateSource : Activity
    {
        private EditText _titleField;
        private EditText _description;
        private Button _createButton;
        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateSource);          
            _titleField = FindViewById<EditText>(Resource.Id.titleField);
            _description = FindViewById<EditText>(Resource.Id.descriptionField);
            _createButton = FindViewById<Button>(Resource.Id.confirmButton);

            _createButton.Click += _createButton_Click; ;
            // Create your application here
        }

        private void _createButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_titleField.Text) && !string.IsNullOrEmpty(_description.Text))
            {
                _databaseService.CreateSource(_titleField.Text, _description.Text);
                StartActivity(typeof(SourcesActivity));
            }
        }
    }
}