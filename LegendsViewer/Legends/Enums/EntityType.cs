using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum EntityType // legends_plus.xml
    {
        Unknown,
        Civilization,
        [Description("Nomadic Group")]
        NomadicGroup,
        [Description("Migrating Group")]
        MigratingGroup,
        [Description("Outcasts")]
        Outcast,
        [Description("Religious Group")]
        Religion,
        [Description("Site Government")]
        SiteGovernment,
        [Description("Performance Troupe")]
        PerformanceTroupe,
    }
}