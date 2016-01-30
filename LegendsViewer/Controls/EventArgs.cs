using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Controls
{
    public class EventArgs<T> : System.EventArgs
    {
        public T Arg { get; private set; }

        public EventArgs(T arg)
        {
            Arg = arg;
        }

    }
}
