using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RegistryHack.Controls
{
    public class ExtendedTreeView : TreeView
    {
        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("SelectedItem_", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));

        public ExtendedTreeView()
            : base()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
        }

        public object SelectedItem_ {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }

        private void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }
    }
}