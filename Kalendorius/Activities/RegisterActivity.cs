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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private EditText _nameField;
        private EditText _usernameField;
        private EditText _passwordField;
        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            _nameField = FindViewById<EditText>(Resource.Id.name);
            _usernameField = FindViewById<EditText>(Resource.Id.username);
            _passwordField = FindViewById<EditText>(Resource.Id.password);

            var loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_usernameField.Text) && !string.IsNullOrEmpty(_passwordField.Text) && !string.IsNullOrEmpty(_nameField.Text))
            {

                if (_databaseService.Register(_nameField.Text, _usernameField.Text, _passwordField.Text))
                {
                    StartActivity(typeof(LoginActivity));
                }
            }
        }
    }
}