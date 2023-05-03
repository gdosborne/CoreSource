using System.Collections.Generic;

namespace OptiRampDesignerModel
{
    public interface IOptionCategory
    {
        #region Public Properties

        IList<IOptionCategory> Categories { get; }
        IList<IOptionItem> Items { get; }
        string Name { get; }

        #endregion Public Properties
    }
}