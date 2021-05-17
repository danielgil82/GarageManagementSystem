using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxEnergy;
        private float m_LeftEnergy;
        private float m_LeftEnergyPercentage;

        internal Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float LeftEnergyPercentage
        {
            get
            {
                return m_LeftEnergyPercentage;
            }

            set
            {
                m_LeftEnergyPercentage = value;
            }
        }

        public float LeftEnergy
        {
            get
            {
                return m_LeftEnergy;
            }

            set
            {
                m_LeftEnergy = value;
                LeftEnergyPercentage = (m_LeftEnergy / MaxEnergy) * 100;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }
    }
}