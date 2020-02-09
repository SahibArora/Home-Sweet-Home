using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Sweet_Home
{
    class Home
    {
        // Functions/scope 
        List<Area_In_Home> area_of_home;
        List<User> users;
        // special functionalities of home.
        string announcement;

        // normal data members
        string name_home;
        string address_home;
        string description_home;
        int no_of_member_home;
        double length_of_home;
        double width_of_home;

        // initializing
        public Home() {
            area_of_home = new List<Area_In_Home>();
            users = new List<User>();

            announcement = null;

            name_home = null;
            address_home = null;
            description_home = null;
            no_of_member_home = 0;
            length_of_home = 0.0;
            width_of_home = 0.0;
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
        bool completed;

        // initializing
        public Activity_Area_In_Home() {
            users_assigned = new List<User>();

            name_activity = null;
            description = null;
            no_of_members = 0;
            completed = false;
        }
    }
}
