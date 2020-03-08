using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Sweet_Home
{
    class Home
    {
        // Functions/scope 
        List<Area_In_Home> area_of_home;
        List<string> users;
        // special functionalities of home.
        string announcement;

        // normal data members
        public string name_home;
        string address_home;
        string description_home;
        int no_of_member_home;
        double length_of_home;
        double width_of_home;

        // initializing
        public Home() {
            area_of_home = new List<Area_In_Home>();
            users = new List<string>();

            announcement = null;

            name_home = null;
            address_home = null;
            description_home = null;
            no_of_member_home = 0;
            length_of_home = 0.0;
            width_of_home = 0.0;
        }

        public Home register() {

            bool widthFlag = false, lengthFlag = false, flagHomeName = false, flagInsert = false;

            try { 

                SQLClass sql = new SQLClass();

                do {
                    Console.WriteLine("Please enter the name of your Home: ");
                    name_home = Console.ReadLine();
                    flagHomeName = sql.uniqueHomeName(name_home);
                    if (!flagHomeName) {
                        Console.WriteLine("This name is already taken, please choose another one!");
                    }
                } while (!flagHomeName);

                Console.WriteLine("Please enter the address: ");
                address_home = Console.ReadLine();

                Console.WriteLine("Please enter the description (Maximum 100 characters): ");
                description_home = Console.ReadLine();

                do
                {
                    try
                    {
                        Console.WriteLine("Please enter the length of your home: ");
                        length_of_home = Double.Parse(Console.ReadLine());
                        lengthFlag = true;
                    }
                    catch (Exception e) {
                        lengthFlag = false;
                    }
                } while (!lengthFlag);

                do
                {
                    try
                    {
                        Console.WriteLine("Please enter the width of your home: ");
                        width_of_home = Double.Parse(Console.ReadLine());
                        widthFlag = true;
                    }
                    catch (Exception e)
                    {
                        widthFlag = false;
                    }
                } while (!widthFlag);

                flagInsert = sql.insertHome(null, name_home, address_home, description_home, length_of_home, width_of_home);

                if (flagInsert)
                {
                    Console.Clear();
                    Console.WriteLine("\nHome " + name_home + " created\n");
                }
                else {
                    Console.WriteLine("\n\nFailed--- \nHome " + name_home + " cannot be created due to above reason\n");
                }
                return this;
            }
            catch (Exception e) {
                Home h = new Home();
                return h;
            }
        }
    }

    class Area_In_Home {

        // Functionalities/scope
        List<Activity_Area_In_Home> activities;

        // normal data members
        string name_area;
        string description_area;
        double length_of_area;
        double width_of_area;

        // initializing
        public Area_In_Home() {
            name_area = null;
            description_area = null;
            length_of_area = 0.0;
            width_of_area = 0.0;
        }

        // Can have announcement in the home!

    }

    class Activity_Area_In_Home {

        // Functionality/Scope
        List<User> users_assigned;

        string name_activity;
        string description;
        int no_of_members;
        bool outcome;

        // initializing
        public Activity_Area_In_Home() {
            users_assigned = new List<User>();

            name_activity = null;
            description = null;
            no_of_members = 0;
            outcome = false;
        }
    }
}
