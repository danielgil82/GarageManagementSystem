using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricalEngine : Engine
    {
        internal ElectricalEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        internal void ChargingBattery(float i_HoursToCharge)
        {
            if (i_HoursToCharge < 0)
            {
                throw new ArgumentException("Can't fill with negative amount !");
            }
            else if (i_HoursToCharge + LeftEnergy > MaxEnergy)
            {
                throw new ValueOutOfRangeException(MaxEnergy, 0);
            }
            else
            {
                LeftEnergy += i_HoursToCharge;
            }
        }
    }
}