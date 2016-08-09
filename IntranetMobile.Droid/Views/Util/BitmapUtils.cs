using Android.Graphics;
using Android.Graphics.Drawables;

namespace IntranetMobile.Droid.Views.Util
{
    public static class BitmapUtils
    {
        public static Bitmap DrawableToBitmap(Drawable drawable)
        {
            var bitmapDrawable = drawable as BitmapDrawable;
            if (bitmapDrawable != null)
            {
                return bitmapDrawable.Bitmap;
            }

            if (drawable.IntrinsicHeight == -1 || drawable.IntrinsicHeight == -1)
            {
                return null;
            }

            var bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }
    }
}