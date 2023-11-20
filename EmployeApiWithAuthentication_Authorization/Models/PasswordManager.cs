using System.Security.Cryptography;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class PasswordManager
    {
        public string HashPassword(string password)
        {
            // Generate a Array of Size of batch 16
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // Create a password hash 
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20); // 20 is the size of the hash

            // Combine the array and password hash
            byte[] hashWithSalt = new byte[36]; // 16 (salt) + 20 (hash)
            Array.Copy(salt, 0, hashWithSalt, 0, 16);
            Array.Copy(hash, 0, hashWithSalt, 16, 20);

            // Convert the combined bytes to a string (for the  database storage)
            string hashedPassword = Convert.ToBase64String(hashWithSalt);

            return hashedPassword;
        }

        public bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Convert the stored password back to bytes
            byte[] hashWithSalt = Convert.FromBase64String(storedPassword);

            // Extract the array and hash
            byte[] salt = new byte[16];
            Array.Copy(hashWithSalt, 0, salt, 0, 16);
            byte[] storedHash = new byte[20];
            Array.Copy(hashWithSalt, 16, storedHash, 0, 20);

            // Generate hash from entered password using stored array
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] enteredHash = pbkdf2.GetBytes(20);

            // Compare the hashes ,it will only return true when both indexs have same values
            for (int i = 0; i < 20; i++)
            {
                if (storedHash[i] != enteredHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

