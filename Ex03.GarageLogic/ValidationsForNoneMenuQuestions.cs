using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal static class ValidationsForNoneMenuQuestions
    {
        internal static void ValidationCheckingIfCarryingDangerousChemicals(string i_UserChoice)
        {
            if (i_UserChoice != "Yes" && i_UserChoice != "No")
            {
                throw new ArgumentException("Only Yes or No!");
            }
        }

        internal static void ValidatingMaxCarryWeight(string i_UserInput)
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

        internal static void ValidatingEngineVolumeInput(string i_UserInput)
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