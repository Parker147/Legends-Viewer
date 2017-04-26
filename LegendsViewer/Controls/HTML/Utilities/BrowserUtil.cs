using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace LegendsViewer.Controls.HTML.Utilities
{
    public static class BrowserUtil
    {
        /// <summary>
        /// WebBrowser Control is as default rendered with IE7 rendering engine
        /// Setting FEATURE_BROWSER_EMULATION Registry Key of the process to MODE sets it to rendering mode IE10
        /// </summary>
        public static void SetBrowserEmulationMode()
        {
            try
            {
                var appName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

                if (String.Compare(appName, "devenv.exe", StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(appName, "XDesProc.exe", StringComparison.OrdinalIgnoreCase) == 0)
                    return;
                const uint MODE = 10000;
                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION\",
                    RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key != null)
                    {
                        key.SetValue(appName, MODE, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
