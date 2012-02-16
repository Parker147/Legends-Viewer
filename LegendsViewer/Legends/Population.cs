using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class Population
    {
        public string Race { get; set; }
        public int Count { get; set; }
        public Population(string type, int count) { Race = type; Count = count; }
    }
}
