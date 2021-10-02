using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private const string k_CarColorMessage = "Choose the color of the car: ";
        private const string k_NumberOfDoorsMessage = "Choose the number of doors your car have: ";
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        internal Car(string i_LicenseNumber, byte i_NumberOfWheels) : base(i_LicenseNumber, i_NumberOfWheels)
        {
        }

        internal enum eCarColor
        {
            None,
            Red,
            Silver,
            White,
            Black
        }

        internal enum eNumberOfDoors
        {
            None,
            TwoDoors,
            ThreeDoors,
            FourDoors,
            FiveDoors
        }

        public sealed override Hashtable FetchUniqueInfo()
        {
            GarageManager tempGarageManager = new GarageManager();
            Hashtable extraInfoMenu = new Hashtable();
            eCarColor noneCarColor = eCarColor.None;
            eNumberOfDoors noneNumberOfDoors = eNumberOfDoors.None;

            extraInfoMenu.Add(k_CarColorMessage, tempGarageManager.GeneralMenu(noneCarColor).ToString());
            extraInfoMenu.Add(k_NumberOfDoorsMessage, tempGarageManager.GeneralMenu(noneNumberOfDoors).ToString());

            return extraInfoMenu;
        }

        public sealed override void UpdateUniqueInfo(string i_KeyMessage, string i_UserInput)
        {
            GarageManager tempGarageManager = new GarageManager();

            switch (i_KeyMessage)
            {
                case k_CarColorMessage:
                    eCarColor noneCarColor = eCarColor.None;
                    tempGarageManager.ValidateUsersInputBasedOnTheRangeOfThisEnum(i_UserInput, noneCarColor);
                    m_CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_UserInput);
                    break;
                case k_NumberOfDoorsMessage:
                    eNumberOfDoors noneNumberOfDoors = eNumberOfDoors.None;
                    tempGarageManager.ValidateUsersInputBasedOnTheRangeOfThisEnum(i_UserInput, noneNumberOfDoors);
                    m_NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_UserInput);
                    break;
            }
        }

        internal override string DisplayVehicleInfo()
        {
            string msg = base.DisplayVehicleInfo();

            msg += string.Format(@"
Car color: {0}
Number of Doors: {1}",
                m_CarColor,
                m_NumberOfDoors);

            return msg;
        }
    }
}