using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class CreatingConcreteVehicles
    {
        /// fuel car const info
        private const float k_wheelPressureForFuelCars = 32;
        private const FuelEngine.eTypeOfFuel k_FuelCarTypeOfFuel = FuelEngine.eTypeOfFuel.Octan95;
        private const float k_FuelCarMaxTankCapacity = 45f;
        private const byte k_NumberOfWheelsForFuelCars = 4;

        /// fuel motorcycle const info
        private const float k_wheelPressureForFuelMotorycle = 30;
        private const FuelEngine.eTypeOfFuel k_FuelMotorcycleTypeOfFuel = FuelEngine.eTypeOfFuel.Octan98;
        private const float k_FuelMotorcycleMaxTankCapacity = 6f;
        private const byte k_NumberOfWheelsForFuelMotorcycle = 2;

        /// truck const info
        private const float k_wheelPressureForTrucks = 26;
        private const FuelEngine.eTypeOfFuel k_TrucksTypeOfFuel = FuelEngine.eTypeOfFuel.Soler;
        private const float k_TruckMaxTankCapacity = 120f;
        private const byte k_NumberOfWheelsForTruck = 16;

        /// Electrical motorcycle const info
        private const float k_ElectricalMotorcycleMaxBatteryLife = 1.8f;
        private const float k_wheelPressureForElectricalMotorycle = 30;
        private const byte k_NumberOfWheelsForElectricalMotorcycle = 2;

        /// Electrical car const info
        private const float k_wheelPressureForElectricalCars = 32;
        private const float k_ElectricalCarMaxBatteryLife = 3.2f;
        private const byte k_NumberOfWheelsForElectricalCars = 4;

        internal static Vehicle CreateTheVehicle(string i_UserChoice, string i_LicenseNumber)
        {
            eVehicleTypes userChoiceForVehicleType;
            Vehicle tempVehicle = null;

            userChoiceForVehicleType = (eVehicleTypes)Enum.Parse(typeof(eVehicleTypes), i_UserChoice);
            switch (userChoiceForVehicleType)
            {
                case eVehicleTypes.FuelCar:
                    tempVehicle = new FuelCar(
                        k_wheelPressureForFuelCars,
                        k_FuelCarTypeOfFuel,
                        k_FuelCarMaxTankCapacity, 
                        k_NumberOfWheelsForFuelCars,
                        i_LicenseNumber);
                    break;
                case eVehicleTypes.FuelMotorcycle:
                    tempVehicle = new FuelMotorcycle(
                        k_wheelPressureForFuelMotorycle,
                        k_FuelMotorcycleTypeOfFuel,
                        k_FuelMotorcycleMaxTankCapacity, 
                        k_NumberOfWheelsForFuelMotorcycle, 
                        i_LicenseNumber);
                    break;
                case eVehicleTypes.Truck:
                    tempVehicle = new Truck(
                        k_wheelPressureForTrucks,
                        k_TrucksTypeOfFuel,
                        k_TruckMaxTankCapacity,
                        k_NumberOfWheelsForTruck,
                        i_LicenseNumber);
                    break;
                case eVehicleTypes.ElectricalCar:
                    tempVehicle = new ElectricalCar(
                        k_wheelPressureForElectricalMotorycle,
                        k_ElectricalMotorcycleMaxBatteryLife,
                        k_NumberOfWheelsForElectricalMotorcycle,
                        i_LicenseNumber);
                    break;
                case eVehicleTypes.ElectricalMotorcycle:
                    tempVehicle = new ElectricalMotorcycle(
                        k_wheelPressureForElectricalCars,
                        k_ElectricalCarMaxBatteryLife,
                        k_NumberOfWheelsForElectricalCars,
                        i_LicenseNumber);
                    break;
            }

            return tempVehicle;
        }

        public enum eVehicleTypes
        {
            None,
            FuelCar,
            FuelMotorcycle,
            Truck,
            ElectricalCar,
            ElectricalMotorcycle,
        }
    }
}