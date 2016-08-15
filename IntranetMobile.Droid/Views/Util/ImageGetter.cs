using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Object = Java.Lang.Object;

namespace IntranetMobile.Droid.Views.Util
{
    public class ImageGetter : Object, Html.IImageGetter
    {
        public Drawable GetDrawable(string source)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(new Uri(source)).Result;

                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            using (var memStream = new MemoryStream())
                            {
                                stream.CopyToAsync(memStream).Wait();
                                memStream.Position = 0;
                                var bitmap = BitmapFactory.DecodeStream(memStream);
                                var bitmapDrawable = new BitmapDrawable(bitmap);
                                bitmapDrawable.SetBounds(0, 0, bitmap.Width, bitmap.Height);
                                return bitmapDrawable;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // TODO: Log here
                }
            }
            return null;
        }
    }
}