using System;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UiMenu
    {
        private UiManager m_UiManager = new UiManager();
        private const int k_LowerEnumBound = 1;
        private const int k_UpperEnumBound = 8;
        private enum eMenu : byte
        {
            None,
            AddNewVehicle,
            PresentLicensePlates,
            ChangeTheStateOfTheVehicle,
            PumpAirInTheWheelsToMax,
            RefuelYourVehicle,
            ChargeYourVehicle,
            DisplayInfo,
            Exit
        }

        public void Start()
        {
            byte numberForParse;
            string userChoice;
            bool continueOrNot = true;
            StringBuilder garageUi = new StringBuilder();

            garageUi.Append("Hello user: ");
            garageUi.AppendLine();
            garageUi.Append("1) Add vehicle to the garage");
            garageUi.AppendLine();
            garageUi.Append("2) Present the license plates of cars in the garage");
            garageUi.AppendLine();
            garageUi.Append("3) Change the state of a car in the garage");
            garageUi.AppendLine();
            garageUi.Append("4) Pump air into the wheels of a car to the max");
            garageUi.AppendLine();
            garageUi.Append("5) Refuel your vehicle");
            garageUi.AppendLine();
            garageUi.Append("6) Charge your vehicle");
            garageUi.AppendLine();
            garageUi.Append("7) Display info about a car");
            garageUi.AppendLine();
            garageUi.Append("8) Exit");
            garageUi.AppendLine();
            garageUi.AppendLine();
            garageUi.Append("Enter your choice:");

            while (continueOrNot)
            {
                Console.WriteLine(garageUi);
                userChoice = Console.ReadLine();
                Console.Clear();
                try
                {
                    checkIfTheInputIsValid(userChoice, out numberForParse);
                    //if (!byte.TryParse(userChoice, out numberForParse))
                    //{
                    //    throw new FormatException("Wrong input");
                    //}
                    
                    checkIfTheInputSuitsTheEnumRange(numberForParse);

                    //if (numberForParse < k_LowerEnumBound || numberForParse > k_UpperEnumBound)
                    //{
                    //    throw new ValueOutOfRangeException(8, 1);
                    //}

                    if (!LetTheUserChoose(numberForParse))
                    {
                        continueOrNot = false;
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public void checkIfTheInputSuitsTheEnumRange(byte i_NumberForParse)
        {
            if (i_NumberForParse< k_LowerEnumBound || i_NumberForParse > k_UpperEnumBound)
            {
                throw new ValueOutOfRangeException(8, 1);
            }
        }

        public void checkIfTheInputIsValid(string i_UsersChoice, out byte o_NumberForParse)
        {
            if (!byte.TryParse(i_UsersChoice, out o_NumberForParse))
            {
                throw new FormatException("Wrong input");
            }
        }

        public bool LetTheUserChoose(byte i_UserChoice)
        {
            eMenu menu = eMenu.None;
            bool continueOrNot = true;

            menu = (eMenu)Enum.Parse(typeof(eMenu), i_UserChoice.ToString());
            switch (menu)
            {
                case eMenu.AddNewVehicle:
                    m_UiManager.AddNewVehicleToTheGarage();
                    break;
                case eMenu.PresentLicensePlates:
                    m_UiManager.DisplayingVehiclesLicenseNumber();
                    break;
                case eMenu.ChangeTheStateOfTheVehicle:
                    m_UiManager.ChangeVehiclesStatusInTheGarage();
                    break;
                case eMenu.PumpAirInTheWheelsToMax:
                    m_UiManager.FillAirToMaximum();
                    break;
                case eMenu.RefuelYourVehicle:
                    m_UiManager.FillFuelVehicleTank();
                    break;
                case eMenu.ChargeYourVehicle:
                    m_UiManager.ChargeElectricalVehicle();
                    break;
                case eMenu.DisplayInfo:
                    m_UiManager.DisplayVehicleInformation();
                    break;
                case eMenu.Exit:
                    continueOrNot = false;
                    break;
            }

            if (menu != eMenu.Exit)
            {
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                Console.Clear();
            }

            return continueOrNot;
        }
    }
}
