using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prime_Generator
{
    class CustomTimeSpan
    {
        private long hours, minutes, milliseconds;
        private double seconds;

        public CustomTimeSpan()
        {
            hours = minutes = 0;
            seconds = 0.0;
        }
        public CustomTimeSpan(long ms)
        {
            hours = ms / (1000 * 60 * 60);
            ms = ms % (1000 * 60 * 60);
            minutes = ms / (1000 * 60);
            ms = ms % (1000 * 60);
            seconds = ms / 1000.0;
            milliseconds = ms;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("");
            if (hours > 0)
                result.Append(hours + (hours > 1 ? " hours, " : " hour, "));
            if (minutes > 0)
                result.Append(minutes + (minutes > 1 ? " minutes, " : " minute, "));
            if (seconds >= 1)
                result.Append(seconds + " seconds.");
            else
                result.Append(milliseconds + " milliseconds.");
            return result.ToString();
        }
    }
}
