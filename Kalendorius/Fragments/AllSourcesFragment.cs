using System.Collections.Generic;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Kalendorius.Adapters;
using Kalendorius.Models;
using Fragment = Android.Support.V4.App.Fragment;

namespace Kalendorius.Fragments
{
    public class AllSourcesFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            RecyclerView recyclerView = (RecyclerView)inflater.Inflate(
                Resource.Layout.fragmentsRecyclerView, container, false);

            SourcesAdapter adapter = new SourcesAdapter(new List<Source>());
            adapter.ItemClick += Adapter_ItemClick;
            recyclerView.SetAdapter(adapter);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            return recyclerView;
        }

        private void Adapter_ItemClick(object sender, Source e)
        {
          /*  Intent i = new Intent(ApplicationContext, typeof(ViewEvent));
            i.PutExtra("ID", e.Id);
            StartActivity(i); */
        }
    }
}