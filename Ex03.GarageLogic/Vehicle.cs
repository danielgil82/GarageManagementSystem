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

        public Engine Engine
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

        public class Wheel
        {
            private readonly float r_MaxAirPressure;
            private string m_ManufactureName;
            private float m_CurrentAirPressure;

            public Wheel(float i_MaxAirPressure)
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

            public string ManufactureName
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
           
            public float MaxAirPressure
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            public float CurrentAirPressure
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
                listOfWheels.Add(new Wheel(i_MaxAirPressureForWheels));
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
                listOfWheels.Count,
                listOfWheels[0].ManufactureName,
                listOfWheels[0].CurrentAirPressure,
                listOfWheels[0].MaxAirPressure);

            return msg;
        }
       
        public string ModelName
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

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        private List<Wheel> listOfWheels
        {
            get
            {
                return r_ListOfWheels;
            }
        }

        public abstract Hashtable FetchUniqueInfo();

        public abstract void UpdateUniqueInfo(string i_KeyMessage, string i_UserInput);
    }
}