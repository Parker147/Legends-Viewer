using System;

namespace LegendsViewer
{
    public static class LocalFileProvider
    {

        public static readonly string LocalPrefix = "";
        public static readonly string RootFolder = "";

        static LocalFileProvider()
        {
            RootFolder = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            LocalPrefix = "file:///" + RootFolder.Replace("\\", "/") + "/";
        }
    }
}
