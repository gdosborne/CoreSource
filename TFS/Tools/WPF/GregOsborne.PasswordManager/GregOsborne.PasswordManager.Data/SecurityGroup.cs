// <copyright file="SecurityGroup.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>2/26/2020</date>

namespace GregOsborne.PasswordManager.Data {
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Linq;

    using GregOsborne.Application;

    public class SecurityGroup : SecurityItemBase {
        private ObservableCollection<SecurityGroup> groups = default;
        private ObservableCollection<SecurityItem> items = default;
        public SecurityGroup() {
            Items = new ObservableCollection<SecurityItem>();
            Groups = new ObservableCollection<SecurityGroup>();
        }

        public ObservableCollection<SecurityGroup> Groups {
            get => this.groups;
            set {
                this.groups = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ObservableCollection<SecurityItem> Items {
            get => this.items;
            set {
                this.items = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public XElement ToXElement() {
            var result = new XElement("group",
                new XAttribute("name", Name),
                new XElement("description",
                    new XCData(Description)));
            var grpElem = new XElement("groups");
            if (Groups.Any()) {
                foreach (var group in Groups) {
                    grpElem.Add(group.ToXElement());
                }
            }
            result.Add(grpElem);
            var itemElem = new XElement("items");
            if (Items.Any()) {
                foreach (var item in Items) {
                    itemElem.Add(item.ToXElement());
                }
            }
            result.Add(itemElem);
            return result;
        }
    }
}
