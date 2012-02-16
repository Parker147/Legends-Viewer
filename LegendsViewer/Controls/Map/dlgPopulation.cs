using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LegendsViewer;
using LegendsViewer.Legends;

namespace LegendsViewer
{
    public partial class dlgPopulation : Form
    {
        List<Population> Populations = new List<Population>();
        public List<string> SelectedPopulations = new List<string>();
        public dlgPopulation(World world)
        {
            InitializeComponent();
            
            var populationGrouped = from population in world.SitePopulations
                                    group population by population.Race into popType
                                    select new { Type = popType.Key, Count = popType.Sum(population => population.Count) };
            populationGrouped = populationGrouped.OrderBy(population => population.Type);
            foreach (var population in populationGrouped)
            {
                listPopulations.Items.Add(population.Type + ": " + population.Count);
                Populations.Add(new Population(population.Type, population.Count));
            }
        }


        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listPopulations.Items.Count; i++)
                listPopulations.SetSelected(i, true);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listPopulations.Items.Count; i++)
                listPopulations.SetSelected(i, false);
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            listPopulations.Items.Clear();
            Populations = Populations.OrderBy(population => population.Race).ToList();
            Populations.ForEach(population => listPopulations.Items.Add(population.Race + ": " + population.Count));
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            listPopulations.Items.Clear();
            Populations = Populations.OrderByDescending(population => population.Count).ToList();
            Populations.ForEach(population => listPopulations.Items.Add(population.Race + ": " + population.Count));
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listPopulations.SelectedIndices.Count; i++)
                SelectedPopulations.Add(Populations[listPopulations.SelectedIndices[i]].Race);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedPopulations.Clear();
            Close();
        }

    }



}
