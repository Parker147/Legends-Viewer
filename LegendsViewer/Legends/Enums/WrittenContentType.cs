using System.ComponentModel;

namespace LegendsViewer.Legends.Enums
{
    public enum WrittenContentType
    {
        Unknown,
        Autobiography,
        Biography,
        Chronicle,
        Dialog,
        Essay,
        Guide,
        Letter,
        Manual,
        Novel,
        Play,
        Poem,
        [Description("Short Story")]
        ShortStory,
        Choreography,
        [Description("Musical Composition")]
        MusicalComposition,
        [Description("Star Chart")]
        StarChart,
        [Description("Cultural History")]
        CulturalHistory
    }
}
