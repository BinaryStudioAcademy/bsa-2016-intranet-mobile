using MvvmCross.Platform.Plugins;
using MvvmCross.Plugins.DownloadCache;

namespace IntranetMobile.Droid.Bootstrap
{
    public class DownloadCachePluginBootstrap
        : MvxPluginBootstrapAction<PluginLoader>
    {
        protected override void Load(IMvxPluginManager manager)
        {
            MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
            base.Load(manager);
        }
    }
}