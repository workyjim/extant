//-----------------------------------------------------------------------
// <copyright file="ExtantMembershipProvider.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using Extant.Data.Entities;
using Extant.Data.Repositories;
using StructureMap;

namespace Extant.Web.Infrastructure
{
    public class ExtantMembershipProvider : MembershipProvider
    {
        private string applicationName;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;

        protected IUserRepository UserRepo
        {
            get { return ObjectFactory.GetInstance<IUserRepository>(); }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            applicationName = MembershipHelper.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(MembershipHelper.GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(MembershipHelper.GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(MembershipHelper.GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "0"));
            minRequiredPasswordLength = Convert.ToInt32(MembershipHelper.GetConfigValue(config["minRequiredPasswordLength"], "8"));
            
            base.Initialize(name, config);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            
            //check for existing user with same username/email
            var user = UserRepo.GetByEmail(email);
            if ( null != user )
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            //check password
            if ( password.Length < minRequiredPasswordLength )
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string hash;
            string salt;
            var hasher = new SaltedHash();
            hasher.GetHashAndSaltString(password, out hash, out salt);
            var newUser = new User
                              {
                                  UserName = username,
                                  Email = email,
                                  Password = hash,
                                  Salt = salt,
                                  CreationDate = DateTime.Now,
                                  IsApproved = isApproved
                              };
            newUser = UserRepo.Save(newUser);
            status = MembershipCreateStatus.Success;
            return UserToMembershipUser(newUser);
        }

        private MembershipUser UserToMembershipUser(User user)
        {
            return new MembershipUser(
                Name, 
                user.Email, 
                user.Id, 
                user.Email, 
                null, 
                user.UserName, 
                user.IsApproved, 
                user.IsLockedOut, 
                user.CreationDate, 
                user.LastLoginDate ?? DateTime.MinValue, 
                user.LastLoginDate ?? DateTime.MinValue, 
                user.LastPasswordChangeDate ?? DateTime.MinValue, 
                user.LastLockedOutDate ?? DateTime.MinValue);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            var user = UserRepo.GetByEmail(username);
            var hash = SaltedHash.GetUrlEncodedBytes(16);
            user.PasswordResetCode = hash;
            user.PasswordResetDate = DateTime.Now;
            UserRepo.Save(user);
            return hash;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword)) return false;
            ChangeUserPassword(username, newPassword);
            return true;
        }

        private void ChangeUserPassword(string username, string password)
        {
            var user = UserRepo.GetByEmail(username);
            string hash;
            string salt;
            var hasher = new SaltedHash();
            hasher.GetHashAndSaltString(password, out hash, out salt);
            user.Password = hash;
            user.Salt = salt;
            user.PasswordResetCode = null;
            user.PasswordResetDate = null;
            UserRepo.Save(user);
        }

        public override string ResetPassword(string username, string newPassword)
        {
            ChangeUserPassword(username, newPassword);
            return null;
        }

        public override void UpdateUser(MembershipUser user)
        {
            var current = UserRepo.Get(Convert.ToInt32(user.ProviderUserKey));
            current.Email = user.UserName;
            current.IsApproved = user.IsApproved;
            current.IsLockedOut = user.IsLockedOut;
            UserRepo.Save(current);
        }

        public override bool ValidateUser(string username, string password)
        {
            var hasher = new SaltedHash();
            var user = UserRepo.GetByEmail(username);
            if (null == user) return false;
            if (user.Deleted) return false;
            var result = hasher.VerifyHashString(password, user.Password, user.Salt);
            if ( result )
            {
                user.LastLoginDate = DateTime.Now;
                user.IncorrectPasswordCount = 0;
            }
            else
            {
                if ( 0 == user.IncorrectPasswordCount || DateTime.Now.Subtract(user.IncorrectPasswordWindowStart ?? DateTime.Now).Minutes >= passwordAttemptWindow)
                {
                    user.IncorrectPasswordCount = 0;
                    user.IncorrectPasswordWindowStart = DateTime.Now;
                }
                user.IncorrectPasswordCount++;
                if ( user.IncorrectPasswordCount > MaxInvalidPasswordAttempts )
                {
                    user.IsLockedOut = true;
                    user.IncorrectPasswordCount = 0;
                    user.LastLockedOutDate = DateTime.Now;
                }
            }
            UserRepo.Save(user);
            return result;
        }

        public override bool UnlockUser(string userName)
        {
            var user = UserRepo.GetByEmail(userName);
            user.IsLockedOut = false;
            UserRepo.Save(user);
            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = UserRepo.Get(Convert.ToInt32(providerUserKey));
            if (null == user)
                return null;
            return UserToMembershipUser(user);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = UserRepo.GetByEmail(username);
            if ( null == user )
                return null;
            return UserToMembershipUser(user);
        }

