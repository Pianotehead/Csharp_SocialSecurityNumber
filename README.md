# Social Security Number

Console app that lets a user enter a social security number (SSN), 
upon which the gender and age of the person is reported.

## Getting started

1. Clone the repository.
2. Run solution.

## Running the program

If the app is run from the terminal,
input must be in one of the following formats:

1) Csharp_SocialSecurityNumber John Doe 890101-2010 or
2) Csharp_SocialSecurityNumber John Doe 19890101-2010

If you want to find out the age of someone born 2000 or later,
you must enter an SSN in the long format (13 characters).

If you enter an SSN in shorter format, 11 characters,
the program will assume that person is born in the 20th century

If you enter an SSN with a date (YYYYMMDD) in the future,
the program will run, but age will not be published,
and under the heading Generation, the program will show
as output, that the person has not yet been born.

If no parameters are entered, the program will ask for them
If too few parameters are entered, you'll be asked to start all over again.

If the program is run from within Visual Studio or another code editor, with
no parameters, you will be asked to enter them manually.