using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Collections;

namespace Home_Sweet_Home
{
    class SQLClass
    {
        // Connection String kept private, due to securtiy issues.
        SQLiteConnection sqlite = new SQLiteConnection("Data Source=home_sweet_home.db;");
        public SQLClass()
        {
            try
            {

                // All data is being saved to Azure..
                // SQL Server management is being used to query data and manage tables..
                // Functions regarding various functionalities will go in the this class!!

                // Checking if connection set-up
                sqlite.Open();

                // Basic Tables

                string activityTable = "CREATE TABLE if not exists Activity (ActivityPk INT, Name varchar(20), Description varchar(100), no_of_member INT, outcome INT, PRIMARY KEY(ActivityPk))";
                SQLiteCommand acTable = sqlite.CreateCommand();
                acTable.CommandText = activityTable;
                acTable.ExecuteNonQuery();

                string areaTable = "CREATE TABLE if not exists Area (AreaPk INT, Name varchar(20), Description varchar(100), length decimal(10,2), width decimal(10,2), ActivityFk INT, PRIMARY KEY(AreaPk), FOREIGN KEY(ActivityFk) REFERENCES ACTIVITY(ActivityPk))";
                SQLiteCommand aTable = sqlite.CreateCommand();
                aTable.CommandText = areaTable;

                string userTable = "CREATE TABLE if not exists Users (Name varchar(20),email varchar(40) PRIMARY KEY, password varchar(20), gender char(1), salt varchar(20), hash varchar(100))";
                SQLiteCommand uTable = sqlite.CreateCommand();
                uTable.CommandText = userTable;
                uTable.ExecuteNonQuery();

                string homeTable = "CREATE TABLE if not exists Home ( Announcement varchar(100), Home_Name varchar(20) PRIMARY KEY, Address varchar(40), Description varchar(100), no_of_member INT, length decimal(10,2), width decimal(10,2), AreaFK INT, FOREIGN KEY(AreaFK) REFERENCES AREA(AreaPk))";
                SQLiteCommand hTable = sqlite.CreateCommand();
                hTable.CommandText = homeTable;
                hTable.ExecuteNonQuery();

                // Foreign Keys
                // Not valid in SQLite
                /*
                string homeAreaFK = "ALTER TABLE Home ADD CONSTRAINT HomeAreaFK FOREIGN KEY(AreaFk) REFERENCES Area(AreaPk)";
                SQLiteCommand haFk = sqlite.CreateCommand();
                haFk.CommandText = homeAreaFK;
                haFk.ExecuteNonQuery();

                string areaActivityFK = "ALTER TABLE Area ADD CONSTRAINT AreaActivityFK FOREIGN KEY(ActivityFk) REFERENCES Activity(ActivityPk)";
                SQLiteCommand aAFk = sqlite.CreateCommand();
                aAFk.CommandText = areaActivityFK;
                aAFk.ExecuteNonQuery();*/

                // Bridge Tables
                string userHome = "CREATE TABLE if not exists UserHome (email varchar(60), Home_name varchar(20), Permission char(1), FOREIGN KEY(email) REFERENCES Users(email), FOREIGN KEY(Home_name) REFERENCES Home(Home_name), PRIMARY KEY(email,Home_name),check(permission = 'u' OR permission = 'U' OR permission = 'a' OR permission = 'A'))";
                SQLiteCommand uHome = sqlite.CreateCommand();
                uHome.CommandText = userHome;
                uHome.ExecuteNonQuery();
                
                string userActivity = "CREATE TABLE if not exists UserActivity (UserPk INT, ActivityPk INT, FOREIGN KEY(UserPk) REFERENCES Users(UserPk), FOREIGN KEY(ActivityPk) REFERENCES Activity(ActivityPk), PRIMARY KEY(UserPk,ActivityPk))";
                SQLiteCommand uActivity = sqlite.CreateCommand();
                uActivity.CommandText = userActivity;
                uActivity.ExecuteNonQuery();

                // Alter bridge tables
                // Does not work in SQLITE
                /*
                string checkStatement = "alter table UserHome add check(permission = 'u' OR permission = 'U' OR permission = 'a' OR permission = 'A')";
                SQLiteCommand cStatement = sqlite.CreateCommand();
                cStatement.CommandText = checkStatement;
                cStatement.ExecuteNonQuery();*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally {
                sqlite.Close();
            }
        }

        // Checks if the email already exists in the database!
        public bool uniqueEmail(string email)
        {
            try
            {
                sqlite.Open();
                string query = "select email from users";
                SQLiteCommand get = sqlite.CreateCommand();
                get.CommandText = query;


                SQLiteDataReader reader = get.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString().Equals(email))
                    {
                        return false;
                    }
                }
                sqlite.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // To check if the Home Name is Unique.

        public bool uniqueHomeName(string home_name)
        {
            try
            {
                sqlite.Open();
                string query = "select home_name from home";
                
                SQLiteCommand get = sqlite.CreateCommand();
                get.CommandText = query;

                SQLiteDataReader reader = get.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString().Equals(home_name))
                    {
                        return false;
                    }
                }
                sqlite.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //GET FUNCTIONS

            // User Login
        public bool login(string email)
        {

            // to use hash function to validate the password!
            User u = new User();

            bool flagEmailLogin = false;
            string salt = null, hashDatabase = null, hash = null, password = null;

            try
            {
                sqlite.Open();
                string query = "select email,salt,hash from users";
                SQLiteCommand getUser = sqlite.CreateCommand();
                getUser.CommandText = query;

                SQLiteDataReader reader = getUser.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(0).ToString().Equals(email))
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
                else
                {
                    Console.WriteLine("Please enter your Password: ");
                    password = Console.ReadLine();
                }

                hash = u.generateSHA256Hash(password, salt);

                if (hashDatabase.Equals(hash))
                {
                    Console.WriteLine("Successfully Logged In!");
                    Console.Clear();
                    sqlite.Close();
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid Password!");
                    sqlite.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                sqlite.Close();
                return false;
            }
        }

        // get permission of user

        public char getPermission(string home_name, string email) {

            char permission = '\0';
            try
            {
                sqlite.Open();

                string query = "SELECT permission FROM userhome WHERE email = '" + email + "' AND home_name = '" + home_name + "'";
                SQLiteCommand getHome = sqlite.CreateCommand();
                getHome.CommandText = query;

                SQLiteDataReader reader = getHome.ExecuteReader();
                reader.Read();
                
                permission = Char.Parse(reader.GetString(0));

                sqlite.Close();

                return permission;
            }
            catch (Exception e) {
                return '\0';
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
            try
            {
                sqlite.Open();
                
                // NUMBER OF MEMBERS WILL BE ADDED DYNAMICALLY!

                string query = "INSERT INTO HOME (Announcement , Home_Name , Address , Description , length , width ) VALUES (" + "'" + announcement + "'" + "," + "'" + name_home + "'" + "," + "'" + address_home + "'" + "," + "'" + description_home + "'" + "," + length_of_home + "," + width_of_home + ")";
                SQLiteCommand Insert = sqlite.CreateCommand();
                Insert.CommandText = query;
                Insert.ExecuteNonQuery();
                Insert.Dispose();
                sqlite.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // add user to home

        public bool addUserHome(string email, string home_name, char permission = 'u') {
            try {
                sqlite.Open();
                string query = "INSERT INTO USERHOME (email , Home_name , permission ) VALUES (" + "'" + email + "'" + "," + "'" + home_name + "'" + "," + "'" + permission + "'" + ")";
                SQLiteCommand Insert = sqlite.CreateCommand();
                Insert.CommandText = query;
                Insert.ExecuteNonQuery();
                Insert.Dispose();


                // increasing the number of member in the home

                string query1 = "UPDATE home SET no_of_member = (no_of_member + 1) WHERE Home_Name = '" + home_name + "'" ;
                
                SQLiteCommand Update = sqlite.CreateCommand();
                Update.CommandText = query1;
                Update.ExecuteNonQuery();
                Update.Dispose();

                sqlite.Close();
                return true;

            } catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }


        // get home of specific user

        public User getHomeOfUser(User u) {
            try {
                sqlite.Open();

                string query = "SELECT home_name FROM userhome WHERE email = '" + u.email + "'";
                SQLiteCommand getHome = sqlite.CreateCommand();
                getHome.CommandText = query;

                SQLiteDataReader reader = getHome.ExecuteReader();

                while (reader.Read()) {
                    int flag = 0;
                    for (int i = 0; i < u.homes.Count; i++) {
                        if (reader.GetString(0).Equals(u.homes[i])) {
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        u.homes.Add(reader.GetString(0).ToString());
                    }
                }
                sqlite.Close();

                return u;
            }
            catch (Exception e) {
                return u;
            }
        }

        /*
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
        */

        public bool insertUser(string name,
        string email,
        string salt,
        string gender,
        string hash) {

            try
            {
                sqlite.Open();
                string query = "INSERT INTO USERS (Name, email, salt, gender, hash) VALUES (" + "'" + name + "'" + "," + "'" + email + "'" + "," + "'" + salt + "'" + "," + "'" + gender + "'" + "," + "'" + hash + "'" + ")";
                SQLiteCommand Insert = sqlite.CreateCommand();
                Insert.CommandText = query;
                Insert.ExecuteNonQuery();
                Insert.Dispose();
                sqlite.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /*
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
        */
    }
}
