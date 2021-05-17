using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricalMotorcycle : Motorcycle
    {
        internal ElectricalMotorcycle(
            float i_MaxAirPressureForWheels,
            float i_MaxBatteryLife, 
            byte i_NumberOfWheels, 
            string i_LicenseNumber) : base(i_LicenseNumber, i_NumberOfWheels)
        {
            CreateTheWheels(i_MaxAirPressureForWheels, i_NumberOfWheels);
            Engine = new ElectricalEngine(i_MaxBatteryLife);
        }
    }
}