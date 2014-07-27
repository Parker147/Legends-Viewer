using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace LegendsViewer.Legends
{
    class WorldPlusParser : XMLParser
    {
        private Boolean inMiddleOfSection = false;
        private List<Property> CurrentItem;

        public WorldPlusParser(World world, String xmlFile) : base(xmlFile)
        {
            this.World = world;
            StreamReader reader = new  StreamReader(xmlFile, Encoding.GetEncoding("windows-1252"));
            XML = new XmlTextReader(reader);
            XML.WhitespaceHandling = WhitespaceHandling.Significant;
            Parse();
        }

        new public void Parse()
        {
            if (XML.ReadState == ReadState.Closed)
                return;

            while (!XML.EOF && CurrentItem == null)
            {

                if (!inMiddleOfSection)
                    CurrentSection = GetSectionType(XML.Name);
                if (CurrentSection == Section.Junk)
                {
                    XML.Read();
                }
                else if (CurrentSection == Section.Unknown)
                    SkipSection();
                else
                    ParseSection();
            }

            if (XML.EOF)
            {
                XML.Close();
            }
        }

        private void ParseSection()
        {
            
            while (XML.NodeType == XmlNodeType.EndElement || XML.NodeType == XmlNodeType.None)
            {
                if (XML.NodeType == XmlNodeType.None)
                    return;
                XML.ReadEndElement();
            }

            if (!inMiddleOfSection)
            {
                XML.ReadStartElement();
                inMiddleOfSection = true;
            }

            CurrentItem = ParseItem();

            if (XML.NodeType == XmlNodeType.EndElement)
            {
                XML.ReadEndElement();
                inMiddleOfSection = false;
            }
        }

        public void AddNewProperties(List<Property> existingProperties, Section xmlParserSection)
        {
            if (CurrentItem == null)
                return;

            if (xmlParserSection > this.CurrentSection)
            {
                while (xmlParserSection > this.CurrentSection)
                {
                    AddItemToWorld(CurrentItem);
                    CurrentItem = null;
                    Parse();
                }
            }
           
            if (xmlParserSection < this.CurrentSection)
            {
                return;
            }

            Property id = existingProperties.SingleOrDefault(property => property.Name == "id");
            Property currentId = CurrentItem.SingleOrDefault(property => property.Name == "id");
            if (id != null && currentId != null && id.ValueAsInt().Equals(currentId.ValueAsInt()))
            {
                foreach (var property in CurrentItem)
                {
                    Property matchingProperty = existingProperties.SingleOrDefault(p => p.Name == property.Name);
                    if (CurrentSection == Section.Events && matchingProperty!= null && matchingProperty.Name == "type")
                    {
                        continue;
                    }

                    if (matchingProperty != null)
                    {
                        matchingProperty.Value = property.Value;
                        matchingProperty.Known = false;
                    }
                    else
                    {
                        existingProperties.Add(property);
                    }
                }
                CurrentItem = null;
                Parse();
            }
        }
    }
}
