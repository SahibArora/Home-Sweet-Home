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
                        optionCheck = false;
                    }
                } while (!optionCheck);

                if (option == 1) {
                    
                    u = u.register();
                    Console.WriteLine("Email: " + u.email);
                    // to make the user object empty again - will be used in future 
                    u = new User();
                }
                if (option == 2) {

                    bool logedIn = false;

                    u = u.login();

                    logedIn = (u.email != null);

                    if (logedIn) {
                        int loggedOption = 0;
                        bool loggedOptionCheck = false;

                        Console.WriteLine("\nWelcome " + u.email + "\n");

                        do
                        {
                            Console.WriteLine("1 - Create a new Home");
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
                                    loggedOptionCheck = false;
                                }
                            } while (!loggedOptionCheck);

                            switch (loggedOption)
                            {
                                case 1:
                                    Home h = new Home();
                                    Console.WriteLine("You are in Create new Home!\n");
                                    h = h.register();

                                    if (h.name_home != null) {
                                        SQLClass sql = new SQLClass();
                                        if (sql.addUserHome(u.email, h.name_home, 'a'))
                                        {
                                            Console.WriteLine(u.email + " is succefully added in " + h.name_home + " with admin rights!\n\n");
                                            u.sendEmailHome(u.email,h.name_home);
                                        }
                                        else {
                                            Console.WriteLine("Unable to add " + u.email + " to " + h.name_home + "!\n\n");
                                        }

                                    }

                                    break;
                                case 2:
                                    Console.WriteLine("Following are the list of home/s you are part off -\n");

                                    SQLClass sql1 = new SQLClass();
                                    u = sql1.getHomeOfUser(u);

                                    for (int i = 0; i < u.homes.Count; i++) {
                                        Console.WriteLine(i+1 + ". " + u.homes[i]);
                                    }
                                    Console.WriteLine("\n");
                                    break;
                                case 0:
                                    u = new User();
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
