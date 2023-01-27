namespace Common.Applicationn.Process {
    public static class Extensions {
        public static long UsedMemory(this System.Diagnostics.Process proc) => proc.PrivateMemorySize64;
    }
}
