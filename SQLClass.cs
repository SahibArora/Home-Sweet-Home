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

                /*User = new SqlCommand("CREATE TABLE Users (Name varchar(20),email varchar(40),password varchar(20), gender char(1))", cnn);
                User.ExecuteNonQuery();
                User.Dispose();

                Home = new SqlCommand("CREATE TABLE Home (Announcement varchar(100), Home_Name varchar(20), Address varchar(40), Description varchar(100), no_of_member decimal(5,0), length decimal(10,2), width decimal(10,2))", cnn);
                Home.ExecuteNonQuery();
                Home.Dispose();

                Area = new SqlCommand("CREATE TABLE Area (Name varchar(20), Description varchar(100), length decimal(10,2), width decimal(10,2))", cnn);
                Area.ExecuteNonQuery();
                Area.Dispose();

                Activity = new SqlCommand("CREATE TABLE Activity (Name varchar(20), Description varchar(100), no_of_member decimal(10,2), completed decimal(1,0))", cnn);
                Activity.ExecuteNonQuery();
                Activity.Dispose();*/
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
        public bool insertHome(string announcement,
        string name_home,
        string address_home,
        string description_home,
        int no_of_member_home,
        double length_of_home,
        double width_of_home) {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                
                string query = "INSERT INTO HOME (Announcement , Home_Name , Address , Description , no_of_member , length , width ) VALUES (" + "'" + announcement + "'" + "," + "'" + name_home + "'" + "," + "'" + address_home + "'" + "," + "'" + description_home + "'" + "," + no_of_member_home + "," + length_of_home + "," + width_of_home + ")";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally {
                cnn.Close();
            }
            return true;
        }

        public bool insertArea(string name_area,
        string description_area,
        double length_of_area,
        double width_of_area) {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = "INSERT INTO AREA (Name, Description, length, width) VALUES (" + "'" + name_area + "'" + "," + "'" + description_area + "'" + "," + length_of_area + "," + width_of_area + ")";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return true;
        }
        
        public bool insertActivity(string name_activity,
        string description,
        int no_of_members,
        bool completed) {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                double comp = 0;
                if (completed) {
                    comp = 1;
                }
                cnn.Open();
                string query = "INSERT INTO ACTIVITY (Name, Description, no_of_member, completed) VALUES (" + "'" + name_activity + "'" + "," + "'" + description + "'" + "," + no_of_members + "," + comp + ")";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return true;
        }

        // Passwords yet to be hashed!!
        public bool insertUser(string name,
        string email,
        string password,
        char gender) {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = "INSERT INTO USERS (Name, email, password, gender) VALUES (" + "'" + name + "'" + "," + "'" + email + "'" + "," + "'" + password + "'" + "," + "'" + gender + "'" + ")";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return true;
        }

        // code the delete function
        // Delete queries will be written once, primary and foriegn keys will be identified!
        public bool deleteHome(string announcement,
        string name_home,
        string address_home,
        string description_home,
        int no_of_member_home,
        double length_of_home,
        double width_of_home)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool deleteArea(string name_area,
        string description_area,
        double length_of_area,
        double width_of_area)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool deleteActivity(string name_activity,
        string description,
        int no_of_members,
        bool completed)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool deleteUser(string name,
        string email,
        string password,
        char gender)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        // code the update function
        // Update queries will be written once, primary and foriegn keys will be identified!
        public bool updateHome(string announcement,
        string name_home,
        string address_home,
        string description_home,
        int no_of_member_home,
        double length_of_home,
        double width_of_home)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool updateArea(string name_area,
        string description_area,
        double length_of_area,
        double width_of_area)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool updateActivity(string name_activity,
        string description,
        int no_of_members,
        bool completed)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public bool updateUser(string name,
        string email,
        string password,
        char gender)
        {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = null;
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

    }
}
