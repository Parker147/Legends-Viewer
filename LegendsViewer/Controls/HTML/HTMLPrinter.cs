using System;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using System.Drawing.Imaging;

namespace LegendsViewer.Controls
{
    public abstract class HTMLPrinter
    {
        protected StringBuilder HTML;
        protected const string LineBreak = "</br>";
        protected const string ListItem = "<li>";
        public abstract string GetTitle();
        public abstract string Print();

        public static string LegendsCSS;
        public static string ChartJS;

        public static HTMLPrinter GetPrinter(object printObject, World world)
        {
            Type printType = printObject.GetType();
            if (printType == typeof(Battle))
                return new BattlePrinter(printObject as Battle, world);
            if (printType == typeof(BeastAttack))
                return new BeastAttackPrinter(printObject as BeastAttack, world);
            if (printType == typeof(Entity))
                return new EntityPrinter(printObject as Entity, world);
            if (printType == typeof(Era))
                return new EraPrinter(printObject as Era);
            if (printType == typeof(HistoricalFigure))
                return new HistoricalFigureHTMLPrinter(printObject as HistoricalFigure, world);
            if (printType == typeof(WorldRegion))
                return new RegionPrinter(printObject as WorldRegion, world);
            if (printType == typeof(SiteConquered))
                return new SiteConqueredPrinter(printObject as SiteConquered, world);
            if (printType == typeof(Site))
                return new SitePrinter(printObject as Site, world);
            if (printType == typeof(UndergroundRegion))
                return new UndergroundRegionPrinter(printObject as UndergroundRegion, world);
            if (printType == typeof(War))
                return new WarPrinter(printObject as War, world);
            if (printType == typeof(World))
                return new WorldStatsPrinter(world);
            if (printType == typeof(Artifact))
                return new ArtifactPrinter(printObject as Artifact);

            if (printType == typeof(string))
                return new StringPrinter(printObject as string);

            throw new Exception("No HTML Printer for type: " + printObject.GetType().ToString());
        }

        public string GetHTMLPage()
        {
            var htmlPage = new StringBuilder();
            htmlPage.Append("<!DOCTYPE html><html><head>");
            htmlPage.Append("<title>" + GetTitle() + "</title>");
            htmlPage.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            htmlPage.Append(GetStyle());
            htmlPage.Append("</head>");
            htmlPage.Append("<body>" + Print() + "</body>");
            htmlPage.Append("</html>");
            return htmlPage.ToString();
        }

        public string GetStyle()
        {
            if (string.IsNullOrWhiteSpace(LegendsCSS))
            {
                return "<style type=\"text/css\">"
                    + "body {font-size: 0.8em;}"
                    + "a:link {text-decoration: none; color: #000000}"
                    + "a:visited {text-decoration: none; color: #000000}"
                    + "a:hover {text-decoration: underline; color: #000000}"
                    + "td {font-size: 13px;}"
                    + "ol { margin-top: 0;}"
                    + "ul { margin-top: 0;}"
                    + "img {border:none;}"
                    + "h1 {font-size: 18px;font-weight: bold;margin-top: 0px;margin-bottom: 0px;}"
                    + "h2 {font-size: 16px;font-weight: normal;margin-top: 0px;margin-bottom: 0px;}"
                    + "h3 {font-size: 14px;font-weight: normal;margin-top: 0px;margin-bottom: 0px;margin-left: 5px;}"
                    + "</style>";
            }
            else
            {
                return "<style type=\"text/css\">"+ LegendsCSS + "</style>";
            }
        }

        protected static string Bold(string text)
        {
            return "<b>" + text + "</b>";
        }

        protected static string Font(string text, string color)
        {
            return "<font color=\"" + color + "\">" + text + "</font>";
        }

        protected enum ListType
        {
            Unordered,
            Ordered
        }

        protected void StartList(ListType listType)
        {
            switch (listType)
            {
                case ListType.Ordered:
                    HTML.AppendLine("<ol>"); break;
                case ListType.Unordered:
                    HTML.AppendLine("<ul>"); break;
            }
        }

        protected void EndList(ListType listType)
        {
            switch (listType)
            {
                case ListType.Ordered:
                    HTML.AppendLine("</ol>"); break;
                case ListType.Unordered:
                    HTML.AppendLine("</ul>"); break;
            }
        }

        protected string MakeLink(string text, LinkOption option)
        {
            return "<a href=\"" + option.ToString() + "\">" + text + "</a>";
        }

