using MvvmCross.Platform.Plugins;

namespace IntranetMobile.Droid.Bootstrap
{
    public class DownloadCachePluginBootstrap
        : MvxPluginBootstrapAction<MvvmCross.Plugins.DownloadCache.PluginLoader>
    {
        protected override void Load(IMvxPluginManager manager)
        {
            MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
            base.Load(manager);
        }
    }
}