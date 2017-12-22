using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Kalendorius.Adapters;
using Kalendorius.Database;
using Kalendorius.Models;
using Fragment = Android.Support.V4.App.Fragment;

namespace Kalendorius.Fragments
{
    public class AllSourcesFragment : Fragment
    {
        private DatabaseService _databaseService;
        private int _dialogClickId;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            RecyclerView recyclerView = (RecyclerView)inflater.Inflate(
                Resource.Layout.fragmentsRecyclerView, container, false);

            SourcesAdapter adapter = new SourcesAdapter(_databaseService.GetAllSources());
            adapter.ItemClick += Adapter_ItemClick;
            recyclerView.SetAdapter(adapter);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            return recyclerView;
        }

        private void Adapter_ItemClick(object send, Source ex)
        {
            _dialogClickId = ex.Id;
            using (var builder = new AlertDialog.Builder(Activity))
            {
                var title = "Do you wish to subscribe to this source?";
                builder.SetTitle(title);
                builder.SetPositiveButton("Yes", OkAction);
                builder.SetNegativeButton("No", CancelAction);
                var myCustomDialog = builder.Create();
                myCustomDialog.Show();
            }
           
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            _databaseService.SubscribeUserToSource(_dialogClickId);
        }
        private void CancelAction(object sender, DialogClickEventArgs e)
        {
           
        }
    }
}