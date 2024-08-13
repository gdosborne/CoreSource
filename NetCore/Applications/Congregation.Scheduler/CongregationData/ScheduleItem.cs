namespace CongregationData {
    public abstract class ScheduleItem : DomainItem {

        #region Date Property
        private DateTime _Date = default;
        public DateTime Date {
            get => _Date;
            set {
                _Date = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Member Property
        private Member _Member = default;
        public Member Member {
            get => _Member;
            set {
                _Member = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
