using System.Collections.Generic;

namespace OptiRampDesignerModel.Concrete
{
    public sealed class OptionSet : IOptionSet
    {
        #region Public Constructors

        public OptionSet(string moduleName)
        {
            ModuleName = moduleName;
            Categories = new List<IOptionCategory>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<IOptionCategory> Categories { get; private set; }
        public string ModuleName { get; private set; }

        #endregion Public Properties
    }
}