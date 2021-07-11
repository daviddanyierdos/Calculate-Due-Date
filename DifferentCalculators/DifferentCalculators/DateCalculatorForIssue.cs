using System;
using System.Text.RegularExpressions;

namespace DifferentCalculators
{
    public class DateCalculatorForIssue
    {
        static void Main(string[] args)
        {
            DateCalculatorForIssue d = new DateCalculatorForIssue();
            string reportedProblemIsDueByThisDate = d.CalculateDueDate("13/07/2021 11:00AM", 7);
            Console.WriteLine("The reported problem is due by this date/time: " + reportedProblemIsDueByThisDate);
        }

        public string CalculateDueDate(string submitDateTime, uint turnaroundTime)
        {
            checkSubmitDateTimeAsPattern(submitDateTime);

            string getSubmitDayFromSubmitDateTime = submitDateTime.Substring(0, 2);
            string getSubmitMonthFromSubmitDateTime = submitDateTime.Substring(3, 2);
            string getSubmitYearFromSubmitDateTime = submitDateTime.Substring(6, 4);
            string getSubmitHourFromSubmitDateTime = submitDateTime.Substring(11, 2);
            string getSubmitMinuteFromSubmitDateTime = submitDateTime.Substring(14, 2);

            uint submitDay = uint.Parse(getSubmitDayFromSubmitDateTime);
            uint submitMonth = uint.Parse(getSubmitMonthFromSubmitDateTime);
            uint submitYear = uint.Parse(getSubmitYearFromSubmitDateTime);
            uint submitHour = uint.Parse(getSubmitHourFromSubmitDateTime);
            uint submitMinute = uint.Parse(getSubmitMinuteFromSubmitDateTime);

            submitHour = convertSubmitHourFrom12ClockSystemTo24(submitHour);

            checkCalculateDueDateInputParametersInDetails(submitDay, submitMonth, submitYear, submitHour, submitMinute, submitDateTime, turnaroundTime);

            long daysFromSubmitDayUntilDueDate = countDaysFromSubmitDayUntilDueDate(submitDay, submitMonth, submitYear, submitHour, submitMinute, turnaroundTime);

            return getDueDate(submitDay, submitMonth, submitYear, submitHour, submitMinute, turnaroundTime, daysFromSubmitDayUntilDueDate);
        }

        void checkSubmitDateTimeAsPattern(string submitDateTime)
        {
            string submitDateTimePattern = @"^[0-9]{2}\/{1}[0-9]{2}\/{1}[0-9]{4}\s{1}[0-9]{2}\:{1}[0-9]{2}[AP][M]$";
            Regex rgx = new Regex(submitDateTimePattern);
            if (!rgx.IsMatch(submitDateTime))
                throw new FormatException("The correct format for the input date/time (submitDateTime parameter): dd/MM/yyyy hh:mmAM (or PM instead of AM).");
        }

        public uint convertSubmitHourFrom12ClockSystemTo24(uint submitHour)
        {
            uint minHourAsPm = 1;
            uint maxHourAsPm = 5;
            if (submitHour >= minHourAsPm && submitHour <= maxHourAsPm) return submitHour += 12;

            return submitHour;
        }

