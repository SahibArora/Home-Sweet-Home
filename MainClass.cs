using System;

namespace Home_Sweet_Home
{
    class MainClass
    {
        static void Main(string[] args)
        {
            SQLClass s = new SQLClass();

            if (s.insertUser("XXX", "XXX", "XXXX", 'M')) {
                Console.WriteLine("User Added");
            }
            if (s.insertHome("I am creating Home XXX", "XXX", "470 xxx xxx", "My Home", 4, 561.0, 700.98)) {
                Console.WriteLine("Home Added");
            }
            if (s.insertArea("Room-1", "Main Bedroom", 200.0, 300.0)) {
                Console.WriteLine("Area Added");
            }
            if (s.insertActivity("Cleaning", "Clean whole room", 3, false)) {
                Console.WriteLine("Aactivity Added");
            }

            // REGISTER or LOG-IN

            // ADD TO HOME or ADD A NEW HOME (ONE HOME SHOWN AT ONE TIME, IF MORE THAN TWO ASK USER WHICH ONE THEY WANT TO SEE!) 

            // IN THE HOME THEY CAN SEE MEMBERS, CREATE ANNOUNCEMENTS, ADD AREAS and ADD ACTIVITIES (according to permissions)
        }
    }
}
