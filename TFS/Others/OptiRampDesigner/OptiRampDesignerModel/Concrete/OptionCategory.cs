using System.Collections.Generic;

namespace OptiRampDesignerModel.Concrete
{
    public sealed class OptionCategory : IOptionCategory
    {
        #region Public Constructors

        public OptionCategory(string name)
        {
            Name = name;
            Categories = new List<IOptionCategory>();
            Items = new List<IOptionItem>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<IOptionCategory> Categories { get; private set; }
        public IList<IOptionItem> Items { get; private set; }
        public string Name { get; private set; }

        #endregion Public Properties
    }
}