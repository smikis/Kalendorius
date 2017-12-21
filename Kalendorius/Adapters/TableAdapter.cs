using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;

namespace Kalendorius.Adapters
{
    public class TableAdapter : FragmentPagerAdapter
    {
        private List<Fragment> _mFragmentList = new List<Fragment>();
        private List<string> _mFragmentTitleList = new List<string>();


        public TableAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => _mFragmentList.Count;
        public override Fragment GetItem(int position)
        {
            return _mFragmentList[position];
        }

        public void AddFragment(Fragment fragment, string title)
        {
            _mFragmentList.Add(fragment);
            _mFragmentTitleList.Add(title);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new String(_mFragmentTitleList[position]);
        }
    }
}