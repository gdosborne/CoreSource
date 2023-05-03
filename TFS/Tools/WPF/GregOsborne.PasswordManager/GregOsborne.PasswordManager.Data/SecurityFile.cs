// <copyright file="SecurityFile.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>2/26/2020</date>

namespace GregOsborne.PasswordManager.Data {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml.Linq;

    using OSCrypto;

    public class SecurityFile {
        private SecurityFile(string fileName) {
            FileName = fileName;
            var crypto = new Crypto(EncryptionSalt);
            var doc = default(XDocument);
            if (File.Exists(fileName)) {
                var allData = default(string);
                var cannotGetDataMessage = $"Cannot get the data from the Password Manager file. " +
                        $"Check your username/password to ensure you've input the correct credentials.";
                try {
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    using (var sr = new StreamReader(fs)) {
                        allData = sr.ReadToEnd();
                    }
                    var xml = crypto.Decrypt(allData);
                    doc = XDocument.Parse(xml);

                    var root = doc.Root;
                    var verAttr = root.Attribute("version");
                    var verCreated = root.Attribute("created");
                    if (verAttr != null) {
                        if (!Version.TryParse(verAttr.Value, out var thisVer)) {
                            throw new ApplicationException(cannotGetDataMessage);
                        } else {
                            if (DocumentVersion < thisVer) {
                                throw new ApplicationException($"Invalid version for this file name. Expected {DocumentVersion}, got {thisVer}.");
                            }
                        }
                    }
                    if (verCreated != null) {
                        if (!DateTime.TryParse(verCreated.Value, out var createdDate)) {
                            throw new ApplicationException(cannotGetDataMessage);
                        } else {
                            FileCreatedDate = createdDate;
                        }
                    }
                    Groups = new ObservableCollection<SecurityGroup>();
                    var groupRootElement = root.Element("groups");
                    if (groupRootElement == null) {
                        throw new ApplicationException("Invalid security file - group parent missing");
                    }

                    foreach (var grpElement in groupRootElement.Elements()) {
                        var name = grpElement.Attribute("name").Value;
                        var desc = grpElement.Element("description").Value;
                        var sg = new SecurityGroup {
                            Name = name,
                            Description = desc
                        };
                        Groups.Add(sg);
                    }
                } catch (Exception) {
                    throw new ApplicationException(cannotGetDataMessage);
                }
            } else {
                Save();
                Groups = new ObservableCollection<SecurityGroup>();
            }
        }

        public Version DocumentVersion => new Version(1, 0);

        public DateTime FileCreatedDate { get; private set; } = DateTime.MinValue;

        public string FileName {
            get; set;
        }

        public ObservableCollection<SecurityGroup> Groups {
            get; set;
        }

        internal static byte[] EncryptionSalt { get; private set; } = default;

        public static SecurityFile Open(string fileName, byte[] encryptionSalt) {
            EncryptionSalt = encryptionSalt;
            return new SecurityFile(fileName);
        }

        public void Save() {
            var doc = new XDocument(
                new XElement("password_manager",
                    new XAttribute("version", DocumentVersion.ToString(2)),
                    new XAttribute("created", DateTime.Now.ToString("yyyy-MM-dd")),
                new XElement("groups")));
            var root = doc.Root;
            var groupRoot = root.Element("groups");
            foreach (var item in Groups) {
                groupRoot.Add(item.ToXElement());
            }
            root.Add(groupRoot);
            var crypto = new Crypto(EncryptionSalt);
            var docData = crypto.Encrypt<string>(doc.ToString());
            using (var fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs)) {
                sw.Write(docData);
            }
        }

        public void Save(string fileName) {
            FileName = fileName;
            Save();
        }
    }
}
