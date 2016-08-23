using Android.Content;
using Android.Util;

namespace IntranetMobile.Droid.Views.Util
{
    public static class DisplayUtils
    {
        /// <summary>
        ///     This method converts dp unit to equivalent pixels, depending on device density.
        ///     <param name="dp">dp A value in dp (density independent pixels) unit. Which we need to convert into pixels</param>
        ///     <param name="context">Context to get resources and device specific display metrics</param>
        ///     <returns>A float value to represent px equivalent to dp depending on device density</returns>
        /// </summary>
        public static float ConvertDpToPixel(float dp, Context context)
        {
            var resources = context.Resources;
            var metrics = resources.DisplayMetrics;
            var px = dp*((float) metrics.DensityDpi/(float) DisplayMetricsDensity.Default);
            return px;
        }

        /// <summary>
        ///     This method converts device specific pixels to density independent pixels.
        ///     <param name="px">A value in px (pixels) unit. Which we need to convert into db</param>
        ///     <param name="context">Context to get resources and device specific display metrics</param>
        ///     <returns>A float value to represent dp equivalent to px value</returns>
        /// </summary>
        public static float ConvertPixelsToDp(float px, Context context)
        {
            var resources = context.Resources;
            var metrics = resources.DisplayMetrics;
            var dp = px/((float) metrics.DensityDpi/(float) DisplayMetricsDensity.Default);
            return dp;
        }
    }
}