using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace IntranetMobile.Droid.Views.Controls
{
    public class RecycleNewsView : LinearLayout
    {
        public RecycleNewsView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public RecycleNewsView(Context context) : base(context)
        {
            Init();
        }

        public RecycleNewsView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public RecycleNewsView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        public RecycleNewsView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init();
        }

        private void Init()
        {
            Inflate(Context, Resource.Layout.recycle_news, this);
        }
    }
}