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
                cnn.Open();
                Console.WriteLine("Connection setted-up!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't Connect to the Database " + e);
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
