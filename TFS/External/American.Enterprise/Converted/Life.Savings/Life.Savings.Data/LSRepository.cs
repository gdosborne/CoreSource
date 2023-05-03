using GregOsborne.Application.Primitives;
using Life.Savings.Data.Model;
using OSCrypto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Life.Savings.Data {
    public interface IRepository {
        bool EncryptClients { get; set; }
        int LastClientId { get; set; }
        void AddClient(Client client);
        void SaveClients();
        void RemoveClientById(int id);
        void RefreshClientData();
        void RefreshStaticData();
        IList<Client> Clients { get; }
        IList<State> States { get; }
        IList<Gender> Genders { get; }
        IList<DayOfWeek> DaysOfTheWeek { get; }
        IList<Month> MonthsOfTheYear { get; }
        IAppData Ls2Data { get; }
        IAppData Ls3Data { get; }
        IAppData SelectedDataSet { get; }
        string Location { get; set; }
        bool IsLS3DataSelected { get; set; }
    }

    public sealed class LsRepository : IRepository {
        public bool EncryptClients { get; set; }
        public int LastClientId { get; set; }
        public bool IsLS3DataSelected { get; set; }

        private const string Salt = "aMeriCanEnterPrise";
        private string _location;
        public void AddClient(Client client) {
            if (client.Id == 0)
            {
                LastClientId++;
                client.Id = LastClientId;
            }
            if (!Clients.Any(x => x.Id == client.Id))
                Clients.Add(client);
            SaveClients();
        }
        public void RemoveClientById(int id) {
            if (!Clients.Any(x => x.Id == id))
                return;
            Clients.Remove(Clients.FirstOrDefault(x => x.Id == id));
            SaveClients();
        }
        private void ReplaceFile<T>(string fileName, T data) {
            var folderName = Path.GetDirectoryName(fileName);
            var fName = Path.GetFileNameWithoutExtension(fileName);
            var backupFolderName = Path.Combine(folderName, "backup");
            if (!Directory.Exists(backupFolderName))
                Directory.CreateDirectory(backupFolderName);
            var index = 0;
            var backupFileName = Path.Combine(backupFolderName, $"{fName}.{index}.bak");
            while (File.Exists(backupFileName))
            {
                index++;
                backupFileName = Path.Combine(backupFolderName, $"{fName}.{index}.bak");
            }
            if (File.Exists(fileName))
                File.Move(fileName, backupFileName);
            if (typeof(T) == typeof(XDocument))
                data.As<XDocument>().Save(fileName);
            else if (typeof(T) == typeof(byte[]))
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(data.As<byte[]>());
                }
            }
        }
        public void SaveClients() {
            var doc = new XDocument(new XElement("Clients", new XAttribute("lastid", LastClientId)));
            Clients.ToList().ForEach(x => doc.Root.Add(x.ToXElement()));
            var clientLocation = new DirectoryInfo(Location).Parent.FullName;
            var clientFileName = Path.Combine(clientLocation, "Clients.dat");
            //ValidateDataFile("Clients.xml");
            //var dataFile = Path.Combine(Location, "Clients.dat");
            if (!EncryptClients)
                ReplaceFile(clientFileName, doc);
            else
                ReplaceFile(clientFileName, new Crypto(Salt).Encrypt<byte[]>(doc.ToString()));
        }
        public IAppData SelectedDataSet {
            get { return IsLS3DataSelected ? Ls3Data : Ls2Data; }
        }
        public LsRepository(string location, bool encryptData) {
            EncryptClients = encryptData;
            LastClientId = 0;
            States = new List<State>();
            Genders = new List<Gender>();
            DaysOfTheWeek = new List<DayOfWeek>();
            MonthsOfTheYear = new List<Month>();
            Clients = new List<Client>();
            Ls2Data = new AppData(location);
            //Ls3Data = new AppData();
            Location = location;

            IsLS3DataSelected = false;
        }

        public IAppData Ls2Data { get; }
        public IAppData Ls3Data { get; }

        public string Location {
            get => _location;
            set {
                _location = value;
                if (!Directory.Exists(Location))
                    Directory.CreateDirectory(Location);
                RefreshStaticData();
                RefreshClientData();
            }
        }

        public IList<State> States { get; }
        public IList<Gender> Genders { get; }
        public IList<DayOfWeek> DaysOfTheWeek { get; }
        public IList<Month> MonthsOfTheYear { get; }
        public IList<Client> Clients { get; }

        public void RefreshClientData() {
            Clients.Clear();
            var dataSet = new DataSet();
            XDocument doc = null;
            var clientLocation = new DirectoryInfo(Location).Parent.FullName;
            var clientFileName = Path.Combine(clientLocation, "Clients.dat");
            if (EncryptClients && File.Exists(clientFileName))
            {
                var crypto = new Crypto(Salt);
                byte[] data = null;
                using (var fs = new FileStream(clientFileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    var len = (int)fs.Length;
                    data = new byte[len];
                    fs.Read(data, 0, len);
                }
                if (data != null)
                {
                    var xmlData = crypto.Decrypt<byte[]>(data);
                    doc = XDocument.Parse(xmlData);
                }
            }
            else
            {
                if (!File.Exists(clientFileName))
                {
                    var result = ValidateDataFile("Clients.xml");
                    File.Move(result, clientFileName);
                }
                doc = XDocument.Load(clientFileName);
            }

            if (doc == null)
                return;
            var root = doc.Root;
            LastClientId = root.Attribute("lastid") == null ? 0 : int.Parse(root.Attribute("lastid").Value);
            root.Elements().ToList().ForEach(x => {
                var c = Client.FromXElement(x,
                    States,
                    Genders,
                    SelectedDataSet.LsClientPlans,
                    SelectedDataSet.LsClientOptions,
                    SelectedDataSet.LsClientRiderOptions,
                    SelectedDataSet.LsSubStandardRatings,
                    SelectedDataSet.LsClientGPOs,
                    SelectedDataSet.LsClientWPDs,
                    SelectedDataSet.LsClientCOLAs
                );
                Clients.Add(c);
            });
        }

        public void RefreshStaticData() {
            if (States != null)
                States.Clear();
            if (Genders != null)
                Genders.Clear();
            if (Ls2Data != null)
                Ls2Data.Clear();
            if (Ls3Data != null)
                Ls3Data.Clear();

            #region shared data
            var dataSet = new DataSet();
            var result = ValidateDataFile("Genders.xml");
            dataSet.ReadXml(result, XmlReadMode.InferSchema);

            foreach (var item in dataSet.Tables["Gender"].AsEnumerable())
            {
                var dataItem = new Gender {
                    Id = int.Parse((string)item["Id"]),
                    Name = (string)item["Name"],
                    Abbreviation = ((string)item["Abbreviation"]).ToCharArray()[0]
                };
                Genders.Add(dataItem);
            }
            dataSet = new DataSet();
            result = ValidateDataFile("States.xml");
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["State"].AsEnumerable())
            {
                var dataItem = new State {
                    Id = int.Parse((string)item["Id"]),
                    Name = (string)item["Name"],
                    Abbreviation = (string)item["Abbreviation"]
                };
                States.Add(dataItem);
            }
            #endregion

            var source = new DirectoryInfo(Location).Name.StartsWith("ls2", StringComparison.OrdinalIgnoreCase) ? "LS2" : "LS3";

            #region LS2Data
            #region "weightminmax.xml" Height/Weight
            var lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("WeightMinMax.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["Weight"].AsEnumerable())
            {
                var subItem = new WeightValue();
                for (int i = 0; i < dataSet.Tables["Weight"].Columns.Count; i++)
                {
                    var col = dataSet.Tables["Weight"].Columns[i];
                    if (col.Caption.Equals("Id")) continue;
                    var key = 0;
                    var min = 0.0;
                    var max = 0.0;
                    if (col.Caption.StartsWith("weightmin", StringComparison.OrdinalIgnoreCase))
                    {
                        var num = col.Caption.ToLower().Replace("weightmin", string.Empty);
                        if (!string.IsNullOrEmpty(num))
                        {
                            key = int.Parse(num);
                        }
                        min = double.Parse((string)item[col.Caption]);
                    }
                    else if (col.Caption.StartsWith("weightmax", StringComparison.OrdinalIgnoreCase))
                    {
                        var num = col.Caption.ToLower().Replace("weightmax", string.Empty);
                        if (!string.IsNullOrEmpty(num))
                        {
                            key = int.Parse(num);
                        }
                        max = double.Parse((string)item[col.Caption]);
                    }
                    lastId++;
                    var id = dataSet.Tables["Weight"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                    AddOrUpdateWeightItem(col.Caption, id, key, min, max);
                }
            }
            #endregion

            #region "lscorr.xml" Corridor of Coverage
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsCorr.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsCorr"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsCorr"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsCorr {
                    Id = id,
                    Value = double.Parse((string)item["Value"])
                };
                Ls2Data.LsCorrItems.Add(dataItem);
            }
            #endregion

            #region "lscs080.xml" CS80 Values for Guideline
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsCsO80.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsCsO80"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsCsO80"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new LsCso80 {
                    Id = id,
                    MaleNonSmoker = double.Parse((string)item["MaleNS"]),
                    MaleSmoker = double.Parse((string)item["MaleSM"]),
                    FemaleNonSmoker = double.Parse((string)item["FemaleNS"]),
                    FemaleSmoker = double.Parse((string)item["FemaleSM"])
                };
                Ls2Data.LsCso80Items.Add(dataItem);
            }
            #endregion

            #region "lsminp.xml" Minimum Premiums
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsMinp.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsMinp"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsMinp"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsMinp {
                    Id = id
                };
                dataItem.MaleNonSmokerMinp.As<List<double>>().AddRange(((string)item["MaleNSMinp"]).Split(',').Select(x => double.Parse(x)));
                dataItem.MaleSmokerMinp.As<List<double>>().AddRange(((string)item["MaleSMMinp"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleNonSmokerMinp.As<List<double>>().AddRange(((string)item["FemaleNSMinp"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleSmokerMinp.As<List<double>>().AddRange(((string)item["FemaleSMMinp"]).Split(',').Select(x => double.Parse(x)));
                Ls2Data.LsMinpItems.Add(dataItem);
            }
            #endregion

            #region "lsrategp.xml" GPO Rates
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsRateGp.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsRateGp"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsRateGp"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsRateGp {
                    Id = id,
                    Value = double.Parse((string)item["Value"])
                };
                Ls2Data.LsRateGpItems.Add(dataItem);
            }
            #endregion

            #region "lsratepr.xml" Principal Insured
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsRatePr.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsRatePr"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsRatePr"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsRatePr {
                    Id = id
                };
                dataItem.MaleNonSmoker.As<List<double>>().AddRange(((string)item["MaleNS"]).Split(',').Select(x => double.Parse(x)));
                dataItem.MaleSmoker.As<List<double>>().AddRange(((string)item["MaleSM"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleNonSmoker.As<List<double>>().AddRange(((string)item["FemaleNS"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleSmoker.As<List<double>>().AddRange(((string)item["FemaleSM"]).Split(',').Select(x => double.Parse(x)));
                Ls2Data.LsRatePrItems.Add(dataItem);
            }
            #endregion

            #region "lsrateb.xml" Substandard Rates
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsRateB.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsRateB"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsRateB"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsRateSb {
                    Id = id,
                    SmokerSubstandard = double.Parse((string)item["SmokerSub"]),
                    NonSmokerSubstandard = double.Parse((string)item["NonSmokerSub"])
                };
                Ls2Data.LsRateSbItems.Add(dataItem);
            }
            #endregion

            #region "lsratesi.xml" Spouse/Additional Insured
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsRateSi.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsRateSi"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsRateSi"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsRateSi {
                    Id = id
                };
                dataItem.MaleNonSmoker.As<List<double>>().AddRange(((string)item["MaleNS"]).Split(',').Select(x => double.Parse(x)));
                dataItem.MaleSmoker.As<List<double>>().AddRange(((string)item["MaleSM"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleNonSmoker.As<List<double>>().AddRange(((string)item["FemaleNS"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleSmoker.As<List<double>>().AddRange(((string)item["FemaleSM"]).Split(',').Select(x => double.Parse(x)));
                Ls2Data.LsRateSiItems.Add(dataItem);
            }
            #endregion

            #region "lsratewp.xml" Waiver of Premiums
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsRateWp.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsRateWp"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsRateWp"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsRateWp {
                    Id = id,
                    MaleWPD = double.Parse((string)item["MaleWPD"]),
                    FemaleWPD = double.Parse((string)item["FemaleWPD"])
                };
                //values not used?
                dataItem.MaleWPD = dataItem.MaleWPD.IsBetween(0, 1, true) ? dataItem.MaleWPD : 0;
                dataItem.FemaleWPD = dataItem.FemaleWPD.IsBetween(0, 1, true) ? dataItem.FemaleWPD : 0;
                //****************
                Ls2Data.LsRateWpItems.Add(dataItem);
            }
            #endregion

            #region "lsspouse.xml" Spouse Death Benefit Adjustment
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsSpouse.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsSpouse"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsSpouse"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsSpouse {
                    Id = id,
                    Value = double.Parse((string)item["Value"])
                };
                Ls2Data.LsSpouseItems.Add(dataItem);
            }
            #endregion

            #region "lssurr.xml" Surrender Charges
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsSurr.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsSurr"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsSurr"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsSurr {
                    Id = id
                };
                dataItem.MaleSurr.As<List<double>>().AddRange(((string)item["MaleSurr"]).Split(',').Select(x => double.Parse(x)));
                dataItem.FemaleSurr.As<List<double>>().AddRange(((string)item["FemaleSurr"]).Split(',').Select(x => double.Parse(x)));
                Ls2Data.LsSurrItems.Add(dataItem);
            }
            #endregion

            #region "lstarg.xml" Target Premium
            lastId = 0;
            dataSet = new DataSet();
            result = ValidateDataFile("LsTarg.xml", source);
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["LsTarg"].AsEnumerable())
            {
                lastId++;
                var id = dataSet.Tables["LsTarg"].Columns.Contains("Id") ? int.Parse((string)item["Id"]) : lastId;
                var dataItem = new Model.LsTarg {
                    Id = id,
                    MaleTarget = double.Parse((string)item["MaleTarg"]),
                    FemaleTarget = double.Parse((string)item["FemaleTarg"])
                };
                Ls2Data.LsTargItems.Add(dataItem);
            }
            #endregion
            #endregion

            #region "standarddata.xml" Values for dropdowns
            IDictionary<double, string> localNames = new Dictionary<double, string>();
            dataSet = new DataSet();
            result = ValidateDataFile("StandardData.xml");
            dataSet.ReadXml(result, XmlReadMode.InferSchema);
            foreach (var item in dataSet.Tables["text_value"].AsEnumerable())
            {
                localNames.Add(double.Parse((string)item["id"]), (string)item["value"]);
            }
            SimpleValue.NegativeValues = localNames;
            foreach (var item in dataSet.Tables["client_plan"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientPlans.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["client_option"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientOptions.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["client_rider_option"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientRiderOptions.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["client_wpd"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientWPDs.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["client_gpo"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientGPOs.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["client_cola"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsClientCOLAs.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["substandard_table_rating"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsSubStandardRatings.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["premium_mode"].AsEnumerable())
            {
                var dataItem = new SimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsPremiumModes.Add(dataItem);
            }
            foreach (var item in dataSet.Tables["years_to_pay"].AsEnumerable())
            {
                var dataItem = new YearsToPaySimpleValue {
                    Id = int.Parse((string)item["id"]),
                    Value = double.Parse((string)item["value"])
                };
                Ls2Data.LsYearToPay.Add(dataItem);
            }
            #endregion
        }

        private string ValidateDataFile(string fileName, string source = null) {
            var result = string.Empty;
            var repoFileName = Path.Combine(Location, fileName);
            if (!File.Exists(repoFileName))
            {
                source = string.IsNullOrEmpty(source) ? string.Empty : $"{source}Data_";
                using (var inStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Life.Savings.Data.{source}{fileName}"))
                using (var outStream = GregOsborne.Application.IO.File.OpenFile(repoFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    if (inStream == null) return result;
                    using (var reader = new StreamReader(inStream))
                    using (var writer = new StreamWriter(outStream))
                    {
                        result = reader.ReadToEnd();
                        writer.Write(result);
                    }
                }
            }
            return repoFileName;
        }

        private void AddOrUpdateWeightItem(string attributeName, int id, int? key, double min, double max) {
            var isMax = attributeName.StartsWith("weightmax", StringComparison.OrdinalIgnoreCase);
            if (Ls2Data.WeightMinMax.All(w => w.Id != id))
            {
                var w = new WeightMinMax { Id = id };
                w.WeightValues.Add(new WeightValue { Key = key.ToString(), Min = min, Max = max });
                Ls2Data.WeightMinMax.Add(w);
                return;
            }
            var item = Ls2Data.WeightMinMax.FirstOrDefault(x => x.Id == id);
            var value = item?.WeightValues.FirstOrDefault(x => x.Key == key.ToString());
            if (value == null)
            {
                item?.WeightValues.Add(new WeightValue { Key = key.ToString(), Min = min, Max = max });
                return;
            }
            if (isMax)
                value.Max = max;
            else
                value.Min = min;
        }
    }
}