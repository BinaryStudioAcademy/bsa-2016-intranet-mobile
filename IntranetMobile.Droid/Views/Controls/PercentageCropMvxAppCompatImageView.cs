using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;

namespace IntranetMobile.Droid.Views.Controls
{
    public class PercentageCropMvxAppCompatImageView : MvxAppCompatImageView
    {
        private float _cropXCenterOffsetPct = 0.5f;
        private float _cropYCenterOffsetPct = 0.3f;

        public PercentageCropMvxAppCompatImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public PercentageCropMvxAppCompatImageView(Context context) : base(context)
        {
        }

        public PercentageCropMvxAppCompatImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public float GetCropYCenterOffsetPct()
        {
            return _cropYCenterOffsetPct;
        }

        public void SetCropYCenterOffsetPct(float cropYCenterOffsetPct)
        {
            if (cropYCenterOffsetPct > 1.0)
            {
                throw new ArgumentException("Value too large: Must be <= 1.0");
            }
            _cropYCenterOffsetPct = cropYCenterOffsetPct;
        }

        public float GetCropXCenterOffsetPct()
        {
            return _cropXCenterOffsetPct;
        }

        public void SetCropXCenterOffsetPct(float cropXCenterOffsetPct)
        {
            if (cropXCenterOffsetPct > 1.0)
            {
                throw new ArgumentException("Value too large: Must be <= 1.0");
            }
            _cropXCenterOffsetPct = cropXCenterOffsetPct;
        }

        private void MyConfigureBounds()
        {
            if (GetScaleType() == ScaleType.Matrix)
            {
                var d = Drawable;
                if (d != null)
                {
                    var dwidth = d.IntrinsicWidth;
                    var dheight = d.IntrinsicHeight;

                    var m = new Matrix();

                    var vwidth = Width - PaddingLeft - PaddingRight;
                    var vheight = Height - PaddingTop - PaddingBottom;

                    float scale;
                    float dx = 0, dy = 0;

                    if (dwidth*vheight > vwidth*dheight)
                    {
                        var cropXCenterOffsetPct = _cropXCenterOffsetPct;
                        scale = vheight/(float) dheight;
                        dx = (vwidth - dwidth*scale)*cropXCenterOffsetPct;
                    }
                    else
                    {
                        var cropYCenterOffsetPct = _cropYCenterOffsetPct;
                        scale = vwidth/(float) dwidth;
                        dy = (vheight - dheight*scale)*cropYCenterOffsetPct;
                    }

                    m.SetScale(scale, scale);
                    m.PostTranslate((int) (dx + 0.5f), (int) (dy + 0.5f));

                    ImageMatrix = m;
                }
            }
        }

        // These 3 methods call configureBounds in ImageView.java class, which
        // adjusts the matrix in a call to center_crop (android's built-in 
        // scaling and centering crop method). We also want to trigger
        // in the same place, but using our own matrix, which is then set
        // directly at line 588 of ImageView.java and then copied over
        // as the draw matrix at line 942 of ImageVeiw.java
        protected override bool SetFrame(int l, int t, int r, int b)
        {
            var changed = base.SetFrame(l, t, r, b);
            MyConfigureBounds();
            return changed;
        }

        public override void SetImageDrawable(Drawable d)
        {
            base.SetImageDrawable(d);
            MyConfigureBounds();
        }

        public override void SetImageResource(int resId)
        {
            base.SetImageResource(resId);
            MyConfigureBounds();
        }

        public void Redraw()
        {
            if (Drawable != null)
            {
                // Force toggle to recalculate our bounds
                SetImageDrawable(null);
                SetImageDrawable(Drawable);
            }
        }
    }
}