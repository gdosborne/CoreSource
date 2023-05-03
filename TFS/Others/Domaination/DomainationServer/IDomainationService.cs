using DomainationData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
namespace DomainationServer
{
	[ServiceContract]
	public interface IDomainationService
	{
		[OperationContract]
		bool Test();
        [OperationContract]
        User Login(string userName, string password, out bool newUserRequired);
        [OperationContract]
        void CreateUser(User user, string password, out bool userAlreadyExists);
	}
}
