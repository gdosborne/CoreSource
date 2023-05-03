namespace OptiRampDesignerModel
{
    using System.Collections.Generic;

    public interface IOptionSet
    {
        #region Public Properties

        IList<IOptionCategory> Categories { get; }
        string ModuleName { get; }

        #endregion Public Properties
    }
}