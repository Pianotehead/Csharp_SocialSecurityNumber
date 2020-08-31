using Microsoft.Win32.SafeHandles;
using System;

namespace Csharp_SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Social Security Number (YYMMDD-XXX): ");
            string socialSecurityNumber = Console.ReadLine();

            string gender = "female";
            string pronoun = "her";
            int age = 30;

            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            //Om substring(9, 1), inte bra, inga magiska nummer i koden!
            if (genderNumber % 2 != 0)
            {
                gender = "male";
                pronoun = "his";
            }

            int bornYear = Convert.ToInt32(socialSecurityNumber.Substring(0, 2)) + 1900;
            int bornMonth = Convert.ToInt32(socialSecurityNumber.Substring(2, 2));
            int bornDay = Convert.ToInt32(socialSecurityNumber.Substring(4, 2));
            DateTime thisDay = DateTime.Today;

            //Calculate age. No idea what to do with someone born 2000 or later
            age = thisDay.Year - bornYear;
            int livedMonths = thisDay.Month - bornMonth;
            int livedDays = thisDay.Day - bornDay;
            if (livedMonths < 0 || (livedMonths == 0 && livedDays < 0))
            {
                age--;
            }
            Console.WriteLine($"The person with this SSN is a { gender}, and {pronoun} age is { age}");

        }
    }
}
