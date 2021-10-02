using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UiManager
    {
        private GarageManager m_GarageManager = new GarageManager();

        internal void AddNewVehicleToTheGarage()
        {
            string userChoiceForVehicleType;
            string usersLicenseNumber;
            CreatingConcreteVehicles.eVehicleTypes vehicleTypesForValidate = CreatingConcreteVehicles.eVehicleTypes.None;
            GarageManager.VehicleInGarage vehicleInGarage = new GarageManager.VehicleInGarage();

            Console.WriteLine("Hello, Please enter your license number between 0 - 7 digits positive number : ");
            usersLicenseNumber = Console.ReadLine();
            try
            {
                m_GarageManager.ValidateUsersLicenseInput(usersLicenseNumber);
                if (m_GarageManager.CheckIfTheVehicleAlreadyExists(usersLicenseNumber))
                {
                    m_GarageManager.AlreadyExistsVehicle(usersLicenseNumber);
                }

                Console.WriteLine("Choose the type of vehicle you'd like to enter to the garage : ");
                Console.WriteLine(m_GarageManager.GeneralMenu(vehicleTypesForValidate));
                Console.WriteLine("Enter your choice: ");
                userChoiceForVehicleType = Console.ReadLine();
                m_GarageManager.ValidateUsersInputBasedOnTheRangeOfThisEnum(userChoiceForVehicleType, vehicleTypesForValidate);
                vehicleInGarage = m_GarageManager.CreateVehicleToTheGarage(usersLicenseNumber, userChoiceForVehicleType);
                EnteringInfo(vehicleInGarage);
                m_GarageManager.AddNewVehicleToTheDictionary(vehicleInGarage);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void EnteringInfo(GarageManager.VehicleInGarage io_VehicleInGarage)
        {
            EnteringGeneralInfo(io_VehicleInGarage);
            EnteringUniqueInfo(io_VehicleInGarage);
        }

        internal void EnteringGeneralInfo(GarageManager.VehicleInGarage i_VehicleInGarage)
        {
            string fullName = string.Empty;
            string phoneNumber = string.Empty;
            string modelName = string.Empty;
            string manufactureOfTheWheels = string.Empty;

            Console.WriteLine("Please enter your name: ");
            fullName = Console.ReadLine();
            ValidateThatTheInputIsNotEmpty(fullName);
            Console.WriteLine("Please enter your phone number: ");
            phoneNumber = Console.ReadLine();
            ValidateThatTheInputIsNotEmpty(phoneNumber);
            Console.WriteLine("Please enter the model of the vehicle: ");
            modelName = Console.ReadLine();
            ValidateThatTheInputIsNotEmpty(modelName);
            Console.WriteLine("Please enter the manufacture of the wheels: ");
            manufactureOfTheWheels = Console.ReadLine();
            ValidateThatTheInputIsNotEmpty(manufactureOfTheWheels);
            m_GarageManager.AddGeneralInfo(i_VehicleInGarage, fullName, phoneNumber, modelName);
            i_VehicleInGarage.CurrentVehicle.InsertManufactureName(manufactureOfTheWheels);
        }

        internal void ValidateThatTheInputIsNotEmpty(string i_Input)
        {
            if (m_GarageManager.ValidateVehiclesGeneralInfo(i_Input))
            {
                throw new ArgumentException("Can't have no value");
            }
        }

        internal void EnteringUniqueInfo(GarageManager.VehicleInGarage io_VehicleInGarage)
        {
            Hashtable uniqueInfo = new Hashtable();
            string userChoice = string.Empty;

            uniqueInfo = io_VehicleInGarage.CurrentVehicle.FetchUniqueInfo();
            
            foreach (string stringInfo in uniqueInfo.Keys)
            {
                Console.WriteLine(stringInfo);
                if (uniqueInfo[stringInfo] != null)
                {
                    Console.WriteLine(uniqueInfo[stringInfo]);
                }

                Console.WriteLine("Your choice: ");
                userChoice = Console.ReadLine();
                m_GarageManager.UpdateVehicleUniqueInfo(stringInfo, userChoice, io_VehicleInGarage);
            }
        }

        internal void DisplayingVehiclesLicenseNumber()
        {
            string userInput;
            GarageManager.VehicleInGarage.eStateInTheGarage typeOfEnum = GarageManager.VehicleInGarage.eStateInTheGarage.None;

            Console.Write("Enter how you'd want to filter all vehicles licenses, or display them all");
            Console.Write(m_GarageManager.GeneralMenu(typeOfEnum));
            Console.WriteLine(Enum.GetNames(typeof(GarageManager.VehicleInGarage.eStateInTheGarage)).Length + ") show all license numbers in the garage");
            Console.WriteLine();
            Console.WriteLine("Enter your choice: ");
            
            userInput = Console.ReadLine();
            try
            {
                StringBuilder displayingLicenseNumber;
                displayingLicenseNumber = m_GarageManager.DisplayingLicenseNumberAccordingToState(userInput);
                Console.WriteLine();
                Console.WriteLine("The license numbers are: ");
                Console.WriteLine(displayingLicenseNumber);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void ChangeVehiclesStatusInTheGarage()
        {
            string licenseNumber;
            string stateToBeChangedTo;
            GarageManager.VehicleInGarage.eStateInTheGarage newState;
            GarageManager.VehicleInGarage.eStateInTheGarage typeOfEnum = GarageManager.VehicleInGarage.eStateInTheGarage.None;

            Console.WriteLine("Please enter your license number: ");
            licenseNumber = Console.ReadLine();
            try
            {
                m_GarageManager.ValidateLicenseNumberInput(licenseNumber);
                m_GarageManager.isTheVehicleExists(licenseNumber);
                Console.Write("\nPlease enter the new state you'd like:");
                Console.WriteLine(m_GarageManager.GeneralMenu(typeOfEnum));
                Console.Write("Enter your choice: ");
                stateToBeChangedTo = Console.ReadLine();
                m_GarageManager.ValidateUsersInputBasedOnTheRangeOfThisEnum(stateToBeChangedTo, typeOfEnum);
                m_GarageManager.ChangingTheStateOfTheVehicle(licenseNumber, stateToBeChangedTo);
                newState = (GarageManager.VehicleInGarage.eStateInTheGarage)Enum.Parse(typeof(GarageManager.VehicleInGarage.eStateInTheGarage), stateToBeChangedTo);
                Console.WriteLine("Changed status successfully to {0}", newState);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void FillAirToMaximum()
        {
            Console.WriteLine("Please enter your license number: ");
            try
            {
                m_GarageManager.FillAirInTheTiresToTheMaximum(Console.ReadLine());
                Console.WriteLine("Pumped air to maximum successfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void FillFuelVehicleTank()
        {
            string licenseNumber;
            string amountToFill;
            string typeOfFuel;
            FuelEngine.eTypeOfFuel typeOfEnum = FuelEngine.eTypeOfFuel.None;
            
            Console.WriteLine("Please enter your license number: ");
            licenseNumber = Console.ReadLine();
            Console.WriteLine();
            try
            {
                m_GarageManager.ValidateLicenseNumberInput(licenseNumber);
                m_GarageManager.isTheVehicleExists(licenseNumber);
                m_GarageManager.CheckIfTheVehicleHasFuelEngine(licenseNumber);
                Console.Write("Choose the type of fuel you'd like to fill your vehicle with :");
                Console.WriteLine(m_GarageManager.GeneralMenu(typeOfEnum));
                Console.Write("Enter your choice: ");
                typeOfFuel = Console.ReadLine();
                m_GarageManager.ValidateUsersInputBasedOnTheRangeOfThisEnum(typeOfFuel, typeOfEnum);
                Console.WriteLine();
                Console.WriteLine("Please enter the amount you'd like to fill your fuel tank with: ");
                amountToFill = Console.ReadLine();
                m_GarageManager.FillingTheFuelTank(licenseNumber, typeOfFuel, amountToFill);
                Console.WriteLine("Vehicle with license number: {0} was Successfully fueled ", licenseNumber);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void ChargeElectricalVehicle()
        {
            string licenseNumber;
            string minutesToCharge;
            Console.WriteLine("Please enter your license number: ");
            licenseNumber = Console.ReadLine();

            try
            {
                m_GarageManager.ValidateLicenseNumberInput(licenseNumber);
                m_GarageManager.isTheVehicleExists(licenseNumber);
                m_GarageManager.CheckIfTheVehicleHasElectricalEngine(licenseNumber);
                Console.WriteLine();
                Console.WriteLine("Please enter how many minutes you'd like to charge your vehicle: ");
                minutesToCharge = Console.ReadLine();
                m_GarageManager.ChargingElectricalVehicle(licenseNumber, minutesToCharge);
                Console.WriteLine("Vehicle with license number: {0} was Successfully charged ", licenseNumber);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void DisplayVehicleInformation()
        {
            string licenseNumber = string.Empty;
            Console.WriteLine("Please enter your license number: ");
            licenseNumber = Console.ReadLine();
            try
            {
                m_GarageManager.ValidateLicenseNumberInput(licenseNumber);
                m_GarageManager.isTheVehicleExists(licenseNumber);
                Console.WriteLine(m_GarageManager.DisplayVehicleInfo(licenseNumber));
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
