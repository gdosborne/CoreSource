using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using GregOsborne.Application.Primitives;
using Life.Savings.Data;
using Life.Savings.Data.Model;

public class ModelBase {
    public int Id { get; set; }
}
public class LsRatePr : ModelBase {
    public LsRatePr() {
        MaleNonSmoker = new List<float>();
        MaleSmoker = new List<float>();
        FemaleNonSmoker = new List<float>();
        FemaleSmoker = new List<float>();
    }
    public IList<float> MaleNonSmoker { get; }
    public IList<float> MaleSmoker { get; }
    public IList<float> FemaleNonSmoker { get; }
    public IList<float> FemaleSmoker { get; }
}
public class LsRateSi : ModelBase {
    public LsRateSi() {
        MaleNonSmoker = new List<float>();
        MaleSmoker = new List<float>();
        FemaleNonSmoker = new List<float>();
        FemaleSmoker = new List<float>();
    }
    public IList<float> MaleNonSmoker { get; }
    public IList<float> MaleSmoker { get; }
    public IList<float> FemaleNonSmoker { get; }
    public IList<float> FemaleSmoker { get; }
}
public class LsCS80 : ModelBase {
    public float MaleNonSmoker { get; set; }
    public float MaleSmoker { get; set; }
    public float FemaleNonSmoker { get; set; }
    public float FemaleSmoker { get; set; }
}
public class LsRateGp : ModelBase {
    public float Value { get; set; }
}
public class LsCorr : ModelBase {
    public float Value { get; set; }
}
public class LsRateWp : ModelBase {
    public float MaleWPD { get; set; }
    public float FemaleWPD { get; set; }
}
public class LsSpouse : ModelBase {
    public float Value { get; set; }
}
public class LsSurr : ModelBase {
    public LsSurr() {
        MaleSurr = new List<float>();
        FemaleSurr = new List<float>();
    }
    public IList<float> MaleSurr { get; private set; }
    public IList<float> FemaleSurr { get; private set; }
}
public class LsMinp : ModelBase {
    public LsMinp() {
        MaleNonSmoker = new List<float>();
        MaleSmoker = new List<float>();
        FemaleNonSmoker = new List<float>();
        FemaleSmoker = new List<float>();
    }
    public IList<float> MaleNonSmoker { get; }
    public IList<float> MaleSmoker { get; }
    public IList<float> FemaleNonSmoker { get; }
    public IList<float> FemaleSmoker { get; }
}
public class LsTarg : ModelBase {
    public float MaleTarget { get; set; }
    public float FemaleTarget { get; set; }
}
public class LsRateSb : ModelBase {
    public float SmokerSubstandard { get; set; }
    public float NonSmokerSubstandard { get; set; }
}

static class DataAccess {
    private static string dataLocation = "C:\\Users\\greg\\OneDrive\\Customers\\American Enterprise\\Life.Savings\\Ls2Data\\";
    private static List<float> GetListOfValues(string valueList) {
        var items = valueList.Split(',');
        var result = new List<float>();
        for (int index = 0; index <= items.Length - 1; index++)
        {
            result.Add(float.Parse(items[index]));
        }
        return result;
    }

