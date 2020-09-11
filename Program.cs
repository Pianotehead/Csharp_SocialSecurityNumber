using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Csharp_SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Usage from the terminal. Input must be in one of the following formats:\n");
            Console.WriteLine("1) Csharp_SocialSecurityNumber John Doe 890101-2010 or");
            Console.WriteLine("2) Csharp_SocialSecurityNumber John Doe 19890101-2010\n");

            Console.WriteLine("Note that you can enter an SSN for someone born 2000 or later, but if you enter");
            Console.WriteLine("an SSN with a date of birth in the future, you'll be asked to correct your input\n");

            Console.WriteLine("If no parameters are entered, the program will ask for them");
            Console.WriteLine("If too few parameters are entered, you'll be asked to start all over again.\n");
            Console.WriteLine("If the program is run from within Visual Studio or another code editor, with");
            Console.WriteLine("no parameters, you will be asked to enter them manually.\n");

            string socialSecurityNumber = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

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
                Console.Write("Please type your last name: ");
                lastName = Console.ReadLine();
                Console.Write("Please type your Social security number as [YY]YYMMDD-XXXX: ");
                socialSecurityNumber = Console.ReadLine();
            }
            else if (args.Length >= 3)

            {
                firstName = args[0];
                lastName = args[1];
                socialSecurityNumber = args[2];
            }
            firstName = RejectEmptyInput(firstName);
            lastName = RejectEmptyInput(lastName);
            socialSecurityNumber = RejectEmptyInput(socialSecurityNumber);
            socialSecurityNumber = ValidateSSN(socialSecurityNumber);

            string gender = GetGender(socialSecurityNumber);
            int age = CalculateAge(socialSecurityNumber);
            string generation = GetGeneration(age);
            const int LEFTJUSTIFY = -25;
            // OUTPUT
            Console.Clear();
            Console.WriteLine($"{"Name:",LEFTJUSTIFY} {firstName} {lastName}");
            Console.WriteLine($"{"Social Security Number:",LEFTJUSTIFY} { socialSecurityNumber}");
            Console.WriteLine($"{"Gender:",LEFTJUSTIFY} {gender}");
            if (age >= 0)
            {
                Console.WriteLine($"{"Age:",LEFTJUSTIFY} {age}");
            }
            Console.WriteLine($"{"Generation:",LEFTJUSTIFY} {generation}");
            Console.SetCursorPosition(0, 20);

        }

        private static string GetGender(string socialSecurityNumber)
        {
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumber.Length - 2, 1));
            if (genderNumber % 2 != 0)
            {
                return "Male";
            }
            else
            {
                return "Female";
            }
        }

        public static string RejectEmptyInput(string str)
        {
            while(String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str))
            {
                Console.Write("You entered nothing, or space(s). Please try again: ");
                str = Console.ReadLine();
            }
            return str;
        }

        public static string GetGeneration(int age)
        {
            if (age >= 93)
            {
                return "Greatest";
            }
            else if (age <= 92 && age >= 75)
            {
                return "Silent Generation";
            }
            else if (age <= 74 && age >= 56)
            {
                return "Baby Boomer";
            }
            else if (age <= 55 && age >= 40)
            {
                return "Generation X";
            }
            else if (age <= 39 && age >= 24)
            {
                return "Millennial";
            }
            else if (age <= 23 && age >= 0)
            {
                return  "Zoomer";
            }
            else
            {
                return "This person has not yet been born";
            }

        }

        public static string ValidateSSN(string socialSecurityNumber)
        {

            string expr = @"^(19|20)?\d{2}(01|02|03|04|05|06|07|08|09|10|11|12)((0[1-9])|(1|2)[0-9]|(30|31))-\d{4}$";
            Regex legalSSN = new Regex(expr);

            while (!legalSSN.IsMatch(socialSecurityNumber))
            {
                Console.WriteLine("Your input was not a legal social security number");
                Console.Write("Please try again: ");
                socialSecurityNumber = Console.ReadLine();

            }

            return socialSecurityNumber;
        }

        public static int CalculateAge(string socialSecurityNumber)
        {
            
            DateTime thisDay = DateTime.Today;
            bool shortSSN = socialSecurityNumber.Length == 11;
            bool longSSN = socialSecurityNumber.Length == 13;
            DateTime birthDate = new DateTime();
            int age = 0;
            if (longSSN)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.CurrentCulture);
                age = thisDay.Year - birthDate.Year;
            }
            else if (shortSSN)
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.CurrentCulture);
                // With short SSN, I want to assume a person born in the 20th century
                if (birthDate > thisDay)
                {
                    age = thisDay.Year - birthDate.Year + 100;
                }
                else
                {
                    age = thisDay.Year - birthDate.Year;
                }
                
            }
            int livedMonths = thisDay.Month - birthDate.Month;
            int livedDays = thisDay.Day - birthDate.Day;
            if (livedMonths < 0 || (livedMonths == 0 && livedDays < 0))
            {
                age--;
            }
            return age;
        }

    }
}

