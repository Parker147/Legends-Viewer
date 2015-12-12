namespace LegendsViewer.Legends
{
    public abstract class DwarfObject
    {
        public virtual string ToLink(bool link = true, DwarfObject pov = null)
        {
            return "";
        }
        public virtual string Print(bool link = true)
        {
            return "";
        }
    }
}
