using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LegendsViewer.Legends;

namespace LegendsViewer
{
    public partial class dlgHF : Form
    {
        //List<string> Populations = new List<string>();
        public string SelectedRace;
        public dlgHF(World world)
        {
            InitializeComponent();

            var hfByRace = world.HistoricalFigures.GroupBy(hf => hf.Race).Select(hf => hf.Key).OrderBy(hf => hf);
            foreach (var race in hfByRace)
            {
                listHFRaces.Items.Add(race);
            }
        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listHFRaces.Items.Count; i++)
                listHFRaces.SetSelected(i, true);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listHFRaces.Items.Count; i++)
                listHFRaces.SetSelected(i, false);
        }

        private void btnName_Click(object sender, EventArgs e)
        {

        }

        private void btnNumber_Click(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SelectedRace = listHFRaces.SelectedItem as string;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedRace = "";
            Close();
        }

    }



}
