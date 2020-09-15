using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    public struct Percent
    {
        public double Value { get; }

        public Percent(double Value)
        {
            if (Value < 0)
                throw new ArgumentOutOfRangeException("Value", Value, "Value is under 0%");

            if (Value > 100)
                throw new ArgumentOutOfRangeException("Value", Value, "Value is over 100%");

            this.Value = Value;
        }
    }
}