        public override string GetUserNameByEmail(string email)
        {
            return GetUser(email, false).UserName;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var user = UserRepo.GetByEmail(username);
            user.Deleted = true;
            UserRepo.Save(user);
            return true;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return null; }
        }

    }

    /// <summary>
    /// See http://www.dijksterhuis.org/creating-salted-hash-values-in-c/
    /// </summary>
    class SaltedHash
    {
        private readonly HashAlgorithm HashProvider;
        private readonly int SaltLength;

        /// <summary>
        /// The constructor takes a HashAlgorithm as a parameter.
        /// </summary>
        /// <param name="hashAlgorithm">
        /// A <see cref="HashAlgorithm"/> HashAlgorihm which is derived from HashAlgorithm. C# provides
        /// the following classes: SHA1Managed,SHA256Managed, SHA384Managed, SHA512Managed and MD5CryptoServiceProvider
        /// </param>
        /// <param name="theSaltLength"></param>
        public SaltedHash(HashAlgorithm hashAlgorithm, int theSaltLength)
        {
            HashProvider = hashAlgorithm;
            SaltLength = theSaltLength;
        }

        /// <summary>
        /// Default constructor which initialises the SaltedHash with the SHA256Managed algorithm
        /// and a Salt of 4 bytes ( or 4*8 = 32 bits)
        /// </summary>
        public SaltedHash() : this(new SHA256Managed(), 4)
        {
        }

        /// <summary>
        /// The actual hash calculation is shared by both GetHashAndSalt and the VerifyHash functions
        /// </summary>
        /// <param name="data">A byte array of the Data to Hash</param>
        /// <param name="salt">A byte array of the Salt to add to the Hash</param>
        /// <returns>A byte array with the calculated hash</returns>
        private byte[] ComputeHash(byte[] data, byte[] salt)
        {
            // Allocate memory to store both the Data and Salt together
            var dataAndSalt = new byte[data.Length + SaltLength];

            // Copy both the data and salt into the new array
            Array.Copy(data, dataAndSalt, data.Length);
            Array.Copy(salt, 0, dataAndSalt, data.Length, SaltLength);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            return HashProvider.ComputeHash(dataAndSalt);
        }

        /// <summary>
        /// Given a data block this routine returns both a Hash and a Salt
        /// </summary>
        /// <param name="data">
        /// A <see cref="System.Byte"/>byte array containing the data from which to derive the salt
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.Byte"/>byte array which will contain the hash calculated
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.Byte"/>byte array which will contain the salt generated
        /// </param>

        public void GetHashAndSalt(byte[] data, out byte[] hash, out byte[] salt)
        {
            // Allocate memory for the salt
            salt = new byte[SaltLength];

            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            var random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(salt);

            // Compute hash value of our data with the salt.
            hash = ComputeHash(data, salt);
        }

        /// <summary>
        /// The routine provides a wrapper around the GetHashAndSalt function providing conversion
        /// from the required byte arrays to strings. Both the Hash and Salt are returned as Base-64 encoded strings.
        /// </summary>
        /// <param name="data">
        /// A <see cref="System.String"/> string containing the data to hash
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.String"/> base64 encoded string containing the generated hash
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.String"/> base64 encoded string containing the generated salt
        /// </param>

        public void GetHashAndSaltString(string data, out string hash, out string salt)
        {
            byte[] hashOut;
            byte[] saltOut;

            // Obtain the Hash and Salt for the given string
            GetHashAndSalt(Encoding.UTF8.GetBytes(data), out hashOut, out saltOut);

            // Transform the byte[] to Base-64 encoded strings
            hash = Convert.ToBase64String(hashOut);
            salt = Convert.ToBase64String(saltOut);
        }

        /// <summary>
        /// This routine verifies whether the data generates the same hash as we had stored previously
        /// </summary>
        /// <param name="data">The data to verify </param>
        /// <param name="hash">The hash we had stored previously</param>
        /// <param name="salt">The salt we had stored previously</param>
        /// <returns>True on a succesfull match</returns>

        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            byte[] newHash = ComputeHash(data, salt);

            //  No easy array comparison in C# -- we do the legwork
            if (newHash.Length != hash.Length) return false;

            for (int lp = 0; lp < hash.Length; lp++ )
                if (!hash[lp].Equals(newHash[lp]))
                    return false;

            return true;
        }

        /// <summary>
        /// This routine provides a wrapper around VerifyHash converting the strings containing the
        /// data, hash and salt into byte arrays before calling VerifyHash.
        /// </summary>
        /// <param name="data">A UTF-8 encoded string containing the data to verify</param>
        /// <param name="hash">A base-64 encoded string containing the previously stored hash</param>
        /// <param name="salt">A base-64 encoded string containing the previously stored salt</param>
        /// <returns></returns>

        public bool VerifyHashString(string data, string hash, string salt)
        {
            byte[] hashToVerify = Convert.FromBase64String(hash);
            byte[] saltToVerify = Convert.FromBase64String(salt);
            byte[] dataToVerify = Encoding.UTF8.GetBytes(data);
            return VerifyHash(dataToVerify, hashToVerify, saltToVerify);
        }

        public static string GetSalt(int bytes)
        {
            var salt = new byte[bytes];

            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            var random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public static string GetUrlEncodedBytes(int bytes)
        {
            var randombytes = new byte[bytes];
            var random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(randombytes);
            return HttpServerUtility.UrlTokenEncode(randombytes);
        }
    }

}