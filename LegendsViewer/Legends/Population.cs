namespace LegendsViewer.Legends
{
    public class Population
    {
        public bool IsMainRace
        {
            get
            {
                return World.MainRaces.ContainsKey(Race);
            }
        }

        public bool IsOutcasts
        {
            get
            {
                return Race.Contains("Outcasts");
            }
        }

        public bool IsPrisoners
        {
            get
            {
                return Race.Contains("Prisoners");
            }
        }

        public bool IsSlaves
        {
            get
            {
                return Race.Contains("Slaves");
            }
        }

        public bool IsAnimalPeople
        {
            get
            {
                return Race.Contains(" Men") && !IsSlaves && !IsPrisoners && !IsOutcasts;
            }
        }

        public string Race { get; set; }
        public int Count { get; set; }
        public Population(string type, int count) { Race = type; Count = count; }
    }
}
