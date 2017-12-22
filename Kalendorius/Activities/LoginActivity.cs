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
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private EditText _usernameField;
        private EditText _passwordField;
        private DatabaseService _databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            _usernameField = FindViewById<EditText>(Resource.Id.username);
            _passwordField = FindViewById<EditText>(Resource.Id.password);

            var loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;

            var registerLink = FindViewById<TextView>(Resource.Id.registerLink);
            registerLink.Click += RegisterLink_Click; ;
        }

        private void RegisterLink_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_usernameField.Text) && !string.IsNullOrEmpty(_passwordField.Text))
            {

                if (_databaseService.Login(_usernameField.Text, _passwordField.Text))
                {                 
                    StartActivity(typeof(HomeActivity));
                }
                else
                {
                    _passwordField.Text = "";
                }
                //TODO Show error             
            }
        }
    }
}