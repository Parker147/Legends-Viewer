using System;
using System.Collections.Generic;

namespace LegendsViewer.Legends.Parser
{
    public class Property
    {
        public string Name = "";
        private string _value = "";
        public bool Known = false;
        public List<Property> SubProperties = new List<Property>();
        public string Value { get { Known = true; return _value; } set { _value = value; } }

        public int ValueAsInt()
        {
            return Convert.ToInt32(Value);
        }
    }
}