using System;
using System.Collections.Generic;
using System.Linq;
using GregOsborne.Application.Primitives;

namespace Life.Savings.Data.Model.Application {

    public enum RateTypes { Guaranteed = 0, Projected = 1 }

    public interface ICalculations {

        CalculatedPremiums BeginCalculations(IllustrationInfo info, IRepository repo, IAppData dataSet, int startAge, out string errorMessage);

        int GetBandIndex(double? faceAmount);

        void GetFullLedger(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, double modalPremium, double clientCola, double spouseCola, int deathBenefitOption, double opt2StartCashValue, out string errorMessage);

        void GetGuidelineAnnualPremium(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false);

        void GetGuidelineSinglePremium(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false);

        void GetMinimumPremiums(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false);

        IList<MortalityItem> GetMortalityRates(ref CalculationValues calcValues, out string errorMessage, bool isSpouse = false);

        void GetTargetPremiums(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false);

        void PremiumToObtainCashValue(double cashValue, int cashValueAge, ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, double modalPremium, double clientCola, double spouseCoal, int deathBenefitOption, double opt2StartCashValue, out string errorMessage, bool isSpouse = false);

        bool ValidatePremiumMode(SimpleValue mode, int annMo);
    }

    public class Calculations : ICalculations {
        private const double additionalValue = 0.005;
        private const double baseMutiplier = 1.081081;

        private void AddCola(CalculatingOnItems calcOnItem, IIndividualData clientOrSpouse, int annYr, IllustrationInfo info, int riderIndex, IIndividualData rider, YearlyData md) {
            var tempAge = 0;
            var oneYearCola = 0d;

            switch (calcOnItem)
            {
                case CalculatingOnItems.Insured:
                    tempAge = clientOrSpouse.Age - annYr + info.Riders[riderIndex].Age;
                    if (info.Riders[riderIndex].FaceAmount.Value < 200000d && info.Riders[riderIndex].Cola.Value < info.Riders[riderIndex].FaceAmount.Value * 2d && tempAge < 64)
                    {
                        oneYearCola = (info.Riders[riderIndex].FaceAmount.Value + info.Riders[riderIndex].Cola.Value) * (info.Riders[riderIndex].Cola.Value / 100d);
                        if (oneYearCola > info.Riders[riderIndex].FaceAmount.Value * 0.2)
                            oneYearCola = rider == null && clientOrSpouse.FaceAmount.HasValue ? clientOrSpouse.FaceAmount.Value : rider.FaceAmount.Value * 0.2;
                        if (oneYearCola > 25000d)
                            oneYearCola = 25000d;
                        md.SpouseCola = info.Riders[riderIndex].Cola.Value + oneYearCola;
                        info.Riders[riderIndex].Cola.Value = info.Riders[riderIndex].FaceAmount.Value + oneYearCola;
                    }
                    if (info.Riders[riderIndex].FaceAmount.Value > 200000d)
                        info.Riders[riderIndex].Cola.Value = 200000d;
                    if (info.Riders[riderIndex].Cola.Value > info.Riders[riderIndex].FaceAmount.Value * 2d)
                        info.Riders[riderIndex].Cola.Value = info.Riders[riderIndex].FaceAmount.Value * 2d;
                    break;

                case CalculatingOnItems.Principal:
                    if (clientOrSpouse.Cola.Value < 200000d && clientOrSpouse.Cola.Value < clientOrSpouse.FaceAmount * 2d && annYr < 64)
                    {
                        oneYearCola = (md.FaceValue + clientOrSpouse.Cola.Value) * (clientOrSpouse.Cola.Value / 100d);
                        if (oneYearCola > (md.FaceValue + clientOrSpouse.Cola.Value) * 0.2)
                            oneYearCola = (md.FaceValue + clientOrSpouse.Cola.Value) * 0.2;
                        if (oneYearCola > 25000d)
                            oneYearCola = 25000d;
                        clientOrSpouse.Cola.Value = clientOrSpouse.Cola.Value + oneYearCola;
                    }
                    if (clientOrSpouse.Cola.Value > 200000d)
                        clientOrSpouse.Cola.Value = 200000d;
                    if (clientOrSpouse.Cola.Value > (clientOrSpouse.FaceAmount.Value * 2d))
                        clientOrSpouse.Cola.Value = clientOrSpouse.FaceAmount.Value * 2d;
                    break;

                case CalculatingOnItems.Spouse:
                    if (rider != null)
                    {
                        tempAge = clientOrSpouse.Age - annYr + (rider != null ? rider.Age : clientOrSpouse.Age);
                        if (rider.Cola.Value < 200000 && rider.Cola.Value < rider.FaceAmount.Value * 2d && tempAge < 64)
                        {
                            oneYearCola = (rider.FaceAmount.Value + md.SpouseCola) * (rider.Cola.Value / 100d);
                            if (oneYearCola > rider.FaceAmount.Value * 0.2)
                                oneYearCola = rider.FaceAmount.Value * 0.2;
                            if (oneYearCola > 25000d)
                                oneYearCola = 25000d;
                            rider.Cola.Value = rider.Cola.Value + oneYearCola;
                        }
                        if (rider.Cola.Value > 200000d)
                            rider.Cola.Value = 200000d;
                        if (rider.Cola.Value > rider.FaceAmount.Value * 2d)
                            rider.Cola.Value = rider.FaceAmount.Value * 2d;
                    }
                    break;

                case CalculatingOnItems.Additional:
                    break;
            }
        }

        private void CalculateBackwards(ref CalculationValues calcValues, IIndividualData clientOrSpouse, ref double cashValue, ref double additionalValue, double currentPremium) {
            var costOfLiving = 0d;
            cashValue = clientOrSpouse.FaceAmount.Value * 0.98;
            var deathBenefit = clientOrSpouse.FaceAmount.Value;
            var waiverPercentage = 0d;

            var interestRate = System.Math.Pow(1.04, (1d / 12d));
            var charges = clientOrSpouse.FaceAmount.Value < 100000d ? 3.5 : 3.0;
            var loanBalance = 0d;
            var validCorr = false;
            var annYr = 0;
            var returnRate = 0d;
            var tempPremium = 0d;

            for (annYr = 94; annYr > clientOrSpouse.Age; annYr--)
            {
                validCorr = LsCorr.Verify(calcValues.DataSet.LsCorrItems, annYr, out returnRate);
                for (int annMo = 1; annMo <= 12; annMo++)
                {
                    tempPremium = annMo < 12 ? 0d : annYr > clientOrSpouse.Age + 9 ? currentPremium * 0.975 : currentPremium * 0.925;
                    if (validCorr && deathBenefit < (cashValue * returnRate))
                        deathBenefit = cashValue * returnRate;
                    calcValues.MortalityRate = CalculatePrincipalMortality(calcValues.ClientRates, clientOrSpouse, deathBenefit, false, annYr, costOfLiving, loanBalance);
                    ReverseCashValue(ref calcValues, false, clientOrSpouse.Age, clientOrSpouse.HasSubstandardRate, waiverPercentage, tempPremium, false, clientOrSpouse.FaceAmount.Value, costOfLiving, charges, interestRate, ref cashValue, ref additionalValue);
                }
            }
            interestRate = System.Math.Pow(1 + (clientOrSpouse.InterestRate.Value / 100d), (1d / 12d));
            annYr = clientOrSpouse.Age;
            validCorr = LsCorr.Verify(calcValues.DataSet.LsCorrItems, annYr, out returnRate);
            for (int annMo = 1; annMo <= 12; annMo++)
            {
                tempPremium = annMo < 12 ? 0d : currentPremium * 0.925;
                if (validCorr && deathBenefit < (cashValue * returnRate))
                    deathBenefit = cashValue * returnRate;
                calcValues.MortalityRate = CalculatePrincipalMortality(calcValues.ClientRates, clientOrSpouse, deathBenefit, false, annYr, costOfLiving, loanBalance);
                ReverseCashValue(ref calcValues, false, clientOrSpouse.Age, clientOrSpouse.HasSubstandardRate, waiverPercentage, tempPremium, false, clientOrSpouse.FaceAmount.Value, costOfLiving, charges, interestRate, ref cashValue, ref additionalValue);
            }
        }

        private void CalculateInsuredRiderMortality(ref CalculationValues calcValues, bool isGuideSingle, IIndividualData rider, bool isGuaranteed, int primaryAge, int annYr) {
            var tempMortItems = new List<double> { 0d, 0d };

            var insuredAge = (annYr - primaryAge) + rider.Age;
            var Ledger_Year = insuredAge - rider.Age + 1;
            if (Ledger_Year >= rider.Age && Ledger_Year <= rider.EndYear.Value)
            {
                if (isGuideSingle)
                {
                    var mortItem0 = calcValues.ClientRates.First(x => x.IsGuaranteed && x.Year == annYr);
                    var mortItem1 = calcValues.ClientRates.First(x => !x.IsGuaranteed && x.Year == annYr);
                    tempMortItems[0] = mortItem0.InsuredCs80[rider.Index] < mortItem0.InsuredBase[rider.Index]
                        ? mortItem0.InsuredCs80[rider.Index]
                        : mortItem0.InsuredBase[rider.Index];
                    tempMortItems[1] = mortItem1.InsuredCs80[rider.Index] < mortItem1.InsuredBase[rider.Index]
                        ? mortItem1.InsuredCs80[rider.Index]
                        : mortItem1.InsuredBase[rider.Index];
                }
            }
            if (rider.Age == Ledger_Year)
            {
                calcValues.InsuredMortCosts += ((rider.FaceAmount.Value * tempMortItems[0]) / 1000d);
                if (rider.HasSubstandardRate)
                    calcValues.InsuredMortCosts += ((rider.FaceAmount.Value * calcValues.ClientRates.First(x => !x.IsGuaranteed && x.Year == insuredAge).InsuredSub[rider.Index]) / 1000d);
            }
            else
            {
                calcValues.InsuredMortCosts += ((rider.FaceAmount.Value * tempMortItems[isGuaranteed ? 0 : 1]) / 1000d);
                if (rider.HasSubstandardRate)
                    calcValues.InsuredMortCosts += ((rider.FaceAmount.Value * calcValues.ClientRates.First(x => x.IsGuaranteed == isGuaranteed && x.Year == insuredAge).InsuredSub[rider.Index]) / 1000d);
            }
        }

