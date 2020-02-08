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

                /*cnn.Open();
                SqlCommand command;
                command = new SqlCommand("CREATE TABLE Users (Name varchar,email varchar,password varchar, gender char)", cnn);
                command.ExecuteNonQuery();
                command.Dispose();
                Console.WriteLine("User table created!");*/
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message! ", e);
            }
            finally {
                cnn.Close();
            }
        }
    }
}
