using System;

namespace Common.OzApplication {
    public class ThickFlagAttribute : Attribute {
    }

    public abstract class ThickEnum<T> where T : IComparable {
        protected ThickEnum(T value) {
            Value = value;
            if (long.TryParse(value.ToString(), out var test)) {
                FlagValue = Convert.ToInt64(test);
            }
        }

        private long FlagValue { get; }
        public T Value { get; protected set; }

        public bool HasFlag(ThickEnum<T> flag) {
            if (!GetType().IsDefined(typeof(ThickFlagAttribute), true)) {
                return false;
            }

            if (long.TryParse(flag.Value.ToString(), out var test)) {
                return (FlagValue | test) == test;
            }

            return false;
        }
    }
}