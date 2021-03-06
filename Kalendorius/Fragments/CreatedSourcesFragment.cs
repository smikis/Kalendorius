﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Kalendorius.Adapters;
using Kalendorius.Database;
using Kalendorius.Models;
using Fragment = Android.Support.V4.App.Fragment;

namespace Kalendorius.Fragments
{
    public class CreatedSourcesFragment : Fragment
    {
        private DatabaseService _databaseService;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _databaseService = new DatabaseService();
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            RecyclerView recyclerView = (RecyclerView)inflater.Inflate(
                Resource.Layout.fragmentsRecyclerView, container, false);

            SourcesAdapter adapter = new SourcesAdapter(_databaseService.GetUserCreatedSources());
            adapter.ItemClick += Adapter_ItemClick;
            recyclerView.SetAdapter(adapter);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            return recyclerView;
        }

        private void Adapter_ItemClick(object send, Source ex)
        {
       

        }

    }
}