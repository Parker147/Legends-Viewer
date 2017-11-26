using System.Collections.Generic;
using System.Text;

namespace LegendsViewer.Legends.Parser
{
    public class ParsingErrors
    {
        private List<string> _reportedErrorTypes;
        private StringBuilder _log;

        public ParsingErrors()
        {
            _reportedErrorTypes = new List<string>();
            _log = new StringBuilder();
        }

        public void Report(string description, string details = "", bool forceReport = false)
        {
            if (forceReport || _reportedErrorTypes.FindIndex(error => error == description) == -1)
            {
                _log.Append(description);
                if (details != "")
                {
                    _log.AppendLine(" (" + details + ")");
                }
                else
                {
                    _log.AppendLine();
                }

                _reportedErrorTypes.Add(description);
            }
        }

        public string Print()
        {
            return _log.ToString();
        }
    }
}
