using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum BattleOutcome
    {
        Unknown,
        [Description("Attacker Won")]
        AttackerWon,
        [Description("Defender Won")]
        DefenderWon
    }
}