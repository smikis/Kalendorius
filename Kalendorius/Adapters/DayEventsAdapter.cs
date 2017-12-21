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
    public class DayEventsAdapter : RecyclerView.Adapter
    {
        public List<DayEvent> _events;
        public event EventHandler<DayEvent> ItemClick;

        public DayEventsAdapter(List<DayEvent> events)
        {
            //_events = events;
            _events = new List<DayEvent>();
            _events.Add(new DayEvent
            {
                Category = "Egzaminas",
                Time = "18:00",
                Title = "Ekonomikos egzaminas"
            });

            _events.Add(new DayEvent
            {
                Category = "Teorija",
                Time = "08:00",
                Title = "Ekonomikos teorija"
            });

            _events.Add(new DayEvent
            {
                Category = "Praktika",
                Time = "19:00",
                Title = "Ekonomikos teorija"
            });

            _events.Add(new DayEvent
            {
                Category = "Laboratoriniai",
                Time = "22:00",
                Title = "Ekonomikos teorija"
            });
        }

        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new ListContentView(LayoutInflater.From(parent.Context), parent, OnClick);
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ListContentView vh = holder as ListContentView;
            switch (_events[position].Category)
            {
                case "Egzaminas":
                    vh.Image.SetImageResource(Resource.Drawable.egzas);
                    break;
                case "Teorija":
                    vh.Image.SetImageResource(Resource.Drawable.teorija);
                    break;
                case "Testas":
                    vh.Image.SetImageResource(Resource.Drawable.laborai);
                    break;
                case "Praktika":
                    vh.Image.SetImageResource(Resource.Drawable.laborai);
                    break;
                case "Laboratoriniai":
                    vh.Image.SetImageResource(Resource.Drawable.laborai);
                    break;
            }
            
            vh.Category.Text = _events[position].Category;
            vh.Title.Text = _events[position].Title;
            vh.Time.Text = _events[position].Time;
        }

        public override int ItemCount => _events.Count;

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, _events[position]);
        }
    }


    public class ListContentView : RecyclerView.ViewHolder
    {
        public ImageView Image { get; }
        public TextView Title { get; }
        public TextView Category { get; }
        public TextView Time { get; }

        public ListContentView(LayoutInflater inflater, ViewGroup parent, Action<int> listener) : base(inflater.Inflate(Resource.Layout.ListDayEvents, parent, false))
        {
            Image = ItemView.FindViewById<ImageView>(Resource.Id.list_avatar);
            Title = ItemView.FindViewById<TextView>(Resource.Id.list_title);
            Category = ItemView.FindViewById<TextView>(Resource.Id.list_type);
            Time = ItemView.FindViewById<TextView>(Resource.Id.time);

            ItemView.Click += (sender, e) => listener(AdapterPosition);
        }


    }
}