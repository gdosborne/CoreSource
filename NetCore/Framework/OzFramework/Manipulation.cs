/* File="Manipulation"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace OzFramework {
    public static class Manipulation {

        public static void Swap<T>(this T value, ref T swap) {
            T temp = value;
            value = swap;
            swap = temp;
        }

    }
}
