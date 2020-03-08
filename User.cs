using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace Home_Sweet_Home
{
    class User
    {
        // Special features
        public List<string> homes;

        // normal data members
        string name;
        public string email;
        string salt; // generate a salt for password before saving it to database (one-step-to-security)
        string gender;
        string hash; 

        // initializing 
        public User() {
            homes = new List<string>();

            name = null;
            email = null;
            salt = null;
            gender = null;
            hash = null;
        }

        // REGISTER FUNCTION

        public User register() {
            try {
                SQLClass sql = new SQLClass();

                Console.WriteLine("\nRegister\n");

                int flagName = 0, flagGender = 0, flagPassword = 0, verification_code = 0, verification_code_check = 0;
                bool flagEmail = false, flagCode = false;
                string userPassword = null, confirmPassword = null;
                
                Console.WriteLine("Please Enter your name -> ");
                name = Console.ReadLine();
                do {
                    if (name.Length > 20)
                    {
                        Console.WriteLine("Name can't exceed 20 characters, please enter your name again ->");
                        name = Console.ReadLine();
                    }
                    else {
                        flagName = 1;
                    }
                } while (flagName != 1);

                
                do {
                    Console.WriteLine("Please enter your gender [M/m(Male) - F/f(Female) - O/o(other)] -> ");
                    gender = Console.ReadLine();

                    if (gender == "M" || gender == "m" || gender == "F" || gender == "f" || gender == "O" || gender == "o")
                    {
                        flagGender = 1;
                    }
                    else {
                        Console.WriteLine("Please choose between given options! [ M/m( - F/f - O/o] ->");
                    }
                } while (flagGender != 1);

                do {
                    Console.WriteLine("Please enter your Password (Minimum 8 characters): ");
                    userPassword = Console.ReadLine();
                    if (userPassword.Length > 20 || userPassword.Length < 8)
                    {
                        Console.WriteLine("Password should be between 8 - 20!");
                        continue;
                    }
                    else {
                        flagPassword = 1;
                    }
                    Console.WriteLine("Please confirm your Password: ");
                    confirmPassword = Console.ReadLine();
                    if (userPassword.CompareTo(confirmPassword) == 1)
                    {
                        flagPassword = 0;
                        Console.WriteLine("Password Does not match, please enter it again!");
                    }
                    else {
                        flagPassword = 1;
                    }
                } while (flagPassword != 1);
                salt = createSalt(10);
                hash = generateSHA256Hash(userPassword, salt);

                do
                {
                    Console.WriteLine("Please enter your email: ");
                    email = Console.ReadLine();
                    flagEmail = IsValidEmail(email);

                    if (!flagEmail)
                    {
                        Console.WriteLine("Incorrect E-mail address!");
                    }

                    flagEmail = sql.uniqueEmail(email);

                    if (!flagEmail) {
                        Console.WriteLine("Email already exists!");
                    }
                    else
                    {
                        verification_code = sendEmailVerificationCode(email);
                        if (verification_code == 0)
                        {
                            Console.WriteLine("Invalid E-mail address!");
                            flagEmail = false;
                        }
                    }
                } while (!flagEmail);

                do {
                    Console.WriteLine("Please enter the verification code: (if, didn't receive enter any value to follow the program)");
                    verification_code_check = Int32.Parse(Console.ReadLine());
                    if (verification_code != verification_code_check)
                    {
                        Console.WriteLine("Code does not match!\n");
                        Console.WriteLine("Do you want a new code? (Y/ any value) \n");
                        string ans = Console.ReadLine();
                        if (ans == "Y" || ans == "y")
                        {
                            verification_code = sendEmailVerificationCode(email);
                        }
                    }
                    else {
                        flagCode = true;
                    }
                } while (!flagCode);

                sql.insertUser(name,email,salt,gender,hash);

                Console.Clear();

                Console.WriteLine("Succefully Registered!");

                return this;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return this;
            }
        }

        // REGISTER SUPPORTING FUNCTIONS ---

        // Deaclaring a function to generate Salt for User Passwords..
        public string createSalt(int size) {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            // Using rng object to generate random values.
            rng.GetBytes(buff);
            // using ToBase64String to change the values to characters (one big string -> [10]).
            return System.Convert.ToBase64String(buff);
        }

        //Adding the salt into password and creating a hash 
        public String generateSHA256Hash(string pass, string salt) {
            // generating bytes of password + salt.
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass + salt);
            // Using SHA256MAnaged to generate hash Value;
            System.Security.Cryptography.SHA256Managed sha256hashString =
                new System.Security.Cryptography.SHA256Managed();
            // Storing the byte hash.
            byte[] hash = sha256hashString.ComputeHash(bytes);
            // Converting it to string and returning!
            return System.Convert.ToBase64String(hash);
        }

        // Sendinhg email verification codes and checking if the email exist (only in few possible cases)
        public int sendEmailVerificationCode(string email) {
            // Random Code
            try
            {
                int code = 0;
                Random num = new Random();
                code = num.Next(321, 23543);
                string code_string = Convert.ToString(code);
                // Email
                MailMessage msg = new MailMessage();
                SmtpClient sc = new SmtpClient();
                msg.From = new MailAddress("home.sweet.home.the.year.2.0.2.0@gmail.com");
                msg.To.Add(new MailAddress(email));
                msg.Subject = "Email Verification";
                msg.Body = "Your verification code is: " + code_string;
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("home.sweet.home.the.year.2.0.2.0@gmail.com", "mynameissahibarora");
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.Send(msg);

                return code;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return 0;
            }
        }

        // Send email when a home is creatd on someone's email

        public void sendEmailHome(string email, string home_name)
        {
            // Random Code
            try
            {
                // Email
                MailMessage msg = new MailMessage();
                SmtpClient sc = new SmtpClient();
                msg.From = new MailAddress("home.sweet.home.the.year.2.0.2.0@gmail.com");
                msg.To.Add(new MailAddress(email));
                msg.Subject = "New Home '" + home_name + "' Created";
                msg.Body = "A new home '" + home_name + "' has been created at your id " + email + "!";
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("home.sweet.home.the.year.2.0.2.0@gmail.com", "mynameissahibarora");
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.Send(msg);
            }
            catch (Exception e)
            {
                
            }
        }

        // Regex for email verification
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                string email = emailaddress;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        // Login Function

        public User login() {

            bool flagEmail = false, logedIn = false; 
            string Useremail = null;

            SQLClass sql = new SQLClass();

            do
            {
                Console.WriteLine("Please enter your email: ");
                Useremail = Console.ReadLine();
                // Using function from users class!
                flagEmail = IsValidEmail(Useremail);

                if (!flagEmail)
                {
                    Console.WriteLine("Incorrect E-mail address!");
                }
            } while (!flagEmail);

            logedIn = sql.login(Useremail);

            if (logedIn)
            {
                email = Useremail;
            }
            return this;
        }

    }
}
