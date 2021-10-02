using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private const string m_InRepair = "1";
        private readonly Dictionary<string, VehicleInGarage> r_VehiclesInGarageDictionary = new Dictionary<string, VehicleInGarage>();

        public class VehicleInGarage
        {
            private Vehicle m_CurrentVehicle;
            private string m_OwnerName;
            private string m_PhoneNumber;
            private eStateInTheGarage m_StateInTheGarage = eStateInTheGarage.InRepair;

            public enum eStateInTheGarage
            {
                None,
                InRepair,
                Fixed,
                Paid
            }

            public eStateInTheGarage StateInTheGarage
            {
                get { return m_StateInTheGarage; }
                set { m_StateInTheGarage = value; }
            }

            public Vehicle CurrentVehicle
            {
                get { return m_CurrentVehicle; }
                set { m_CurrentVehicle = value; }
            }

            internal string OwnerName
            {
                get { return m_OwnerName; }
                set { m_OwnerName = value; }
            }

            internal string PhoneNumber
            {
                get { return m_PhoneNumber; }
                set { m_PhoneNumber = value; }
            }

            internal string DisplayVehicleInGarageInfo()
            {
                string msg = string.Format(@"Owner name: {0}
Phone number: {1}
State in Garage: {2}
Type of vehicle: {3}", 
                    OwnerName,
                    PhoneNumber,
                    StateInTheGarage,
                    CurrentVehicle.GetType().Name);
                msg += CurrentVehicle.DisplayVehicleInfo();

                return msg;
            }
        }

        public void AddNewVehicleToTheDictionary(VehicleInGarage i_VehicleToAdd)
        {
            r_VehiclesInGarageDictionary.Add(i_VehicleToAdd.CurrentVehicle.LicenseNumber, i_VehicleToAdd);
        }

        public VehicleInGarage CreateVehicleToTheGarage(string i_UsersLicenseNumber, string i_UserChoice)
        {
            VehicleInGarage tempVehicleInGarage = new VehicleInGarage();

            tempVehicleInGarage.CurrentVehicle = CreatingConcreteVehicles.CreateTheVehicle(i_UserChoice, i_UsersLicenseNumber);

            return tempVehicleInGarage;
        }

        public void ValidateUsersLicenseInput(string i_UsersLicenseNumber)
        {
            uint numberForParse;
            if (!uint.TryParse(i_UsersLicenseNumber, out numberForParse))
            {
                throw new FormatException("Wrong input, only numbers between 0 - 9999999 !");
            }

            if (numberForParse < 0 || numberForParse > 9999999)
            {
                throw new ValueOutOfRangeException(9999999, 0);
            }
        }

        public bool ValidateVehiclesGeneralInfo(string i_UserChoice)
        {
            return i_UserChoice == string.Empty;
        }

        public bool CheckIfTheVehicleAlreadyExists(string i_UsersLicenseNumber)
        {
            return r_VehiclesInGarageDictionary.ContainsKey(i_UsersLicenseNumber);
        }

        public void AlreadyExistsVehicle(string i_UsersLicenseNumber)
        {
            ChangingTheStateOfTheVehicle(i_UsersLicenseNumber, m_InRepair);
            throw new ArgumentException("This vehicle is already in the garage, the vehicle is in repair");
        }

        public StringBuilder DisplayingLicenseNumberAccordingToState(string i_UserChoice)
        {
            byte numberForParse;
            VehicleInGarage.eStateInTheGarage vehicleState;
            StringBuilder licenseNumbers = new StringBuilder();

            /// gets the size of the enum
            byte enumSize = (byte)Enum.GetNames(typeof(VehicleInGarage.eStateInTheGarage)).Length; 
            ValidateUsersInputForMenuInTheRange(enumSize, i_UserChoice);
            numberForParse = byte.Parse(i_UserChoice);

            /// checking if the user wanted to display all the vehicles in the garage
            if (numberForParse == enumSize) 
            {
                foreach (KeyValuePair<string, VehicleInGarage> vehicleInGarage in r_VehiclesInGarageDictionary)
                {
                    licenseNumbers.Append(vehicleInGarage.Key);
                    licenseNumbers.AppendLine();
                }
            }
            else
            {
                vehicleState = (VehicleInGarage.eStateInTheGarage)Enum.Parse(typeof(VehicleInGarage.eStateInTheGarage), i_UserChoice);
                foreach (KeyValuePair<string, VehicleInGarage> vehicleInGarage in r_VehiclesInGarageDictionary)
                {
                    if (vehicleInGarage.Value.StateInTheGarage == vehicleState)
                    {
                        licenseNumbers.Append(vehicleInGarage.Key);
                        licenseNumbers.AppendLine();
                    }
                }

                if (licenseNumbers.Length == 0)
                {
                    licenseNumbers.Append("None of the vehicles are " + vehicleState);
                }
            }

            return licenseNumbers;
        }

        public void ChangingTheStateOfTheVehicle(string i_LicenseNumber, string i_NewVehiclesState)
        {
            VehicleInGarage.eStateInTheGarage vehicleState;

            vehicleState = (VehicleInGarage.eStateInTheGarage)Enum.Parse(typeof(VehicleInGarage.eStateInTheGarage), i_NewVehiclesState);
            r_VehiclesInGarageDictionary[i_LicenseNumber].StateInTheGarage = vehicleState;
        }

        public void FillAirInTheTiresToTheMaximum(string i_LicenseNumber)
        {
            ValidateLicenseNumberInput(i_LicenseNumber);
            isTheVehicleExists(i_LicenseNumber);
            r_VehiclesInGarageDictionary[i_LicenseNumber].CurrentVehicle.PumpingAirToTheMax();
        }

        public void ValidateUsersInputForMenuInTheRange(byte i_SizeOfCurrentEnum, string i_UserChoice)
        {
            byte numberForParse;

            if (!byte.TryParse(i_UserChoice, out numberForParse))
            {
                throw new FormatException("Wrong input");
            }

            if (numberForParse < 1 || numberForParse > i_SizeOfCurrentEnum)
            {
                throw new ValueOutOfRangeException((float)i_SizeOfCurrentEnum, 1);
            }
        }

        public void UpdateVehicleUniqueInfo(string i_KeyMessage, string i_UserInput, VehicleInGarage i_SpecificVehicle)
        {
            i_SpecificVehicle.CurrentVehicle.UpdateUniqueInfo(i_KeyMessage, i_UserInput);
        }

        public void ValidateUsersInputBasedOnTheRangeOfThisEnum(string i_UserChoice, Enum i_EnumType)
        {
            byte statesCount;

            statesCount = (byte)(Enum.GetNames(i_EnumType.GetType()).Length - 1); // gets the size of the enum -1 because of None state
            ValidateUsersInputForMenuInTheRange(statesCount, i_UserChoice);
        }

        public void ChargingElectricalVehicle(string i_LicenseNumber, string i_MinutesToCharge)
        {
            float numberToParse;

            validatingUserInputAsFloatNumber(i_MinutesToCharge);
            numberToParse = float.Parse(i_MinutesToCharge);
            (r_VehiclesInGarageDictionary[i_LicenseNumber].CurrentVehicle.Engine as ElectricalEngine).ChargingBattery(numberToParse / 60);
        }

        public void CheckIfTheVehicleHasElectricalEngine(string i_LicenseNumber)
        {
            if (!(r_VehiclesInGarageDictionary[i_LicenseNumber].CurrentVehicle.Engine is ElectricalEngine))
            {
                throw new FormatException("It's not an Electrical vehicle ");
            }
        }

        private void validatingUserInputAsFloatNumber(string i_UserInput)
        {
            float numberToParse;

            if (!float.TryParse(i_UserInput, out numberToParse))
            {
                throw new FormatException("Wrong input");
            }
        }

        public void CheckIfTheVehicleHasFuelEngine(string i_LicenseNumber)
        {
            if (!(r_VehiclesInGarageDictionary[i_LicenseNumber].CurrentVehicle.Engine is FuelEngine))
            {
                throw new FormatException("It's not a regular vehicle ");
            }
        }

        public void FillingTheFuelTank(string i_LicenseNumber, string i_TypeOfFuel, string i_AmountToFill)
        {
            float amountToParseForFuel;
            FuelEngine.eTypeOfFuel fuelTypeForParse;

            validatingUserInputAsFloatNumber(i_AmountToFill);
            amountToParseForFuel = float.Parse(i_AmountToFill);
            fuelTypeForParse = (FuelEngine.eTypeOfFuel)Enum.Parse(typeof(FuelEngine.eTypeOfFuel), i_TypeOfFuel);
            (r_VehiclesInGarageDictionary[i_LicenseNumber].CurrentVehicle.Engine as FuelEngine).FillTheFuelTank(amountToParseForFuel, fuelTypeForParse);
        }

        public string DisplayVehicleInfo(string i_LicenseNumber)
        {
            return r_VehiclesInGarageDictionary[i_LicenseNumber].DisplayVehicleInGarageInfo();
        }

        public void ValidateLicenseNumberInput(string i_LicenseNumber)
        {
            uint numberToParse;

            if (!uint.TryParse(i_LicenseNumber, out numberToParse))
            {
                throw new FormatException("Wrong input");
            }

            //if (!r_VehiclesInGarageDictionary.ContainsKey(i_LicenseNumber))
            //{
            //    throw new ArgumentException("There is no such car in the garage");
            //}
        }

        public void isTheVehicleExists(string i_LicenseNumber)
        {
            uint numberToParse;
            numberToParse = uint.Parse(i_LicenseNumber);

            if (!r_VehiclesInGarageDictionary.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("There is no such car in the garage");
            }
        }

        public void AddGeneralInfo(VehicleInGarage io_VehicleInGarage, string i_OwnerName, string i_PhoneNumber, string i_ModelName)
        {
            io_VehicleInGarage.OwnerName = i_OwnerName;
            io_VehicleInGarage.PhoneNumber = i_PhoneNumber;
            io_VehicleInGarage.CurrentVehicle.ModelName = i_ModelName;
        }

        public StringBuilder GeneralMenu(Enum i_TypeOfEnum)
        {
            int index = 0;

            StringBuilder tempStringBuilder = new StringBuilder();
            Type typeOfEnum = i_TypeOfEnum.GetType();
            tempStringBuilder.AppendLine();
            foreach (string state in Enum.GetNames(typeOfEnum))
            {
                if (state != "None")
                {
                    tempStringBuilder.Append(index + ") ");
                    tempStringBuilder.Append(state);
                    tempStringBuilder.AppendLine();
                }

                index++;
            }

            return tempStringBuilder;
        }
    }
}