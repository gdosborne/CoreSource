using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Life.Savings.Data.Model {
    public interface IIndividualData : ICloneable, IToNamedXElement, INotifyPropertyChanged {
        SimpleValue BenefitOption { get; set; }
        SimpleValue PlanType { get; set; }
        SimpleValue YearToAgeOption { get; set; }
        SimpleValue SubstandardRate { get; set; }
        SimpleValue Gpo { get; set; }
        SimpleValue Wpd { get; set; }
        SimpleValue Cola { get; set; }
        SimpleValue RemoveYearToAge { get; set; }
        double? InitialDeathBenefit { get; set; }
        double? FaceAmount { get; set; }
        double? LumpSum { get; set; }
        int? AgeOrYear { get; set; }
        int? EndYear { get; set; }
        IndividualData.KnownIndices IndividualType { get; set; }
        int Age { get; set; }
        Gender Gender { get; set; }
        DateTime? BirthDate { get; set; }
        State State { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleInitial { get; set; }
        double? InterestRate { get; set; }
        bool IsSelected { get; set; }
        bool IsEnabled { get; set; }
        int Index { get; set; }
        string Name { get; set; }
        Visibility Visibility { get; set; }
        ObservableCollection<Gender> Genders { get; set; }
        ObservableCollection<SimpleValue> PlanTypes { get; set; }
        ObservableCollection<SimpleValue> TableRatings { get; set; }
        ObservableCollection<SimpleValue> ColaPercents { get; set; }
        ObservableCollection<SimpleValue> YearsToAges { get; set; }
        double PlannedPrem { get; set; }
        int Option { get; set; }
        bool IsFutureWD { get; set; }
        bool FutureChanges { get; set; }
        bool HasSubstandardRate { get; }
        bool HasGpo { get; }
        bool HasWpd { get; }
        bool HasFaceAmount { get; }
    }
    public class IndividualData : IIndividualData {
        public enum KnownIndices {
            Primary = 0,
            Spouse = 1,
            Rider = 2,
            Child = 3
        }

        internal const string firstNameName = "first";
        internal const string lastNameName = "last";
        internal const string middleInitialName = "middle";
        internal const string ageName = "age";
        internal const string birthDateName = "birthdate";
        internal const string stateName = "state";
        internal const string genderName = "gender";
        internal const string planTypeName = "plantype";
        internal const string initialDeathBenefitName = "initialdeathbenefit";
        internal const string interestRateName = "interestrate";
        internal const string optionCodeName = "optioncode";
        internal const string deathBenefitAmountName = "deathbenefitamount";
        internal const string termYearsName = "termyears";
        internal const string yearToAgeOptionName = "yearorage";
        internal const string subStandardRateName = "substandardrate";
        internal const string gpoName = "gpo";
        internal const string wpdName = "wpd";
        internal const string colaName = "cola";
        internal const string removeYearsName = "removetermyears";
        internal const string removeYearsToAgeName = "removeyearstoage";
        internal const string nameName = "name";
        internal const string indexName = "index";
        internal const string spouseBenefitAmountName = "spousebenefitamount";
        internal const string individualTypeName = "individualtype";

        private static IList<State> _states;
        private static IList<Gender> _genders;
        private static IList<SimpleValue> _planTypes;
        private static IList<SimpleValue> _optionCodes;
        private static IList<SimpleValue> _yearToAgeOptions;
        private static IList<SimpleValue> _substandardRates;
        private static IList<SimpleValue> _gpos;
        private static IList<SimpleValue> _wpds;
        private static IList<SimpleValue> _colas;
        private int? _RemoveYears;
        private string _Name;
        private Visibility _Visibility;
        private ObservableCollection<Gender> _Genders;
        private ObservableCollection<SimpleValue> _PlanTypes;
        private ObservableCollection<SimpleValue> _TableRatings;
        private ObservableCollection<SimpleValue> _ColaPercents;
        private ObservableCollection<SimpleValue> _YearsToAges;
        private SimpleValue _RemoveYearToAge;
        private bool _IsEnabled;
        private KnownIndices _IndividualType;
        private bool _IsSelected;
        private string _FirstName;
        private string _LastName;
        private string _MiddleInitial;
        private double? _InitialDeathBenefit;
        private double? _DeathBenefitAmount;
        private int? _TermYears;
        private int _Age;
        private int _ChildIndex;
        private DateTime? _BirthDate;
        private double? _InterestRate;
        private SimpleValue _BenefitOption;
        private SimpleValue _PlanType;
        private SimpleValue _YearToAgeOption;
        private SimpleValue _SubstandardRate;
        private SimpleValue _Gpo;
        private SimpleValue _Wpd;
        private SimpleValue _Cola;
        private Gender _Gender;
        private State _State;

        public bool? IsYearRangeValid(out string errorMessage) {
            errorMessage = null;
            int maxAge = 95;
            if (Age < 1 || AgeOrYear < 1 || EndYear < 1)
                return null;
            bool result = true;
            var tempAge1 = Age;
            var tempAge2 = EndYear ?? 0;
            var tempAge3 = 0;
            if (YearToAgeOption == null)
                return null;

            switch (YearToAgeOption.DisplayedValue)
            {
                case "Age":
                    switch (YearToAgeOption.DisplayedValue)
                    {
                        case "Age":
                            tempAge3 = AgeOrYear ?? 0;
                            break;
                        case "Year":
                            tempAge3 = (AgeOrYear ?? 0) + tempAge1;
                            break;
                    }
                    if (tempAge2 < tempAge3 || tempAge2 > maxAge)
                    {
                        errorMessage = $"End age for additional insured must be greater than or eaual to start age and less than {maxAge}.";
                        result = false;
                    }
                    break;
                case "Year":
                    switch (YearToAgeOption.DisplayedValue)
                    {
                        case "Age":
                            tempAge3 = (AgeOrYear ?? 0) - tempAge1;
                            break;
                        case "Year":
                            tempAge3 = tempAge1;
                            break;
                    }
                    if (tempAge2 > maxAge - tempAge1 || tempAge2 < tempAge3)
                    {
                        errorMessage = $"Years for addition insured cannot be greater than insured age {maxAge} or less than start year";
                        result = false;
                    }
                    break;
            }

            return result;
        }

        internal static T GetElementValue<T>(XElement element) {
            if (element == null || string.IsNullOrEmpty(element.Value))
                return default(T);
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                if (int.TryParse(element.Value, out var t))
                    return (T)(object)t;
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
            {
                if (bool.TryParse(element.Value, out var t))
                    return (T)(object)t;
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
            {
                if (double.TryParse(element.Value, out var t))
                    return (T)(object)t;
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                if (DateTime.TryParse(element.Value, out var t))
                    return (T)(object)t;
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(State))
            {
                if (_states.Any(x => x.Id == int.Parse(element.Value)))
                    return (T)(object)_states.FirstOrDefault(x => x.Id == int.Parse(element.Value));
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(Gender))
            {
                if (_genders.Any(x => x.Id == int.Parse(element.Value)))
                    return (T)(object)_genders.FirstOrDefault(x => x.Id == int.Parse(element.Value));
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(SimpleValue))
            {
                IList<SimpleValue> values = null;
                if (element.Name.LocalName.Equals(planTypeName))
                    values = _planTypes;
                else if (element.Name.LocalName.Equals(optionCodeName))
                    values = _optionCodes;
                else if (element.Name.LocalName.Equals(yearToAgeOptionName) || element.Name.LocalName.Equals(removeYearsToAgeName))
                    values = _yearToAgeOptions;
                else if (element.Name.LocalName.Equals(subStandardRateName))
                    values = _substandardRates;
                else if (element.Name.LocalName.Equals(gpoName))
                    values = _gpos;
                else if (element.Name.LocalName.Equals(wpdName))
                    values = _wpds;
                else if (element.Name.LocalName.Equals(colaName))
                    values = _colas;

                if(values.Any(x=>x.DisplayedValue.Equals(element.Value)))
                    return (T)(object)values.FirstOrDefault(x => x.DisplayedValue.Equals(element.Value));
                else if (values.Any(x => x.Id == int.Parse(element.Value)))
                    return (T)(object)values.FirstOrDefault(x => x.Id == int.Parse(element.Value));
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(KnownIndices))
            {
                if (int.TryParse(element.Value, out var t))
                {
                    var values = Enum.GetValues(typeof(KnownIndices)).Cast<int>();
                    if (!values.Contains(t))
                        return default(T);
                    else
                        return (T)(object)t;
                }
                else
                    return default(T);
            }
            else if (typeof(T) == typeof(string))
                return (T)(object)element.Value;
            else // unsupported types
                return default(T);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool HasSubstandardRate {
            get { return SubstandardRate != null && SubstandardRate.Value > 0; }
        }

        public bool HasGpo {
            get { return Gpo != null && !Gpo.DisplayedValue.Equals("none", StringComparison.OrdinalIgnoreCase); }
        }

        public bool HasWpd {
            get { return Wpd != null; }
        }

        public bool HasFaceAmount {
            get { return FaceAmount.HasValue && FaceAmount.Value > 0; }
        }

        private bool _FutureChanges;
        public bool FutureChanges {
            get => _FutureChanges;
            set {
                _FutureChanges = value;
                InvokePropertyChanged(nameof(FutureChanges));
            }
        }
        
        private bool _IsFutureWD;
        public bool IsFutureWD {
            get => _IsFutureWD;
            set {
                _IsFutureWD = value;
                InvokePropertyChanged(nameof(IsFutureWD));
            }
        }

        private int _Option;
        public int Option {
            get => _Option;
            set {
                _Option = value;
                InvokePropertyChanged(nameof(Option));
            }
        }

        private double? _LumpSum;
        public double? LumpSum {
            get => _LumpSum;
            set {
                _LumpSum = value;
                InvokePropertyChanged(nameof(LumpSum));
            }
        }

        private double _PlannedPrem;
        public double PlannedPrem {
            get => _PlannedPrem;
            set {
                _PlannedPrem = value;
                InvokePropertyChanged(nameof(PlannedPrem));
            }
        }

        public int? EndYear {
            get => _RemoveYears;
            set {
                _RemoveYears = value;
                InvokePropertyChanged(nameof(EndYear));
            }
        }

        public string Name {
            get => _Name;
            set {
                _Name = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public Visibility Visibility {
            get => _Visibility;
            set {
                _Visibility = value;
                InvokePropertyChanged(nameof(Visibility));
            }
        }

        public ObservableCollection<Gender> Genders {
            get => _Genders;
            set {
                _Genders = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<SimpleValue> PlanTypes {
            get => _PlanTypes;
            set {
                _PlanTypes = value;
                InvokePropertyChanged(nameof(PlanTypes));
            }
        }

        public ObservableCollection<SimpleValue> TableRatings {
            get => _TableRatings;
            set {
                _TableRatings = value;
                InvokePropertyChanged(nameof(TableRatings));
            }
        }

        public ObservableCollection<SimpleValue> ColaPercents {
            get => _ColaPercents;
            set {
                _ColaPercents = value;
                InvokePropertyChanged(nameof(ColaPercents));
            }
        }

        public ObservableCollection<SimpleValue> YearsToAges {
            get => _YearsToAges;
            set {
                _YearsToAges = value;
                InvokePropertyChanged(nameof(YearsToAges));
            }
        }

        public SimpleValue RemoveYearToAge {
            get => _RemoveYearToAge;
            set {
                _RemoveYearToAge = value;
                InvokePropertyChanged(nameof(RemoveYearToAge));
            }
        }

        public bool IsEnabled {
            get => _IsEnabled;
            set {
                _IsEnabled = value;
                InvokePropertyChanged(nameof(IsEnabled));
            }
        }

        public int Index {
            get => _ChildIndex;
            set {
                _ChildIndex = value;
                InvokePropertyChanged(nameof(Index));
            }
        }

        public IndividualData.KnownIndices IndividualType {
            get => _IndividualType;
            set {
                _IndividualType = value;
                InvokePropertyChanged(nameof(IndividualType));
            }
        }

        public bool IsSelected {
            get => _IsSelected;
            set {
                _IsSelected = value;
                InvokePropertyChanged(nameof(IsSelected));
            }
        }

        public string FirstName {
            get => _FirstName;
            set {
                _FirstName = value;
                InvokePropertyChanged(nameof(FirstName));
            }
        }

        public string LastName {
            get => _LastName;
            set {
                _LastName = value;
                InvokePropertyChanged(nameof(LastName));
            }
        }

        public string MiddleInitial {
            get => _MiddleInitial;
            set {
                _MiddleInitial = value;
                InvokePropertyChanged(nameof(MiddleInitial));
            }
        }

        public double? InitialDeathBenefit {
            get => _InitialDeathBenefit;
            set {
                _InitialDeathBenefit = value;
                InvokePropertyChanged(nameof(InitialDeathBenefit));
            }
        }

        public double? FaceAmount {
            get => _DeathBenefitAmount;
            set {
                _DeathBenefitAmount = value;
                InvokePropertyChanged(nameof(FaceAmount));
            }
        }

        public int? AgeOrYear {
            get => _TermYears;
            set {
                _TermYears = value;
                InvokePropertyChanged(nameof(AgeOrYear));
            }
        }

        public int Age {
            get => _Age;
            set {
                _Age = value;
                InvokePropertyChanged(nameof(Age));
            }
        }

        public DateTime? BirthDate {
            get => _BirthDate;
            set {
                _BirthDate = value;
                InvokePropertyChanged(nameof(BirthDate));
            }
        }

        public double? InterestRate {
            get => _InterestRate;
            set {
                _InterestRate = value;
                InvokePropertyChanged(nameof(InterestRate));
            }
        }

        public SimpleValue BenefitOption {
            get => _BenefitOption;
            set {
                _BenefitOption = value;
                InvokePropertyChanged(nameof(BenefitOption));
            }
        }

        public SimpleValue PlanType {
            get => _PlanType;
            set {
                _PlanType = value;
                InvokePropertyChanged(nameof(PlanType));
            }
        }

        public SimpleValue YearToAgeOption {
            get => _YearToAgeOption;
            set {
                _YearToAgeOption = value;
                InvokePropertyChanged(nameof(YearToAgeOption));
            }
        }

        public SimpleValue SubstandardRate {
            get => _SubstandardRate;
            set {
                _SubstandardRate = value;
                InvokePropertyChanged(nameof(SubstandardRate));
            }
        }

        public SimpleValue Gpo {
            get => _Gpo;
            set {
                _Gpo = value;
                InvokePropertyChanged(nameof(Gpo));
            }
        }

        public SimpleValue Wpd {
            get => _Wpd;
            set {
                _Wpd = value;
                InvokePropertyChanged(nameof(Wpd));
            }
        }

        public SimpleValue Cola {
            get => _Cola;
            set {
                _Cola = value;
                InvokePropertyChanged(nameof(Cola));
            }
        }

        public Gender Gender {
            get => _Gender;
            set {
                _Gender = value;
                InvokePropertyChanged(nameof(Gender));
            }
        }

        public State State {
            get => _State;
            set {
                _State = value;
                InvokePropertyChanged(nameof(State));
            }
        }

        public static IndividualData FromXElement(XElement element, int index, IList<State> states, IList<Gender> genders, IList<SimpleValue> planTypes, IList<SimpleValue> optionCodes, IList<SimpleValue> yearToAgeOptions, IList<SimpleValue> substandardRates, IList<SimpleValue> gpos, IList<SimpleValue> wpds, IList<SimpleValue> colas) {
            _states = states;
            _genders = genders;
            _planTypes = planTypes;
            _optionCodes = optionCodes;
            _yearToAgeOptions = yearToAgeOptions;
            _substandardRates = substandardRates;
            _gpos = gpos;
            _wpds = wpds;
            _colas = colas;
            return new IndividualData {
                Genders = new ObservableCollection<Gender>(_genders),
                PlanTypes = new ObservableCollection<SimpleValue>(_planTypes),
                TableRatings = new ObservableCollection<SimpleValue>(_substandardRates),
                ColaPercents = new ObservableCollection<SimpleValue>(_colas),
                YearsToAges = new ObservableCollection<SimpleValue>(_yearToAgeOptions),
                FirstName = GetElementValue<string>(element.Element(firstNameName)),
                LastName = GetElementValue<string>(element.Element(lastNameName)),
                MiddleInitial = GetElementValue<string>(element.Element(middleInitialName)),
                InitialDeathBenefit = GetElementValue<double>(element.Element(initialDeathBenefitName)),
                InterestRate = GetElementValue<double>(element.Element(interestRateName)),
                FaceAmount = GetElementValue<double>(element.Element(deathBenefitAmountName)),
                AgeOrYear = GetElementValue<int>(element.Element(termYearsName)),
                Age = GetElementValue<int>(element.Element(ageName)),
                BirthDate = GetElementValue<DateTime>(element.Element(ageName)),
                BenefitOption = GetElementValue<SimpleValue>(element.Element(optionCodeName)),
                PlanType = GetElementValue<SimpleValue>(element.Element(planTypeName)),
                YearToAgeOption = GetElementValue<SimpleValue>(element.Element(yearToAgeOptionName)),
                SubstandardRate = GetElementValue<SimpleValue>(element.Element(subStandardRateName)),
                Gpo = GetElementValue<SimpleValue>(element.Element(gpoName)),
                Wpd = GetElementValue<SimpleValue>(element.Element(wpdName)),
                Cola = GetElementValue<SimpleValue>(element.Element(colaName)),
                Gender = GetElementValue<Gender>(element.Element(genderName)),
                State = GetElementValue<State>(element.Element(stateName)),
                Name = GetElementValue<string>(element.Element(nameName)),
                IndividualType = GetElementValue<KnownIndices>(element.Element(individualTypeName)),
                Index = index,
                EndYear = GetElementValue<int?>(element.Element(removeYearsName)),
                RemoveYearToAge = GetElementValue<SimpleValue>(element.Element(removeYearsToAgeName)),
                IsSelected = true
            };
        }

        public virtual XElement ToXElement(string name) {
            return new XElement(name,
                new XElement(firstNameName, FirstName ?? string.Empty),
                new XElement(lastNameName, LastName ?? string.Empty),
                new XElement(middleInitialName, MiddleInitial ?? string.Empty),
                new XElement(ageName, Age),
                new XElement(birthDateName, BirthDate.HasValue ? BirthDate.Value.ToString() : string.Empty),
                new XElement(stateName, State == null ? 0 : State.Id),
                new XElement(genderName, Gender == null ? 0 : Gender.Id),
                new XElement(planTypeName, PlanType == null ? 0 : PlanType.Id),
                new XElement(initialDeathBenefitName, InitialDeathBenefit ?? 0),
                new XElement(interestRateName, InterestRate ?? 0),
                new XElement(optionCodeName, BenefitOption == null ? 0 : BenefitOption.Id),
                new XElement(subStandardRateName, SubstandardRate == null ? 0 : SubstandardRate.Id),
                new XElement(gpoName, Gpo == null ? 0 : Gpo.Id),
                new XElement(wpdName, Wpd),
                new XElement(colaName, Cola == null ? 0 : Cola.Id),
                new XElement(deathBenefitAmountName, FaceAmount ?? 0),
                new XElement(termYearsName, AgeOrYear ?? 0),
                new XElement(yearToAgeOptionName, YearToAgeOption == null ? 0 : YearToAgeOption.Id),
                new XElement(nameName, Name),
                new XElement(indexName, Index),
                new XElement(individualTypeName, (int)IndividualType),
                new XElement(removeYearsName, EndYear),
                new XElement(removeYearsToAgeName, RemoveYearToAge == null ? 0 : RemoveYearToAge.Id));
        }

        public object Clone() {
            return (IndividualData)MemberwiseClone();
        }

    }
}
