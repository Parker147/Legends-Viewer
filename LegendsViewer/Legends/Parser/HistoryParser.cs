using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends.Parser
{
    class HistoryParser
    {
        World World;
        StreamReader History;
        string CurrentLine;
        Entity CurrentCiv = null;
        StringBuilder Log;

        public HistoryParser(World world, string historyFile)
        {
            World = world;
            History = new StreamReader(historyFile, Encoding.GetEncoding("windows-1252"));
            Log = new StringBuilder();
        }

        private bool CivStart()
        {
            return !CurrentLine.StartsWith(" ");
        }

        private void SkipAnimalPeople()
        {
            while (!CurrentLine.Contains(",") || CurrentLine.StartsWith(" "))
                ReadLine();
        }

        private void SkipToNextCiv()
        {
            while (!History.EndOfStream && !CivStart())
                ReadLine();
        }

        private void ReadLine()
        {
            CurrentLine = History.ReadLine();
        }

        private bool ReadCiv()
        {
            string civName = CurrentLine.Substring(0, CurrentLine.IndexOf(","));
            try
            {
                CurrentCiv = World.GetEntity(civName);
            }
            catch (Exception e)
            {
                Log.AppendLine(e.Message + ", Civ");
                ReadLine();
                return false;
            }
            CurrentCiv.Race = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(",") + 2, CurrentLine.Length - CurrentLine.IndexOf(",") - 2).ToLower());
            foreach (Entity group in CurrentCiv.Groups) group.Race = CurrentCiv.Race;
            CurrentCiv.IsCiv = true;
            ReadLine();
            return true;
        }

        private void ReadEOSCiv()
        {
            string civName = CurrentLine.Substring(0, CurrentLine.IndexOf(","));
            try
            {
                CurrentCiv = World.GetEntity(civName);
            }
            catch (Exception e)
            {
                Log.AppendLine(e.Message + ", Civ");
            }
            CurrentCiv.Race = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(",") + 2, CurrentLine.Length - CurrentLine.IndexOf(",") - 2).ToLower());
            foreach (Entity group in CurrentCiv.Groups) group.Race = CurrentCiv.Race;
            CurrentCiv.IsCiv = true;
        }

        private void ReadWorships()
        {
            if (CurrentLine.Contains("Worship List"))
            {
                ReadLine();
                while (CurrentLine != null && CurrentLine.StartsWith("  "))
                {
                    string worshipName = Formatting.InitCaps(Formatting.ReplaceNonAscii(CurrentLine.Substring(2, CurrentLine.IndexOf(",") - 2)));
                    HistoricalFigure worship = null;
                    try
                    {
                        worship = World.GetHistoricalFigure(worshipName);
                    }
                    catch (Exception e)
                    {
                        worship = World.HistoricalFiguresByName.FirstOrDefault(h => h.Name.Equals(worshipName, StringComparison.OrdinalIgnoreCase) && h.Deity);
                        if (worship == null)
                        {
                            Log.AppendLine(e.Message + ", a Worship of " + CurrentCiv.Name);
                            ReadLine();
                            continue;
                        }
                    }
                    worship.WorshippedBy = CurrentCiv;
                    CurrentCiv.Worshipped.Add(worship);
                    ReadLine();
                }
            }
        }

        private bool LeaderStart()
        {
            return !History.EndOfStream && CurrentLine.Contains(" List") && !CurrentLine.Contains("[*]") && !CurrentLine.Contains("%");
        }

        private void ReadLeaders()
        {
            while(LeaderStart())
            {
                string leaderType = Formatting.InitCaps(CurrentLine.Substring(1, CurrentLine.IndexOf("List") - 2));
                CurrentCiv.LeaderTypes.Add(leaderType);
                CurrentCiv.Leaders.Add(new List<HistoricalFigure>());
                ReadLine();
                while (CurrentLine != null && CurrentLine.StartsWith("  "))
                {
                    if (CurrentLine.Contains("[*]"))
                    {
                        string leaderName = Formatting.ReplaceNonAscii(CurrentLine.Substring(CurrentLine.IndexOf("[*]") + 4, CurrentLine.IndexOf("(b") - CurrentLine.IndexOf("[*]") - 5));
                        HistoricalFigure leader = null;
                        try
                        {
                            leader = World.GetHistoricalFigure(leaderName);
                        }
                        catch (Exception e)
                        {
                            Log.AppendLine(e.Message + ", a Leader of " + CurrentCiv.Name);
                            ReadLine();
                            continue;
                        }

                        int reignBegan = Convert.ToInt32(CurrentLine.Substring(CurrentLine.IndexOf(":") + 2, CurrentLine.IndexOf("), ") - CurrentLine.IndexOf(":") - 2));
                        if (CurrentCiv.Leaders[CurrentCiv.LeaderTypes.Count - 1].Count > 0) //End of previous leader's reign
                            CurrentCiv.Leaders[CurrentCiv.LeaderTypes.Count - 1].Last().Positions.Last().Ended = reignBegan - 1;
                        if (leader.Positions.Count > 0 && leader.Positions.Last().Ended == -1) //End of leader's last leader position (move up rank etc.)
                        {
                            HistoricalFigure.Position lastPosition = leader.Positions.Last();
                            lastPosition.Ended = reignBegan;
                            lastPosition.Length = lastPosition.Began - reignBegan;
                        }
                        HistoricalFigure.Position newPosition = new HistoricalFigure.Position(CurrentCiv, reignBegan, -1, leaderType);
                        if (leader.DeathYear != -1)
                        {
                            newPosition.Ended = leader.DeathYear;
                            newPosition.Length = leader.DeathYear - newPosition.Ended;
                        }
                        else
                            newPosition.Length = World.Events.Last().Year - newPosition.Began;
                        leader.Positions.Add(newPosition);
                        CurrentCiv.Leaders[CurrentCiv.LeaderTypes.Count - 1].Add(leader);

                    }
                    ReadLine();
                }
            }
        }

        public string Parse()
        {
            World.Name = Formatting.ReplaceNonAscii(History.ReadLine());
            World.Name += ", " + History.ReadLine();
            ReadLine();
            SkipAnimalPeople();
            
            while (!History.EndOfStream)
            {
                if (ReadCiv())
                {
                    ReadWorships();
                    ReadLeaders();
                }
                else
                    SkipToNextCiv();
            }

            History.Close();

            if (CurrentLine != null && CivStart())
                ReadEOSCiv();

            return Log.ToString();
        }
    }
}
