using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.ConsoleUI;
using Ex03.GarageLogic;
using Xunit;

namespace GarageLogic.Tests
{
    public class UiMenuTests
    {
        private UiMenu m_UiMenu = new UiMenu();

        public UiMenu UiMenu
        {
            get
            {
                return m_UiMenu;
            }

        }

        /// <summary>
        /// The test passed according to that an exception wasn't thrown.
        /// </summary>
        [Fact]
        public void checkIfTheInputIsValid_ShouldWork()
        {
            //UiMenu UiMenu = new UiMenu();
            byte numberForParse;
            Assert.Throws<FormatException>(() => UiMenu.checkIfTheInputIsValid("2", out numberForParse));
        }


        [Fact]
        public void checkIfTheInputIsValid_ShouldFailNegativeNumber()
        {
            //  UiMenu UiMenu = new UiMenu();
            byte numberForParse;
            Assert.Throws<FormatException>(() => UiMenu.checkIfTheInputIsValid("-1", out numberForParse));
        }

        [Fact]
        public void checkIfTheInputIsValid_ShouldFailNotANumber()
        {
            // UiMenu UiMenu = new UiMenu();
            byte numberForParse;
            Assert.Throws<FormatException>(() => UiMenu.checkIfTheInputIsValid("@", out numberForParse));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(9)]
        [InlineData(byte.MaxValue)]
        public void checkIfTheInputSuitsTheEnumRange_ShouldFail(byte NumberForRange)
        {
            Assert.Throws<ValueOutOfRangeException>(() => UiMenu.checkIfTheInputSuitsTheEnumRange(NumberForRange));
        }
    }
}