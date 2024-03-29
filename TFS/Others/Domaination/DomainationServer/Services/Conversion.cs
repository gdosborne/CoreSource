using DomainationData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DomainationServer.Services
{
    public static class Conversion
    {
        public static User ToEntity(this DomUser item)
        {
            return new User
            {
                ID = item.Id,
                ApplicationID = item.ApplicationId,
                EMailAddress = item.EmailAddress,
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }
        public static DomUser ToEntity(this User item, string password)
        {
            return new DomUser
            {
                EmailAddress = item.EMailAddress,
                EncryptedPassword = password,
                FirstName = item.FirstName,
                LastName = item.LastName,
                ApplicationId = Guid.NewGuid()
            };
        }
    }
}
