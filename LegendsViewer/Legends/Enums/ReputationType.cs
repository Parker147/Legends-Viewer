using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum ReputationType
    {
        Unknown,
        [Description("Enemy Fighter")]
        EnemyFighter,
        [Description("Trade Partner")]
        TradePartner,
        Killer,
        Poet,
        Bard,
        Storyteller,
        Dancer,
        Friendly,
        Buddy,
        Grudge,
        Bonded,
        Quarreler,
        Psychopath,
        [Description("Loyal Soldier")]
        LoyalSoldier
    }
}
