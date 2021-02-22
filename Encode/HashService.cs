using System;
using System.Security.Cryptography;
using System.Text;

namespace API.Encode
{
    public class HashService
    {
        public string ComputeStringToSha512Hash(string value)
        {
            // Create a SHA256 hash from string   
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }

        }
    }
}