        private double CalculatePrincipalMortality(IList<MortalityItem> mortalityItems, IIndividualData data, double faceAmount, bool isGuaranteedItem, int annYr, double costOfLiving, double loanBalance) {
            var band = 0;
            var bandFaceAmount = faceAmount + costOfLiving + loanBalance;
            if (bandFaceAmount.IsBetween(50000d, 99999d, true))
                band = 1;
            else if (bandFaceAmount.IsBetween(100000d, 249999d, true))
                band = 2;
            else if (bandFaceAmount >= 250000d)
                band = 3;
            var result = 0d;
            var items = mortalityItems.Where(x => x.Year == mortalityItems.Max(y => y.Year)).ToList();
            var mortItem = mortalityItems.FirstOrDefault(x => x.IsGuaranteed == isGuaranteedItem && x.Year == annYr);
            if (mortItem != null)
            {
                result = mortItem.PrincipalBase[band];
                if (data.HasSubstandardRate)
                    result += mortItem.PrincipalSub[band];
            }
            else
                throw new ApplicationException($"Cannot find mortality item for band {band}.");
            return result;
        }

        private void CalculateSpouseRiderMortality(ref CalculationValues calcValues, bool isGuideSingle, IIndividualData rider, bool isGuaranteed, int primaryAge, int annYr) {
            var tempMortItems = new List<double>();
            var spouseItems = calcValues.DataSet.LsSpouseItems;
            var mortalityItems = calcValues.ClientRates;

            var adjustFace = rider.FaceAmount.Value;
            var spouseAge = (annYr - primaryAge) + rider.Age;
            if (spouseAge > 94 || (rider.EndYear.HasValue && (spouseAge >= rider.Age + rider.EndYear.Value))) return;
            if (spouseAge > 64)
            {
                if (LsSpouse.Verify(spouseItems, spouseAge, out var ReturnRate))
                    adjustFace = adjustFace * ReturnRate;
            }
            var mortItem0 = mortalityItems.First(x => x.IsGuaranteed && x.Year == annYr);
            var mortItem1 = mortalityItems.First(x => !x.IsGuaranteed && x.Year == annYr);
            tempMortItems.Add(!isGuideSingle || (mortItem0.SpouseCs80 < mortItem0.SpouseBase) ? mortItem0.SpouseCs80 : mortItem0.SpouseBase);
            tempMortItems.Add(!isGuideSingle || (mortItem1.SpouseCs80 < mortItem1.SpouseBase) ? mortItem1.SpouseCs80 : mortItem1.SpouseBase);
            if (spouseAge == rider.Age)
            {
                calcValues.InsuredMortCosts += ((adjustFace * tempMortItems[0]) / 1000d);
                if (rider.HasSubstandardRate)
                    calcValues.InsuredMortCosts += ((adjustFace * mortalityItems.First(x => !x.IsGuaranteed && x.Year == spouseAge).SpouseSub) / 1000d);
            }
            else
            {
                calcValues.InsuredMortCosts += ((adjustFace * tempMortItems[isGuaranteed ? 0 : 1]) / 1000d);
                if (rider.HasSubstandardRate)
                    calcValues.InsuredMortCosts += ((adjustFace * mortalityItems.First(x => x.IsGuaranteed == isGuaranteed && x.Year == spouseAge).SpouseSub) / 1000d);
            }
        }

        private void CheckAnnivarsary(IIndividualData clientOrSpouse, IList<Gender> genders, int annYr, IList<LsSurr> surrItems, YearlyData md) {
            var gender = GetGender(clientOrSpouse, genders, clientOrSpouse.State);
            var duration = 0;
            duration = annYr - clientOrSpouse.Age + 1;
            if (LsSurr.Verify(surrItems, CalculatingOnItems.Principal, gender, clientOrSpouse.Age, duration, out var surrenderCharge))
            {
                surrenderCharge *= (clientOrSpouse.FaceAmount.Value / 1000d);
                md.CashValue -= surrenderCharge;
            }
            if (md.CashValue < 0d)
                md.CashValue = 0d;
        }

        private void CheckFutureChanges(IIndividualData clientOrSpouse, IllustrationInfo info, int annYr, YearlyData md, ref double minimumPremium) {
            var isDeathBenefitChange = false;
            var isPremiumChange = false;
            var isInterestRateChange = false;
            var isOptionChange = false;

            for (int i = 0; i < 5; i++)
            {
                isDeathBenefitChange = false;
                isPremiumChange = false;
                isInterestRateChange = false;
                isOptionChange = false;

                if (info.FutureSpecificDeathBenefits[i].Age == annYr + 1)
                {
                    md.FaceValue = info.FutureSpecificDeathBenefits[i].Value.Value;
                    minimumPremium = info.FutureSpecificDeathBenefits[i].Value.Value;
                    isDeathBenefitChange = true;
                }
                if (info.FutureModalPremiums[i].Age == annYr + 1)
                {
                    md.ModalPremium = info.FutureModalPremiums[i].Value.Value;
                    isPremiumChange = true;
                }
                if (info.FutureCurrentInterestRates[i].Age == annYr + 1)
                {
                    md.InterestRate = info.FutureCurrentInterestRates[i].Value.Value / 100d;
                    isInterestRateChange = true;
                }
                if (info.FutureDeathBenefitOptions[i].Age == annYr + 1)
                {
                    if (info.FutureDeathBenefitOptions[i].Value.Value == 1d && md.DeathBenefitOption == 2)
                    {
                        md.FaceValue += md.CashValue - md.StartCashValue;
                        minimumPremium += md.CashValue - md.StartCashValue;
                        md.StartCashValue = 0d;
                    }
                    if (info.FutureDeathBenefitOptions[i].Value.Value == 2d && md.DeathBenefitOption == 1)
                        md.StartCashValue = md.CashValue;

                    isOptionChange = true;
                }
                if (isDeathBenefitChange && isPremiumChange && isInterestRateChange && isOptionChange)
                    return;
            }
        }

        private WithdrawlTotal CheckFutureWithdrawls(int currentYear, IList<FutureWithdrawls> withdrawls, ref CalculatedPremiums calcPremiums) {
            var withdrawl = 0d;
            var loanAmt = 0d;
            var loanPay = 0d;
            var result = new WithdrawlTotal();

            for (int i = 0; i < 5; i++)
            {
                if (!withdrawls[i].IsValid(currentYear))
                    break;
                withdrawl = withdrawl + withdrawls[i].WD_Amount;
                loanAmt = loanAmt + withdrawls[i].Loan_Amount;
                loanPay = loanPay + withdrawls[i].LoanPay_Amount;
            }
            calcPremiums.LedgerItems[currentYear + 1].WithdrawAmount = withdrawl;
            calcPremiums.LedgerItems[currentYear + 1].LoanAmount = loanAmt;
            calcPremiums.LedgerItems[currentYear + 1].LoanRepayAmount = loanPay;
            calcPremiums.LedgerItems[currentYear + 1].LoanBalance = calcPremiums.LedgerItems[currentYear].LoanBalance + loanAmt - loanPay;
            result.LoanBalance = calcPremiums.LedgerItems[currentYear].LoanBalance;
            if (calcPremiums.LedgerItems[currentYear + 1].LoanBalance < 0)
            {
                loanPay += calcPremiums.LedgerItems[currentYear + 1].LoanBalance;
                calcPremiums.LedgerItems[currentYear + 1].LoanRepayAmount = loanPay;
                calcPremiums.LedgerItems[currentYear + 1].LoanBalance = 0;
                result.LoanBalance = 0;
            }
            result.CashValue -= withdrawl - loanAmt + loanPay;
            result.FaceValue -= withdrawl - loanAmt + loanPay;
            result.DeathBenefit -= withdrawl - loanAmt + loanPay;
            result.MinimumDeathBenefit -= withdrawl - loanAmt + loanPay;
            if (withdrawl > 0)
            {
                result.CashValue -= 25d;
                result.FaceValue -= 25d;
                result.DeathBenefit -= 25d;
                result.MinimumDeathBenefit -= 25d;
            }
            return result;
        }

        private void GetChildRiderMortalityCost(int annYr, IIndividualData clientOrSpouse, IllustrationInfo info, bool isSpouse, YearlyData md) {
            if (info.IsChildRiderSelected && (annYr - clientOrSpouse.Age) + info.ChildRiderYoungestAge < 23)
                md.InsuredMortalityCosts += (info.ChildRiderDeathBenefitAmount / 1000d) * (isSpouse || (annYr - clientOrSpouse.Age) == 0 ? 0.4 : 0.5);
        }

        private Gender GetGender(IIndividualData indData, IList<Gender> genders, State principalState) {
            return principalState.Abbreviation.Equals("mt", StringComparison.OrdinalIgnoreCase)
                ? genders.FirstOrDefault(x => x.Abbreviation.Equals('M'))
                : indData.Gender;
        }

        private void GetInsuredRiderMortalityCost(bool isGuideSingle, int annYr, IList<MortalityItem> mortalityItems, IIndividualData clientOrSpouse, IIndividualData rider, YearlyData md) {
            var insuredAge = 0;
            var ledgerYear = 0d;
            var tempMort = new double[] { 0, 0 };

            if (!rider.AgeOrYear.HasValue || !rider.EndYear.HasValue)
                return;
            insuredAge = annYr - clientOrSpouse.Age + rider.Age;
            ledgerYear = insuredAge - rider.Age + 1;
            if (ledgerYear >= rider.AgeOrYear.Value && ledgerYear <= rider.EndYear.Value)
            {
                var mortItem0 = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == insuredAge);
                var mortItem1 = mortalityItems.FirstOrDefault(x => !x.IsGuaranteed && x.Year == insuredAge);
                if (isGuideSingle)
                {
                    if (mortItem0 != null && mortItem1 != null)
                    {
                        tempMort[0] = mortItem0.InsuredCs80[rider.Index] < mortItem0.InsuredBase[rider.Index]
                            ? mortItem0.InsuredCs80[rider.Index]
                            : mortItem0.InsuredBase[rider.Index];
                        tempMort[1] = mortItem1.InsuredCs80[rider.Index] < mortItem1.InsuredBase[rider.Index]
                            ? mortItem1.InsuredCs80[rider.Index]
                            : mortItem1.InsuredBase[rider.Index];
                    }
                }
                md.InsuredMortalityCosts += ((rider.FaceAmount.Value * tempMort[rider.AgeOrYear == ledgerYear ? 1 : 0]) / 1000d);
                if (rider.HasSubstandardRate)
                    md.InsuredMortalityCosts += (rider.FaceAmount.Value * (rider.AgeOrYear == ledgerYear ? mortItem0 : mortItem1).InsuredSub[rider.Index] / 1000d);
            }
        }

        private void GetModalPremiumValues(IIndividualData clientOrSpouse, IllustrationInfo info, int annYr, int annMo, YearlyData md) {
            md.Charges = clientOrSpouse.FaceAmount < 100000d ? 3.5 : 3.0;
            var yearsToPay = clientOrSpouse.YearToAgeOption.DisplayedValue.Equals("Age") ? clientOrSpouse.EndYear - clientOrSpouse.Age : clientOrSpouse.AgeOrYear;
            if (annYr < clientOrSpouse.Age + yearsToPay)
            {
                md.MonthlyPremium = 0;
                if (ValidatePremiumMode(info.PremiumMode, annMo))
                {
                    md.Outlay += md.ModalPremium;
                    md.MonthlyPremium = md.ModalPremium;
                    md.CashValue += md.ModalPremium;
                    md.Charges += (md.ModalPremium * (annYr - clientOrSpouse.Age < 10 ? 0.075 : 0.025));
                }
            }
            md.CashValue -= md.Charges;
        }

