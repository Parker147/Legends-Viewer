using System;
using LegendsViewer.Controls;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

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
                case "rep_buddy": Type = ReputationType.Buddy; Strength = Convert.ToInt32(property.Value); break;
                case "rep_grudge": Type = ReputationType.Grudge; Strength = Convert.ToInt32(property.Value); break;
                case "rep_bonded": Type = ReputationType.Bonded; Strength = Convert.ToInt32(property.Value); break;
                case "rep_quarreler": Type = ReputationType.Quarreler; Strength = Convert.ToInt32(property.Value); break;
                case "rep_trade_partner": Type = ReputationType.TradePartner; Strength = Convert.ToInt32(property.Value); break;
                case "rep_psychopath": Type = ReputationType.Psychopath; Strength = Convert.ToInt32(property.Value); break;
                case "rep_storyteller": Type = ReputationType.Storyteller; Strength = Convert.ToInt32(property.Value); break;
                case "rep_loyal_soldier": Type = ReputationType.LoyalSoldier; Strength = Convert.ToInt32(property.Value); break;
                case "rep_bully": Type = ReputationType.Bully; Strength = Convert.ToInt32(property.Value); break;
            }
        }

        public string Print()
        {
            return Type.GetDescription() + " (" + Strength + ")";
        }
    }
}
