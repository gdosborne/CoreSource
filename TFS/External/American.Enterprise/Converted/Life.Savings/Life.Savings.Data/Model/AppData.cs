using GregOsborne.Application.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
    public interface IAppData
    {
        void Clear();
        IList<WeightMinMax> WeightMinMax { get; }
        IList<LsCorr> LsCorrItems { get; }
        IList<LsRateGp> LsRateGpItems { get; }
        IList<LsCso80> LsCso80Items { get; }
        IList<LsMinp> LsMinpItems { get; }
        IList<LsRatePr> LsRatePrItems { get; }
        IList<LsRateSi> LsRateSiItems { get; }
        IList<LsRateSb> LsRateSbItems { get; }
        IList<LsRateWp> LsRateWpItems { get; }
        IList<LsSpouse> LsSpouseItems { get; }
        IList<LsSurr> LsSurrItems { get; }
        IList<LsTarg> LsTargItems { get; }
        IList<SimpleValue> LsSubStandardRatings { get; }
        IList<SimpleValue> LsClientPlans { get; }
        IList<SimpleValue> LsClientOptions { get; }
        IList<SimpleValue> LsClientRiderOptions { get; }
        IList<SimpleValue> LsClientWPDs { get; }
        IList<SimpleValue> LsClientGPOs { get; }
        IList<SimpleValue> LsClientCOLAs { get; }
        IList<SimpleValue> LsPremiumModes { get; }
        IList<YearsToPaySimpleValue> LsYearToPay { get; }
    }

    public class AppData : IAppData
    {
        public AppData(string dataDirectory)
        {
            WeightMinMax = new List<WeightMinMax>();
            LsCorrItems = new List<LsCorr>();
            LsRateGpItems = new List<LsRateGp>();
            LsCso80Items = new List<LsCso80>();
            LsMinpItems = new List<LsMinp>();
            LsRatePrItems = new List<LsRatePr>();
            LsRateSiItems = new List<LsRateSi>();
            LsRateSbItems = new List<LsRateSb>();
            LsRateWpItems = new List<LsRateWp>();
            LsSpouseItems = new List<LsSpouse>();
            LsSurrItems = new List<LsSurr>();
            LsTargItems = new List<LsTarg>();
            LsSubStandardRatings = new List<SimpleValue>();
            LsClientPlans = new List<SimpleValue>();
            LsClientOptions = new List<SimpleValue>();
            LsClientRiderOptions = new List<SimpleValue>();
            LsClientWPDs = new List<SimpleValue>();
            LsClientGPOs = new List<SimpleValue>();
            LsClientCOLAs = new List<SimpleValue>();
            LsPremiumModes = new List<SimpleValue>();
            LsYearToPay = new List<YearsToPaySimpleValue>();
        }
        public void Clear()
        {
            WeightMinMax.Clear();
            LsCorrItems.Clear();
            LsRateGpItems.Clear();
            LsCso80Items.Clear();
            LsMinpItems.Clear();
            LsRatePrItems.Clear();
            LsRateSiItems.Clear();
            LsRateSbItems.Clear();
            LsRateWpItems.Clear();
            LsSpouseItems.Clear();
            LsSurrItems.Clear();
            LsTargItems.Clear();
            LsSubStandardRatings.Clear();
            LsClientPlans.Clear();
            LsClientOptions.Clear();
            LsClientRiderOptions.Clear();
            LsClientWPDs.Clear();
            LsClientGPOs.Clear();
            LsClientCOLAs.Clear();
            LsPremiumModes.Clear();
            LsYearToPay.Clear();
        }

        public IList<WeightMinMax> WeightMinMax { get; }
        public IList<LsCorr> LsCorrItems { get; }
        public IList<LsRateGp> LsRateGpItems { get; }
        public IList<LsCso80> LsCso80Items { get; }
        public IList<LsMinp> LsMinpItems { get; }
        public IList<LsRatePr> LsRatePrItems { get; }
        public IList<LsRateSi> LsRateSiItems { get; }
        public IList<LsRateSb> LsRateSbItems { get; }
        public IList<LsRateWp> LsRateWpItems { get; }
        public IList<LsSpouse> LsSpouseItems { get; }
        public IList<LsSurr> LsSurrItems { get; }
        public IList<LsTarg> LsTargItems { get; }
        public IList<SimpleValue> LsSubStandardRatings { get; }
        public IList<SimpleValue> LsClientPlans { get; }
        public IList<SimpleValue> LsClientOptions { get; }
        public IList<SimpleValue> LsClientRiderOptions { get; }
        public IList<SimpleValue> LsClientWPDs { get; }
        public IList<SimpleValue> LsClientGPOs { get; }
        public IList<SimpleValue> LsClientCOLAs { get; }
        public IList<SimpleValue> LsPremiumModes { get; }
        public IList<YearsToPaySimpleValue> LsYearToPay { get; }
    }
}
