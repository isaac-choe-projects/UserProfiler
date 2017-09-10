using EPShared.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EPShared.Shared_Classes
{
    public sealed class CustomerInfo
    {
        #region USER RELATED FUNCTIONS

        /// <summary>
        /// Find user by user name. Returns NULL if not found.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public SiteUser FindUserByUserName(string userName)
        {
            SiteUser user = null;

            try
            {
                using (UserProfilerSiteEntities context = new UserProfilerSiteEntities())
                {
                    var foundRow = context.Users.Where(x => x.UserName.Trim() == userName.Trim()).FirstOrDefault();

                    // Since we cannot cast parent to child, reflection is used to copy the values from the entity to the wrapper class
                    user = new SiteUser(foundRow);
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.SimpleLog.Error(ex.Message);
            }
            return user;
        }

        /// <summary>
        /// Pass in property name and this function will return all non NULL values of that
        /// value of all users.
        /// Pass in the public property from
        /// the EPShared.DBContext.User class along with the value to override with.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public List<object> GetAllUserDataOfSpecificProperty(string propertyName)
        {
            List<object> returnData = new List<object>();
            try
            {
                using (UserProfilerSiteEntities context = new UserProfilerSiteEntities())
                {

                    IEnumerable<object> allData = context.Users;

                    returnData = allData.Select(x => x.GetType().GetProperty(propertyName).GetValue(x, null)).ToList();
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.SimpleLog.Error(ex.Message);
            }
            return returnData;
        }
        
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="age"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="gender"></param>
        public int CreateNewUser(string lastName, string firstName, int age, string email, string phoneNumber, string gender, string userName, string password)
        {
            // If -1 is returned then a new user was not created successfully
            int newID = -1;

            try
            {
                using (UserProfilerSiteEntities context = new UserProfilerSiteEntities())
                {
                    // username must be unique (email duplicates are not allowed)
                    var foundRow = context.Users.Where(x => x.UserName.Trim() == userName.Trim());

                    if(foundRow.Count() == 0)
                    {
                        EPShared.DBContext.User newUser = new DBContext.User();

                        newUser.LastName = lastName;
                        newUser.FirstName = firstName;
                        newUser.Age = age;
                        newUser.Email = email;
                        newUser.PhoneNumber = phoneNumber;
                        newUser.Gender = gender;
                        newUser.UserName = userName;
                        newUser.PasswordHashAndSalt = EPSecurity.CreatePasswordSalt(password);

                        context.Users.Add(newUser);
                        context.SaveChanges();

                        // Get new ID
                        newID = newUser.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.SimpleLog.Error(ex.Message);
            }
            return newID;
        }

        /// <summary>
        /// Retrieves the hash + salted form of the password initially given by the user and then validates against the
        /// passed in password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthenticateUserLogin(string userName, string password)
        {
            bool isAuthenticated = false;
            CustomerInfo obj = new CustomerInfo();

            using (UserProfilerSiteEntities context = new UserProfilerSiteEntities())
            {
                var foundUser = context.Users.Where(x => x.UserName.Trim() == userName.Trim()).FirstOrDefault();
                if(foundUser != null)
                {
                    var pulledSaltAndHashedPasswordByUserName = foundUser.PasswordHashAndSalt;
                    isAuthenticated = EPSecurity.IsPasswordValid(password, pulledSaltAndHashedPasswordByUserName);
                }
            }

            return isAuthenticated;

        }

        #endregion

    }
}
