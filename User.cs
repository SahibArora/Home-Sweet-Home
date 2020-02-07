using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Sweet_Home
{
    class User
    {

        // normal data members
        string name;
        string email;
        string password; // generate a hashcode before saving it to database (one-step-to-security)
        char gender;

        // initializing 
        public User() {
            name = null;
            email = null;
            password = null;
            gender = '\0';
        }

        public void register() {
            try {
                Console.WriteLine("Please enter your name: ");

            }
            catch (Exception e) {
                Console.WriteLine("Encountered an Error!", e);
            }
        }
    }
}
