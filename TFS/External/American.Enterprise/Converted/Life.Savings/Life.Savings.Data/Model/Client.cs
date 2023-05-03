using GregOsborne.Application.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using Life.Savings.Data.Model.Application;

namespace Life.Savings.Data.Model
{
    public class Client : IToXElement
    {
        public Client(IList<Gender> genders, IList<SimpleValue> planTypes, IList<SimpleValue> yearToAgeOptions, IList<SimpleValue> substandardRates, IList<SimpleValue> colas)
        {
            Riders = new List<IIndividualData>();
            var rider = new IndividualData
            {
                Index = 0,
                IndividualType = IndividualData.KnownIndices.Spouse,
                Name = "Spouse Rider",
                IsSelected = false,
                Visibility = Visibility.Collapsed,
                Genders = new ObservableCollection<Gender>(genders),
                PlanTypes = new ObservableCollection<SimpleValue>(planTypes),
                TableRatings = new ObservableCollection<SimpleValue>(substandardRates),
                ColaPercents = new ObservableCollection<SimpleValue>(colas),
                YearsToAges = new ObservableCollection<SimpleValue>(yearToAgeOptions),
            };
            Riders.Add(rider);

            for (int i = 1; i <= 9; i++)
            {
                rider = new IndividualData
                {
                    Index = i,
                    IndividualType = IndividualData.KnownIndices.Rider,
                    Name = $"Insured Rider #{i}",
                    IsSelected = false,
                    Visibility = Visibility.Collapsed,
                    Genders = new ObservableCollection<Gender>(genders),
                    PlanTypes = new ObservableCollection<SimpleValue>(planTypes),
                    TableRatings = new ObservableCollection<SimpleValue>(substandardRates),
                    ColaPercents = new ObservableCollection<SimpleValue>(colas),
                    YearsToAges = new ObservableCollection<SimpleValue>(yearToAgeOptions),
                };
                Riders.Add(rider);
            }

            FutureSpecificDeathBenefits = new List<FutureAgeValue>();
            FutureModalPremiums = new List<FutureAgeValue>();
            FutureCurrentInterestRates = new List<FutureAgeValue>();
            FutureDeathBenefitOptions = new List<FutureAgeValue>();
            FutureWithdrawls = new List<FutureAgeValueWithEndAge>();
            FutureAnnualPolicyLoans = new List<FutureAgeValueWithEndAge>();
            FutureAnnualLoanRepayments = new List<FutureAgeValueWithEndAge>();
            IsFutureWD = false;
        }

        internal const string primaryName = "primary";
        internal const string spouseName = "spouse";
        internal const string childName = "child";
        internal const string ridersName = "riders";
        internal const string riderName = "rider";
        internal const string futureName = "future";
        internal const string futureValueName = "futurevalue";
        internal const string ageName = "age";
        internal const string valueName = "value";
        internal const string endAgeName = "endage";

        internal const string futureSpecificDeathBenefitsName = "future_sdb";
        internal const string futureModalPremiumsName = "future_mp";
        internal const string futureCurrentInterestRatesName = "future_cir";
        internal const string futureDeathBenefitOptionsName = "future_dbo";
        internal const string futureWithdrawlsName = "future_w";
        internal const string futureAnnualPolicyLoansName = "future_apl";
        internal const string futureAnnualLoanRepaymentsName = "future_alr";

        private const string clientName = "client";
        private const string idName = "id";
        private const string plannedPremiumModeName = "premium_plannedmodalpremium";        
        private const string initialPremiumLumpSumAmountName = "premium_initiallumpsumamount";
        private const string isChildRiderSelectedName = "child_isSelected";
        private const string childRiderYoungestAgeName = "child_youngestage";
        private const string childRiderBenefitAmountName = "child_benefitamount";
        private const string isSpouseInsuredName = "isspouseprincipalinsured";

