using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eTypeOfFuel r_TypeOfFuel;

        public enum eTypeOfFuel
        {
            None,
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public FuelEngine(float i_MaxEnergy, eTypeOfFuel i_TypeOfFuel) : base(i_MaxEnergy)
        {
            r_TypeOfFuel = i_TypeOfFuel;
        }

        public eTypeOfFuel TypeOfFuel
        {
            get { return r_TypeOfFuel; }
        }

        internal void FillTheFuelTank(float i_AmountToFill, eTypeOfFuel i_TypeOfFuel)
        {
            if (i_TypeOfFuel != TypeOfFuel)
            {
                throw new ArgumentException("You entered wrong type of fuel");
            }
            else if (i_AmountToFill < 0)
            {
                throw new ArgumentException("Can't fill with negative amount !");
            }
            else if (i_AmountToFill + LeftEnergy > MaxEnergy)
            {
                throw new ValueOutOfRangeException(MaxEnergy, 0);
            }
            else
            {
                LeftEnergy += i_AmountToFill;
            }
        }
    }
}