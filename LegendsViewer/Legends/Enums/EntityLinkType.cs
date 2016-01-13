using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum EntityLinkType
    {
        Unknown,
        Criminal,
        Enemy,
        Member,
        [Description("Former Member")]
        FormerMember,
        Position,
        [Description("Former Position")]
        FormerPosition,
        Prisoner,
        [Description("Former Prisoner")]
        FormerPrisoner,
        [Description("Former Slave")]
        FormerSlave,
        Slave,
        [Description("Respected for heroic acts")]
        Hero,
    }
}