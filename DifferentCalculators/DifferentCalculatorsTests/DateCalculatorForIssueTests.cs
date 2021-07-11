using Microsoft.VisualStudio.TestTools.UnitTesting;
using DifferentCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentCalculators.Tests
{
    [TestClass()]
    public class DateCalculatorForIssueTests
    {
        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenDayIsJustOneCharacter_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("2/07/1970 01:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenMoreThanOneSlash_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("2//07/1970 01:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenYearIsJustTwoCharacter_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/70 01:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenTwoSpaceCharacter_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1970  01:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenTwoColonCharacter_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1970 01::00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenNotAmOrPmAfterMinute_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1970 01:00EK", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenSlashMissingAfterDay_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("0207/1970 01:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenSpaceMissingAfterYear_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/197001:00PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenColonMissingAfterHour_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1970 0100PM", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkSubmitDateTimeAsPattern_WhenJustOneCharacterAfterMinute_ShouldThrowFormat()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1970 01:00P", 8);
            }
            catch (System.FormatException e)
            {
                StringAssert.Contains(e.Message, "The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenYearLessThan1970_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/07/1969 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitYear value can't be less than 1970.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/00/1970 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitMonth value can be between 1 and 12.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthGreaterThan12_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("02/13/1970 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitMonth value can be between 1 and 12.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenLeapYearFebruaryAndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/02/2000 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of February can be between 1 and 29 (as the current submitMonth is leap year).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenLeapYearFebruaryAndDayGreaterThan29_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/02/2000 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of February can be between 1 and 29 (as the current submitMonth is leap year).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotLeapYearFebruaryAndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/02/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of February can be between 1 and 28 (as the current submitMonth is not leap year).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotLeapYearFebruaryAndDayGreaterThan28_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("29/02/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of February can be between 1 and 28 (as the current submitMonth is not leap year).");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotFebruary_AndMonthIsEvenAndBeforeAugust_AndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/04/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 30.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotFebruary_AndMonthIsEvenAndBeforeAugust_AndDayGreaterThan30_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("31/04/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 30.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotFebruary_AndMonthIsOddAndBeforeAugust_AndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/03/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 31.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenNotFebruary_AndMonthIsOddAndBeforeAugust_AndDayGreaterThan31_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("32/03/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 31.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthIsEvenAndAfterJuly_AndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/08/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 31.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthIsEvenAndAfterJuly_AndDayGreaterThan31_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("32/10/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 31.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthIsOddAndAfterJuly_AndDayLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("00/09/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 30.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMonthIsOddAndAfterJuly_AndDayGreaterThan30_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("31/11/1999 01:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitDay value of this month can be between 1 and 30.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenHourIsLessThan9_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 08:00AM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "We are using a 12-hour clock system (with AM/PM), and the submitHour value can be between 9AM and 5PM.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenHourIsGreaterThan17_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 18:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "We are using a 12-hour clock system (with AM/PM), and the submitHour value can be between 9AM and 5PM.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenHourIsGreaterThan11_AndThereIsAMInsteadOfPM_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 12:00AM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "AM if the submitHour value is less than 12 (using a 12-hour clock system), otherwise PM.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenHourIsLessThan12_AndThereIsPMInsteadOfAM_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 10:00PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "AM if the submitHour value is less than 12 (using a 12-hour clock system), otherwise PM.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMinuteIsGreaterThan59_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 10:60AM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The submitMinute value can be between 0 and 59.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenMinuteIsGreaterThanZeroAndHourIs17_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 17:45PM", 8);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The time - submitHour and submitMinute - can't be more than 17:00.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenTurnaroundTimeIsLessThan1_ShouldThrowArgumentOutOfRange()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("30/11/1999 01:45PM", 0);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "The turnaroundTime value can be between 1 and 4294967295.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void checkCalculateDueDateInputParametersInDetails_WhenReportedProblemIsOnSunday()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            try
            {
                d.CalculateDueDate("11/07/2021 01:45PM", 3);
            }
            catch (System.Exception e)
            {
                StringAssert.Contains(e.Message, "You can report problems only during working hours.");
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod()]
        public void convertSubmitHourFrom12ClockSystemTo24_WhenSubmitHourIsGreaterThanZeroAndLessThan6()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 15;

            uint actual = d.convertSubmitHourFrom12ClockSystemTo24(3);

            Assert.AreEqual(expected, actual, 0.1, "The submitHour value is not correct.");
        }

        [TestMethod()]
        public void convertSubmitHourFrom12ClockSystemTo24_WhenSubmitHourIsGreaterThan8AndLessThan13()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 10;

            uint actual = d.convertSubmitHourFrom12ClockSystemTo24(10);

            Assert.AreEqual(expected, actual, 0.1, "The submitHour value is not correct.");
        }

        [TestMethod()]
        public void countDaysFromSubmitDayUntilDueDate_WhenSubmitMinuteIsZero_AndTurnAroundTimeIsGreaterThan17MinusSubmitHour()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            long expected = 4;

            long actual = d.countDaysFromSubmitDayUntilDueDate(02, 07, 2021, 09, 00, 17);

            Assert.AreEqual(expected, actual, 0.1, "The counted days number is not correct.");
        }

        [TestMethod()]
        public void countDaysFromSubmitDayUntilDueDate_WhenSubmitMinuteIsGreaterThanZero_AndTurnAroundTimeIsLessThan17MinusSubmitHour()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            long expected = 0;

            long actual = d.countDaysFromSubmitDayUntilDueDate(02, 07, 2021, 09, 25, 3);

            Assert.AreEqual(expected, actual, 0.1, "The counted days number is not correct.");
        }

        [TestMethod()]
        public void countDaysFromUnixTimeUntilSubmitDay_WhenChangeJustDayAndMonthCompareTo1970()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            long expected = 37;

            long actual = d.countDaysFromUnixTimeUntilSubmitDay(06, 02, 1970);

            Assert.AreEqual(expected, actual, 0.1, "The counted days number is not correct.");
        }

        [TestMethod()]
        public void countDaysFromUnixTimeUntilSubmitDay_WhenChangeDayAndMonthAndYearTooCompareTo1970()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            long expected = 402;

            long actual = d.countDaysFromUnixTimeUntilSubmitDay(06, 02, 1971);

            Assert.AreEqual(expected, actual, 0.1, "The counted days number is not correct.");
        }

        [TestMethod()]
        public void getDueDate_WhenTurnAroundTimeIsMultiplesOf8_AndSubmitMinuteIsZero()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            string expected = "7/7/2021 10:0AM";

            string actual = d.getDueDate(05, 07, 2021, 10, 00, 16, 2);

            Assert.AreEqual(expected, actual, "This due date/time is not correct.");
        }

        [TestMethod()]
        public void getDueDate_WhenTurnAroundTimeIsNotMultiplesOf8_AndSubmitMinuteIsGreaterThanZero()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            string expected = "7/7/2021 3:5PM";

            string actual = d.getDueDate(05, 07, 2021, 10, 05, 21, 2);

            Assert.AreEqual(expected, actual, "This due date/time is not correct.");
        }

        [TestMethod()]
        public void calculateDueDateHour_WhenSubmitMinuteIsZero_AndTurnAroundTimeIsLessThan17MinusSubmitHour()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 15;

            uint actual = d.calculateDueDateHour(10, 00, 5);

            Assert.AreEqual(expected, actual, 0.1, "The calculated dueDateHour value is not correct.");
        }

        [TestMethod()]
        public void calculateDueDateHour_WhenSubmitMinuteIsGreaterThanZero_AndTurnAroundTimeIsGreaterThan17MinusSubmitHour()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 11;

            uint actual = d.calculateDueDateHour(10, 12, 9);

            Assert.AreEqual(expected, actual, 0.1, "The calculated dueDateHour value is not correct.");
        }

        [TestMethod()]
        public void isItAmOrPm_WhenDueDateHourIsLessThan12()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            string expected = "AM";

            string actual = d.isItAmOrPm(10);

            Assert.AreEqual(expected, actual, "The Am / PM value is not correct.");
        }

        [TestMethod()]
        public void isItAmOrPm_WhenDueDateHourIsGreaterThan11()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            string expected = "PM";

            string actual = d.isItAmOrPm(12);

            Assert.AreEqual(expected, actual, "The Am / PM value is not correct.");
        }

        [TestMethod()]
        public void converDueDatetHourFrom24ClockSystemTo12_WhenDueDateHourIsGreaterThan12()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 2;

            uint actual = d.converDueDatetHourFrom24ClockSystemTo12(14);

            Assert.AreEqual(expected, actual, 0.1, "The converted dueDateHour value is not correct.");
        }

        [TestMethod()]
        public void converDueDatetHourFrom24ClockSystemTo12_WhenDueDateHourIsLessThan13()
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();

            uint expected = 12;

            uint actual = d.converDueDatetHourFrom24ClockSystemTo12(12);

            Assert.AreEqual(expected, actual, 0.1, "The converted dueDateHour value is not correct.");
        }

    }
}