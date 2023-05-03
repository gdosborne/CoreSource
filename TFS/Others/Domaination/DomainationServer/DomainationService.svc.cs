using DomainationData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DomainationServer.Services;
namespace DomainationServer
{
    public class DomainationService : IDomainationService
    {
        public bool Test()
        {
            return true;
        }
        public User Login(string userName, string password, out bool newUserRequired)
        {
            newUserRequired = false;
            User result = null;
            using (var ctx = new DomainationDataContext())
            {
                if (ctx.DomUsers.Any(x => x.EmailAddress.ToLower() == userName.ToLower()))
                    result = ctx.DomUsers.First(x => x.EmailAddress.ToLower() == userName.ToLower()).ToEntity();
                else
                    newUserRequired = true;
            }
            return result;
        }
        public void CreateUser(User user, string password, out bool userAlreadyExists)
        {
            userAlreadyExists = false;
            using (var ctx = new DomainationDataContext())
            {
                if (ctx.DomUsers.Any(x => x.EmailAddress.ToLower() == user.EMailAddress.ToLower()))
                    userAlreadyExists = true;
                else
                {
                    var dbUser = user.ToEntity(password);
                    ctx.DomUsers.InsertOnSubmit(dbUser);
                    ctx.SubmitChanges();
                }
            }
        }
    }
}
