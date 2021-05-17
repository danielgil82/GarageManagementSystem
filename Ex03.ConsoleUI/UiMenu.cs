using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UiMenu
    {
       // private GarageManger m_GarageManger = new GarageManger();
        private UiManager m_UiManager = new UiManager();
        
        public void Ex3()
        {
            byte numberForParse;
            bool continuteOrNot = true;
            string userChoice;
            StringBuilder garageUi = new StringBuilder();

            garageUi.Append("Hello user: ");
            garageUi.AppendLine();
            garageUi.Append("1) Add car to the garage");
            garageUi.AppendLine();
            garageUi.Append("2) Present the license plates of cars in the garage");
            garageUi.AppendLine();
            garageUi.Append("3) Change the state of a car in the garage");
            garageUi.AppendLine();
            garageUi.Append("4) Pump air into the wheels of a car to the max");
            garageUi.AppendLine();
            garageUi.Append("5) Refuel a car");
            garageUi.AppendLine();
            garageUi.Append("6) Charge a car");
            garageUi.AppendLine();
            garageUi.Append("7) Display info about a car");
            garageUi.AppendLine();
            garageUi.Append("8) Exit");
            garageUi.AppendLine(); 
            garageUi.AppendLine(); 
            garageUi.Append("Enter your choice:");

            while (continuteOrNot)
            {
                Console.WriteLine(garageUi);
                userChoice = Console.ReadLine();
                Console.Clear();
                try
                {
                  if(!byte.TryParse(userChoice, out numberForParse))
                  {
                      throw new FormatException("Wrong input");
                  }

                  if (!LetTheUserChoose(numberForParse))
                  {
                      continuteOrNot = false;
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

        public bool LetTheUserChoose(byte i_UserChoice)
        {
            bool continuteOrNot = true;

            switch (i_UserChoice)
            {
                case 1:
                    m_UiManager.AddNewVehicleToTheGarage();
                    break;
                case 2:
                    m_UiManager.DisplayingVehiclesLicenseNumber();
                    break;
                case 3:
                    m_UiManager.ChangeVehiclesStatusInTheGarage();
                    break;
                case 4:
                    m_UiManager.FillAirToMaximum();
                    break;
                case 5:
                    m_UiManager.FillFuelVehicleTank();
                    break;
                case 6:
                    m_UiManager.ChargeElectricalVehicle();
                    break;
                case 7:
                    m_UiManager.DisplayVehicleInformation();
                    break;
                case 8:
                    continuteOrNot = false;
                    break;
                default:
                    throw new ValueOutOfRangeException(8, 1);
            }

            if (i_UserChoice != 8)
            {
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                Console.Clear();
            }

            return continuteOrNot;
        }
    }
}
