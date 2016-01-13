using System.Collections.Generic;
using System.Text;

namespace LegendsViewer.Legends.Parser
{
    public class ParsingErrors
    {
        private List<string> ReportedErrorTypes;
        private StringBuilder Log;

        public ParsingErrors()
        {
            ReportedErrorTypes = new List<string>();
            Log = new StringBuilder();
        }

        public void Report(string description, string details = "", bool forceReport = false)
        {
            if (forceReport || ReportedErrorTypes.FindIndex(error => error == description) == -1)
            {
                Log.Append(description);
                if (details != "")
                    Log.AppendLine(" (" + details + ")");
                else
                    Log.AppendLine();
                ReportedErrorTypes.Add(description);
            }
        }

        public string Print()
        {
            return Log.ToString();
        }
    }

}