    static IList<LsRatePr> static_GetRatePR_ratePrs;
    public static LsRatePr GetRatePR(int recordNumber, IAppData data) {
        if (static_GetRatePR_ratePrs == null)
        {
            static_GetRatePR_ratePrs = new List<LsRatePr>();
            data.LsRatePrItems.ToList().ForEach(item => {
                var dataItem = new LsRatePr {
                    Id = item.Id
                };
                dataItem.MaleNonSmoker.As<List<float>>().AddRange(item.MaleNonSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.MaleSmoker.As<List<float>>().AddRange(item.MaleSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleNonSmoker.As<List<float>>().AddRange(item.FemaleNonSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleSmoker.As<List<float>>().AddRange(item.FemaleSmoker.Select(x => Convert.ToSingle(x)));
                static_GetRatePR_ratePrs.Add(dataItem);
            });
        }
        return static_GetRatePR_ratePrs.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsRateSi> static_GetRateSI_rateSis;
    public static LsRateSi GetRateSI(int recordNumber, IAppData data) {
        if (static_GetRateSI_rateSis == null)
        {
            static_GetRateSI_rateSis = new List<LsRateSi>();
            data.LsRateSiItems.ToList().ForEach(item => {
                var dataItem = new LsRateSi {
                    Id = item.Id
                };
                dataItem.MaleNonSmoker.As<List<float>>().AddRange(item.MaleNonSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.MaleSmoker.As<List<float>>().AddRange(item.MaleSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleNonSmoker.As<List<float>>().AddRange(item.FemaleNonSmoker.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleSmoker.As<List<float>>().AddRange(item.FemaleSmoker.Select(x => Convert.ToSingle(x)));
                static_GetRateSI_rateSis.Add(dataItem);
            });
        }
        return static_GetRateSI_rateSis.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsRateWp> static_GetRateWP_rateWPs;
    public static LsRateWp GetRateWP(int recordNumber, IAppData data) {
        if (static_GetRateWP_rateWPs == null)
        {
            static_GetRateWP_rateWPs = new List<LsRateWp>();
            data.LsRateWpItems.ToList().ForEach(item => {
                var dataItem = new LsRateWp {
                    Id = item.Id
                };
                dataItem.MaleWPD = Convert.ToSingle(item.MaleWPD);
                dataItem.FemaleWPD = Convert.ToSingle(item.FemaleWPD);
                static_GetRateWP_rateWPs.Add(dataItem);
            });
        }
        return static_GetRateWP_rateWPs.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsRateSb> static_GetRateSB_rateSbs;
    public static LsRateSb GetRateSB(int recordNumber, IAppData data) {
        if (static_GetRateSB_rateSbs == null)
        {
            static_GetRateSB_rateSbs = new List<LsRateSb>();
            data.LsRateSbItems.ToList().ForEach(item => {
                var dataItem = new LsRateSb {
                    Id = item.Id
                };
                dataItem.SmokerSubstandard = Convert.ToSingle(item.SmokerSubstandard);
                dataItem.NonSmokerSubstandard = Convert.ToSingle(item.NonSmokerSubstandard);
                static_GetRateSB_rateSbs.Add(dataItem);
            });
        }
        return static_GetRateSB_rateSbs.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsTarg> static_GetRateTarg_rateTargs;
    public static LsTarg GetRateTarg(int recordNumber, IAppData data) {
        if (static_GetRateTarg_rateTargs == null)
        {
            static_GetRateTarg_rateTargs = new List<LsTarg>();
            data.LsTargItems.ToList().ForEach(item => {
                var dataItem = new LsTarg {
                    Id = item.Id
                };
                dataItem.MaleTarget = Convert.ToSingle(item.MaleTarget);
                dataItem.FemaleTarget = Convert.ToSingle(item.FemaleTarget);
                static_GetRateTarg_rateTargs.Add(dataItem);
            });
        }
        return static_GetRateTarg_rateTargs.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsCS80> static_GetRateCS80_rateCS80s;
    public static LsCS80 GetRateCS80(int recordNumber, IAppData data) {
        if (static_GetRateCS80_rateCS80s == null)
        {
            static_GetRateCS80_rateCS80s = new List<LsCS80>();
            data.LsCso80Items.ToList().ForEach(item => {
                var dataItem = new LsCS80 {
                    Id = item.Id
                };
                dataItem.MaleNonSmoker = Convert.ToSingle(item.MaleNonSmoker);
                dataItem.MaleSmoker = Convert.ToSingle(item.MaleSmoker);
                dataItem.FemaleNonSmoker = Convert.ToSingle(item.FemaleNonSmoker);
                dataItem.FemaleSmoker = Convert.ToSingle(item.FemaleSmoker);
                static_GetRateCS80_rateCS80s.Add(dataItem);
            });
        }
        return static_GetRateCS80_rateCS80s.FirstOrDefault(x => x.Id == recordNumber);
    }

    static IList<LsMinp> static_GetRateMinp_rateMinps;
    public static LsMinp GetRateMinp(int recordNumber, IAppData data) {
        if (static_GetRateMinp_rateMinps == null)
        {
            static_GetRateMinp_rateMinps = new List<LsMinp>();
            data.LsMinpItems.ToList().ForEach(item => {
                var dataItem = new LsMinp {
                    Id = item.Id
                };
                dataItem.MaleNonSmoker.As<List<float>>().AddRange(item.MaleNonSmokerMinp.Select(x => Convert.ToSingle(x)));
                dataItem.MaleSmoker.As<List<float>>().AddRange(item.MaleSmokerMinp.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleNonSmoker.As<List<float>>().AddRange(item.FemaleNonSmokerMinp.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleSmoker.As<List<float>>().AddRange(item.FemaleSmokerMinp.Select(x => Convert.ToSingle(x)));
                static_GetRateMinp_rateMinps.Add(dataItem);
            });
        }
        return static_GetRateMinp_rateMinps.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsRateGp> static_GetRateGp_rateGps;
    public static LsRateGp GetRateGp(int recordNumber, IAppData data) {
        if (static_GetRateGp_rateGps == null)
        {
            static_GetRateGp_rateGps = new List<LsRateGp>();
            data.LsRateGpItems.ToList().ForEach(item => {
                var dataItem = new LsRateGp {
                    Id = item.Id
                };
                dataItem.Value = Convert.ToSingle(item.Value);
                static_GetRateGp_rateGps.Add(dataItem);
            });
        }
        return static_GetRateGp_rateGps.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsCorr> static_GetRateLsCorr_rateCorr;
    public static LsCorr GetRateLsCorr(int recordNumber, IAppData data) {
        if (static_GetRateLsCorr_rateCorr == null)
        {
            static_GetRateLsCorr_rateCorr = new List<LsCorr>();
            data.LsRateGpItems.ToList().ForEach(item => {
                var dataItem = new LsCorr {
                    Id = item.Id
                };
                dataItem.Value = Convert.ToSingle(item.Value);
                static_GetRateLsCorr_rateCorr.Add(dataItem);
            });
        }
        return static_GetRateLsCorr_rateCorr.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsSpouse> static_GetRateLsSpouse_rateSpouse;
    public static LsSpouse GetRateLsSpouse(int recordNumber, IAppData data) {
        if (static_GetRateLsSpouse_rateSpouse == null)
        {
            static_GetRateLsSpouse_rateSpouse = new List<LsSpouse>();
            data.LsRateGpItems.ToList().ForEach(item => {
                var dataItem = new LsSpouse {
                    Id = item.Id
                };
                dataItem.Value = Convert.ToSingle(item.Value);
                static_GetRateLsSpouse_rateSpouse.Add(dataItem);
            });
        }
        return static_GetRateLsSpouse_rateSpouse.FirstOrDefault(x => x.Id == recordNumber);
    }
    static IList<LsSurr> static_GetRateLsSurr_rateSurr;
    public static LsSurr GetRateLsSurr(int recordNumber, IAppData data) {
        if (static_GetRateLsSurr_rateSurr == null)
        {
            static_GetRateLsSurr_rateSurr = new List<LsSurr>();
            data.LsSurrItems.ToList().ForEach(item => {
                var dataItem = new LsSurr {
                    Id = item.Id
                };
                dataItem.MaleSurr.As<List<float>>().AddRange(item.MaleSurr.Select(x => Convert.ToSingle(x)));
                dataItem.FemaleSurr.As<List<float>>().AddRange(item.FemaleSurr.Select(x => Convert.ToSingle(x)));
                static_GetRateLsSurr_rateSurr.Add(dataItem);
            });
        }
        return static_GetRateLsSurr_rateSurr.FirstOrDefault(x => x.Id == recordNumber);
    }
}
