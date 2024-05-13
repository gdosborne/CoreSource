/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace OzFramework.Process {
    public static class Extensions {
        public static long UsedMemory(this System.Diagnostics.Process proc) => proc.PrivateMemorySize64;

    }
}
