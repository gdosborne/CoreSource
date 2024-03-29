using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FormatCode.Classes
{
	public abstract class OptionFlag : INotifyPropertyChanged
	{
		public abstract void InitializeOptions();
		public event PropertyChangedEventHandler PropertyChanged;
		protected OptionFlag() 
		{ 
		}
		private FormatCode.Classes.Enumerations.OptionFlags _OptionFlags;
		public FormatCode.Classes.Enumerations.OptionFlags OptionFlags
		{
			get { return _OptionFlags; }
			set
			{
				_OptionFlags = value;
				OptionHex = ((long)_OptionFlags).ToString("x");
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OptionFlags"));
			}
		}
		private string _OptionHex;
		public string OptionHex
		{
			get { return _OptionHex; }
			set
			{
				_OptionHex = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OptionHex"));
			}
		}
		public bool RemoveHTMLComments
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.RemoveHTMLComments) == Classes.Enumerations.OptionFlags.RemoveHTMLComments; }
			set
			{
				if (RemoveHTMLComments && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.RemoveHTMLComments);
				else if (!RemoveHTMLComments && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.RemoveHTMLComments;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveHTMLComments"));
			}
		}
		public bool RemoveComments
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.RemoveComments) == Classes.Enumerations.OptionFlags.RemoveComments; }
			set
			{
				if (RemoveComments && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.RemoveComments);
				else if (!RemoveComments && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.RemoveComments;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveComments"));
			}
		}
		public bool RemoveRegions
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.RemoveRegions) == Classes.Enumerations.OptionFlags.RemoveRegions; }
			set
			{
				if (RemoveRegions && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.RemoveRegions);
				else if (!RemoveRegions && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.RemoveRegions;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveRegions"));
			}
		}
		public bool RemoveBlankLines
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.RemoveBlankLines) == Classes.Enumerations.OptionFlags.RemoveBlankLines; }
			set
			{
				if (RemoveBlankLines && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.RemoveBlankLines);
				else if (!RemoveBlankLines && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.RemoveBlankLines;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveBlankLines"));
			}
		}
		public bool GroupItemsOfSameScope
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.GroupItemsOfSameScope) == Classes.Enumerations.OptionFlags.GroupItemsOfSameScope; }
			set
			{
				if (GroupItemsOfSameScope && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.GroupItemsOfSameScope);
				else if (!GroupItemsOfSameScope && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.GroupItemsOfSameScope;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("GroupItemsOfSameScope"));
			}
		}
		public bool AlphabetizeItemsOfSameScope
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.AlphabetizeItemsOfSameScope) == Classes.Enumerations.OptionFlags.AlphabetizeItemsOfSameScope; }
			set
			{
				if (AlphabetizeItemsOfSameScope && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.AlphabetizeItemsOfSameScope);
				else if (!AlphabetizeItemsOfSameScope && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.AlphabetizeItemsOfSameScope;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AlphabetizeItemsOfSameScope"));
			}
		}
		public bool AlphabetizeUsings
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.AlphabetizeUsings) == Classes.Enumerations.OptionFlags.AlphabetizeUsings; }
			set
			{
				if (AlphabetizeUsings && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.AlphabetizeUsings);
				else if (!AlphabetizeUsings && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.AlphabetizeUsings;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AlphabetizeUsings"));
			}
		}
		public bool CreateRegionsEvenIfNoItems
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.CreateRegionsEvenIfNoItems) == Classes.Enumerations.OptionFlags.CreateRegionsEvenIfNoItems; }
			set
			{
				if (CreateRegionsEvenIfNoItems && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.CreateRegionsEvenIfNoItems);
				else if (!CreateRegionsEvenIfNoItems && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.CreateRegionsEvenIfNoItems;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CreateRegionsEvenIfNoItems"));
			}
		}
		public bool PrivateUsingRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateUsingRegion) == Classes.Enumerations.OptionFlags.PrivateUsingRegion; }
			set
			{
				if (PrivateUsingRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateUsingRegion);
				else if (!PrivateUsingRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateUsingRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateUsingRegion"));
			}
		}
		public bool PrivateClassRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateClassRegion) == Classes.Enumerations.OptionFlags.PrivateClassRegion; }
			set
			{
				if (PrivateClassRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateClassRegion);
				else if (!PrivateClassRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateClassRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateClassRegion"));
			}
		}
		public bool PrivateStructRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateStructRegion) == Classes.Enumerations.OptionFlags.PrivateStructRegion; }
			set
			{
				if (PrivateStructRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateStructRegion);
				else if (!PrivateStructRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateStructRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateStructRegion"));
			}
		}
		public bool PrivateEnumRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateEnumRegion) == Classes.Enumerations.OptionFlags.PrivateEnumRegion; }
			set
			{
				if (PrivateEnumRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateEnumRegion);
				else if (!PrivateEnumRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateEnumRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateEnumRegion"));
			}
		}
		public bool PrivateDelegateRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateDelegateRegion) == Classes.Enumerations.OptionFlags.PrivateDelegateRegion; }
			set
			{
				if (PrivateDelegateRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateDelegateRegion);
				else if (!PrivateDelegateRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateDelegateRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateDelegateRegion"));
			}
		}
		public bool PrivateFieldRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateFieldRegion) == Classes.Enumerations.OptionFlags.PrivateFieldRegion; }
			set
			{
				if (PrivateFieldRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateFieldRegion);
				else if (!PrivateFieldRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateFieldRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateFieldRegion"));
			}
		}
		public bool PrivatePropertyRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivatePropertyRegion) == Classes.Enumerations.OptionFlags.PrivatePropertyRegion; }
			set
			{
				if (PrivatePropertyRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivatePropertyRegion);
				else if (!PrivatePropertyRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivatePropertyRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivatePropertyRegion"));
			}
		}
		public bool PrivateMethodRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PrivateMethodRegion) == Classes.Enumerations.OptionFlags.PrivateMethodRegion; }
			set
			{
				if (PrivateMethodRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PrivateMethodRegion);
				else if (!PrivateMethodRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PrivateMethodRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PrivateMethodRegion"));
			}
		}
		public bool InternalClassRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalClassRegion) == Classes.Enumerations.OptionFlags.InternalClassRegion; }
			set
			{
				if (InternalClassRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalClassRegion);
				else if (!InternalClassRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalClassRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalClassRegion"));
			}
		}
		public bool InternalStructRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalStructRegion) == Classes.Enumerations.OptionFlags.InternalStructRegion; }
			set
			{
				if (InternalStructRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalStructRegion);
				else if (!InternalStructRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalStructRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalStructRegion"));
			}
		}
		public bool InternalEnumRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalEnumRegion) == Classes.Enumerations.OptionFlags.InternalEnumRegion; }
			set
			{
				if (InternalEnumRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalEnumRegion);
				else if (!InternalEnumRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalEnumRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalEnumRegion"));
			}
		}
		public bool InternalDelegateRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalDelegateRegion) == Classes.Enumerations.OptionFlags.InternalDelegateRegion; }
			set
			{
				if (InternalDelegateRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalDelegateRegion);
				else if (!InternalDelegateRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalDelegateRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalDelegateRegion"));
			}
		}
		public bool InternalFieldRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalFieldRegion) == Classes.Enumerations.OptionFlags.InternalFieldRegion; }
			set
			{
				if (InternalFieldRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalFieldRegion);
				else if (!InternalFieldRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalFieldRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalFieldRegion"));
			}
		}
		public bool InternalPropertyRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalPropertyRegion) == Classes.Enumerations.OptionFlags.InternalPropertyRegion; }
			set
			{
				if (InternalPropertyRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalPropertyRegion);
				else if (!InternalPropertyRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalPropertyRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalPropertyRegion"));
			}
		}
		public bool InternalMethodRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalMethodRegion) == Classes.Enumerations.OptionFlags.InternalMethodRegion; }
			set
			{
				if (InternalMethodRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalMethodRegion);
				else if (!InternalMethodRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalMethodRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalMethodRegion"));
			}
		}
		public bool InternalEventRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.InternalEventRegion) == Classes.Enumerations.OptionFlags.InternalEventRegion; }
			set
			{
				if (InternalEventRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.InternalEventRegion);
				else if (!InternalEventRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.InternalEventRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InternalEventRegion"));
			}
		}
		public bool ProtectedClassRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedClassRegion) == Classes.Enumerations.OptionFlags.ProtectedClassRegion; }
			set
			{
				if (ProtectedClassRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedClassRegion);
				else if (!ProtectedClassRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedClassRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedClassRegion"));
			}
		}
		public bool ProtectedStructRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedStructRegion) == Classes.Enumerations.OptionFlags.ProtectedStructRegion; }
			set
			{
				if (ProtectedStructRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedStructRegion);
				else if (!ProtectedStructRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedStructRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedStructRegion"));
			}
		}
		public bool ProtectedEnumRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedEnumRegion) == Classes.Enumerations.OptionFlags.ProtectedEnumRegion; }
			set
			{
				if (ProtectedEnumRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedEnumRegion);
				else if (!ProtectedEnumRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedEnumRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedEnumRegion"));
			}
		}
		public bool ProtectedDelegateRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedDelegateRegion) == Classes.Enumerations.OptionFlags.ProtectedDelegateRegion; }
			set
			{
				if (ProtectedDelegateRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedDelegateRegion);
				else if (!ProtectedDelegateRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedDelegateRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedDelegateRegion"));
			}
		}
		public bool ProtectedFieldRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedFieldRegion) == Classes.Enumerations.OptionFlags.ProtectedFieldRegion; }
			set
			{
				if (ProtectedFieldRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedFieldRegion);
				else if (!ProtectedFieldRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedFieldRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedFieldRegion"));
			}
		}
		public bool ProtectedPropertyRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedPropertyRegion) == Classes.Enumerations.OptionFlags.ProtectedPropertyRegion; }
			set
			{
				if (ProtectedPropertyRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedPropertyRegion);
				else if (!ProtectedPropertyRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedPropertyRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedPropertyRegion"));
			}
		}
		public bool ProtectedMethodRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedMethodRegion) == Classes.Enumerations.OptionFlags.ProtectedMethodRegion; }
			set
			{
				if (ProtectedMethodRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedMethodRegion);
				else if (!ProtectedMethodRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedMethodRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedMethodRegion"));
			}
		}
		public bool ProtectedEventRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.ProtectedEventRegion) == Classes.Enumerations.OptionFlags.ProtectedEventRegion; }
			set
			{
				if (ProtectedEventRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.ProtectedEventRegion);
				else if (!ProtectedEventRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.ProtectedEventRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProtectedEventRegion"));
			}
		}
		public bool PublicClassRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicClassRegion) == Classes.Enumerations.OptionFlags.PublicClassRegion; }
			set
			{
				if (PublicClassRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicClassRegion);
				else if (!PublicClassRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicClassRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicClassRegion"));
			}
		}
		public bool PublicStructRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicStructRegion) == Classes.Enumerations.OptionFlags.PublicStructRegion; }
			set
			{
				if (PublicStructRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicStructRegion);
				else if (!PublicStructRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicStructRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicStructRegion"));
			}
		}
		public bool PublicEnumRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicEnumRegion) == Classes.Enumerations.OptionFlags.PublicEnumRegion; }
			set
			{
				if (PublicEnumRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicEnumRegion);
				else if (!PublicEnumRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicEnumRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicEnumRegion"));
			}
		}
		public bool PublicDelegateRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicDelegateRegion) == Classes.Enumerations.OptionFlags.PublicDelegateRegion; }
			set
			{
				if (PublicDelegateRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicDelegateRegion);
				else if (!PublicDelegateRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicDelegateRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicDelegateRegion"));
			}
		}
		public bool PublicFieldRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicFieldRegion) == Classes.Enumerations.OptionFlags.PublicFieldRegion; }
			set
			{
				if (PublicFieldRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicFieldRegion);
				else if (!PublicFieldRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicFieldRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicFieldRegion"));
			}
		}
		public bool PublicPropertyRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicPropertyRegion) == Classes.Enumerations.OptionFlags.PublicPropertyRegion; }
			set
			{
				if (PublicPropertyRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicPropertyRegion);
				else if (!PublicPropertyRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicPropertyRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicPropertyRegion"));
			}
		}
		public bool PublicMethodRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicMethodRegion) == Classes.Enumerations.OptionFlags.PublicMethodRegion; }
			set
			{
				if (PublicMethodRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicMethodRegion);
				else if (!PublicMethodRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicMethodRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicMethodRegion"));
			}
		}
		public bool PublicEventRegion
		{
			get { return (OptionFlags & Classes.Enumerations.OptionFlags.PublicEventRegion) == Classes.Enumerations.OptionFlags.PublicEventRegion; }
			set
			{
				if (PublicEventRegion && !value)
					OptionFlags &= ~(Classes.Enumerations.OptionFlags.PublicEventRegion);
				else if (!PublicEventRegion && value)
					OptionFlags |= Classes.Enumerations.OptionFlags.PublicEventRegion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PublicEventRegion"));
			}
		}
	}
}
