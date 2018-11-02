using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum SiteConqueredType
    {
        Unknown,
        Pillaging,
        Destruction,
        Conquest,
        [Description("Tribute Enforcement")]
        TributeEnforcement,
        Invasion
    }
}