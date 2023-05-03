using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DomainationData.Entities
{
    public sealed class User : BaseEntity
    {
        public Guid ApplicationID { get; set; }
        public string EMailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
