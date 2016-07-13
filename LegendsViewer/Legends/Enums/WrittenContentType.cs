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
        CulturalHistory,
        [Description("Comparative Biography")]
        ComparativeBiography,
        [Description("Cultural Comparison")]
        CulturalComparison,
        Atlas,
        [Description("Treatise On Technological Evolution")]
        TreatiseOnTechnologicalEvolution,
        [Description("Alternate History")]
        AlternateHistory,
        [Description("Star Catalogue")]
        StarCatalogue,
        Dictionary,
        Genealogy,
        Encyclopedia,
        [Description("Biographical Dictionary")]
        BiographicalDictionary
    }
}
