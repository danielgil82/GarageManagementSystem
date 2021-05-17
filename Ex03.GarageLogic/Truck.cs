using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const string k_DangerMaterialsMessage = "Is the truck carrying dangerous materials? Yes/No";
        private const string k_MaxWightLoadMessage = "Choose the max weight load of the truck between 0 to 3000: ";
        private bool m_IsCarryingDangerousMateriales;
        private float m_MaxWeightLoad;

        internal Truck(
            float i_MaxAirPressureForWheels,
            FuelEngine.eTypeOfFuel i_TypeOfFuel,
            float i_MaxTankCapacity,
            byte i_NumberOfWheels,
            string i_LicenseNumber) : base(i_LicenseNumber, i_NumberOfWheels)
        {
            CreateTheWheels(i_MaxAirPressureForWheels, i_NumberOfWheels);
            Engine = new FuelEngine(i_MaxTankCapacity, i_TypeOfFuel);
        }

        public bool IsCarryingDangerousMateriales
        {
            get { return m_IsCarryingDangerousMateriales; }

            set { m_IsCarryingDangerousMateriales = value; }
        }

        public float MaxWeightLoad
        {
            get { return m_MaxWeightLoad; }
            set { m_MaxWeightLoad = value; }
        }

        internal sealed override string DisplayVehicleInfo()
        {
            string msg = base.DisplayVehicleInfo();

            msg += string.Format(@"
Is carrying dangerous materials: {0}
Max wight load: {1}", 
                IsCarryingDangerousMateriales, 
                MaxWeightLoad);

            return msg;
        }

        public override Hashtable FetchUniqueInfo()
        {
            Hashtable extraInfoMenu = new Hashtable();

            extraInfoMenu.Add(k_DangerMaterialsMessage, null);
            extraInfoMenu.Add(k_MaxWightLoadMessage, null);

            return extraInfoMenu;
        }

        public override void UpdateUniqueInfo(string i_KeyMessage, string i_UserInput)
        {
            switch (i_KeyMessage)
            {
                case k_DangerMaterialsMessage:
                    ValidationsForNoneMenuQuestions.ValidationCheckingIfCarryingDangerousChemicals(i_UserInput);
                    if (i_UserInput == "Yes")
                    {
                        IsCarryingDangerousMateriales = true;
                    }

                    break;

                case k_MaxWightLoadMessage:
                    ValidationsForNoneMenuQuestions.ValidatingMaxCarryWeight(i_UserInput);
                    MaxWeightLoad = float.Parse(i_UserInput);
                    break;
            }
        }
    }
}