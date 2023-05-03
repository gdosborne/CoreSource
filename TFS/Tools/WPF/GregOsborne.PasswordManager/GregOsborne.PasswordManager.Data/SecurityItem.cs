// <copyright file="SecurityItem.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>2/27/2020</date>

namespace GregOsborne.PasswordManager.Data {
    using System;
    using System.ComponentModel;
    using System.Xml.Linq;

    using GregOsborne.Application;

    public class SecurityItem : SecurityItemBase {
        private DateTime lastAccessed = default;
        private string password = default;
        private string siteUrl = default;
        private string userName = default;

        public DateTime LastAccessed {
            get => this.lastAccessed;
            set {
                this.lastAccessed = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string Password {
            get => this.password;
            set {
                this.password = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string SiteUrl {
            get => this.siteUrl;
            set {
                this.siteUrl = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string UserName {
            get => this.userName;
            set {
                this.userName = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public XElement ToXElement() {
            var result = new XElement("item",
                new XAttribute("name", Name),
                new XElement("description",
                    new XCData(Description)),
                new XElement("siteurl", SiteUrl),
                new XElement("lastaccess", LastAccessed),
                new XElement("username", UserName),
                new XElement("password", Password));
            return result;
        }
    }
}