        void checkCalculateDueDateInputParametersInDetails(uint submitDay, uint submitMonth, uint submitYear, uint submitHour, uint submitMinute, string submitDateTime, uint turnaroundTime)
        {
            string getAmOrPmFromSubmitDateTime = submitDateTime.Substring(16, 2);
            uint workStartHourIn24ClockSystem = 9;
            uint workFinishHourIn24ClockSystem = 17;

            // I used the Unix-time - 1970.01.01. 12:00AM - as a basis for this project
            if (submitYear < 1970)
                throw new ArgumentOutOfRangeException("submitYear", submitYear, "The submitYear value can't be less than 1970.");

            if (submitMonth < 1 || submitMonth > 12)
                throw new ArgumentOutOfRangeException("submitMonth", submitMonth, "The submitMonth value can be between 1 and 12.");

            if (((submitYear % 4 == 0) || (submitYear % 4 == 0 && submitYear % 100 == 0 && submitYear % 400 == 0) && submitMonth == 2) && (submitDay < 1 || submitDay > 29))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of February can be between 1 and 29 (as the current submitMonth is leap year).");

            if (submitMonth == 2 && submitYear % 4 != 0 && (submitDay < 1 || submitDay > 28))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of February can be between 1 and 28 (as the current submitMonth is not leap year).");

            if (submitMonth < 8 && submitMonth % 2 == 0 && (submitDay < 1 || submitDay > 30))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of this month can be between 1 and 30.");

            if (submitMonth < 8 && submitMonth % 2 != 0 && (submitDay < 1 || submitDay > 31))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of this month can be between 1 and 31.");

            if (submitMonth > 7 && submitMonth % 2 == 0 && (submitDay < 1 || submitDay > 31))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of this month can be between 1 and 31.");

            if (submitMonth > 7 && submitMonth % 2 != 0 && (submitDay < 1 || submitDay > 30))
                throw new ArgumentOutOfRangeException("submitDay", submitDay, "The submitDay value of this month can be between 1 and 30.");

            if (submitHour < workStartHourIn24ClockSystem || submitHour > workFinishHourIn24ClockSystem)
                throw new ArgumentOutOfRangeException("submitHour", submitHour, "We are using a 12-hour clock system (with AM/PM), and the submitHour value can be between 9AM and 5PM.");

            if (submitHour >= 12 && getAmOrPmFromSubmitDateTime == "AM")
                throw new ArgumentOutOfRangeException("submitHour", submitHour, "AM if the submitHour value is less than 12 (using a 12-hour clock system), otherwise PM.");

            if (submitHour < 12 && getAmOrPmFromSubmitDateTime == "PM")
                throw new ArgumentOutOfRangeException("submitHour", submitHour, "AM if the submitHour value is less than 12 (using a 12-hour clock system), otherwise PM.");

            if (submitMinute > 59)
                throw new ArgumentOutOfRangeException("submitMinute", submitMinute, "The submitMinute value can be between 0 and 59.");

            if (submitMinute > 0 && submitHour == workFinishHourIn24ClockSystem)
                throw new ArgumentOutOfRangeException("submitHour", submitHour, "The time - submitHour and submitMinute - can't be more than 17:00.");

            if (turnaroundTime < 1)
                throw new ArgumentOutOfRangeException("turnaroundTime", turnaroundTime, "The turnaroundTime value can be between 1 and 4294967295.");

            // check if the submitDay is weekend or not - more info in the countDaysFromSubmitDayUntilDueDate method
            long submitDayAsNumber = countDaysFromSubmitDayUntilDueDate(submitDay, submitMonth, submitYear, submitHour, submitMinute, turnaroundTime);
            if (submitDayAsNumber == -1)
                throw new Exception("You can report problems only during working hours.");
        }

