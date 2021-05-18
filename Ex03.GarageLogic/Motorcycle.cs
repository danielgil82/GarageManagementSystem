using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        private const string k_MotorcycleLicenseTypeMessage = "Choose the license type of the motorcycle:";
        private const string k_MotorcycleEngineVolumeMessage = "Please enter your engine volume a positive number between 0 - 3000: ";
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        internal Motorcycle(string i_LicenseNumber, byte i_NumberOfWheels) : base(i_LicenseNumber, i_NumberOfWheels)
        {
        }

        private enum eLicenseType
        {
            None,
            A,
            B1,
            BB,
            AA
        }

        internal override string DisplayVehicleInfo()
        {
            string msg = base.DisplayVehicleInfo();
            msg += string.Format(@"
License type: {0}
Engine Volume: {1}",
                m_LicenseType, 
                m_EngineVolume);

            return msg;
        }

        public sealed override Hashtable FetchUniqueInfo()
        {
            GarageManger tempGarageManger = new GarageManger();
            Hashtable extraInfoMenu = new Hashtable();
            eLicenseType noneLicenseType = eLicenseType.None;

            extraInfoMenu.Add(k_MotorcycleLicenseTypeMessage, tempGarageManger.GeneralMenu(noneLicenseType).ToString());
            extraInfoMenu.Add(k_MotorcycleEngineVolumeMessage, null);

            return extraInfoMenu;
        }

        public sealed override void UpdateUniqueInfo(string i_KeyMessage, string i_UserInput)
        {
            GarageManger tempGarageManger = new GarageManger();
            switch (i_KeyMessage)
            {
                case k_MotorcycleLicenseTypeMessage:
                    eLicenseType noneLicenseType = eLicenseType.None;
                    tempGarageManger.ValidateUsersInputBasedOnTheRangeOfThisEnum(i_UserInput, noneLicenseType);
                    m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_UserInput);
                    break;
                case k_MotorcycleEngineVolumeMessage:
                    validatingEngineVolumeInput(i_UserInput);
                    m_EngineVolume = int.Parse(i_UserInput);
                    break;
            }
        }

        private static void validatingEngineVolumeInput(string i_UserInput)
        {
            int numberForParse;

            if (!int.TryParse(i_UserInput, out numberForParse))
            {
                throw new FormatException("Wrong input");
            }

            if (numberForParse < 0)
            {
                throw new ValueOutOfRangeException(3000, 0);
            }
        }
    }
}