using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum Claim
    {
        Unknown,
        [Description("symbol")]
        Symbol,
        [Description("family heirloom")]
        Heirloom,
        [Description("treasure")]
        Treasure
    }
}
