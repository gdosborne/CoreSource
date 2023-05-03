using System.ComponentModel;

namespace OptiRampDesignerModel
{
    public interface IOptionItem : INotifyPropertyChanged
    {
        string Name { get; }
        object Value { get; set; }
    }
}