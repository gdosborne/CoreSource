namespace MyMinistry.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
	using MyMinistry.Utilities;

    public class MyMinistryUser
    {
        #region Public Properties

        public Enumerations.CongregationAssignments Assignment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        #endregion Public Properties
    }
}
