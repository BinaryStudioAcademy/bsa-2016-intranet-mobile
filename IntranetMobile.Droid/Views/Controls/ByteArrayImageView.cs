using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace IntranetMobile.Droid.Views.Controls
{
    class ByteArrayImageView : ImageView
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

        public ByteArrayImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public ByteArrayImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }
    }
}