        protected string MakeLink(string text, DwarfObject dObject, ControlOption option = ControlOption.HTML)
        {
            //<a href=\"collection#" + attack.ID + "\">" + attack.GetOrdinal(attack.Ordinal)
            string objectType = "";
            int id = 0;
            if (dObject is EventCollection)
            {
                objectType = "collection";
                id = (dObject as EventCollection).ID;
            }
            else if (dObject.GetType() == typeof(HistoricalFigure))
            {
                objectType = "hf";
                id = (dObject as HistoricalFigure).ID;
            }
            else if (dObject.GetType() == typeof(Entity))
            {
                objectType = "entity";
                id = (dObject as Entity).ID;
            }
            else if (dObject.GetType() == typeof(WorldRegion))
            {
                objectType = "region";
                id = (dObject as WorldRegion).ID;
            }
            else if (dObject.GetType() == typeof(UndergroundRegion))
            {
                objectType = "uregion";
                id = (dObject as UndergroundRegion).ID;
            }
            else if (dObject.GetType() == typeof(Site))
            {
                objectType = "site";
                id = (dObject as Site).ID;
            }
            else throw new Exception("Unhandled make link for type: " + dObject.GetType());
            string optionString = "";
            if (option != ControlOption.HTML)
                optionString = "-" + option.ToString();
            return "<a href=\"" + objectType + "#" + id + optionString + "\">" + text + "</a>";
        }

        protected string BitmapToHTML(Bitmap image)
        {
            int imageSectionCount = 5;
            Size imageSectionSize = new Size(image.Width / imageSectionCount, image.Height / imageSectionCount);
            string html = "";
            for (int row = 0; row < imageSectionCount; row++)
            {
                for (int column = 0; column < imageSectionCount; column++)
                {
                    using (Bitmap section = new Bitmap(imageSectionSize.Width, imageSectionSize.Height))
                    {
                        using (Graphics drawSection = Graphics.FromImage(section))
                        {
                            drawSection.DrawImage(image, new Rectangle(new Point(0, 0), section.Size), new Rectangle(new Point(section.Size.Width * column, section.Size.Height * row), section.Size), GraphicsUnit.Pixel);
                            html += StringToImageHTML(BitmapToString(section));
                        }
                    }
                }
                html += "</br>";
            }
            image.Dispose();
            return html;
        }


        protected string BitmapToString(Bitmap image)
        {
            string imageString;
            using (System.IO.MemoryStream miniStream = new System.IO.MemoryStream())
            {
                image.Save(miniStream, ImageFormat.Bmp);
                byte[] miniMapBytes = miniStream.GetBuffer();
                imageString = Convert.ToBase64String(miniMapBytes);
            }

            return imageString;
        }

        protected string StringToImageHTML(string image)
        {
            string html = "<img src=\"data:image/gif;base64,";
            html += image;
            html += "\" align=absmiddle />";
            return html;
        }

        protected string SkillToString(Skill skill)
        {
            SkillDescription desc = SkillDictionary.lookupSkill(skill);
            string rank = skill.Rank.ToLower().Replace(" ", string.Empty).Substring(0, 5);

            return
                "<li class='" + desc.Category
                + " " + rank
                + "' title='" + desc.Token
                + " | " + skill.Rank
                + " | " + skill.Points
                + "'>" + desc.Name + "</li>";
        }
    }

    public enum LinkOption
    {
        LoadHFKills,
        LoadHFBattles,
        LoadSiteBattles,
        LoadSiteDeaths,
        LoadRegionBattles,
        LoadRegionDeaths,
        LoadEntityWars,
        LoadEntitySites,
        LoadEntityLeaders,
        LoadWarBattles,
        LoadBattleAttackers,
        LoadBattleDefenders,
        LoadMap,
        LoadChart,
        LoadSearch
    }

    public enum ControlOption
    {
        HTML,
        Map,
        Chart,
        Search
    }

    public class TableMaker
    {
        StringBuilder HTML;
        bool Numbered;
        int count;
        public TableMaker(bool numbered = false, int width = 0)
        {
            HTML = new StringBuilder();
            string tableStart = "<table border=\"0\"";
            if (width > 0)
                tableStart += " width=\"" + width + "\"";
            tableStart += ">";
            HTML.AppendLine(tableStart);
            Numbered = numbered;
            count = 1;
        }

        public void StartRow()
        {
            HTML.AppendLine("<tr>");
            if (Numbered)
            {
                AddData(count.ToString(), 20, TableDataAlign.Right);
                AddData("", 10);
            }
        }

        public void EndRow()
        {
            HTML.AppendLine("</tr>");
            count++;
        }

