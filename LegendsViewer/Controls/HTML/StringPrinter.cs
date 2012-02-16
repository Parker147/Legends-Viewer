using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Controls
{
    class StringPrinter : HTMLPrinter
    {
        string Title;
        public StringPrinter(string htmlString)
        {
            HTML = new StringBuilder();
            Title = htmlString.Substring(0, htmlString.IndexOf("\n"));
            HTML.AppendLine(htmlString.Substring(htmlString.IndexOf("\n") + 1));
        }

        public override string Print()
        {
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Title;
        }
    }
}
