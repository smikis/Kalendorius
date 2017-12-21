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

namespace Kalendorius.Models
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
    }
}