using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Home_Sweet_Home
{
    class SQLClass
    {
        // Connection String kept private, due to securtiy issues.
        String connectionString;
        public SQLClass()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {

                // Layout Design for the tables in the constructor, below commands are working!!
                // All data is being saved to Azure..
                // SQL Server management is being used to query data and manage tables..
                // Functions regarding various functionalities will go in the this class!!

                cnn.Open();
                SqlCommand User, Home, Area, Activity;

                User = new SqlCommand("CREATE TABLE Users (Name varchar,email varchar,password varchar, gender char)", cnn);
                User.ExecuteNonQuery();
                User.Dispose();

                Home = new SqlCommand("CREATE TABLE Home (Announcement varchar, Home_Name varchar, Address varchar, Description varchar, no_of_member int, length decimal, width decimal)", cnn);
                Home.ExecuteNonQuery();
                Home.Dispose();

                Area = new SqlCommand("CREATE TABLE Area (Name varchar, Desciption varchar, length decimal, width decimal)", cnn);
                Area.ExecuteNonQuery();
                Area.Dispose();

                Activity = new SqlCommand("CREATE TABLE Activity (Name varchar, Description varchar, no_of_member int, completed int)", cnn);
                Activity.ExecuteNonQuery();
                Activity.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally {
                cnn.Close();
            }
        }

        // Code the insert function for the table, get the table name as parameter.

        // code the delet function

        // code the update function
    }
}
