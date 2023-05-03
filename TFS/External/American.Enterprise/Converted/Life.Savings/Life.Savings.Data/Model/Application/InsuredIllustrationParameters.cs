using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model.Application {
    public class InsuredIllustrationParameters {
        public InsuredIllustrationParameters() {
            Riders = new List<InsuredRiderIllustrationParameters> {
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters(),
                new InsuredRiderIllustrationParameters()
            };
        }
        public bool ChildRider { get; set; }
        public bool SpouseRider { get; set; }
        public int AgeYoungestChild { get; set; }
        public int SpouseAge { get; set; }
        public double ChildBenefitAmount { get; set; }
        public double SpouseBenefitAmount { get; set; }
        public SimpleValue SpousePlanType { get; set; }
        public SimpleValue SpouseSubstandardRating { get; set; }
        public SimpleValue SpouseCola { get; set; }
        public SimpleValue SpouseYearToAge { get; set; }
        public IList<InsuredRiderIllustrationParameters> Riders { get; private set; }
    }
    public class InsuredRiderIllustrationParameters {
        public bool IsSelected { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double DeathBenefitAmount { get; set; }
        public SimpleValue PlanType { get; set; }
        public SimpleValue SubstandardRate { get; set; }
        public SimpleValue Cola { get; set; }
        public int StartYearOrAge { get; set; }
        public int EndYearOrAge { get; set; }
        public SimpleValue YearsToAges { get; set; }
    }
}
