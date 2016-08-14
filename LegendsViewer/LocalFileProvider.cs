using System;

namespace LegendsViewer
{
    public static class LocalFileProvider
    {

        public static string LocalPrefix = "";

        static LocalFileProvider()
        {
            LocalPrefix = "file:///" + AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\').Replace("\\", "/") + "/";
        }
    }
}
