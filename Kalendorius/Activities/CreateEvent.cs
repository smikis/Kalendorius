using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Text.Format;
using Kalendorius.Database;
using Kalendorius.Models;

namespace Kalendorius.Activities
{
    [Activity(Label = "CreateEvent")]
    public class CreateEvent : Activity
    {
        private Spinner _categoryField;
        private Spinner _sourceField;
        private EditText _titleField;
        private EditText _locationField;
        private CheckBox _selectSingle;
        private EditText _selectFromDate;
        private EditText _selectToDate;
        private TextView _selectToDateLabel;
        private EditText _repeat;
        private EditText _time;
        private EditText _description;
        private Button _createButton;

        private DateTime _selectedFromDateTime;
        private DateTime _selectedToDateTime;
        private DateTime _selectedTime;

        private DatabaseService _databaseService;
        private List<Source> _sources;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEvent);

            _categoryField = FindViewById<Spinner>(Resource.Id.categoryField);
            _sourceField = FindViewById<Spinner>(Resource.Id.sourceField);

            _titleField = FindViewById<EditText>(Resource.Id.titleField);
            _locationField = FindViewById<EditText>(Resource.Id.locationField);
            _selectFromDate = FindViewById<EditText>(Resource.Id.selectFromDate);
            _selectToDate = FindViewById<EditText>(Resource.Id.selectToDate);
            _selectToDateLabel = FindViewById<TextView>(Resource.Id.selectToLabel);
            _repeat = FindViewById<EditText>(Resource.Id.selectRepeat);
            _time = FindViewById<EditText>(Resource.Id.selectTime);
            _description = FindViewById<EditText>(Resource.Id.descriptionField);
            _selectSingle = FindViewById<CheckBox>(Resource.Id.selectSingle);
            _createButton = FindViewById<Button>(Resource.Id.confirmButton);

            _selectSingle.Click += _selectSingle_Click;

            _selectFromDate.Click += _selectFromDate_Click;
            _selectToDate.Click += _selectToDate_Click;
            _time.Click += _time_Click;
            _createButton.Click += _createButton_Click;

            _categoryField.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, new List<string> { "Egzaminas", "Teorija", "Testas", "Praktika", "Laboratoriniai" });

            _sources = _databaseService.GetAllSources();

            _sourceField.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line,  _sources.Select(x=> x.Name).ToList());
            // Create your application here
        }

        private void _createButton_Click(object sender, EventArgs e)
        {
            var sourceId = (int)_sourceField.SelectedItemId;
            if (_selectSingle.Checked)
            {
                var date = _selectedFromDateTime.AddHours(_selectedTime.Hour).AddMinutes(_selectedTime.Minute);
                _databaseService.CreateEvent(_titleField.Text, _categoryField.SelectedItem.ToString(), date,
                    _description.Text, _locationField.Text, _sources[sourceId].Id);
                StartActivity(typeof(HomeActivity));
            }
            _databaseService.CreateEvents(_titleField.Text, _categoryField.SelectedItem.ToString(), _selectedTime,
                _description.Text, _locationField.Text, _sources[sourceId].Id, _selectedFromDateTime,
                _selectedToDateTime, int.Parse(_repeat.Text));
            StartActivity(typeof(HomeActivity));
        }

        private void _time_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
                delegate (DateTime time)
                {
                    _selectedTime = time;
                    _time.Text = time.ToShortTimeString();
                });

            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void _selectToDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment1 frag = DatePickerFragment1.NewInstance(delegate (DateTime time)
            {
                _selectedToDateTime = time;
                _selectToDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment1.TAG);
        }

        private void _selectFromDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment1 frag = DatePickerFragment1.NewInstance(delegate (DateTime time)
            {
                _selectedFromDateTime = time;
                _selectFromDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment1.TAG);
        }

        private void _selectSingle_Click(object sender, EventArgs e)
        {
            if (_selectSingle.Checked)
            {
                _selectToDate.Visibility = ViewStates.Gone;
                _selectToDateLabel.Visibility = ViewStates.Gone;
            }
            else
            {
                _selectToDate.Visibility = ViewStates.Visible;
                _selectToDateLabel.Visibility = ViewStates.Visible;
            }
        }
    }


    public class DatePickerFragment1 : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment1).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragment1 NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment1 frag = new DatePickerFragment1();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                this,
                currently.Year,
                currently.Month - 1,
                currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }

    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "MyTimePickerFragment";
        Action<DateTime> timeSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<DateTime> onTimeSelected)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag.timeSelectedHandler = onTimeSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currentTime = DateTime.Now;
            bool is24HourFormat = DateFormat.Is24HourFormat(Activity);
            TimePickerDialog dialog = new TimePickerDialog
                (Activity, this, currentTime.Hour, currentTime.Minute, is24HourFormat);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            DateTime currentTime = DateTime.Now;
            DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hourOfDay, minute, 0);
            Log.Debug(TAG, selectedTime.ToLongTimeString());
            timeSelectedHandler(selectedTime);
        }
    }

}