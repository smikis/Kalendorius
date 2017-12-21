using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Kalendorius.Adapters;
using Kalendorius.Fragments;

namespace Kalendorius.Activities
{
    [Activity(Label = "SourcesActivity")]
    public class SourcesActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Sources);
           
             // Adding Toolbar to Main screen           
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Problemos";
            SetSupportActionBar(toolbar);
            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);

            TableAdapter adapter = new TableAdapter(SupportFragmentManager);
            

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            adapter.AddFragment(new SubscribedSourcesFragment(), "Subscribed");
            adapter.AddFragment(new AllSourcesFragment(), "All");
            adapter.AddFragment(new CreatedSourcesFragment(), "Created");
            viewPager.Adapter = adapter;

            tabs.SetupWithViewPager(viewPager);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
          //  StartActivity(typeof(CreateProblemActivity));
        }

    }
}