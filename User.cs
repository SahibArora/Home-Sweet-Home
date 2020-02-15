﻿using System;
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
        string salt; // generate a salt for password before saving it to database (one-step-to-security)
        char gender;
        string hash; 

        // initializing 
        public User() {
            homes = new List<Home>();

            name = null;
            email = null;
            salt = null;
            gender = '\0';
            hash = null;
        }

        public void register() {
            try {
                SQLClass sql = new SQLClass();

                Console.WriteLine("\nRegister\n");

                int flagName = 0, flagGender = 0, flagPassword = 0 ;
                string userPassword = null, confirmPassword = null;

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

                do {
                    Console.WriteLine("Please enter your Password: ");
                    userPassword = Console.ReadLine();
                    if (userPassword.Length > 20)
                    {
                        Console.WriteLine("Password length cannot exceed 20!");
                        continue;
                    }
                    else {
                        flagPassword = 1;
                    }
                    Console.WriteLine("Please confirm your Password: ");
                    confirmPassword = Console.ReadLine();
                    if (userPassword.CompareTo(confirmPassword) == 1)
                    {
                        flagPassword = 0;
                        Console.WriteLine("Password Does not match, please enter it again!");
                    }
                    else {
                        flagPassword = 1;
                    }
                } while (flagPassword != 1);
                salt = createSalt(10);
                hash = generateSHA256Hash(userPassword, salt);
            }
            catch (Exception e) {
                Console.WriteLine("Encountered an Error!", e);
            }
        }

        // Deaclaring a function to generate Salt for User Passwords..
        public string createSalt(int size) {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            // Using rng object to generate random values.
            rng.GetBytes(buff);
            // using ToBase64String to change the values to characters (one big string -> [10]).
            return System.Convert.ToBase64String(buff);
        }

        //Adding the salt into password and creating a hash 
        public String generateSHA256Hash(string pass, string salt) {
            // generating bytes of password + salt.
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass + salt);
            byte[] bytes1 = System.Text.Encoding.UTF8.GetBytes(pass + salt);
            // Using SHA256MAnaged to generate hash Value;
            System.Security.Cryptography.SHA256Managed sha256hashString =
                new System.Security.Cryptography.SHA256Managed();
            // Storing the byte hash.
            byte[] hash = sha256hashString.ComputeHash(bytes);
            // Converting it to string and returning!
            return System.Convert.ToBase64String(hash);
        }
    }
}
