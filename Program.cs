// using Microsoft.Win32.SafeHandles;
using System;

namespace Csharp_SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            // INPUT
            // 1) From the terminal: Csharp_SocialSecurityNumber John Doe 890101-2010
            // 2) From the terminal: Csharp_SocialSecurityNumber {no parameters}
            // 3) Run in Visual Studio (same as running it from the terminal with no parameters)
            // OUTPUT
            // Name:                   John Doe
            // Social Security Number: 800101 - 2010
            // Gender:                 Male
            // Age:                    31
            // Generation:             Millennial
            string socialSecurityNumber = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;


            // Make it clear to the user, that the input has to be in the correct...
            // ...format to prevent exception errors
            if (args.Length < 3)
            {
                if (args.Length > 0)
                {
                    Console.WriteLine("You entered too few arguments.");
                    Console.WriteLine("Your previous input will be ignored.");
                    Console.WriteLine("You will have to type them in all over again.");

                }
                Console.Write("Please type your first name: ");
                firstName = Console.ReadLine();
                firstName = ValidateName(firstName);
                Console.Write("Please type your last name: ");
                lastName = Console.ReadLine();
                lastName = ValidateName(lastName);
                Console.Write("Please type your Social security number as YYMMDD-XXXX: ");
                socialSecurityNumber = Console.ReadLine();
                socialSecurityNumber = ValidateSSN(socialSecurityNumber);
            }
            // The code works up to this line
            else if (args.Length == 3)

            {   // Read the console input and validate it
                firstName = ValidateName(args[0]);
                lastName = ValidateName(args[1]);
                socialSecurityNumber = ValidateSSN(args[2]);
            }
            string gender = "female";
            int age = 30;

            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            //Om substring(9, 1), inte bra, inga magiska nummer i koden!
            if (genderNumber % 2 != 0)
            {
                gender = "male";
            }
            int indexYear = 0;
            int startIndex = 0;
            if (socialSecurityNumber.Length == 11)
            {
                startIndex = 0;
            }
            else if (socialSecurityNumber.Length == 13)
            {
                startIndex = 2;
            }
            int bornYear = Convert.ToInt32(socialSecurityNumber.Substring(startIndex, 2)) + 1900;
            int bornMonth = Convert.ToInt32(socialSecurityNumber.Substring(startIndex + 2, 2));
            int bornDay = Convert.ToInt32(socialSecurityNumber.Substring(startIndex + 4, 2));
            DateTime thisDay = DateTime.Today;

            //Calculate age. No idea what to do with someone born 2000 or later
            age = thisDay.Year - bornYear;
            int livedMonths = thisDay.Month - bornMonth;
            int livedDays = thisDay.Day - bornDay;
            if (livedMonths < 0 || (livedMonths == 0 && livedDays < 0))
            {
                age--;
            }
            string generation = string.Empty;
            generation = GetGeneration(bornYear);
            // OUTPUT
            Console.Clear();
            Console.SetCursorPosition(0, 20);
            Console.Write("Name:");
            Console.CursorLeft = 24;
            Console.WriteLine($"{firstName} {lastName}");
            Console.WriteLine($"Social Security Number: {socialSecurityNumber,2}");
            Console.WriteLine($"Gender: {gender,20}");
            Console.Write("Age:");
            Console.CursorLeft = 24;
            Console.WriteLine(age);
            Console.CursorLeft = 24;
            Console.WriteLine(generation);
            Console.SetCursorPosition(0, 35);

        }

        public static string GetGeneration(int bornYear)
        {
            string generation;
            if (bornYear <= 1927 && bornYear >= 1901)
            {
                generation = "Greatest";
            }
            else if (bornYear <= 1945 && bornYear >= 1928)
            {
                generation = "Silent Generation";
            }
            else if (bornYear <= 1964 && bornYear >= 1946)
            {
                generation = "Baby Boomer";
            }
            else if (bornYear <= 1980 && bornYear >= 1965)
            {
                generation = "Generation X";
            }
            else if (bornYear <= 1996 && bornYear >= 1981)
            {
                generation = "Millennial";
            }
            else
            {
                generation = "Zoomer";
            }

            return generation;
        }

        public static string ValidateName(string name)
        {
            while (String.IsNullOrWhiteSpace(name))
            {
                Console.Write("You entered nothing, or space(s). Please try again: ");
                name = Console.ReadLine();
            }

            return name;
        }

        public static string ValidateSSN(string socialSecurityNumber)
        {
            
            bool validSSN = !String.IsNullOrEmpty(socialSecurityNumber);
            bool rightSizeSSN = false;
            bool isNumeric = false;
            long temp;
            string noHyphen;

            if (validSSN)
            {
                rightSizeSSN = socialSecurityNumber.Length == 11 || socialSecurityNumber.Length == 13;
                //Remove the hyphen(-) from SSN, and save as new string to check for mumericality
                if (rightSizeSSN && socialSecurityNumber.Contains('-'))
                {
                    noHyphen = socialSecurityNumber.Remove(socialSecurityNumber.IndexOf('-'), 1);
                    isNumeric = Int64.TryParse(noHyphen, out temp);
                }
                validSSN = rightSizeSSN && isNumeric;
            }
            while (!validSSN)
            {
                Console.WriteLine("Your input was not in the correct format, or empty");
                Console.WriteLine("It has to be either as YYMMDD-XXXX or YYYYMMDD-XXXX");
                Console.Write("Please try again: ");
                socialSecurityNumber = Console.ReadLine();
                validSSN = !String.IsNullOrEmpty(socialSecurityNumber);
                if (validSSN)
                {
                    rightSizeSSN = socialSecurityNumber.Length == 11 || socialSecurityNumber.Length == 13;
                    if (rightSizeSSN && socialSecurityNumber.Contains('-'))
                    {
                        noHyphen = socialSecurityNumber.Remove(socialSecurityNumber.IndexOf('-'), 1);
                        isNumeric = Int64.TryParse(noHyphen, out temp);
                    }
                    validSSN = rightSizeSSN && isNumeric;
                }
            }

            return socialSecurityNumber;
        }
    }
}
