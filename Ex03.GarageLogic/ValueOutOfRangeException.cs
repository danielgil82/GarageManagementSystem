using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_RMaxValue, float i_RMinValue)
            : base(string.Format("Oops , out of range {0} - {1} ", i_RMinValue, i_RMaxValue))
        {
            r_MaxValue = i_RMaxValue;
            r_MinValue = i_RMinValue;
        }
    }
}