        public int Id { get; set; }
        public IIndividualData ClientData { get; set; }
        public IIndividualData SpouseData { get; set; }
        public IList<IIndividualData> Riders { get; private set; }
        public string Note { get; set; }
        public double? PremiumPlannedModalPremium { get; set; }
        public bool IsSpousePrincipalInsured { get; set; }
        public double? PremiumInitialLumpSumAmount { get; set; }
        public bool IsChildRiderSelected { get; set; }
        public double? ChildRiderDeathBenefitAmount { get; set; }
        public int? ChildRiderYoungestAge { get; set; }
        public IList<FutureAgeValue> FutureSpecificDeathBenefits { get; set; }
        public IList<FutureAgeValue> FutureModalPremiums { get; set; }
        public IList<FutureAgeValue> FutureCurrentInterestRates { get; set; }
        public IList<FutureAgeValue> FutureDeathBenefitOptions { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureWithdrawls { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureAnnualPolicyLoans { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureAnnualLoanRepayments { get; set; }
        public bool IsFutureWD { get; set; }

        public string FullName {
            get {
                if (!string.IsNullOrEmpty(ClientData.FirstName) && !string.IsNullOrEmpty(ClientData.LastName))
                    return $"{ClientData.FirstName} {ClientData.LastName}";
                else if (!string.IsNullOrEmpty(ClientData.FirstName))
                    return $"{ClientData.FirstName}";
                else
                    return $"{ClientData.LastName}";
            }
        }

        public string IdentityInfo {
            get {
                var first = ClientData.Age > 0 ? ClientData.Age.ToString() : ClientData.BirthDate?.Date.ToString("MM/dd/yyyy") ?? string.Empty;
                return string.IsNullOrEmpty(first) ? $" {ClientData.State.Name}" : $" ({first}) {ClientData.State.Name}";
            }
        }

        public void Clear(Gender defaultGender)
        {
            ClientData = new IndividualData();
            SpouseData = new IndividualData();
            PremiumPlannedModalPremium = 0;
            PremiumInitialLumpSumAmount = 0;
            ClientData.Gender = defaultGender;
            Riders.Clear();
            FutureSpecificDeathBenefits.Clear();
            FutureModalPremiums.Clear();
            FutureCurrentInterestRates.Clear();
            FutureDeathBenefitOptions.Clear();
            FutureWithdrawls.Clear();
            FutureAnnualPolicyLoans.Clear();
            FutureAnnualLoanRepayments.Clear();
            IsFutureWD = false;
        }

        public static Client FromXElement(XElement element, IList<State> states, IList<Gender> genders, IList<SimpleValue> planTypes, IList<SimpleValue> optionCodes, IList<SimpleValue> yearToAgeOptions, IList<SimpleValue> substandardRates, IList<SimpleValue> gpos, IList<SimpleValue> wpds, IList<SimpleValue> colas)
        {
            var result = new Client(genders, planTypes, yearToAgeOptions, substandardRates, colas)
            {
                Id = IndividualData.GetElementValue<int>(element.Element(idName)),
                ClientData = IndividualData.FromXElement(element.Element(primaryName), 0, states, genders, planTypes, optionCodes, yearToAgeOptions, substandardRates, gpos, wpds, colas),                
                IsSpousePrincipalInsured = IndividualData.GetElementValue<bool>(element.Element(isSpouseInsuredName)),
                PremiumPlannedModalPremium = IndividualData.GetElementValue<double>(element.Element(plannedPremiumModeName)),
                PremiumInitialLumpSumAmount = IndividualData.GetElementValue<double>(element.Element(initialPremiumLumpSumAmountName)),
                IsChildRiderSelected = IndividualData.GetElementValue<bool>(element.Element(isChildRiderSelectedName)),
                ChildRiderYoungestAge = IndividualData.GetElementValue<int>(element.Element(childRiderYoungestAgeName)),
                ChildRiderDeathBenefitAmount = IndividualData.GetElementValue<double>(element.Element(childRiderBenefitAmountName))
            };
            if (element.Element(spouseName) != null)
                result.SpouseData = IndividualData.FromXElement(element.Element(spouseName), 0, states, genders, planTypes, optionCodes, yearToAgeOptions, substandardRates, gpos, wpds, colas);
            if (element.Element(ridersName) != null)
            {
                var parent = element.Element(ridersName);
                int index = -1;
                parent.Elements().ToList().ForEach(x => {
                    index++;
                    result.Riders.RemoveAt(index);
                    result.Riders.Insert(index, IndividualData.FromXElement(x, index, states, genders, planTypes, optionCodes, yearToAgeOptions, substandardRates, gpos, wpds, colas));
                });
            }
            if (element.Element(futureName) != null)
            {
                var parent = element.Element(futureName);
                var futureNames = new List<string>
                {
                    futureSpecificDeathBenefitsName,
                    futureModalPremiumsName,
                    futureCurrentInterestRatesName,
                    futureDeathBenefitOptionsName,
                    futureWithdrawlsName,
                    futureAnnualPolicyLoansName,
                    futureAnnualLoanRepaymentsName
                };
                foreach (var item in futureNames)
                {
                    if (parent.Element(item) != null)
                    {
                        var subParent = parent.Element(item);
                        foreach (var elem in subParent.Elements())
                        {
                            if (elem.Name.LocalName.Equals(futureValueName))
                            {
                                FutureAgeValue val = null;
                                if (item.Equals(futureWithdrawlsName) || item.Equals(futureAnnualPolicyLoansName) || item.Equals(futureAnnualLoanRepaymentsName))
                                {
                                    val = new FutureAgeValueWithEndAge
                                    {
                                        Age = int.Parse(elem.Element(ageName).Value),
                                        Value = double.Parse(elem.Element(valueName).Value),
                                        EndAge = int.Parse(elem.Element(endAgeName).Value)
                                    };
                                }
                                else
                                {
                                    val = new FutureAgeValue
                                    {
                                        Age = int.Parse(elem.Element(ageName).Value),
                                        Value = double.Parse(elem.Element(valueName).Value)
                                    };
                                }
                                switch (item)
                                {
                                    case futureSpecificDeathBenefitsName:
                                        result.FutureSpecificDeathBenefits.Add(val);
                                        break;
                                    case futureModalPremiumsName:
                                        result.FutureModalPremiums.Add(val);
                                        break;
                                    case futureCurrentInterestRatesName:
                                        result.FutureCurrentInterestRates.Add(val);
                                        break;
                                    case futureDeathBenefitOptionsName:
                                        result.FutureDeathBenefitOptions.Add(val);
                                        break;
                                    case futureWithdrawlsName:
                                        result.FutureWithdrawls.Add(val.As<FutureAgeValueWithEndAge>());
                                        break;
                                    case futureAnnualPolicyLoansName:
                                        result.FutureAnnualPolicyLoans.Add(val.As<FutureAgeValueWithEndAge>());
                                        break;
                                    case futureAnnualLoanRepaymentsName:
                                        result.FutureAnnualLoanRepayments.Add(val.As<FutureAgeValueWithEndAge>());
                                        break;
                                }
                            }
                        }
                    }
                }
                result.IsFutureWD = result.FutureWithdrawls.Any(x => x.Age > 0 && x.EndAge > 0 && x.Value > 0);
            }
            return result;
        }
        private static string FormatPhone(string original)
        {
            if (string.IsNullOrEmpty(original)) return string.Empty;
            switch (original.Length)
            {
                case 7:
                    return $"{original.Substring(0, 3)}-{original.Substring(3)}";
                case 10:
                    return $"({original.Substring(0, 3)}) {original.Substring(3, 3)}-{original.Substring(6)}";
                default:
                    return original;
            }
        }
        public XElement ToXElement()
        {
            var result = new XElement(clientName,
                new XElement(idName, Id.ToString()),
                new XElement(plannedPremiumModeName, PremiumPlannedModalPremium ?? 0),
                new XElement(isSpouseInsuredName, IsSpousePrincipalInsured),
                new XElement(initialPremiumLumpSumAmountName, PremiumInitialLumpSumAmount ?? 0),
                new XElement(isChildRiderSelectedName, IsChildRiderSelected),
                new XElement(childRiderYoungestAgeName, ChildRiderYoungestAge ?? 0),
                new XElement(childRiderBenefitAmountName, ChildRiderDeathBenefitAmount ?? 0));
            result.Add(ClientData.As<IToNamedXElement>().ToXElement(primaryName));
            if (SpouseData != null)
                result.Add(SpouseData.As<IToNamedXElement>().ToXElement(spouseName));
            var ridersElement = new XElement(ridersName);
            if (Riders.Any())
            {
                int index = 0;
                Riders.Where(x => x.IsSelected && x.Age > 0).ToList().ForEach(x => {
                    index++;
                    ridersElement.Add(x.As<IToNamedXElement>().ToXElement(riderName));
                });
            }
            result.Add(ridersElement);
            var futuresElement = new XElement(futureName);
            var futureNames = new List<string>
            {
                futureSpecificDeathBenefitsName,
                futureModalPremiumsName,
                futureCurrentInterestRatesName,
                futureDeathBenefitOptionsName,
                futureWithdrawlsName,
                futureAnnualPolicyLoansName,
                futureAnnualLoanRepaymentsName
            };
            foreach (var item in futureNames)
            {
                var futureElement = new XElement(item);
                switch (item)
                {
                    case futureSpecificDeathBenefitsName:
                        FutureSpecificDeathBenefits.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value)));
                            }
                        });
                        break;
                    case futureModalPremiumsName:
                        FutureModalPremiums.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value)));
                            }
                        });
                        break;
                    case futureCurrentInterestRatesName:
                        FutureCurrentInterestRates.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value)));
                            }
                        });
                        break;
                    case futureDeathBenefitOptionsName:
                        FutureDeathBenefitOptions.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value)));
                            }
                        });
                        break;
                    case futureWithdrawlsName:
                        FutureWithdrawls.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0 && x.EndAge > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value),
                                    new XElement(endAgeName, x.EndAge)));
                            }
                        });
                        break;
                    case futureAnnualPolicyLoansName:
                        FutureAnnualPolicyLoans.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0 && x.EndAge > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value),
                                    new XElement(endAgeName, x.EndAge)));
                            }
                        });
                        break;
                    case futureAnnualLoanRepaymentsName:
                        FutureAnnualLoanRepayments.ToList().ForEach(x => {
                            if (x.Age > 0 && x.Value > 0 && x.EndAge > 0)
                            {
                                futureElement.Add(new XElement(futureValueName,
                                    new XElement(ageName, x.Age),
                                    new XElement(valueName, x.Value),
                                    new XElement(endAgeName, x.EndAge)));
                            }
                        });
                        break;
                }
                futuresElement.Add(futureElement);
            }
            result.Add(futuresElement);
            return result;
        }
    }
}
