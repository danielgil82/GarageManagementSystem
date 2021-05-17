using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        internal FuelCar(
            float i_MaxAirPressureForWheels, 
            FuelEngine.eTypeOfFuel i_TypeOfFuel,
            float i_MaxTankCapacity,
            byte i_NumberOfWheels,
            string i_LicenseNumber) : base(i_LicenseNumber, i_NumberOfWheels)
        {
            CreateTheWheels(i_MaxAirPressureForWheels, i_NumberOfWheels);
            Engine = new FuelEngine(i_MaxTankCapacity, i_TypeOfFuel);
        }

        internal sealed override string DisplayVehicleInfo()
        {
            string msg = base.DisplayVehicleInfo();

            msg += string.Format(@"
Type of fuel: {0}", (Engine as FuelEngine).TypeOfFuel);

            return msg;
        }
    }
}