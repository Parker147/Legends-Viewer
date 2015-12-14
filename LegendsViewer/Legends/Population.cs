namespace LegendsViewer.Legends
{
    public class Population
    {
        public bool IsMainRace
        {
            get
            {
                return Race == "Dwarves" || Race == "Humans" || Race == "Elves" || Race == "Goblins" || Race == "Kobolds" ||
                       Race == "Dwarf Outcasts" || Race == "Human Outcasts" || Race == "Elf Outcasts" || Race == "Goblin Outcasts" || Race == "Kobold Outcasts" ||
                       Race == "Dwarf Prisoners" || Race == "Human Prisoners" || Race == "Elf Prisoners" || Race == "Goblin Prisoners" || Race == "Kobold Prisoners";
            }
        }
        public string Race { get; set; }
        public int Count { get; set; }
        public Population(string type, int count) { Race = type; Count = count; }
    }
}
