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

        internal sealed override string DisplayVehicleInfo()
        {
            string msg = base.DisplayVehicleInfo();

            msg += string.Format(@"
Is carrying dangerous materials: {0}
Max wight load: {1}", 
                m_IsCarryingDangerousMateriales, 
                m_MaxWeightLoad);

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
                    validationCheckingIfCarryingDangerousChemicals(i_UserInput);
                    if (i_UserInput == "Yes")
                    {
                        m_IsCarryingDangerousMateriales = true;
                    }

                    break;

                case k_MaxWightLoadMessage:
                    validatingMaxCarryWeight(i_UserInput);
                    m_MaxWeightLoad = float.Parse(i_UserInput);
                    break;
            }
        }

        private static void validationCheckingIfCarryingDangerousChemicals(string i_UserChoice)
        {
            if (i_UserChoice != "Yes" && i_UserChoice != "No")
            {
                throw new ArgumentException("Only Yes or No!");
            }
        }

        private static void validatingMaxCarryWeight(string i_UserInput)
        {
            float numberForParse;

            if (!float.TryParse(i_UserInput, out numberForParse))
            {
                throw new FormatException("Wrong input");
            }

            if (numberForParse < 0 || numberForParse > 3000)
            {
                throw new ValueOutOfRangeException(3000f, 0f);
            }
        }
    }
}