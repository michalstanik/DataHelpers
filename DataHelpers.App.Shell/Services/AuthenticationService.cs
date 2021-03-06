﻿using DataHelpers.App.Infrastructure.Interfaces;
using DataHelpers.Data.DataModel.Users;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DataHelpers.App.Shell.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {

        }
        public UserAccount AuthenticateUser(string username, string clearTextPassword)
        {

            var hash = CalculateHash(clearTextPassword, username);

            //TODO: Remove this empty object
            return new UserAccount();
        }
            private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