        public long countDaysFromSubmitDayUntilDueDate(uint submitDay, uint submitMonth, uint submitYear, uint submitHour, uint submitMinute, uint turnaroundTime)
        {
            long daysFromSubmitDayUntilDueDate = 0;
            uint workFinishHourIn24ClockSystem = 17;
            uint numberOfWorkingHours = 8;

            if (submitMinute > 0) submitHour++;

            if (turnaroundTime >= (workFinishHourIn24ClockSystem - submitHour))
            {
                turnaroundTime -= (workFinishHourIn24ClockSystem - submitHour);
                daysFromSubmitDayUntilDueDate = turnaroundTime / numberOfWorkingHours;
                if ((turnaroundTime % numberOfWorkingHours) != 0) daysFromSubmitDayUntilDueDate++;
            }

            long copyOfDaysFromSubmitDayUntilDueDate = daysFromSubmitDayUntilDueDate;

            /* Monday - Sunday are numbers from 1 to 7. And actually we can say that in our case the "Unix-time day" (1970.01.01. 12:00AM) is just Wednesday (so as day number is 3),
            because 1970.01.01. 12:00AM is just the beginning of Thursday (so actually Thursday didn't start yet as Wednesday "ended properly", so therefore we didn't count Thursday as day).
            The distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay value can be between 0 and 6, and it represents the distance (looking at the days of the week) between the "Unix-time day"
            and the submitDay value.
            The dayAsNumberBetween1And7BasedOnSubmitDay value (which is actually the submitDay value) can be between 1 and 7, and it represents the day of a week (Monday - Sunday).
            If distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay value less than 5, it means it is between 0 and 4 (so this is a distance number, not a day number,
            so we can see how far on a week the dayAsNumberBetween1And7BasedOnSubmitDay value is compared to the "Unix-time day").
            For example if the distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay value is 4, we can say the dayAsNumberBetween1And7BasedOnSubmitDay (as a day number) value
            will be that day (so that "day" number) which's distance number is 4, and we know that Unix-time day's day number is 3 (Wednesday), and it's distance number is 0, so:
            Wednesday (it's distance number is 0) + 
            Thursday (it's distance number is 1) +
            Friday (it's distance number is 2) +
            Saturday (it's distance number is 3) +
            Sunday (it's distance number is 4)
            and here we go, the dayAsNumberBetween1And7BasedOnSubmitDay value will be 7 as a day number (which can be equated to Sunday), 
            as we add 4 to 3, so we add 4 days to the "Unix-time day".
            And if distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay value is more than 4 (as a distance number),
            it means that the dayAsNumberBetween1And7BasedOnSubmitDay as a day number value will be "more than Sunday",
            but after Sunday it comes Monday (which's day number is 1),
            so it should be Monday or Tuesday, so 1 or 2 as day number (and not further, as Wednesday already means the "Unix-time day"), so
            therefore we have to subtract 4 (and not add 3) from the distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay value.*/
            uint distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay = countDaysFromUnixTimeUntilSubmitDay(submitDay, submitMonth, submitYear) % 7;
            uint dayAsNumberBetween1And7BasedOnSubmitDay = 0;
            // if the distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay is 3 or 4 it means it is Saturday (3) or Sunday (4), but we can report problems just during working hours
            if (distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay == 3 ||
                distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay == 4) return -1;
            if (distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay < 3) dayAsNumberBetween1And7BasedOnSubmitDay = distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay + 3;
            if (distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay > 4) dayAsNumberBetween1And7BasedOnSubmitDay = distanceInWeekdaysBetweenUnixTimeDayAndSubmitDay - 4;

            uint i = 0;

            // we would like to add the weekend days as well to the daysFromSubmitDayUntilDueDate value
            while (i < copyOfDaysFromSubmitDayUntilDueDate)
            {
                if (dayAsNumberBetween1And7BasedOnSubmitDay == 5)
                {
                    dayAsNumberBetween1And7BasedOnSubmitDay = 0;
                    daysFromSubmitDayUntilDueDate += 2;
                }
                dayAsNumberBetween1And7BasedOnSubmitDay++;
                copyOfDaysFromSubmitDayUntilDueDate--;
            }

            return daysFromSubmitDayUntilDueDate;
        }

        public uint countDaysFromUnixTimeUntilSubmitDay(uint submitDay, uint submitMonth, uint submitYear)
        {
            uint daysFromUnixTimeUntilSubmitDay = 0;

            for (uint i = 1970; i < submitYear; i++)
            {
                if ((i % 4 == 0) || (i % 4 == 0 && i % 100 == 0 && i % 400 == 0)) daysFromUnixTimeUntilSubmitDay += 366;
                else daysFromUnixTimeUntilSubmitDay += 365;
            }

            for (uint i = 1; i < submitMonth; i++)
            {
                if (((submitYear % 4 == 0) || (submitYear % 4 == 0 && submitYear % 100 == 0 && submitYear % 400 == 0) && i == 2)) daysFromUnixTimeUntilSubmitDay += 29;
                if (i == 2 && submitYear % 4 != 0) daysFromUnixTimeUntilSubmitDay += 28;
                if (i < 8 && i % 2 != 0 && i != 2) daysFromUnixTimeUntilSubmitDay += 31;
                if (i < 8 && i % 2 == 0 && i != 2) daysFromUnixTimeUntilSubmitDay += 30;
                if (i > 7 && i % 2 == 0) daysFromUnixTimeUntilSubmitDay += 31;
                if (i > 7 && i % 2 != 0) daysFromUnixTimeUntilSubmitDay += 30;
            }

            daysFromUnixTimeUntilSubmitDay += submitDay;

            return daysFromUnixTimeUntilSubmitDay;
        }

