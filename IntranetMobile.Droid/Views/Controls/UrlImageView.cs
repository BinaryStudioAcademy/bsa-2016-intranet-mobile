using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Java.IO;
using MvvmCross.Binding.Droid.Views;

namespace IntranetMobile.Droid.Views.Controls
{
    internal class UrlImageView : MvxImageView
    {
        private byte[] imageBytes;

        public UrlImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public UrlImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public UrlImageView(Context context) : base(context)
        {
        }

        public UrlImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public byte[] ImageBytes
        {
            set
            {
                imageBytes = value;
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                SetImageBitmap(bitmap);
            }
            get { return imageBytes; }
        }
    }
}