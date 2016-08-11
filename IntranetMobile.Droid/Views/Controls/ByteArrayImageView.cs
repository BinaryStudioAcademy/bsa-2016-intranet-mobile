using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace IntranetMobile.Droid.Views.Controls
{
    internal class ByteArrayImageView : ImageView
    {
        public ByteArrayImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public ByteArrayImageView(Context context) : base(context)
        {
        }

        public ByteArrayImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ByteArrayImageView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
        }

        public ByteArrayImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }
    }
}