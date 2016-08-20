using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;

namespace Intranet.WindowsUWP
{
    public class WindowsSqliteConnectionFactory : MvxSqliteConnectionFactoryBase
    {
        public override string GetPlattformDatabasePath(string databaseName)
        {
            return Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, databaseName);
        }

        public override ISQLitePlatform CurrentPlattform { get; } = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
    }
}
