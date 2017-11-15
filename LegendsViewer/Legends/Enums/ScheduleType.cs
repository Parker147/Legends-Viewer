using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum ScheduleType
    {
        Unknown,
        Procession,
        Ceremony,
        [Description("Foot Race")]
        FootRace,
        [Description("Throwing Competition")]
        ThrowingCompetition,
        [Description("Dance Performance")]
        DancePerformance,
        Storytelling,
        [Description("Poetry Recital")]
        PoetryRecital,
        [Description("Musical Performance")]
        MusicalPerformance,
        [Description("Wrestling Competition")]
        WrestlingCompetition,
        [Description("Gladiatory Competition")]
        GladiatoryCompetition,
        [Description("Poetry Competition")]
        PoetryCompetition,
        [Description("Dance Competition")]
        DanceCompetition,
        [Description("Musical Competition")]
        MusicalCompetition
    }
}