        public string getDueDate(uint submitDay, uint submitMonth, uint submitYear, uint submitHour, uint submitMinute, uint turnaroundTime, long daysFromSubmitDayUntilDueDate)
        {
            uint dueDateDay = submitDay;
            uint dueDateMonth = submitMonth;
            uint dueDateYear = submitYear;

            uint dueDateHour = calculateDueDateHour(submitHour, submitMinute, turnaroundTime);

            string amOrPm = isItAmOrPm(dueDateHour);

            dueDateHour = converDueDatetHourFrom24ClockSystemTo12(dueDateHour);

            uint maxDaysOfCurrentMonth = 0;
            bool isItRunThisWhileCycleFirstTime = true;

            while (daysFromSubmitDayUntilDueDate > 0)
            {
                if (!isItRunThisWhileCycleFirstTime) dueDateDay = 0;

                if (((dueDateYear % 4 == 0) || (dueDateYear % 4 == 0 && dueDateYear % 100 == 0 && dueDateYear % 400 == 0) && dueDateMonth == 2)) maxDaysOfCurrentMonth = 29;
                if (dueDateMonth == 2 && dueDateYear % 4 != 0) maxDaysOfCurrentMonth = 28;
                if (dueDateMonth < 8 && dueDateMonth % 2 != 0) maxDaysOfCurrentMonth = 31;
                if (dueDateMonth < 8 && dueDateMonth % 2 == 0) maxDaysOfCurrentMonth = 30;
                if (dueDateMonth > 7 && dueDateMonth % 2 == 0) maxDaysOfCurrentMonth = 31;
                if (dueDateMonth > 7 && dueDateMonth % 2 != 0) maxDaysOfCurrentMonth = 30;

                while (dueDateDay != maxDaysOfCurrentMonth && daysFromSubmitDayUntilDueDate > 0)
                {
                    dueDateDay++;
                    maxDaysOfCurrentMonth--;
                    daysFromSubmitDayUntilDueDate--;
                }

                if (dueDateMonth == 12 && daysFromSubmitDayUntilDueDate != 0)
                {
                    dueDateMonth = 1;
                    dueDateYear++;
                }

                if (dueDateMonth != 12 && daysFromSubmitDayUntilDueDate != 0) dueDateMonth++;

                isItRunThisWhileCycleFirstTime = false;
            }

            return dueDateDay + "/" + dueDateMonth + "/" + dueDateYear + " " + dueDateHour + ":" + submitMinute + amOrPm;
        }

        public uint calculateDueDateHour(uint submitHour, uint submitMinute, uint turnaroundTime)
        {
            uint dueDateHour = 0;
            uint workStartHourIn24ClockSystem = 9;
            uint workFinishHourIn24ClockSystem = 17;
            uint numberOfWorkingHours = 8;

            bool isItGoodIdeaToAddOneMoreHourToSubmitHour = false;

            if (submitMinute > 0)
            {
                isItGoodIdeaToAddOneMoreHourToSubmitHour = true;
                submitHour++;
            }

            if (turnaroundTime > (workFinishHourIn24ClockSystem - submitHour))
            {
                turnaroundTime -= (workFinishHourIn24ClockSystem - submitHour);
                if (turnaroundTime <= numberOfWorkingHours) dueDateHour = turnaroundTime + workStartHourIn24ClockSystem;
                else dueDateHour = (turnaroundTime % numberOfWorkingHours) + workStartHourIn24ClockSystem;
            }
            else
            {
                dueDateHour = turnaroundTime + submitHour;
            }

            if (isItGoodIdeaToAddOneMoreHourToSubmitHour) dueDateHour--;

            return dueDateHour;
        }

        public string isItAmOrPm(uint dueDateHour)
        {
            if (dueDateHour < 12) return "AM";

            return "PM";
        }

        public uint converDueDatetHourFrom24ClockSystemTo12(uint dueDateHour)
        {
            if (dueDateHour > 12) return dueDateHour -= 12;

            return dueDateHour;
        }
    }
}
