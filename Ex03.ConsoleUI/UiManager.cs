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
        private GarageManger m_GarageManger = new GarageManger();

        internal void AddNewVehicleToTheGarage()
        {
            string userChoiceForVehicleType;
            string usersLicenseNumber;
            CreatingConcreteVehicles.eVehicleTypes vehicleTypesForValidate = CreatingConcreteVehicles.eVehicleTypes.None;
            GarageManger.VehicleInGarage vehicleInGarage = new GarageManger.VehicleInGarage();

            Console.WriteLine("Hello, Please enter your license number between 0 - 7 digits positive number : ");
            usersLicenseNumber = Console.ReadLine();
            try
            {
                m_GarageManger.ValidateUsersLicenseInput(usersLicenseNumber);
                if (m_GarageManger.CheckIfTheVehicleAlreadyExists(usersLicenseNumber))
                {
                    m_GarageManger.AlreadyExistsVehicle(usersLicenseNumber);
                }

                Console.WriteLine("Choose the type of vehicle you'd like to enter to the garage : ");
                Console.WriteLine(m_GarageManger.GeneralMenu(vehicleTypesForValidate));
                Console.WriteLine("Enter your choice: ");
                userChoiceForVehicleType = Console.ReadLine();
                m_GarageManger.ValidateUsersInputBasedOnTheRangeOfThisEnum(userChoiceForVehicleType, vehicleTypesForValidate);
                vehicleInGarage = m_GarageManger.CreateVehicleToTheGarage(usersLicenseNumber, userChoiceForVehicleType);
                EnteringInfo(vehicleInGarage);
                m_GarageManger.VehicleInGarageDictionary.Add(vehicleInGarage.CurrentVehicle.LicenseNumber, vehicleInGarage);
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

        internal void EnteringInfo(GarageManger.VehicleInGarage io_VehicleInGarage)
        {
            EnteringGeneralInfo(io_VehicleInGarage);
            EnteringUniqueInfo(io_VehicleInGarage);
        }

        internal void EnteringGeneralInfo(GarageManger.VehicleInGarage io_VehicleInGarage)
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
            io_VehicleInGarage.OwnerName = fullName;
            io_VehicleInGarage.PhoneNumber = phoneNumber;
            io_VehicleInGarage.CurrentVehicle.ModelName = modelName;
            m_GarageManger.InsertManufactureName(io_VehicleInGarage, manufactureOfTheWheels);
        }

        internal void ValidateThatTheInputIsNotEmpty(string i_Input)
        {
            if (m_GarageManger.ValidateVehiclesGeneralInfo(i_Input))
            {
                throw new ArgumentException("Can't have no value");
            }
        }

        internal void EnteringUniqueInfo(GarageManger.VehicleInGarage io_VehicleInGarage)
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
                m_GarageManger.UpdateVehicleUniqueInfo(stringInfo, userChoice, io_VehicleInGarage);
            }
        }

        internal void DisplayingVehiclesLicenseNumber()
        {
            string userInput;
            GarageManger.VehicleInGarage.eStateInTheGarage typeOfEnum = GarageManger.VehicleInGarage.eStateInTheGarage.None;

            Console.Write("Enter how you'd want to filter all vehicles licenses, or display them all");
            Console.Write(m_GarageManger.GeneralMenu(typeOfEnum));
            Console.WriteLine(Enum.GetNames(typeof(GarageManger.VehicleInGarage.eStateInTheGarage)).Length + ") show all license numbers in the garage");
            Console.WriteLine();
            Console.WriteLine("Enter your choice: ");
            
            userInput = Console.ReadLine();
            try
            {
                StringBuilder displayingLicenseNumber;
                displayingLicenseNumber = m_GarageManger.DisplayingLicenseNumberAccordingToState(userInput);
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
            GarageManger.VehicleInGarage.eStateInTheGarage newState;
            GarageManger.VehicleInGarage.eStateInTheGarage typeOfEnum = GarageManger.VehicleInGarage.eStateInTheGarage.None;

            Console.WriteLine("Please enter your license number: ");
            licenseNumber = Console.ReadLine();
            try
            {
                m_GarageManger.ValidateLicenseNumber(licenseNumber);
                Console.Write("\nPlease enter the new state you'd like:");
                Console.WriteLine(m_GarageManger.GeneralMenu(typeOfEnum));
                Console.Write("Enter your choice: ");
                stateToBeChangedTo = Console.ReadLine();
                m_GarageManger.ValidateUsersInputBasedOnTheRangeOfThisEnum(stateToBeChangedTo, typeOfEnum);
                m_GarageManger.ChangingTheStateOfTheVehicle(licenseNumber, stateToBeChangedTo);
                newState = (GarageManger.VehicleInGarage.eStateInTheGarage)Enum.Parse(typeof(GarageManger.VehicleInGarage.eStateInTheGarage), stateToBeChangedTo);
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
                m_GarageManger.FillAirInTheTiresToTheMaximum(Console.ReadLine());
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
                m_GarageManger.ValidateLicenseNumber(licenseNumber);
                m_GarageManger.CheckIfTheVehicleHasFuelEngine(licenseNumber);
                Console.Write("Choose the type of fuel you'd like to fill your vehicle with :");
                Console.WriteLine(m_GarageManger.GeneralMenu(typeOfEnum));
                Console.Write("Enter your choice: ");
                typeOfFuel = Console.ReadLine();
                m_GarageManger.ValidateUsersInputBasedOnTheRangeOfThisEnum(typeOfFuel, typeOfEnum);
                Console.WriteLine();
                Console.WriteLine("Please enter the amount you'd like to fill your fuel tank with: ");
                amountToFill = Console.ReadLine();
                m_GarageManger.FillingTheFuelTank(licenseNumber, typeOfFuel, amountToFill);
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
                m_GarageManger.ValidateLicenseNumber(licenseNumber);
                m_GarageManger.CheckIfTheVehicleHasElectricalEngine(licenseNumber);
                Console.WriteLine();
                Console.WriteLine("Please enter how many minutes you'd like to charge your vehicle: ");
                minutesToCharge = Console.ReadLine();
                m_GarageManger.ChargingElectricalVehicle(licenseNumber, minutesToCharge);
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
                m_GarageManger.ValidateLicenseNumber(licenseNumber);
                Console.WriteLine(m_GarageManger.DisplayVehicleInfo(licenseNumber));
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
