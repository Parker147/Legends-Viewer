using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;
using System;

namespace LegendsViewer.Legends
{
    public class Reputation
    {
        public ReputationType Type { get; set; }
        public int Strength { get; set; }

        public Reputation(Property property)
        {
            switch (property.Name)
            {
                case "rep_friendly": Type = ReputationType.Friendly; Strength = Convert.ToInt32(property.Value); break;
                case "rep_buddy": Type = ReputationType.Buddy; Convert.ToInt32(property.Value); break;
                case "rep_grudge": Type = ReputationType.Grudge; Convert.ToInt32(property.Value); break;
                case "rep_bonded": Type = ReputationType.Bonded; Convert.ToInt32(property.Value); break;
                case "rep_quarreler": Type = ReputationType.Quarreler; Convert.ToInt32(property.Value); break;
                case "rep_trade_partner": Type = ReputationType.TradePartner; Convert.ToInt32(property.Value); break;
                case "rep_psychopath": Type = ReputationType.Psychopath; Convert.ToInt32(property.Value); break;
                case "rep_storyteller": Type = ReputationType.Storyteller; Convert.ToInt32(property.Value); break;
                case "rep_loyal_soldier": Type = ReputationType.LoyalSoldier; Convert.ToInt32(property.Value); break;
                case "rep_bully": Type = ReputationType.Bully; Convert.ToInt32(property.Value); break;
            }
        }

        public string Print()
        {
            return Type.GetDescription() + " (" + Strength + "%)";
        }
    }
}
