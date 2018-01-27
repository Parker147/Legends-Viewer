using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LegendsViewer.Legends.Enums;

namespace LegendsViewer.Legends.Parser
{
    class HistoryParser : IDisposable
    {
        World _world;
        StreamReader _history;
        string _currentLine;
        Entity _currentCiv;
        StringBuilder _log;

        public HistoryParser(World world, string historyFile)
        {
            _world = world;
            _history = new StreamReader(historyFile, Encoding.GetEncoding("windows-1252"));
            _log = new StringBuilder();
        }

        private bool CivStart()
        {
            return !_currentLine.StartsWith(" ");
        }

        private void SkipAnimalPeople()
        {
            while (!_currentLine.Contains(",") || _currentLine.StartsWith(" "))
            {
                ReadLine();
            }
        }

        private void SkipToNextCiv()
        {
            while (!_history.EndOfStream && !CivStart())
            {
                ReadLine();
            }
        }

        private void ReadLine()
        {
            _currentLine = _history.ReadLine();
        }

        private bool ReadCiv()
        {
            string civName = _currentLine.Substring(0, _currentLine.IndexOf(","));
            try
            {
                _currentCiv = _world.GetEntity(civName);
            }
            catch (Exception e)
            {
                _currentCiv = _world.Entities.FirstOrDefault(entity =>
                    string.Compare(entity.Name, civName, StringComparison.OrdinalIgnoreCase) == 0 && 
                    (entity.Type == EntityType.Civilization || entity.Type == EntityType.Unknown));
                if (_currentCiv == null)
                {
                    _log.AppendLine(e.Message + ", Civ");
                    ReadLine();
                    return false;
                }
            }
            _currentCiv.Race = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(",") + 2, _currentLine.Length - _currentLine.IndexOf(",") - 2).ToLower());
            foreach (Entity group in _currentCiv.Groups)
            {
                group.Race = _currentCiv.Race;
            }

            _currentCiv.IsCiv = true;
            ReadLine();
            return true;
        }

        private void ReadWorships()
        {
            if (_currentLine.Contains("Worship List"))
            {
                ReadLine();
                while (_currentLine != null && _currentLine.StartsWith("  "))
                {
                    string worshipName = Formatting.InitCaps(Formatting.ReplaceNonAscii(_currentLine.Substring(2, _currentLine.IndexOf(",") - 2)));
                    HistoricalFigure worship = null;
                    try
                    {
                        worship = _world.GetHistoricalFigure(worshipName);
                    }
                    catch (Exception e)
                    {
                        worship = _world.HistoricalFiguresByName.FirstOrDefault(h => h.Name.Equals(worshipName, StringComparison.OrdinalIgnoreCase) && (h.Deity || h.Force));
                        if (worship == null)
                        {
                            _log.AppendLine(e.Message + ", a Worship of " + _currentCiv.Name);
                            ReadLine();
                            continue;
                        }
                    }
                    worship.WorshippedBy = _currentCiv;
                    _currentCiv.Worshipped.Add(worship);
                    ReadLine();
                }
            }
        }

        private bool LeaderStart()
        {
            return !_history.EndOfStream && _currentLine.Contains(" List") && !_currentLine.Contains("[*]") && !_currentLine.Contains("%");
        }

        private void ReadLeaders()
        {
            while (LeaderStart())
            {
                string leaderType = Formatting.InitCaps(_currentLine.Substring(1, _currentLine.IndexOf("List") - 2));
                _currentCiv.LeaderTypes.Add(leaderType);
                _currentCiv.Leaders.Add(new List<HistoricalFigure>());
                ReadLine();
                while (_currentLine != null && _currentLine.StartsWith("  "))
                {
                    if (_currentLine.Contains("[*]"))
                    {
                        string leaderName = Formatting.ReplaceNonAscii(_currentLine.Substring(_currentLine.IndexOf("[*]") + 4, _currentLine.IndexOf("(b") - _currentLine.IndexOf("[*]") - 5));
                        HistoricalFigure leader = null;
                        try
                        {
                            leader = _world.GetHistoricalFigure(leaderName);
                        }
                        catch (Exception e)
                        {
                            leader = _world.HistoricalFigures.FirstOrDefault(hf =>
                                string.Compare(hf.Name, leaderName, StringComparison.OrdinalIgnoreCase) == 0);
                            if (leader == null)
                            {
                                _log.AppendLine(e.Message + ", a Leader of " + _currentCiv.Name);
                                ReadLine();
                                continue;
                            }
                        }

                        int reignBegan = Convert.ToInt32(_currentLine.Substring(_currentLine.IndexOf(":") + 2, _currentLine.IndexOf("), ") - _currentLine.IndexOf(":") - 2));
                        if (_currentCiv.Leaders[_currentCiv.LeaderTypes.Count - 1].Count > 0) //End of previous leader's reign
                        {
                            _currentCiv.Leaders[_currentCiv.LeaderTypes.Count - 1].Last().Positions.Last().Ended = reignBegan - 1;
                        }

                        if (leader.Positions.Count > 0 && leader.Positions.Last().Ended == -1) //End of leader's last leader position (move up rank etc.)
                        {
                            HistoricalFigure.Position lastPosition = leader.Positions.Last();
                            lastPosition.Ended = reignBegan;
                            lastPosition.Length = lastPosition.Began - reignBegan;
                        }
                        HistoricalFigure.Position newPosition = new HistoricalFigure.Position(_currentCiv, reignBegan, -1, leaderType);
                        if (leader.DeathYear != -1)
                        {
                            newPosition.Ended = leader.DeathYear;
                            newPosition.Length = leader.DeathYear - newPosition.Ended;
                        }
                        else
                        {
                            newPosition.Length = _world.Events.Last().Year - newPosition.Began;
                        }

                        leader.Positions.Add(newPosition);
                        _currentCiv.Leaders[_currentCiv.LeaderTypes.Count - 1].Add(leader);

                    }
                    ReadLine();
                }
            }
        }

        public string Parse()
        {
            _world.Name = Formatting.ReplaceNonAscii(_history.ReadLine());
            _world.Name += ", " + _history.ReadLine();
            ReadLine();
            SkipAnimalPeople();

            while (!_history.EndOfStream)
            {
                if (ReadCiv())
                {
                    ReadWorships();
                    ReadLeaders();
                }
                else
                {
                    SkipToNextCiv();
                }
            }
            if (_currentLine != null && CivStart())
            {
                ReadCiv();
            }

            _history.Close();


            return _log.ToString();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _history.Dispose();
            }
        }
    }
}
