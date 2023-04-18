namespace Common.OzApplication {
    public static class Manipulation {
        #region Public Methods
        public static void Swap<T>(this T value, ref T swap) {
            T temp = value;
            value = swap;
            swap = temp;
        }
        #endregion Public Methods
    }
}
