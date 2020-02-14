using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Sweet_Home
{
    class User
    {
        // Special features
        List<Home> homes;

        // normal data members
        string name;
        string email;
        string password; // generate a hashcode before saving it to database (one-step-to-security)
        char gender;

        // initializing 
        public User() {
            homes = new List<Home>();

            name = null;
            email = null;
            password = null;
            gender = '\0';
        }

        public void register() {
            try {

                Console.WriteLine("\nRegister\n");

                int flagName = 0, flagGender = 0;
                Console.WriteLine("Please Enter your name -> ");
                name = Console.ReadLine();
                do {
                    if (name.Length > 20)
                    {
                        Console.WriteLine("Name can't exceed 20 characters, please enter your name again ->");
                        name = Console.ReadLine();
                    }
                    else {
                        flagName = 1;
                    }
                } while (flagName != 1);

                Console.WriteLine("Please enter your gender [M/m(Male) - F/f(Female) - O/o(other)] -> ");
                gender = char.Parse(Console.ReadLine());
                do {
                    if (gender == 'M' || gender == 'm' || gender == 'F' || gender == 'f' || gender == 'O' || gender == 'o')
                    {
                        flagGender = 1;
                    }
                    else {
                        Console.WriteLine("Please choose between these options! [ M/m( - F/f - O/o] ->");
                        gender = char.Parse(Console.ReadLine());
                    }
                } while (flagGender != 1);

            }
            catch (Exception e) {
                Console.WriteLine("Encountered an Error!", e);
            }
        }
    }
}
