using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Home_Sweet_Home
{
    class SQLClass
    {
        // Connection String kept private, due to securtiy issues.
        String connectionString = "Server=tcp:home-sweet-home.database.windows.net,1433;Initial Catalog=home_sweet_home_db;Persist Security Info=False;User ID=Home_Sweet_Home;Password=Sahib@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SQLClass()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {

                // All data is being saved to Azure..
                // SQL Server management is being used to query data and manage tables..
                // Functions regarding various functionalities will go in the this class!!

                // Checking if connection set-up
                cnn.Open();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally {
                cnn.Close();
            }
        }

        // INSERT FUNCTIONS

        // Code the insert function for the table, get the table name as parameter.
        public bool insertHome(string announcement,
        string name_home,
        string address_home,
        string description_home,
        double length_of_home,
        double width_of_home) {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                
                // NUMBER OF MEMBERS WILL BE ADDED DYNAMICALLY!

                string query = "INSERT INTO HOME (Announcement , Home_Name , Address , Description , length , width ) VALUES (" + "'" + announcement + "'" + "," + "'" + name_home + "'" + "," + "'" + address_home + "'" + "," + "'" + description_home + "'" + "," + length_of_home + "," + width_of_home + ")";
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
        bool outcome) {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                int done = 0;
                if (outcome) {
                    done = 1;
                }
                cnn.Open();

                // NUMBER OF MEMBERS WILL BE DONE DYNAMICALLY!

                string query = "INSERT INTO ACTIVITY (Name, Description, completed) VALUES (" + "'" + name_activity + "'" + "," + "'" + description + "'" + "," + done + ")";
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

        // Checks if the email already exists in the database!
        public bool uniqueEmail(string email) {
            SqlConnection cnn = new SqlConnection(connectionString);
            int i = 0;
            try
            {
                cnn.Open();
                string query = "select email from users";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                SqlDataReader reader =  Insert.ExecuteReader();
                while (reader.Read()) {
                    if (reader[i].ToString().CompareTo(email) == 0) {
                        return false;
                    }
                    i++;
                }
                cnn.Close();
                return true;
            }catch (Exception e) {
                return false;
            }
        }

        public bool insertUser(string name,
        string email,
        string salt,
        string gender,
        string hash) {

            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = "INSERT INTO USERS (Name, email, salt, gender, hash) VALUES (" + "'" + name + "'" + "," + "'" + email + "'" + "," + "'" + salt + "'" + "," + "'" + gender + "'" + "," + "'" + hash + "'" + ")";
                SqlCommand Insert;
                Insert = new SqlCommand(query, cnn);
                Insert.ExecuteNonQuery();
                Insert.Dispose();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //GET FUNCTIONS

        public bool login(string email) {

            // to use hash function to validate the password!
            User u = new User();

            SqlConnection cnn = new SqlConnection(connectionString);
            bool flagEmailLogin = false;
            string salt = null, hashDatabase = null, hash = null, password = null;

            try
            {
                cnn.Open();
                string query = "select email,salt,hash from users";
                SqlCommand getUser;
                getUser = new SqlCommand(query, cnn);
                SqlDataReader reader = getUser.ExecuteReader();
                
                while (reader.Read())
                {
                    if (reader.GetString(0).ToString().CompareTo(email) == 0)
                    {
                        salt = reader.GetString(1).ToString();
                        hashDatabase = reader.GetString(2).ToString();

                        flagEmailLogin = true;
                    }
                }

                if (!flagEmailLogin)
                {
                    Console.WriteLine("E-mail does not exists in the system!");
                }
                else {
                    Console.WriteLine("Please enter your Password: ");
                    password = Console.ReadLine();
                }

                hash = u.generateSHA256Hash(password,salt);

                if (hashDatabase.CompareTo(hash) == 0) {
                    Console.WriteLine("Successfully Logged In!");
                }
                else {
                    Console.WriteLine("Invalid Password!");
                }

                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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
