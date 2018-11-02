using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class Skill
    {
        private static readonly int[] PointCutoffs = { 29000, 26600, 24300, 22100, 20000, 18000, 16100, 14300, 12600, 11000, 9500, 8100,
                    6800, 5600, 4500, 3500, 2600, 1800, 1100, 500, 1 };
        private static readonly string[] Titles = {
                    "Legendary+5", "Legendary+4", "Legendary+3", "Legendary+2", "Legendary+1", "Legendary",
                    "Grand Master", "High Master", "Master", "Great", "Accomplished", "Professional",
                    "Expert", "Adept", "Talented", "Proficient", "Skilled", "Competent", "Adequate", "Novice", "Dabbling" };

        public string Name { get; set; }
        public int Points { get; set; }

        public string Rank
        {
            get
            {
                for (var i = 0; i < PointCutoffs.Length; i++)
                {
                    if (Points >= PointCutoffs[i])
                    {
                        return Titles[i];
                    }
                }
                return "Unknown";
            }
        }
        public Skill(List<Property> properties)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "skill":
                        Name = string.Intern(Formatting.InitCaps(property.Value.Replace('_', ' ').ToLower()));
                        break;
                    case "total_ip":
                        Points = Convert.ToInt32(property.Value);
                        break;
                }
            }
        }
    }
}