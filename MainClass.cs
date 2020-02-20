using System;

namespace Home_Sweet_Home
{
    class MainClass
    {
        static void Main(string[] args)
        {
            int option = 0;
            bool optionCheck = false;
            // User

            User u = new User();

            do
            {
                Console.WriteLine("\n1 - Register");
                Console.WriteLine("2 - Login");
                Console.WriteLine("\n0 - Exit");

                do {

                    try {
                        Console.WriteLine("\nPlease enter your option: ");
                        option = Int32.Parse(Console.ReadLine());
                        Console.WriteLine();
                        optionCheck = true;
                    }
                    catch (Exception e) {
                        Console.WriteLine("It can only be Integer.");
                    }
                } while (!optionCheck);

                if (option == 1) {
                    
                    u.register();
                }
                if (option == 2) {

                    bool logedIn = false;

                    logedIn = u.login();

                    if (logedIn) {
                        int loggedOption = 0;
                        bool loggedOptionCheck = false;

                        do
                        {
                            Console.WriteLine("\n1 - Create a new Home");
                            Console.WriteLine("2 - Enter to an existing Home");
                            Console.WriteLine("0 - Log Out");
                            do
                            {
                                try
                                {
                                    Console.WriteLine("\nPlease enter your option: ");
                                    loggedOption = Int32.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    loggedOptionCheck = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("It can only be Integer.");
                                }
                            } while (!loggedOptionCheck);

                            switch (loggedOption)
                            {
                                case 1:
                                    Home h = new Home();
                                    Console.WriteLine("You are in Create new Home!\n");
                                    h.register();
                                    break;
                                case 2:
                                    Console.WriteLine("You are in enter Home option!");
                                    break;
                                case 0:
                                    Console.WriteLine("You are logged out!");
                                    break;
                            }
                        } while (loggedOption != 0);
                    }
                }
            } while (option != 0);

            // REGISTER or LOG-IN

            // ADD TO HOME or ADD A NEW HOME (ONE HOME SHOWN AT ONE TIME, IF MORE THAN TWO ASK USER WHICH ONE THEY WANT TO SEE!) 

            // In INSERTION/CREATION OF HOME -> NUMBER OF MEMBERS WILL BE AUTO-INCREMENT AS THE PEOPLE JOINS

            // IN THE HOME THEY CAN SEE MEMBERS, CREATE ANNOUNCEMENTS, ADD AREAS and ADD ACTIVITIES (according to permissions)
        }
    }
}
