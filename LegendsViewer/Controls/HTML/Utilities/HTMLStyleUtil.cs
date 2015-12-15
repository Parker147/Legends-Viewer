namespace LegendsViewer.Controls.HTML.Utilities
{
    public static class HTMLStyleUtil
    {
        public const string SYMBOL_POPULATION   = "<span class=\"legends_symbol_population\">&#9823;</span>";
        public const string SYMBOL_SITE         = "<span class=\"legends_symbol_site\">&#9978;</span>";
        public const string SYMBOL_DEAD         = "<span class=\"legends_symbol_dead\">&#10013;</span>";

        public static string CurrentDwarfObject(string name)
        {
            return "<span class=\"legends_current_dwarfobject\">" + name + "</span>";
        }
    }
}
