using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicenseNumber;
        private readonly List<Wheel> r_ListOfWheels;
        private string m_ModelName;
        private Engine m_Engine;

        internal Vehicle(string i_LicenseNumber, byte i_NumberOfWheels)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_ListOfWheels = new List<Wheel>(i_NumberOfWheels);
        }

        internal Engine Engine
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }

        private class Wheel
        {
            private readonly float r_MaxAirPressure;
            private string m_ManufactureName;
            private float m_CurrentAirPressure;

            internal Wheel(float i_MaxAirPressure)
            {
                ManufactureName = string.Empty;
                CurrentAirPressure = 0;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            internal void PumpingAir(float i_AirPressureToAdd)
            {
                if (i_AirPressureToAdd + m_CurrentAirPressure > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(r_MaxAirPressure, 0);
                }
                else
                {
                    m_CurrentAirPressure += i_AirPressureToAdd;
                }
            }

            internal string ManufactureName
            {
                get
                {
                    return m_ManufactureName;
                }

                set
                {
                    m_ManufactureName = value;
                }
            }
           
            internal float MaxAirPressure
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            internal float CurrentAirPressure
            {
                get
                {
                    return m_CurrentAirPressure;
                }

                set
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        protected void CreateTheWheels(float i_MaxAirPressureForWheels, byte i_NumberOfWheels)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_ListOfWheels.Add(new Wheel(i_MaxAirPressureForWheels));
            }
        }

        internal virtual string DisplayVehicleInfo()
        {
            string msg = string.Format(@"
License number : {0}
Model name : {1}
Left energy amount : {2}
Max energy amount : {3}
Left energy percentage : {4}%
Number of wheels : {5}
Name of the manufacture of the wheels : {6}
Current air pressure of wheels : {7}
Max air pressure of wheels : {8}",
                LicenseNumber,
                ModelName,
                Engine.LeftEnergy,
                Engine.MaxEnergy,
                Engine.LeftEnergyPercentage,
                r_ListOfWheels.Count,
                r_ListOfWheels[0].ManufactureName,
                r_ListOfWheels[0].CurrentAirPressure,
                r_ListOfWheels[0].MaxAirPressure);

            return msg;
        }
       
        internal string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        internal string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public void InsertManufactureName(string i_ManufactureOfTheWheels)
        {
            foreach (Wheel wheel in r_ListOfWheels)
            {
                wheel.ManufactureName = i_ManufactureOfTheWheels;
            }
        }

        internal void PumpingAirToTheMax()
        {
            foreach (Wheel wheel in r_ListOfWheels)
            {
                wheel.PumpingAir(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public abstract Hashtable FetchUniqueInfo();

        public abstract void UpdateUniqueInfo(string i_KeyMessage, string i_UserInput);
    }
}