        private double GetMonthlyInterestRate(double interestRate) {
            var tmp = 1 + interestRate;
            return System.Math.Pow(tmp, (1d / 12d));
        }

        private void GetPrincipalMortalityCost(IIndividualData clientOrSpouse, int currentYear, IList<MortalityItem> mortalityItems, YearlyData md) {
            var bandFaceAmount = md.FaceValue + (md.FaceValue * (md.ClientCola / 100d)) + md.LoanBalance;
            var band = GetBandIndex(bandFaceAmount);
            var mortItem = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == currentYear);
            if (mortItem == null)
                return;
            md.InsuredMortalityCosts = mortItem.PrincipalBase[band];
            if (clientOrSpouse.HasSubstandardRate)
                md.InsuredMortalityCosts += mortItem.PrincipalSub[band];
        }

        private void GetPrincipalPremiumCharges(YearlyData md) {
            var tempDeathBenefit = md.FaceValue + (md.FaceValue * md.ClientCola);
            if (md.DeathBenefitOption == 1)
                md.Charges += (((tempDeathBenefit * 0.996737) - md.CashValue) * md.InsuredMortalityCosts) / 1000d;
            else
            {
                if (md.StartCashValue == 0d)
                    md.Charges += ((tempDeathBenefit * 0.996737) * md.InsuredMortalityCosts) / 1000d;
                else
                {
                    if (md.CashValue > md.StartCashValue)
                        tempDeathBenefit = tempDeathBenefit + md.CashValue - md.StartCashValue;
                    md.Charges += (((tempDeathBenefit * 0.996737) - md.CashValue) * md.InsuredMortalityCosts) / 1000d;
                }
            }
        }

        private void GetPrincipalRiderGpoCost(int guarOrProjIndex, int currentYear, IIndividualData clientOrSpouse, IList<MortalityItem> mortalityItems, YearlyData md) {
            var mortItem = mortalityItems.FirstOrDefault(x => x.IsGuaranteed == (guarOrProjIndex == 0) && x.Year == currentYear);
            if (mortItem == null)
                return;
            if (clientOrSpouse.HasGpo && currentYear < 41)
                md.InsuredMortalityCosts += (clientOrSpouse.Gpo.Value / 1000d * mortItem.PrincipalGpo);
        }

        private void GetPrincipalRiderMortalityCost(int guarOrProjIndex, bool guideSingle, int currentYear, IllustrationInfo info, IIndividualData clientOrSpouse, IList<MortalityItem> mortalityItems, YearlyData md) {
            var tempMort = new double[] { 0d, 0d };
            var tempYr = currentYear - clientOrSpouse.Age;

            var mortItem0 = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == currentYear);
            var mortItem1 = mortalityItems.FirstOrDefault(x => !x.IsGuaranteed && x.Year == currentYear);
            if (mortItem0 == null || mortItem1 == null)
                return;
            tempMort[0] = guideSingle || mortItem0.InsuredCs80[0] <= mortItem0.InsuredBase[0] ? mortItem0.InsuredBase[0] : mortItem0.InsuredCs80[0];
            tempMort[1] = guideSingle || mortItem1.InsuredCs80[0] <= mortItem1.InsuredBase[0] ? mortItem1.InsuredBase[0] : mortItem1.InsuredCs80[0];

            if (clientOrSpouse == info.SpouseAsClientData && info.SpouseAsClientData.HasFaceAmount)
            {
                var spousePrin = info.SpouseAsClientData;
                if (spousePrin.AgeOrYear <= tempYr + 1 && spousePrin.EndYear >= tempYr)
                {
                    md.InsuredMortalityCosts += ((spousePrin.FaceAmount.Value / 1000d) * tempMort[(tempYr == 0 ? 1 : guarOrProjIndex)]);
                    if (spousePrin.HasSubstandardRate)
                        md.InsuredMortalityCosts += ((spousePrin.FaceAmount.Value / 1000d) * tempMort[(tempYr == 0 ? 1 : guarOrProjIndex)]);
                }
            }
        }

        private void GetPrincipalRiderWPDCost(IIndividualData clientOrSpouse, int currentYear, IList<MortalityItem> mortalityItems, YearlyData md) {
            if (!clientOrSpouse.HasWpd || currentYear >= 60)
                return;
            var mortItem = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == currentYear);
            if (mortItem == null)
                return;
            md.InsuredMortalityCosts += ((md.Charges + md.InsuredMortalityCosts) * mortItem.PrincipalWpd)
                + ((md.FaceValue + md.ClientCola < 100000d ? 3.5 : 3.0) * mortItem.PrincipalWpd);
        }

        private double GetRiderTargetPremium(IIndividualData rider, IList<MortalityItem> mortalityItems, SimpleValue premiumMode, int index, ref double wpdMort) {
            if (!rider.IsSelected)
                return 0d;
            var mortalityRate = rider.IndividualType == IndividualData.KnownIndices.Spouse
                ? mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age).SpouseBase
                : mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age).InsuredBase[index];
            if (rider.HasSubstandardRate)
                mortalityRate += (rider.IndividualType == IndividualData.KnownIndices.Spouse
                    ? mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age).SpouseSub
                    : mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age).InsuredSub[index]);
            mortalityRate = (mortalityRate * rider.FaceAmount.Value / 1000d) * 12d;
            var result = mortalityRate;
            wpdMort = wpdMort + result;
            result = Modalize(result, premiumMode);
            return result;
        }

        private Gender GetSpouseGender(IIndividualData indData, IRepository repo, Gender principalGender) {
            if (indData.Gender != null)
                return indData.Gender;
            else
                return principalGender == repo.Genders.FirstOrDefault(x => x.Abbreviation.Equals('M'))
                    ? repo.Genders.FirstOrDefault(x => x.Abbreviation.Equals('F'))
                    : repo.Genders.FirstOrDefault(x => x.Abbreviation.Equals('M'));
        }

        private void GetSpouseRiderMortalityCost(bool guideSingle, int annYr, IIndividualData clientOrSpouse, IIndividualData rider, IAppData dataSet, IList<MortalityItem> mortalityItems, YearlyData md) {
            var spouseAdjustFace = 0d;
            var spouseAge = 0;
            var tempMort = new double[] { 0d, 0d };

            spouseAdjustFace = rider.FaceAmount.Value;
            spouseAge = (annYr - clientOrSpouse.Age) + rider.Age;
            if (spouseAge > 94 || spouseAge >= (rider.Age + rider.EndYear))
                return;
            if (spouseAge > 64 && LsSpouse.Verify(dataSet.LsSpouseItems, spouseAge, out var ReturnRate))
                spouseAdjustFace = spouseAdjustFace * ReturnRate;

            var mortItem0 = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == annYr);
            var mortItem1 = mortalityItems.FirstOrDefault(x => !x.IsGuaranteed && x.Year == annYr);
            if (mortItem0 == null || mortItem1 == null)
                return;

            tempMort[0] = !guideSingle
                ? mortItem0.SpouseBase
                : mortItem0.SpouseCs80 <= mortItem0.SpouseBase ? mortItem0.SpouseCs80 : mortItem0.SpouseBase;
            tempMort[1] = !guideSingle
                ? mortItem1.SpouseBase
                : mortItem1.SpouseCs80 <= mortItem1.SpouseBase ? mortItem1.SpouseCs80 : mortItem1.SpouseBase;

            md.InsuredMortalityCosts += ((spouseAdjustFace * tempMort[spouseAge == rider.Age ? 1 : 0]) / 1000d);
            if (rider.HasSubstandardRate)
                md.InsuredMortalityCosts += ((spouseAdjustFace * (spouseAge == rider.Age ? mortItem0 : mortItem1).SpouseSub) / 1000d);
        }

        private IList<FutureWithdrawls> GetWithDrawls(bool hasInterest, IList<FutureAgeValueWithEndAge> withdrawls, IList<FutureAgeValueWithEndAge> loans, IList<FutureAgeValueWithEndAge> repayments) {
            var result = new List<FutureWithdrawls>();
            for (int i = 0; i < 5; i++)
            {
                var w = withdrawls[i];
                var l = loans[i];
                var r = repayments[i];
                if (w.IsValid() && l.IsValid() && r.IsValid())
                {
                    result.Add(new FutureWithdrawls {
                        IsInitialized = true,
                        WD_Age = w.Age.Value,
                        WD_Amount = w.Value.Value,
                        WD_Years = w.EndAge.Value - w.Age.Value,
                        Loan_Age = r.Age.Value,
                        Loan_Amount = r.Value.Value,
                        Loan_Years = r.EndAge.Value - r.Age.Value,
                        LoanPay_Age = l.Age.Value,
                        LoanPay_Amount = l.Value.Value,
                        LoanPay_Years = l.EndAge.Value - l.Age.Value,
                        Loan_Interest = hasInterest
                    });
                }
                else
                    result.Add(new FutureWithdrawls { IsInitialized = false });
            }
            return result;
        }

        private bool HasAdditionalFaceValue(IllustrationInfo info, bool isSpouse) {
            return !isSpouse 
                && info.SpouseAsClientData != null 
                && info.SpouseAsClientData.FaceAmount.HasValue 
                && (info.SpouseAsClientData.FaceAmount.Value > 0d);
        }

        private bool IsSmoker(IIndividualData indData) {
            return !indData.PlanType.DisplayedValue.Equals("non-smoker", StringComparison.OrdinalIgnoreCase);
        }

        private double Modalize(double value, SimpleValue premiumMode) {
            if (premiumMode.DisplayedValue.Equals("semiannual", StringComparison.OrdinalIgnoreCase))
                return value / 2d;
            else if (premiumMode.DisplayedValue.Equals("quarterly", StringComparison.OrdinalIgnoreCase))
                return value / 4d;
            else if (premiumMode.DisplayedValue.Equals("monthly (app)", StringComparison.OrdinalIgnoreCase))
                return value / 12d;
            else
                return value;
        }

        private void ProcessLedgerYears(int guarOrProjIndex, int startAge, int stopAge, ref CalculatedPremiums calcPremiums, bool search, ref CalculationValues calcValues, IIndividualData clientOrSpouse, ref double cashValue, ref double faceValue, ref double minimumDeathBenefitValue, ref double monthlyPremium, ref double modalPremium, ref double clientCola, ref double spouseCola, int deathBenefitOption, double opt2StartCashValue, ref double[] oneYearCosts, ref double[] numeratorNPs, ref double[] numeratorSCs, ref double[] denominators, ref double interestRate) {
            var yearlyData = new YearlyData {
                DeathBenefitOption = deathBenefitOption,
                FaceValue = faceValue,
                MinimumDeathBenefit = minimumDeathBenefitValue,
                DeathBenefit = faceValue,
                MonthlyPremium = monthlyPremium,
                ModalPremium = modalPremium,
                ClientCola = clientCola,
                SpouseCola = spouseCola,
                InterestRate = interestRate,
                StartCashValue = opt2StartCashValue
            };
            var corrRate = 0d;

            for (int year = startAge; year <= stopAge; year++)
            {
                yearlyData.CashValue = cashValue;
                yearlyData.Outlay = 0d;
                yearlyData.DeathBenefit = faceValue;

                if (!LsCorr.Verify(calcValues.DataSet.LsCorrItems, year, out corrRate))
                    corrRate = 0d;
                var withdrawls = GetWithDrawls(false, calcValues.Info.FutureWithdrawls, calcValues.Info.FutureAnnualPolicyLoans, calcValues.Info.FutureAnnualLoanRepayments);
                if (withdrawls.Any(x => x.Loan_Age > 0 && x.LoanPay_Years > 0 && x.Loan_Amount > 0d))
                {
                    var futureWD = CheckFutureWithdrawls(year, withdrawls, ref calcPremiums);
                    if (futureWD.IsDefined)
                    {
                        yearlyData.CashValue += futureWD.CashValue;
                        yearlyData.FaceValue += futureWD.FaceValue;
                        yearlyData.LoanBalance += futureWD.LoanBalance;
                        yearlyData.MinimumDeathBenefit += futureWD.MinimumDeathBenefit;
                        yearlyData.DeathBenefit += futureWD.DeathBenefit;
                    }
                }
                for (int annMo = 1; annMo <= 12; annMo++)
                {
                    GetModalPremiumValues(clientOrSpouse, calcValues.Info, year, annMo, yearlyData);
                    yearlyData.InsuredMortalityCosts = 0d;
                    if (calcValues.Info.Riders.Any(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected))
                    {
                        var rider = calcValues.Info.Riders.First(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                        GetSpouseRiderMortalityCost(false, year, clientOrSpouse, rider, calcValues.DataSet, calcValues.ClientRates, yearlyData);
                    }
                    if (calcValues.Info.IsChildRiderSelected)
                        GetChildRiderMortalityCost(year, clientOrSpouse, calcValues.Info, clientOrSpouse == calcValues.Info.SpouseAsClientData, yearlyData);
                    foreach (var rider in calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected))
                    {
                        GetInsuredRiderMortalityCost(false, year, calcValues.ClientRates, clientOrSpouse, rider, yearlyData);
                    }
                    GetPrincipalRiderMortalityCost(guarOrProjIndex, false, year, calcValues.Info, clientOrSpouse, calcValues.ClientRates, yearlyData);
                    GetPrincipalRiderGpoCost(guarOrProjIndex, year, clientOrSpouse, calcValues.ClientRates, yearlyData);
                    GetPrincipalMortalityCost(clientOrSpouse, year, calcValues.ClientRates, yearlyData);
                    GetPrincipalPremiumCharges(yearlyData);
                    GetPrincipalRiderWPDCost(clientOrSpouse, year, calcValues.ClientRates, yearlyData);
                    yearlyData.CashValue -= (yearlyData.Charges + yearlyData.InsuredMortalityCosts);
                    yearlyData.CashValue *= GetMonthlyInterestRate(clientOrSpouse.InterestRate.Value);
                    if (corrRate > 0d && yearlyData.DeathBenefitOption == 1)
                    {
                        yearlyData.DeathBenefit = minimumDeathBenefitValue +
                            ((minimumDeathBenefitValue + yearlyData.ClientCola) < (yearlyData.CashValue - yearlyData.StartCashValue) * corrRate
                                ? ((yearlyData.CashValue - yearlyData.StartCashValue) * corrRate) - minimumDeathBenefitValue + yearlyData.ClientCola
                                : 0d);
                    }
                    if (year - clientOrSpouse.Age < 21 && !search)
                        SumCostIndices(clientOrSpouse, year, annMo, guarOrProjIndex, yearlyData, ref oneYearCosts, ref numeratorNPs, ref numeratorSCs, ref denominators);
                }
                if (yearlyData == null)
                    continue;

                CheckAnnivarsary(clientOrSpouse, clientOrSpouse.Genders, year, calcValues.DataSet.LsSurrItems, yearlyData);
                calcPremiums.LedgerItems[year].AnnualOutlay = yearlyData.Outlay;
                calcPremiums.LedgerItems[year].SurrenderValue[guarOrProjIndex] = yearlyData.CashValue;
                calcPremiums.LedgerItems[year].CashValue[guarOrProjIndex] = yearlyData.CashValue < 0d ? 0d : yearlyData.CashValue;
                calcPremiums.LedgerItems[year].DeathBenefit[guarOrProjIndex] = yearlyData.FaceValue + yearlyData.ClientCola +
                    ((yearlyData.DeathBenefitOption == 1 || yearlyData.StartCashValue > yearlyData.CashValue)
                        ? 0d
                        : yearlyData.CashValue - yearlyData.StartCashValue);

                if (((clientOrSpouse.Age % 2) != 0 && (year % 2) == 0) || ((clientOrSpouse.Age % 2) == 0 && (year % 2) != 0))
                {
                    var spouseRider = calcValues.Info.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                    if (clientOrSpouse.Cola != null && clientOrSpouse.Cola.Value != 0d)
                        AddCola(CalculatingOnItems.Principal, clientOrSpouse, year, calcValues.Info, 0, null, yearlyData);

                    if (spouseRider != null)
                        AddCola(CalculatingOnItems.Spouse, clientOrSpouse, year, calcValues.Info, 0, spouseRider, yearlyData);

                    var xVal = yearlyData.FaceValue;
                    var info = calcValues.Info;
                    calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected).ToList().ForEach(rider => {
                        if (rider.Cola != null)
                            AddCola(CalculatingOnItems.Spouse, clientOrSpouse, year, info, rider.Index, spouseRider, yearlyData);
                    });
                    yearlyData.FaceValue = xVal;
                }

                if (yearlyData.CashValue <= 0d && year > 3)
                    break;
                if (clientOrSpouse.FutureChanges)
                    CheckFutureChanges(clientOrSpouse, calcValues.Info, year, yearlyData, ref minimumDeathBenefitValue);
            }
            cashValue = yearlyData.CashValue;
            calcValues.InsuredMortCosts = yearlyData.InsuredMortalityCosts;
            faceValue = yearlyData.FaceValue;
            monthlyPremium = yearlyData.MonthlyPremium;
            modalPremium = yearlyData.ModalPremium;
            clientCola = yearlyData.ClientCola;
            spouseCola = yearlyData.SpouseCola;
            interestRate = yearlyData.InterestRate;
            minimumDeathBenefitValue = yearlyData.MinimumDeathBenefit;
        }

        private void ReverseCashValue(ref CalculationValues calcValues, bool isGuaranteed, int year, bool hasSubstandardRate, double waiverPercentage, double premium, bool additionalInsureds, double deathBenefit, double costOfLiving, double charges, double interestRate, ref double cashValue, ref double additionalValue) {
            var numerator = 0d;
            var denominator = 0d;
            var qFactor = calcValues.MortalityRate / 1000d;
            var tempDeathBenefit = deathBenefit + costOfLiving;

            if (hasSubstandardRate)
            {
                var mortItem = calcValues.ClientRates.First(x => x.IsGuaranteed == isGuaranteed && x.Year == year);
                if (calcValues.MortalityRate > mortItem.PrincipalCs80 && mortItem.PrincipalCs80 > 0d)
                    qFactor = mortItem.PrincipalCs80 / 1000d;
            }
            numerator = charges * qFactor;
            numerator += (tempDeathBenefit * 0.996737 * qFactor);
            numerator += charges;
            numerator *= (1d + waiverPercentage);
            numerator = (numerator * interestRate) + cashValue;
            denominator = 1d + qFactor + (qFactor * waiverPercentage);
            denominator *= interestRate;
            cashValue = (numerator / denominator) - premium + 0.005;
            cashValue *= 100d;
            cashValue = Convert.ToInt32(System.Math.Truncate(cashValue)) / 100;

            //* if additionalInsureds is true, calculate the Guideline Single
            //* Premium inclusive of additional insured mortality
            //* charges in the LS_CSV(GuarCurr) field.

            if (additionalInsureds)
            {
                numerator = (charges + calcValues.InsuredMortCosts) * qFactor;
                numerator = numerator + (tempDeathBenefit * 0.996737 * qFactor);
                numerator = numerator + charges + calcValues.InsuredMortCosts;
                numerator = numerator * (1d + waiverPercentage);
                numerator = (numerator * interestRate) + additionalValue;
                //'Denominator shouldn't change
                additionalValue = (numerator / denominator) - premium + 0.005;
                additionalValue *= 100d;
                additionalValue = Convert.ToInt32(System.Math.Truncate(additionalValue)) / 100;
            }
        }

        private void SumCostIndices(IIndividualData clientOrSpouse, int annYr, int annMo, int clientSpouseIndex, YearlyData md, ref double[] oneYearCosts, ref double[] numeratorNPs, ref double[] numeratorSCs, ref double[] denominators) {
            var power = 0d;
            var costFactor = 0d;
            var tmpDeathBenefit = 0d;

            tmpDeathBenefit = md.FaceValue + md.ClientCola;
            power = annYr - clientOrSpouse.Age + 1;
            if (power <= 11d)
            {
                if (power == 11d)
                    numeratorSCs[0] = numeratorNPs[0] - md.CashValue;
                else
                {
                    oneYearCosts[0] -= (md.InsuredMortalityCosts * 1.081081) + md.MonthlyPremium;
                    if (annMo == 12)
                    {
                        costFactor = System.Math.Pow(1.05, (11d - power));
                        numeratorNPs[0] += oneYearCosts[0] * costFactor;
                        oneYearCosts[0] = 0d;
                        if (clientSpouseIndex == 0d)
                            denominators[0] += tmpDeathBenefit * costFactor;
                    }
                }
            }
            if (power == 21)
                numeratorSCs[1] = numeratorNPs[1] - md.CashValue;
            else
            {
                oneYearCosts[1] -= (md.InsuredMortalityCosts * (power <= 10 ? 1.081081 : 1.025641)) + md.MonthlyPremium;
                if (annMo == 12)
                {
                    costFactor = System.Math.Pow(1.05, (21d - power));
                    numeratorNPs[1] += oneYearCosts[1] * costFactor;
                    oneYearCosts[1] = 0;
                    if (clientSpouseIndex == 0)
                        denominators[1] += tmpDeathBenefit * costFactor;
                }
            }
        }

        public CalculatedPremiums BeginCalculations(IllustrationInfo info, IRepository repo, IAppData dataSet, int startAge, out string errorMessage) {
            var modalPremium = 0d;
            var clientCola = 0d;
            var spouseCola = 0d;
            var deathBenefitOption = 1;
            var opt2StartCashValue = 0d;
            var calcPremiums = new CalculatedPremiums(startAge);
            var calculationValues = new CalculationValues {
                Info = info,
                Repo = repo,
                DataSet = dataSet
            };

            errorMessage = null;

            info.IllustrateInitialCashValue = info.IllustrateInitialCashValue > 0d
                ? info.IllustrateInitialCashValue
                : info.ClientData.LumpSum ?? (info.ClientData.FaceAmount ?? 0d);

            calculationValues.ClientRates = GetMortalityRates(ref calculationValues, out errorMessage, info.IsSpousePrincipalInsured);
            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;
            GetMinimumPremiums(ref calculationValues, ref calcPremiums, out errorMessage, info.IsSpousePrincipalInsured);
            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;
            GetTargetPremiums(ref calculationValues, ref calcPremiums, out errorMessage, info.IsSpousePrincipalInsured);
            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;
            GetGuidelineSinglePremium(ref calculationValues, ref calcPremiums, out errorMessage, info.IsSpousePrincipalInsured);
            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;
            GetGuidelineAnnualPremium(ref calculationValues, ref calcPremiums, out errorMessage, info.IsSpousePrincipalInsured);
            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;

            if (info.SpecialOptionsCashValue > 0 && info.SpecialOptionsAge > 0)
            {
                PremiumToObtainCashValue(info.SpecialOptionsCashValue, info.SpecialOptionsAge, ref calculationValues, ref calcPremiums,
                    modalPremium, clientCola, spouseCola, deathBenefitOption, opt2StartCashValue, out errorMessage, info.IsSpousePrincipalInsured);
                if (!string.IsNullOrEmpty(errorMessage))
                    return calcPremiums;
            }
            else
                calcPremiums.SolvePremium = "0.0";

            GetFullLedger(ref calculationValues, ref calcPremiums, modalPremium, clientCola, spouseCola, deathBenefitOption,
                opt2StartCashValue, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                return calcPremiums;

            calcPremiums.ClientGuaranteed = calculationValues.ClientRates.Where(x => x.IsGuaranteed).OrderBy(x => x.Year).ToList();
            calcPremiums.ClientProjected = calculationValues.ClientRates.Where(x => !x.IsGuaranteed).OrderBy(x => x.Year).ToList();
            return calcPremiums;
        }

        public int GetBandIndex(double? faceAmount) {
            if (!faceAmount.HasValue)
                return 0;
            if (faceAmount.Value.IsBetween(50000d, 99999d, true))
                return 1;
            else if (faceAmount.Value.IsBetween(100000d, 249999d, true))
                return 2;
            else if (faceAmount.Value > 249999d)
                return 3;
            else
                return 0;
        }

        public void GetFullLedger(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, double modalPremium, double clientCola, double spouseCola, int deathBenefitOption, double opt2StartCashValue, out string errorMessage) {
            errorMessage = null;
            try
            {
                var search = true;
                var clientOrSpouse = calcValues.Info.ClientData;

                var cashValue = calcValues.Info.IllustrateInitialCashValue * 0.925;
                var beginAge = calcValues.Info.IllustrateBeginAtAge > 0 ? calcValues.Info.IllustrateBeginAtAge : clientOrSpouse.Age + 1;
                var faceAmount = clientOrSpouse.FaceAmount.Value;
                var outlay = calcValues.Info.InitialLumpSumAmount > 0d
                    ? calcValues.Info.InitialLumpSumAmount
                    : clientOrSpouse.LumpSum != null && clientOrSpouse.LumpSum.Value > 0d
                        ? clientOrSpouse.LumpSum.Value
                        : 0d;
                var faceValue = 0d;
                var minimumDeathBenefit = clientOrSpouse.FaceAmount.Value;
                var interestRate = clientOrSpouse.InterestRate.Value > 1 ? clientOrSpouse.InterestRate.Value / 100d : clientOrSpouse.InterestRate.Value;
                var dbOption = clientOrSpouse.Option;
                modalPremium = calcValues.Info.PlannedModalPremium;
                var indexNumNPGuarCurr = new double[] { 0d, 0d };
                var indexNumSCGuarCurr = new double[] { 0d, 0d };
                var indexDenom = new double[] { 0d, 0d };
                var indexOneYearCost = new double[] { 0d, 0d };
                var colaClient = clientOrSpouse.Cola.Value;
                var colaSpouse = calcValues.Info.SpouseAsClientData.Cola.Value;
                var mothlyPremium = 0d;

                ProcessLedgerYears(0, beginAge, 94, ref calcPremiums, search, ref calcValues, clientOrSpouse, ref cashValue, ref faceValue,
                    ref minimumDeathBenefit, ref mothlyPremium, ref modalPremium, ref colaClient, ref colaSpouse, deathBenefitOption,
                    opt2StartCashValue, ref indexOneYearCost, ref indexNumNPGuarCurr, ref indexNumSCGuarCurr, ref indexDenom,
                    ref interestRate);

                clientOrSpouse = calcValues.Info.SpouseAsClientData;
                outlay = clientOrSpouse.LumpSum != null && clientOrSpouse.LumpSum.Value > 0d ? clientOrSpouse.LumpSum.Value : 0d;
                faceValue = 1d;
                faceAmount = clientOrSpouse.FaceAmount.Value;
                minimumDeathBenefit = clientOrSpouse.FaceAmount.Value;
                interestRate = clientOrSpouse.InterestRate.Value > 1d ? clientOrSpouse.InterestRate.Value / 100d : clientOrSpouse.InterestRate.Value;
                modalPremium = clientOrSpouse.PlannedPrem;
                dbOption = clientOrSpouse.Option;
                cashValue = clientOrSpouse.LumpSum != null && clientOrSpouse.LumpSum.Value > 0d ? clientOrSpouse.LumpSum.Value * 0.925 : 0d;
                indexNumNPGuarCurr = new double[] { 0d, 0d };
                indexNumSCGuarCurr = new double[] { 0d, 0d };
                indexDenom = new double[] { 0d, 0d };

                if (clientOrSpouse.PlannedPrem + clientOrSpouse.LumpSum < calcPremiums.TotMin - 1d)
                {
                    calcPremiums.InForceYears = "be below Minimum";
                    outlay = 0d;
                    modalPremium = 0d;
                }
                ProcessLedgerYears(1, beginAge, 94, ref calcPremiums, search, ref calcValues, clientOrSpouse, ref cashValue, ref faceValue,
                    ref minimumDeathBenefit, ref mothlyPremium, ref modalPremium, ref colaClient, ref colaSpouse, deathBenefitOption,
                    opt2StartCashValue, ref indexOneYearCost, ref indexNumNPGuarCurr, ref indexNumSCGuarCurr, ref indexDenom,
                    ref interestRate);
                calcPremiums.InForceYears = calcPremiums.LedgerItems[95].CashValue[1] > 0d
                    ? "ENDOW at age 95"
                    : (calcPremiums.LedgerItems.Where(x => x.CashValue[1] > 0).Count() > 0d)
                        ? $"LAPSE at age {calcPremiums.LedgerItems.Where(x => x.CashValue[1] > 0d).Max(x => x.Year)}"
                        : string.Empty;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void GetGuidelineAnnualPremium(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false) {
            //Calculate Guideline Annual Premium, using base
            //policy, option 1 death benefit, and the current
            //interest rate (first year)/4% (2nd year and beyond)

            //By definition, the guideline premium is the maximum
            //premium that will not allow the cash value to equal
            //or exceed the initial death benefit (or the corridor
            //of coverage death benefit)

            //Finding this premium is a matter of guesswork and inter-
            //polation.  Start with a reasonable first guess that is
            //guaranteed to be "high": Guideline single premium / 10.
            //Save the resultant cash value at age 95.  Now set up a
            //reasonable guess that is guaranteed to be "low".  ($10)
            //Save the resultant cash value.

            //From this point on, we have a "high" and "low" guess and
            //their resulting cash values. For every new guess, determine
            //the "position" of the target cash value between the
            //resultant high and low values.  This "position" (expressed
            //as a percent) will be used to determine the next guess:
            //That same percent times the difference in "high" and "low"
            //guesses, added to the low guess.

            //The process is repeated until the guesses are less than
            //1.5 cents apart.  The low guess then becomes the Guideline
            //modal premium.

            errorMessage = null;
            try
            {
                var highGuess = 0d;
                var lowGuess = 0d;
                var highValue = 0d;
                var lowValue = 0d;
                var percentage = 0d;
                var currPremium = 0d;
                var additionalValue = 0d;

                var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;
                var cashValue = !clientOrSpouse.FaceAmount.HasValue ? 0d : clientOrSpouse.FaceAmount.Value * 0.98;
                var faceAmount = clientOrSpouse.FaceAmount ?? 0d;

                highGuess = calcPremiums.GuideSingle / 10d;
                currPremium = highGuess;
                lowGuess = 10d;
                CalculateBackwards(ref calcValues, clientOrSpouse, ref cashValue, ref additionalValue, currPremium);

                highValue = cashValue;
                currPremium = lowGuess;
                CalculateBackwards(ref calcValues, clientOrSpouse, ref cashValue, ref additionalValue, currPremium);

                lowValue = cashValue;
                while ((highGuess - lowGuess) > 0.015)
                {
                    if (System.Math.Abs(cashValue) < 0.5)
                    {
                        highGuess = (cashValue < 0 ? currPremium - 0.01 : currPremium);
                        lowGuess = (cashValue < 0 ? currPremium - 0.01 : currPremium);
                    }
                    else
                    {
                        cashValue = highValue - lowValue;
                        percentage = highValue / cashValue;
                        currPremium = highGuess - ((highGuess - lowGuess) * percentage);
                        CalculateBackwards(ref calcValues, clientOrSpouse, ref cashValue, ref additionalValue, currPremium);
                        if (cashValue < 0d)
                        {
                            highValue = cashValue;
                            highGuess = currPremium;
                        }
                        else
                        {
                            lowValue = cashValue;
                            lowGuess = currPremium;
                        }
                    }
                }
                calcPremiums.GuideAnnual = System.Math.Truncate(lowGuess);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void GetGuidelineSinglePremium(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false) {
            //Calculate Guideline Single Premium, using base
            //policy, option 1 death benefit, and the current
            //interest rate (first year)/6% (2nd year and beyond)

            //By definition, the guideline premium is the maximum
            //premium that will not allow the cash value to equal
            //or exceed the initial death benefit (or the corridor
            //of coverage death benefit)

            errorMessage = null;
            try
            {
                var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;
                var costOfLiving = 0d;
                var cashValue = clientOrSpouse.FaceAmount.Value * 0.98;
                var addCashValue = clientOrSpouse.FaceAmount.Value; //for additional insureds
                var deathBenefit = clientOrSpouse.FaceAmount.Value;
                var interestRate = System.Math.Pow(1.06, (1d / 12d));
                var waiverPct = 0d;
                var currPremium = 0d;
                var loanBalance = 0d;
                var chargePct = 0d;
                var validCorr = false;
                var additionalInsureds = false;
                calcValues.InsuredMortCosts = 0d;

                var spouseItems = calcValues.DataSet.LsSpouseItems;
                //var mortalityRates = calcValues.ClientRates;

                for (int annYr = 93; annYr > clientOrSpouse.Age; annYr--)
                {
                    validCorr = LsCorr.Verify(calcValues.DataSet.LsCorrItems, annYr, out var returnRate);
                    for (int annMo = 1; annMo <= 12; annMo++)
                    {
                        chargePct = clientOrSpouse.FaceAmount.Value < 100000d ? 3.5 : 3.0;
                        if (validCorr && deathBenefit < (cashValue * returnRate))
                            deathBenefit = cashValue * returnRate;
                        calcValues.MortalityRate = CalculatePrincipalMortality(calcValues.ClientRates, clientOrSpouse, deathBenefit, false, annYr, costOfLiving, loanBalance);
                        if (calcValues.Info.Riders.Any(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected))
                        {
                            var rider = calcValues.Info.Riders.First(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected);
                            CalculateSpouseRiderMortality(ref calcValues, true, rider, false, clientOrSpouse.Age, annYr);
                            additionalInsureds = true;
                        }
                        var otherRiders = calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected).ToList();
                        foreach (var rider in otherRiders)
                        {
                            CalculateInsuredRiderMortality(ref calcValues, true, rider, false, clientOrSpouse.Age, annYr);
                            additionalInsureds = true;
                        }
                        ReverseCashValue(ref calcValues, false, annYr, (clientOrSpouse.HasSubstandardRate), waiverPct, currPremium, additionalInsureds, deathBenefit, costOfLiving, chargePct, interestRate, ref cashValue, ref addCashValue);
                    }
                }
                interestRate = 1 + (clientOrSpouse.InterestRate.Value / 100d);
                interestRate = System.Math.Pow(interestRate, (1d / 12d));
                var i = clientOrSpouse.Age;
                validCorr = LsCorr.Verify(calcValues.DataSet.LsCorrItems, i, out var corrRate);
                for (int year = 1; year < 13; year++)
                {
                    chargePct = clientOrSpouse.FaceAmount.Value < 100000d ? 3.5 : 3.0;
                    if (validCorr)
                    {
                        if (deathBenefit < cashValue * corrRate)
                            deathBenefit = cashValue * corrRate;
                        calcValues.MortalityRate = CalculatePrincipalMortality(calcValues.ClientRates, clientOrSpouse, deathBenefit, false, clientOrSpouse.Age, costOfLiving, loanBalance);
                        additionalInsureds = false;
                        calcValues.InsuredMortCosts = 0d;
                        if (calcValues.Info.Riders.Any(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected))
                        {
                            var rider = calcValues.Info.Riders.First(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected);
                            CalculateSpouseRiderMortality(ref calcValues, true, rider, false, clientOrSpouse.Age, clientOrSpouse.Age);
                            additionalInsureds = true;
                        }
                        var otherRiders = calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected).ToList();
                        foreach (var rider in otherRiders)
                        {
                            CalculateInsuredRiderMortality(ref calcValues, true, rider, false, rider.Age, clientOrSpouse.Age);
                            additionalInsureds = true;
                        }
                        ReverseCashValue(ref calcValues, false, year, clientOrSpouse.HasSubstandardRate, waiverPct, currPremium, additionalInsureds, deathBenefit, costOfLiving, chargePct, interestRate, ref cashValue, ref addCashValue);
                    }
                }

                calcPremiums.GuideSingle = System.Math.Truncate(((cashValue / 0.00925) + 5d) / 100d);
                calcPremiums.GuideSingleAddInsd = additionalInsureds ? System.Math.Truncate(((addCashValue / 0.00925) + 5d) / 100d) : 0d;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void GetMinimumPremiums(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false) {
            errorMessage = null;
            var minimumPremium = 0d;
            var wpdMort = 0d;
            var threeYrMort = 0d;

            var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;
            if (clientOrSpouse == null)
            {
                errorMessage = "Illustration data missing";
                return;
            }
            var gender = GetGender(clientOrSpouse, calcValues.Repo.Genders, clientOrSpouse.State);
            var isSmoker = IsSmoker(clientOrSpouse);
            var band = GetBandIndex(clientOrSpouse.InitialDeathBenefit);
            if (LsMinp.IsValid(calcValues.DataSet.LsMinpItems, gender, isSmoker, band, clientOrSpouse.Age, out var returnValue))
            {
                minimumPremium = (returnValue * (clientOrSpouse.InitialDeathBenefit.Value / 1000d)) + 38.92;
                if (clientOrSpouse.InitialDeathBenefit.Value < 100000d)
                    minimumPremium += 6.49;
            }
            if (clientOrSpouse.HasSubstandardRate)
            {
                for (int year = 0; year < 3; year++)
                {
                    var mortalityRate = calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == clientOrSpouse.Age + year).PrincipalSub[calcValues.PrincipalBand];
                    mortalityRate *= (clientOrSpouse.InitialDeathBenefit.Value / 1000d);
                    threeYrMort += (mortalityRate * 12d);
                }
                minimumPremium += ((threeYrMort / 3d) * baseMutiplier) + additionalValue;
                wpdMort += threeYrMort;
            }
            if (clientOrSpouse.HasGpo)
            {
                threeYrMort = 0d;
                for (int year = 0; year < 3; year++)
                {
                    var mortalityRate = (clientOrSpouse.Gpo.Value / 1000d) * calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == clientOrSpouse.Age + year).PrincipalGpo;
                    threeYrMort += (mortalityRate * 12d);
                }
                minimumPremium += ((threeYrMort / 3d) * baseMutiplier) + additionalValue;
                wpdMort += threeYrMort;
            }
            if (clientOrSpouse.FaceAmount.Value != 0d)
            {
                threeYrMort = 0d;
                for (int year = 0; year < 3; year++)
                {
                    var mortalityRate = calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == clientOrSpouse.Age + year).InsuredBase[0];
                    if (clientOrSpouse.HasSubstandardRate)
                        mortalityRate += calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == clientOrSpouse.Age + year).InsuredSub[0];
                    mortalityRate += (clientOrSpouse.FaceAmount.Value / 1000d);
                    threeYrMort += (mortalityRate * 12d);
                }
                minimumPremium += ((threeYrMort / 3d) * baseMutiplier) + additionalValue;
                wpdMort += threeYrMort;
            }
            calcPremiums.PrinMin = Modalize(minimumPremium, calcValues.Info.PremiumMode);

            minimumPremium = 0d;
            if (calcValues.Info.Riders.Any())
            {
                var rider = calcValues.Info.Riders.First(x => x.As<IndividualData>().IndividualType == IndividualData.KnownIndices.Spouse);
                gender = GetGender(rider, calcValues.Repo.Genders, clientOrSpouse.State);
                threeYrMort = 0d;
                for (int year = 0; year < 3; year++)
                {
                    var mortalityRate = calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age + year).SpouseBase;
                    if (rider.HasSubstandardRate)
                        mortalityRate += calcValues.ClientRates.FirstOrDefault(x => x.IsGuaranteed && x.Year == rider.Age + year).SpouseSub;
                    mortalityRate += (rider.FaceAmount.Value / 1000d);
                    threeYrMort += (mortalityRate * 12d);
                }
                minimumPremium += ((threeYrMort / 3d) * baseMutiplier) + additionalValue;
                wpdMort += threeYrMort;
                calcPremiums.SpouseMin = Modalize(minimumPremium, calcValues.Info.PremiumMode);

                var c = calcPremiums;
                var info = calcValues.Info;
                var repo = calcValues.Repo;
                var mortalityItems = calcValues.ClientRates;
                calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected).ToList().ForEach(rdr => {
                    gender = GetGender(rdr, repo.Genders, clientOrSpouse.State);
                    threeYrMort = 0d;
                    for (int year = 0; year < 3; year++)
                    {
                        var mortItem = mortalityItems.FirstOrDefault(x => x.IsGuaranteed && x.Year == rdr.Age + year + 1);
                        if (mortItem != null)
                        {
                            var mortalityRate = mortItem.InsuredBase[rdr.Index];
                            if (rdr.HasSubstandardRate)
                                mortalityRate += mortItem.InsuredSub[rdr.Index];
                            mortalityRate += (rdr.FaceAmount.Value / 1000d);
                            threeYrMort += (mortalityRate * 12d);
                        }
                    }
                    minimumPremium += ((threeYrMort / 3d) * baseMutiplier) + additionalValue;
                    wpdMort += threeYrMort;
                    c.InsdMin[rdr.Index] = Modalize(minimumPremium, info.PremiumMode);
                });
                calcPremiums = c;
                if (calcValues.Info.IsChildRiderSelected)
                {
                    minimumPremium = 0d;
                    threeYrMort = 0d;
                    for (int year = 0; year < 3; year++)
                    {
                        if (calcValues.Info.ChildRiderYoungestAge + year < 24)
                        {
                            var mortalityRate = 0.4 * (calcValues.Info.ChildRiderDeathBenefitAmount / 1000d);
                            threeYrMort += (mortalityRate * 12d);
                        }
                    }
                    minimumPremium += ((threeYrMort / 3d) * 1.081081) + 0.005;
                    wpdMort += threeYrMort;
                    calcPremiums.ChildMin = Modalize(minimumPremium, calcValues.Info.PremiumMode);
                }
                else
                    calcPremiums.ChildMin = 0d;
                if (clientOrSpouse.HasWpd)
                {
                    minimumPremium = 0d;
                    threeYrMort = 0d;
                    var average_WPD = 0d;
                    for (int year = 0; year < 3; year++)
                    {
                        var mortItem = calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == clientOrSpouse.Age);
                        average_WPD += mortItem.PrincipalWpd;
                        threeYrMort += mortItem.PrincipalBase[calcValues.PrincipalBand] * (((clientOrSpouse.FaceAmount.Value / 1000d) * 12d) * 0.996737) + (clientOrSpouse.FaceAmount < 100000d ? 42d : 36d);
                    }
                    average_WPD /= 3;
                    minimumPremium += (((threeYrMort / 3d) * 1.081081) + 0.005) + (((wpdMort / 3d) * 1.081081) + 0.005) * average_WPD;
                    calcPremiums.PrinMin += Modalize(minimumPremium, calcValues.Info.PremiumMode);
                }
                calcPremiums.TotMin = calcPremiums.PrinMin + calcPremiums.SpouseMin + calcPremiums.ChildMin;
                for (int index = 0; index < 9; index++)
                {
                    calcPremiums.TotMin += calcPremiums.InsdMin[index];
                }
            }
        }

        public IList<MortalityItem> GetMortalityRates(ref CalculationValues calcValues, out string errorMessage, bool isSpouse = false) {
            errorMessage = null;
            calcValues.PrincipalBand = 0;
            var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;
            var gender = GetGender(clientOrSpouse, calcValues.Repo.Genders, clientOrSpouse.State);
            var isSmoker = IsSmoker(clientOrSpouse);
            var tables = clientOrSpouse.SubstandardRate.Value / 4d;
            string localErrorMessage = null;
            double returnRate;
            var addFace = HasAdditionalFaceValue(calcValues.Info, isSpouse) ? calcValues.Info.SpouseAsClientData.FaceAmount.Value : 0d;
            const int maxBand = 4;

            var result = new List<MortalityItem>();
            if (!clientOrSpouse.FaceAmount.HasValue)
            {
                errorMessage = "Face amount must be set.";
                return result;
            }
            var info = calcValues.Info;
            var repo = calcValues.Repo;
            var prItems = calcValues.DataSet.LsRatePrItems;
            var sbItems = calcValues.DataSet.LsRateSbItems;
            var cso80Items = calcValues.DataSet.LsCso80Items;
            var gpItems = calcValues.DataSet.LsRateGpItems;
            var wpItems = calcValues.DataSet.LsRateWpItems;
            var siItems = calcValues.DataSet.LsRateSiItems;

            var prinBand = 0;
            Enum.GetValues(typeof(RateTypes)).Cast<RateTypes>().ToList().ForEach(rateType => {
                for (int year = 0; year <= 94; year++)
                {
                    var mi = new MortalityItem { IsGuaranteed = rateType == RateTypes.Guaranteed, Year = year };
                    if (!mi.IsGuaranteed && mi.Year > clientOrSpouse.Age)
                        mi.Band = maxBand;
                    else
                    {
                        mi.BandFaceAmount = clientOrSpouse.FaceAmount ?? 0d;
                        if (addFace != 0d && mi.Year.IsBetween(1, clientOrSpouse.AgeOrYear.Value, true))
                            mi.BandFaceAmount += addFace;
                        mi.Band = GetBandIndex(mi.BandFaceAmount);
                    }
                    if (mi.Band == maxBand)
                    {
                        if (LsRatePr.IsValid(prItems, CalculatingOnItems.Principal, gender, isSmoker, mi.Band, clientOrSpouse.Age, out returnRate))
                        {
                            for (int band = 0; band < maxBand; band++)
                                mi.PrincipalBase[band] = returnRate;
                        }
                        else
                        {
                            localErrorMessage = $"Mortality rate not found (sex = {gender.Name}, age = {clientOrSpouse.Age}, smoker = {isSmoker}, face amount band = {mi.Band}. Illustration not valid!)";
                            return;
                        }
                    }
                    else
                    {
                        prinBand = mi.Band;
                        for (int band = 0; band < maxBand; band++)
                        {
                            if (LsRatePr.IsValid(prItems, CalculatingOnItems.Principal, gender, isSmoker, band, clientOrSpouse.Age, out returnRate))
                                mi.PrincipalBase[band] = returnRate;
                            else
                            {
                                localErrorMessage = $"Mortality rate not found (sex = {gender.Name}, age = {clientOrSpouse.Age}, smoker = {isSmoker}, face amount band = {mi.Band}. Illustration not valid!)";
                                return;
                            }
                        }
                    }
                    if (clientOrSpouse.HasSubstandardRate)
                    {
                        if (LsRateSb.IsValid(sbItems, isSmoker, clientOrSpouse.Age, out returnRate))
                        {
                            for (int band = 0; band < 4; band++)
                                mi.PrincipalSub[band] = mi.PrincipalBase[band] * tables * returnRate;
                            mi.PrincipalCs80 = 0d;
                        }
                        else
                            mi.PrincipalCs80 = LsCso80.Verify(cso80Items, gender, isSmoker, clientOrSpouse.Age, out returnRate) ? returnRate : 0d;
                    }
                    if (clientOrSpouse.HasGpo && mi.Year < 41)
                    {
                        if (LsRateGp.IsValid(gpItems, clientOrSpouse.Age, out returnRate))
                            mi.PrincipalGpo = returnRate;
                    }
                    if (clientOrSpouse.HasWpd && mi.Year < 60)
                    {
                        if (LsRateWp.IsValid(wpItems, gender, clientOrSpouse.Age, out returnRate))
                            mi.PrincipalWpd = returnRate;
                    }
                    if (HasAdditionalFaceValue(info, isSpouse))
                    {
                        if (mi.Year.IsBetween(clientOrSpouse.Age, clientOrSpouse.Age + clientOrSpouse.AgeOrYear.Value, true))
                        {
                            if (LsRateSi.IsValid(siItems, CalculatingOnItems.Insured, gender, isSmoker, mi.Year + 1, mi.IsGuaranteed ? 0 : 1, out returnRate))
                                mi.InsuredBase[0] = returnRate;
                            else
                            {
                                localErrorMessage = $"Mortality rate not found Term Rider (age = {clientOrSpouse.Age}, smoker = {isSmoker}. Illustration not valid!)";
                                return;
                            }
                        }
                        if (clientOrSpouse.HasSubstandardRate)
                        {
                            if (LsRateSb.IsValid(sbItems, isSmoker, mi.Year, out returnRate))
                                mi.InsuredSub[0] = mi.InsuredBase[0] * tables * returnRate;
                            mi.InsuredCs80[0] = 0d;
                        }
                        else
                            mi.InsuredCs80[0] = LsCso80.Verify(cso80Items, gender, isSmoker, clientOrSpouse.Age, out double returnRate1) ? returnRate1 : 0d;
                    }
                    if (info.Riders.Any())
                    {
                        var spouseRider = info.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                        if (spouseRider != null && spouseRider.IsSelected)
                        {
                            var isSpouseSmoker = IsSmoker(spouseRider);
                            var spouseGender = GetSpouseGender(spouseRider, repo, gender);
                            if (LsRateSi.IsValid(siItems, CalculatingOnItems.Spouse, spouseGender, isSpouseSmoker, mi.Year + 1, mi.IsGuaranteed ? 0 : 1, out returnRate))
                                mi.SpouseBase = returnRate;
                            else
                            {
                                localErrorMessage = $"Mortality rate not found Spouse Rider (age = {spouseRider.Age}, smoker = {isSpouseSmoker}. Illustration not valid!)";
                                return;
                            }
                            if (spouseRider.HasSubstandardRate)
                            {
                                if (LsRateSb.IsValid(sbItems, isSpouseSmoker, mi.Year, out returnRate))
                                    mi.SpouseSub = mi.InsuredBase[0] * tables * returnRate;
                                mi.SpouseCs80 = 0d;
                            }
                            else
                                mi.SpouseCs80 = LsCso80.Verify(cso80Items, gender, isSmoker, spouseRider.Age, out returnRate) ? returnRate : 0d;
                        }
                    }
                    var index = -1;
                    foreach (var rider in info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider))
                    {
                        index++;
                        if (!rider.IsSelected) continue;
                        var isInsuredSmoker = IsSmoker(rider);
                        if (LsRateSi.IsValid(siItems, CalculatingOnItems.Insured, rider.Gender, isInsuredSmoker, mi.Year + 1, mi.IsGuaranteed ? 0 : 1, out returnRate))
                            mi.InsuredBase[index] = returnRate;
                        else
                        {
                            localErrorMessage = $"Mortality rate not found Insured {info.Riders.IndexOf(rider)} Rider (age = {rider.Age}, smoker = {isInsuredSmoker}. Illustration not valid!)";
                            return;
                        }
                        if (rider.HasSubstandardRate)
                        {
                            if (LsRateSb.IsValid(sbItems, isInsuredSmoker, mi.Year, out returnRate))
                                mi.InsuredSub[index] = mi.InsuredBase[index] * tables * returnRate;
                            mi.InsuredCs80[index] = 0d;
                        }
                        else
                            mi.InsuredCs80[index] = LsCso80.Verify(cso80Items, rider.Gender, isInsuredSmoker, rider.Age, out returnRate) ? returnRate : 0d;
                    }
                    result.Add(mi);
                }
            });
            errorMessage = localErrorMessage;
            if (!string.IsNullOrEmpty(errorMessage))
                result = null;
            calcValues.PrincipalBand = prinBand;
            return result;
        }

        public void GetTargetPremiums(ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, out string errorMessage, bool isSpouse = false) {
            errorMessage = null;
            var targetPremium = 0d;
            var wpdMort = 0d;
            var mortalityRate = 0d;

            var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;
            if (clientOrSpouse == null)
            {
                errorMessage = "Illustration data missing";
                return;
            }
            var gender = GetGender(clientOrSpouse, calcValues.Repo.Genders, clientOrSpouse.State);
            if (LsTarg.IsValid(calcValues.DataSet.LsTargItems, gender, clientOrSpouse.Age, out var returnValue))
                targetPremium = returnValue * (clientOrSpouse.InitialDeathBenefit.Value / 1000d);
            if (clientOrSpouse.HasSubstandardRate)
            {
                mortalityRate = calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == clientOrSpouse.Age).PrincipalSub[calcValues.PrincipalBand];
                mortalityRate *= (clientOrSpouse.InitialDeathBenefit.Value / 1000d);
                targetPremium += (mortalityRate * 12d);
                wpdMort += (mortalityRate * 12d);
            }
            if (clientOrSpouse.HasGpo)
            {
                mortalityRate = (clientOrSpouse.Gpo.Value / 1000d) * calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == clientOrSpouse.Age).PrincipalGpo;
                targetPremium += (mortalityRate * 12d);
                wpdMort += (mortalityRate * 12d);
            }
            if (HasAdditionalFaceValue(calcValues.Info, isSpouse))
            {
                var threeYearMort = 0d;
                for (int i = 0; i < 3; i++)
                {
                    mortalityRate = calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == clientOrSpouse.Age).InsuredBase[0];
                    if (clientOrSpouse.HasSubstandardRate)
                        mortalityRate += calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == clientOrSpouse.Age).InsuredSub[0];
                    mortalityRate *= (calcValues.Info.SpouseAsClientData.FaceAmount.Value / 1000d);
                    threeYearMort += (mortalityRate * 12d);
                }
                targetPremium += ((threeYearMort / 3d) * 1.081081) + 0.005;
                wpdMort += threeYearMort;
            }
            targetPremium = Modalize(targetPremium, calcValues.Info.PremiumMode);
            calcPremiums.PrinTarg = targetPremium;

            targetPremium = 0d;
            var index = -1;

            if (calcValues.Info.Riders.Any())
            {
                var spouseRider = calcValues.Info.Riders.First(x => x.As<IndividualData>().IndividualType == IndividualData.KnownIndices.Spouse);
                var spouseGender = GetSpouseGender(spouseRider, calcValues.Repo, gender);
                var threeYearMort = 0d;
                for (int i = 0; i < 3; i++)
                {
                    mortalityRate = calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == spouseRider.Age).SpouseBase;
                    if (spouseRider.HasSubstandardRate)
                        mortalityRate += calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == spouseRider.Age).SpouseSub;
                    mortalityRate *= (spouseRider.FaceAmount.Value / 1000d);
                    threeYearMort += (mortalityRate * 12d);
                }
                targetPremium += ((threeYearMort / 3d) * 1.081081) + 0.005;
                targetPremium = Modalize(targetPremium, calcValues.Info.PremiumMode);
                calcPremiums.SpouseTarg = targetPremium;

                foreach (var rider in calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.Age > 0))
                {
                    //if (rider.StartYear >= 4)
                    //    break;
                    var riderGender = GetGender(rider, calcValues.Repo.Genders, clientOrSpouse.State);
                    threeYearMort = 0d;
                    index++;
                    for (int i = 0; i < 3; i++)
                    {
                        mortalityRate = calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == rider.Age).InsuredBase[i];
                        if (rider.HasSubstandardRate)
                            mortalityRate += calcValues.ClientRates.FirstOrDefault(x => !x.IsGuaranteed && x.Year == rider.Age).InsuredSub[i];
                        mortalityRate *= (rider.FaceAmount.Value / 1000d);
                        threeYearMort += (mortalityRate * 12d);
                    }
                    targetPremium += ((threeYearMort / 3d) * 1.081081) + 0.005;
                    targetPremium = Modalize(targetPremium, calcValues.Info.PremiumMode);
                    calcPremiums.InsdTarg[index] = targetPremium;
                }
            }
            else
            {
                calcPremiums.SpouseMin = 0d;
                foreach (var rider in calcValues.Info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider))
                {
                    index++;
                    calcPremiums.InsdTarg[index] = 0d;
                }
            }
        }

        public void PremiumToObtainCashValue(double cashValue, int cashValueAge, ref CalculationValues calcValues, ref CalculatedPremiums calcPremiums, double modalPremium, double clientCola, double spouseCola, int deathBenefitOption, double opt2StartCashValue, out string errorMessage, bool isSpouse = false) {
            //Calculate Modal Premium to Obtain Target Cash Value,
            //using all current parameter values to reach the Target
            //in the given number of years.

            //Finding this premium is a matter of guesswork and inter-
            //polation.  Start with Minimum Premium as the "lower
            //bound" and then use Guideline Premium as the "upper
            //bound".  If the Target Cash Value is outside these
            //"bounds", discontinue the search and return the nearest
            //"bound" premium.

            //If the Target Cash Value falls between the "bounds",
            //the limits are established. For every new guess, determine
            //the a value halfway between the high value premium and the
            //low value premium.  Use this value as the next guess.  New
            //result is calculated and compared to Target Cash Value. If
            //the result is low, you have a new Low Premium; otherwise, you
            //have a new High Premium.

            //The process is repeated until the low and high premium are 1
            //cent apart.  The high guess then becomes the Modal Premium
            //for this Search.

            errorMessage = null;
            try
            {
                var HighGuess = 0d;
                var LowGuess = 0d;
                var NewGuess = 0d;
                var HighValue = 0d;
                var LowValue = 0d;
                var EndValue = cashValue;
                var EndYear = cashValueAge;

                var clientOrSpouse = isSpouse ? calcValues.Info.SpouseAsClientData : calcValues.Info.ClientData;

                calcPremiums.LedgerItems[clientOrSpouse.Age + 1].SurrenderValue[1] = 0d;
                calcPremiums.LedgerItems[clientOrSpouse.Age + 1].CashValue[1] = 0d;
                calcPremiums.LedgerItems[clientOrSpouse.Age + 1].DeathBenefit[1] = clientOrSpouse.FaceAmount.Value;

                var LS_Irate_GuarCurr = clientOrSpouse.InterestRate.Value / 100d;
                var beginAge = calcValues.Info.IllustrateBeginAtAge > 0 ? calcValues.Info.IllustrateBeginAtAge : clientOrSpouse.Age;
                var faceAmount = clientOrSpouse.FaceAmount.Value;
                var faceValue = 0d;
                var minimumDeathBenefit = clientOrSpouse.FaceAmount.Value;
                var indexNumNPGuarCurr = new double[] { 0d, 0d };
                var indexNumSCGuarCurr = new double[] { 0d, 0d };
                var indexDenom = new double[] { 0d, 0d };
                var LS_Index_OneYear_Cost_GuarCurr = new double[] { 0d, 0d };
                var monthlyPremium = 0d;

                ProcessLedgerYears(0, clientOrSpouse.Age + 1, 94, ref calcPremiums, true, ref calcValues, clientOrSpouse, ref cashValue, ref faceValue,
                    ref minimumDeathBenefit, ref monthlyPremium, ref modalPremium, ref clientCola, ref spouseCola, deathBenefitOption,
                    opt2StartCashValue, ref LS_Index_OneYear_Cost_GuarCurr, ref indexNumNPGuarCurr, ref indexNumSCGuarCurr, ref indexDenom,
                    ref LS_Irate_GuarCurr);

                LowValue = calcPremiums.LedgerItems[EndYear].CashValue[1];
                if (EndValue < LowValue)
                {
                    calcPremiums.SolvePremium = "< MINIMUM";
                    return;
                }
                cashValue = 0d;
                faceAmount = clientOrSpouse.FaceAmount.Value;
                minimumDeathBenefit = clientOrSpouse.FaceAmount.Value;
                LS_Irate_GuarCurr = clientOrSpouse.InterestRate.Value / 100d;
                if (calcValues.Info.PremiumMode.DisplayedValue.Equals("Semiannual", StringComparison.OrdinalIgnoreCase))
                    modalPremium = calcPremiums.GuideAnnual / 11.688;
                else if (calcValues.Info.PremiumMode.DisplayedValue.Equals("Quarterly", StringComparison.OrdinalIgnoreCase))
                    modalPremium = calcPremiums.GuideAnnual / 3.916;
                else if (calcValues.Info.PremiumMode.DisplayedValue.Equals("Monthly (APP)", StringComparison.OrdinalIgnoreCase))
                    modalPremium = calcPremiums.GuideAnnual / 1.972;
                else
                    modalPremium = calcPremiums.GuideAnnual;

                deathBenefitOption = clientOrSpouse.Option;
                clientCola = clientOrSpouse.Cola.Value;
                spouseCola = calcValues.Info.SpouseAsClientData.Cola.Value;

                ProcessLedgerYears(1, clientOrSpouse.Age + 1, 94, ref calcPremiums, true, ref calcValues, clientOrSpouse, ref cashValue, ref faceValue,
                    ref minimumDeathBenefit, ref monthlyPremium, ref modalPremium, ref clientCola, ref spouseCola, deathBenefitOption,
                    opt2StartCashValue, ref LS_Index_OneYear_Cost_GuarCurr, ref indexNumNPGuarCurr, ref indexNumSCGuarCurr, ref indexDenom,
                    ref LS_Irate_GuarCurr);

                HighValue = calcPremiums.LedgerItems[EndYear].CashValue[1];

                if (EndValue > HighValue)
                {
                    calcPremiums.SolvePremium = "> GUIDELINE";
                    return;
                }

                HighGuess = modalPremium;
                LowGuess = calcPremiums.TotMin;
                NewGuess = HighGuess - ((HighGuess - LowGuess) / 2d);
                modalPremium = NewGuess;

                while (HighGuess - LowGuess > 0.01)
                {
                    cashValue = 0d;
                    faceAmount = clientOrSpouse.FaceAmount.Value;
                    minimumDeathBenefit = clientOrSpouse.FaceAmount.Value;
                    LS_Irate_GuarCurr = clientOrSpouse.InterestRate.Value / 100;
                    deathBenefitOption = clientOrSpouse.Option;
                    clientCola = 0d;
                    spouseCola = 0d;

                    ProcessLedgerYears(0, clientOrSpouse.Age + 1, 94, ref calcPremiums, true, ref calcValues, clientOrSpouse, ref cashValue, ref faceValue,
                        ref minimumDeathBenefit, ref monthlyPremium, ref modalPremium, ref clientCola, ref spouseCola, deathBenefitOption,
                        opt2StartCashValue, ref LS_Index_OneYear_Cost_GuarCurr, ref indexNumNPGuarCurr, ref indexNumSCGuarCurr, ref indexDenom,
                        ref LS_Irate_GuarCurr);

                    if (System.Math.Abs(EndValue - calcPremiums.LedgerItems[EndYear].CashValue[1]) < 1d)
                    {
                        LowGuess = modalPremium;
                        HighGuess = modalPremium;
                        LowValue = calcPremiums.LedgerItems[EndYear].CashValue[1];
                        HighValue = calcPremiums.LedgerItems[EndYear].CashValue[1];
                    }
                    else
                    {
                        if (EndValue >= calcPremiums.LedgerItems[EndYear].CashValue[1])
                        {
                            LowValue = calcPremiums.LedgerItems[EndYear].CashValue[1];
                            LowGuess = modalPremium;
                        }
                        else
                        {
                            HighValue = calcPremiums.LedgerItems[EndYear].CashValue[1];
                            HighGuess = modalPremium;
                        }
                        NewGuess = HighGuess - ((HighGuess - LowGuess) / 2d);
                        modalPremium = NewGuess;
                    }
                }
                calcPremiums.SolvePremium = HighGuess.ToString("{0:C}");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public bool ValidatePremiumMode(SimpleValue mode, int annMo) {
            return (annMo == 1 && mode.DisplayedValue.Equals("Annual", StringComparison.OrdinalIgnoreCase))
                || ((annMo == 1 || annMo == 7) && mode.DisplayedValue.Equals("Semiannual", StringComparison.OrdinalIgnoreCase))
                || ((annMo == 1 || annMo == 4 || annMo == 7 || annMo == 10) && mode.DisplayedValue.Equals("Quarterly", StringComparison.OrdinalIgnoreCase))
                || mode.DisplayedValue.Equals("Monthly (APP)", StringComparison.OrdinalIgnoreCase);
        }
    }
}