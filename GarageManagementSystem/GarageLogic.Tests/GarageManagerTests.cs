using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Xunit;

namespace GarageLogic.Tests
{
    public class GarageManagerTests
    {
        [Fact]
        public void isTheVehicleExists_ShouldFail()
        {
            Assert.Throws<ArgumentException>("UnknownLicenseNumber",() => GarageManager.isTheVehicleExists("215"));
        }

        /// <summary>
        /// An exception wasn't thrown therefore the test passed 
        /// </summary>
        [Fact]
        public void ValidateLicenseNumberInput_ShouldPass()
        {
            Assert.Throws<ArgumentException>("LicenseNumber", () => GarageManager.ValidateLicenseNumberInput("1234567"));
        }

        [Fact]
        public void ValidateLicenseNumberInput_ShouldFail()
        {
            Assert.Throws<ArgumentException>("LicenseNumber", () => GarageManager.ValidateLicenseNumberInput(""));
        }
    }
}
