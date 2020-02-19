using System;

namespace Home_Sweet_Home
{
    class MainClass
    {
        static void Main(string[] args)
        {
            int option = 0;
            bool optionCheck = false;
            
            do
            {
                Console.WriteLine("1 - Register");
                Console.WriteLine("2 - Login");
                Console.WriteLine("\n0 - Exit");

                do {

                    try {
                        Console.WriteLine("\nPlease enter your option: ");
                        option = Int32.Parse(Console.ReadLine());
                        optionCheck = true;
                    }
                    catch (Exception e) {
                        Console.WriteLine("It can only be Integer.");
                    }
                } while (!optionCheck);

                if (option == 1) {
                    User u = new User();
                    u.register();
                }
                if (option == 2) {

                    bool flagEmail = false, logedIn = false;
                    string email = null;

                    SQLClass sql = new SQLClass();
                    User u = new User();

                    do
                    {
                        Console.WriteLine("Please enter your email: ");
                        email = Console.ReadLine();
                        // Using function from users class!
                        flagEmail = u.IsValidEmail(email);

                        if (!flagEmail)
                        {
                            Console.WriteLine("Incorrect E-mail address!");
                        }
                    } while (!flagEmail);

                    // password will be asked if e-mail valid
                    logedIn = sql.login(email);

                    /*if (logedIn) {
                        
                        do
                        {
                            switch ()
                            {

                            }
                        } while ();
                    }*/
                }
            } while (option != 0);

            // REGISTER or LOG-IN

            // ADD TO HOME or ADD A NEW HOME (ONE HOME SHOWN AT ONE TIME, IF MORE THAN TWO ASK USER WHICH ONE THEY WANT TO SEE!) 

            // In INSERTION/CREATION OF HOME -> NUMBER OF MEMBERS WILL BE AUTO-INCREMENT AS THE PEOPLE JOINS

            // IN THE HOME THEY CAN SEE MEMBERS, CREATE ANNOUNCEMENTS, ADD AREAS and ADD ACTIVITIES (according to permissions)
        }
    }
}
