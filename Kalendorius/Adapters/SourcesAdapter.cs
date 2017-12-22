using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Kalendorius.Models;

namespace Kalendorius.Adapters
{
    public class SourcesAdapter : RecyclerView.Adapter
    {
        public List<Source> _sources;
        public event EventHandler<Source> ItemClick;

        public SourcesAdapter(List<Source> sources)
        {
            //_events = events;
            _sources = sources;
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new SourceListContentView(LayoutInflater.From(parent.Context), parent, OnClick);
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SourceListContentView vh = holder as SourceListContentView;    
            vh.Name.Text = _sources[position].Name;
        }

        public override int ItemCount => _sources.Count;

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, _sources[position]);
        }
    }


    public class SourceListContentView : RecyclerView.ViewHolder
    {
        public TextView Name { get; }

        public SourceListContentView(LayoutInflater inflater, ViewGroup parent, Action<int> listener) : base(inflater.Inflate(Resource.Layout.ListDayEvents, parent, false))
        {
            Name = ItemView.FindViewById<TextView>(Resource.Id.list_title);

            ItemView.Click += (sender, e) => listener(AdapterPosition);
        }


    }
}