        public void AddData(string data, int width = 0, TableDataAlign align = TableDataAlign.Left)
        {
            string dataHTML = "<td";
            if (width > 0)
                dataHTML += " width=\"" + width + "\"";
            if (align != TableDataAlign.Left)
            {
                dataHTML += " align=";
                switch (align)
                {
                    case TableDataAlign.Right:
                        dataHTML += "\"right\""; break;
                    case TableDataAlign.Center:
                        dataHTML += "\"center\""; break;
                }
            }
            dataHTML += ">";
            dataHTML += data + "</td>";
            HTML.AppendLine(dataHTML);
        }

        public string GetTable()
        {
            HTML.AppendLine("</table>");
            return HTML.ToString();
        }
    }


    public enum TableDataAlign
    {
        Left,
        Right,
        Center
    }

    public class SkillDescription
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public SkillDescription(string category, string token, string name, string desc)
        {
            Category = category;
            Token = token;
            Name = name;
            Description = desc;
        }
    }

    public static class SkillDictionary
    {
        public static readonly SkillDescription[] skills = new SkillDescription[]
        {
            new SkillDescription("min", "MINING", "Miner", "Mining"),
            new SkillDescription("wod", "WOODCUTTING", "Wood cutter", "Wood Cutting"),
            new SkillDescription("wod", "CARPENTRY", "Carpenter", "Carpentry"),
            new SkillDescription("stn", "DETAILSTONE", "Engraver", "Engraving"),
            new SkillDescription("stn", "MASONRY", "Mason", "Masonry"),
            new SkillDescription("hnt", "ANIMALTRAIN", "Animal trainer", "Animal Training"),
            new SkillDescription("hnt", "ANIMALCARE", "Animal caretaker", "Animal Caretaking"),
            new SkillDescription("fsh", "DISSECT_FISH", "Fish dissector", "Fish Dissection"),
            new SkillDescription("hnt", "DISSECT_VERMIN", "Animal dissector", "Animal Dissection"),
            new SkillDescription("fsh", "PROCESSFISH", "Fish cleaner", "Fish Cleaning"),
            new SkillDescription("but", "BUTCHER", "Butcher", "Butchery"),
            new SkillDescription("hnt", "TRAPPING", "Trapper", "Trapping"),
            new SkillDescription("but", "TANNER", "Tanner", "Tanning"),
            new SkillDescription("crf", "WEAVING", "Weaver", "Weaving"),
            new SkillDescription("cok", "BREWING", "Brewer", "Brewing"),
            new SkillDescription("msc", "ALCHEMY", "Alchemist", "Alchemy"),
            new SkillDescription("crf", "CLOTHESMAKING", "Clothier", "Clothes Making"),
            new SkillDescription("frm", "MILLING", "Miller", "Milling"),
            new SkillDescription("plt", "PROCESSPLANTS", "Thresher", "Threshing"),
            new SkillDescription("mlk", "CHEESEMAKING", "Cheese maker", "Cheese Making"),
            new SkillDescription("mlk", "MILK", "Milker", "Milking"),
            new SkillDescription("cok", "COOK", "Cook", "Cooking"),
            new SkillDescription("frm", "PLANT", "Grower", "Growing"),
            new SkillDescription("plt", "HERBALISM", "Herbalist", "Herbalism"),
            new SkillDescription("fsh", "FISH", "Fisherman", "Fishing"),
            new SkillDescription("mtl", "SMELT", "Furnace operator", "Furnace Operation"),
            new SkillDescription("crf", "EXTRACT_STRAND", "Strand extractor", "Strand Extraction"),
            new SkillDescription("mtl", "FORGE_WEAPON", "Weaponsmith", "Weaponsmithing"),
            new SkillDescription("mtl", "FORGE_ARMOR", "Armorsmith", "Armorsmithing"),
            new SkillDescription("mtl", "FORGE_FURNITURE", "Metalsmith", "Metalsmithing"),
            new SkillDescription("gem", "CUTGEM", "Gem cutter", "Gem Cutting"),
            new SkillDescription("gem", "ENCRUSTGEM", "Gem setter", "Gem Setting"),
            new SkillDescription("wod", "WOODCRAFT", "Wood crafter", "Wood Crafting"),
            new SkillDescription("stn", "STONECRAFT", "Stone crafter", "Stone Crafting"),
            new SkillDescription("mtl", "METALCRAFT", "Metal crafter", "Metal Crafting"),
            new SkillDescription("gls", "GLASSMAKER", "Glassmaker", "Glassmaking"),
            new SkillDescription("but", "LEATHERWORK", "Leatherworker", "Leatherworkering[sic]"),
            new SkillDescription("but", "BONECARVE", "Bone carver", "Bone Carving"),
            new SkillDescription("mil", "AXE", "Axeman", "Axe"),
            new SkillDescription("mil", "SWORD", "Swordsman", "Sword"),
            new SkillDescription("mil", "DAGGER", "Knife user", "Knife"),
            new SkillDescription("mil", "MACE", "Maceman", "Mace"),
            new SkillDescription("mil", "HAMMER", "Hammerman", "Hammer"),
            new SkillDescription("mil", "SPEAR", "Spearman", "Spear"),
            new SkillDescription("mil", "CROSSBOW", "Crossbowman", "Crossbow"),
            new SkillDescription("mil", "SHIELD", "Shield user", "Shield"),
            new SkillDescription("mil", "ARMOR", "Armor user", "Armor"),
            new SkillDescription("sig", "SIEGECRAFT", "Siege engineer", "Siege Engineering"),
            new SkillDescription("sig", "SIEGEOPERATE", "Siege operator", "Siege Operation"),
            new SkillDescription("wod", "BOWYER", "Bowyer", "Bowmaking"),
            new SkillDescription("mil", "PIKE", "Pikeman", "Pike"),
            new SkillDescription("mil", "WHIP", "Lasher", "Lash"),
            new SkillDescription("mil", "BOW", "Bowman", "Bow"),
            new SkillDescription("mil", "BLOWGUN", "Blowgunner", "Blowgun"),
            new SkillDescription("mil", "THROW", "Thrower", "Throwing"),
            new SkillDescription("eng", "MECHANICS", "Mechanic", "Machinery"),
            new SkillDescription("msc", "MAGIC_NATURE", "Druid", "Nature"),
            new SkillDescription("hnt", "SNEAK", "Ambusher", "Ambush"),
            new SkillDescription("eng", "DESIGNBUILDING", "Building designer", "Building Design"),
            new SkillDescription("doc", "DRESS_WOUNDS", "Wound dresser", "Wound Dressing"),
            new SkillDescription("doc", "DIAGNOSE", "Diagnostician", "Diagnostics"),
            new SkillDescription("doc", "SURGERY", "Surgeon", "Surgery"),
            new SkillDescription("doc", "SET_BONE", "Bone doctor", "Bone Setting"),
            new SkillDescription("doc", "SUTURE", "Suturer", "Suturing"),
            new SkillDescription("msc", "CRUTCH_WALK", "Crutch-walker", "Crutch-walking"),
            new SkillDescription("cem", "WOOD_BURNING", "Wood burner", "Wood Burning"),
            new SkillDescription("cem", "LYE_MAKING", "Lye maker", "Lye Making"),
            new SkillDescription("cem", "SOAP_MAKING", "Soaper", "Soap Making"),
            new SkillDescription("cem", "POTASH_MAKING", "Potash maker", "Potash Making"),
            new SkillDescription("crf", "DYER", "Dyer", "Dyeing"),
            new SkillDescription("pmp", "OPERATE_PUMP", "Pump operator", "Pump Operation"),
            new SkillDescription("msc", "SWIMMING", "Swimmer", "Swimming"),
            new SkillDescription("soc", "PERSUASION", "Persuader", "Persuasion"),
            new SkillDescription("soc", "NEGOTIATION", "Negotiator", "Negotiation"),
            new SkillDescription("soc", "JUDGING_INTENT", "Judge of intent", "Judging Intent"),
            new SkillDescription("soc", "APPRAISAL", "Appraiser", "Appraisal"),
            new SkillDescription("soc", "ORGANIZATION", "Organizer", "Organization"),
            new SkillDescription("soc", "RECORD_KEEPING", "Record keeper", "Record Keeping"),
            new SkillDescription("soc", "LYING", "Liar", "Lying"),
            new SkillDescription("soc", "INTIMIDATION", "Intimidator", "Intimidation"),
            new SkillDescription("soc", "CONVERSATION", "Conversationalist", "Conversation"),
            new SkillDescription("soc", "COMEDY", "Comedian", "Comedy"),
            new SkillDescription("soc", "FLATTERY", "Flatterer", "Flattery"),
            new SkillDescription("soc", "CONSOLE", "Consoler", "Consoling"),
            new SkillDescription("soc", "PACIFY", "Pacifier", "Pacification"),
            new SkillDescription("hnt", "TRACKING", "Tracker", "Tracking"),
            new SkillDescription("soc", "KNOWLEDGE_ACQUISITION", "Student", "Studying"),
            new SkillDescription("msc", "CONCENTRATION", "Concentration", "Concentration"),
            new SkillDescription("mil", "DISCIPLINE", "Discipline", "Discipline"),
            new SkillDescription("msc", "SITUATIONAL_AWARENESS", "Observer", "Observation"),
            new SkillDescription("soc", "WRITING", "Wordsmith", "Writing"),
            new SkillDescription("soc", "PROSE", "Writer", "Prose"),
            new SkillDescription("soc", "POETRY", "Poet", "Poetry"),
            new SkillDescription("soc", "READING", "Reader", "Reading"),
            new SkillDescription("soc", "SPEAKING", "Speaker", "Speaking"),
            new SkillDescription("msc", "COORDINATION", "Coordination", "Coordination"),
            new SkillDescription("msc", "BALANCE", "Balance", "Balance"),
            new SkillDescription("soc", "LEADERSHIP", "Leader", "Leadership"),
            new SkillDescription("soc", "TEACHING", "Teacher", "Teaching"),
            new SkillDescription("mil", "MELEE_COMBAT", "Fighter", "Fighting"),
            new SkillDescription("mil", "RANGED_COMBAT", "Archer", "Archery"),
            new SkillDescription("mil", "WRESTLING", "Wrestler", "Wrestling"),
            new SkillDescription("mil", "BITE", "Biter", "Biting"),
            new SkillDescription("mil", "GRASP_STRIKE", "Striker", "Striking"),
            new SkillDescription("mil", "STANCE_STRIKE", "Kicker", "Kicking"),
            new SkillDescription("mil", "DODGING", "Dodger", "Dodging"),
            new SkillDescription("mil", "MISC_WEAPON", "Misc. object user", "Misc. Object"),
            new SkillDescription("msc", "KNAPPING", "Knapper", "Knapping"),
            new SkillDescription("mil", "MILITARY_TACTICS", "Military tactics", "Military Tactics"),
            new SkillDescription("crf", "SHEARING", "Shearer", "Shearing"),
            new SkillDescription("crf", "SPINNING", "Spinner", "Spinning"),
            new SkillDescription("pot", "POTTERY", "Potter", "Pottery"),
            new SkillDescription("pot", "GLAZING", "Glazer", "Glazing"),
            new SkillDescription("frm", "PRESSING", "Presser", "Pressing"),
            new SkillDescription("bee", "BEEKEEPING", "Beekeeper", "Beekeeping"),
            new SkillDescription("bee", "WAX_WORKING", "Wax worker", "Wax Working"),
            new SkillDescription("msc", "CLIMBING", "Climber", "Climbing"),
            new SkillDescription("hnt", "GELD", "Gelder", "Gelding"),
            new SkillDescription("soc", "DANCE", "Dancer", "Dancing"),
            new SkillDescription("soc", "MAKE_MUSIC", "Musician", "Music"),
            new SkillDescription("soc", "SING", "Singer", "Singing"),
            new SkillDescription("soc", "PLAY_KEYBOARD_INSTRUMENT", "Keyboard Instrumentalist", "Keyboard Instrumentalist"),
            new SkillDescription("soc", "PLAY_STRINGED_INSTRUMENT", "String Instrumentalist", "String Instrumentalist"),
            new SkillDescription("soc", "PLAY_WIND_INSTRUMENT", "Wind Instrumentalist", "Wind Instrumentalist"),
            new SkillDescription("soc", "PLAY_PERCUSSION_INSTRUMENT", "Drummer", "Percussionist"),
            new SkillDescription("msc", "CRITICAL_THINKING", "Critical Thinker", "Critical Thinking"),
            new SkillDescription("msc", "LOGIC", "Logician", "Logic"),
            new SkillDescription("msc", "MATHEMATICS", "Mathematician", "Mathematics"),
            new SkillDescription("msc", "ASTRONOMY", "Astronomer", "Astronomy"),
            new SkillDescription("msc", "CHEMISTRY", "Chemist", "Chemistry"),
            new SkillDescription("msc", "GEOGRAPHY", "Geographer", "Geography"),
            new SkillDescription("eng", "OPTICS_ENGINEER", "Optician", "Optics Engineer"),
            new SkillDescription("eng", "FLUID_ENGINEER", "Fluids Engineer", "Fluid Engineering"),
            new SkillDescription("ppr", "PAPERMAKING", "Paper Maker", "Paper Making"),
            new SkillDescription("ppr", "BOOKBINDING", "Book Binder", "Book Binding"),
        };

        public static SkillDescription lookupSkill(Skill skill)
        {
            foreach (SkillDescription sd in skills)
            {
                if (sd.Token.Replace('_', ' ').ToLower().CompareTo(skill.Name.ToLower()) == 0)
                    return sd;
            }

            return new SkillDescription("unk", skill.Name, skill.Name, skill.Name);
        }
    }
}
