using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;

namespace IntranetMobile.Droid.Views.Controls
{
    public class TopCropMvxAppCompatImageView : MvxAppCompatImageView
    {
        public TopCropMvxAppCompatImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public TopCropMvxAppCompatImageView(Context context) : base(context)
        {
            Init();
        }

        public TopCropMvxAppCompatImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Init();
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            RecomputeImgMatrix();
        }

        protected override bool SetFrame(int l, int t, int r, int b)
        {
            RecomputeImgMatrix();
            return base.SetFrame(l, t, r, b);
        }

        private void Init()
        {
            SetScaleType(ScaleType.Matrix);
        }

        private void RecomputeImgMatrix()
        {
            if (Drawable == null)
            {
                return;
            }

            var imageMatrix = new Matrix(ImageMatrix);
            float scale;
            var viewWidth = Width - PaddingLeft - PaddingRight;
            var viewHeight = Height - PaddingTop - PaddingBottom;
            var drawableWidth = Drawable.IntrinsicWidth;
            var drawableHeight = Drawable.IntrinsicHeight;

            if (drawableWidth*viewHeight > drawableHeight*viewWidth)
            {
                scale = viewHeight/(float) drawableHeight;
            }
            else
            {
                scale = viewWidth/(float) drawableWidth;
            }

            imageMatrix.SetScale(scale, scale);
            ImageMatrix = imageMatrix;
        }
    }
}