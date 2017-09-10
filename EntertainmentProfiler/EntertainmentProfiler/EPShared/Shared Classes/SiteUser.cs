using EPShared.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPShared
{
    public sealed class SiteUser : EPShared.DBContext.User
    {
        // Constructor to inherit base class values (directly from entity)
        public SiteUser(EPShared.DBContext.User copy)
        {
            var parentProperties = copy.GetType().GetProperties();
            var parentPropertyNames = new List<string>();

            foreach(var property in parentProperties)
            {
                parentPropertyNames.Add(property.Name);
            }

            // Allows us to "inherit" from base class. This is done to make accessing entity objects easy
            // while providing an wrapper outer class to be used throughout the application instead of accessing
            // the original entity object directly.
            foreach(var propertyName in parentPropertyNames)
            {
                var parentPropertyValue = copy.GetType().GetProperty(propertyName).GetValue(copy, null);
                this.GetType().GetProperty(propertyName).SetValue(this, parentPropertyValue);
            }
        }

        /// <summary>
        /// Modify user value using reflection. Pass in the public property from
        /// the EPShared.DBContext.User class along with the value to override with.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void ModifyUser(string propertyName, object value)
        {
            try
            {
                int userID = this.ID;

                // Check for a valid ID. It cannot be negative
                if (userID >= 0)
                {
                    using (UserProfilerSiteEntities context = new UserProfilerSiteEntities())
                    {
                        var foundRow = context.Users.Where(x => x.ID == userID).FirstOrDefault();

                        var property = foundRow.GetType().GetProperty(propertyName);

                        property.SetValue(foundRow, value);

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.SimpleLog.Error(ex.Message);
            }
        }

    }
}
