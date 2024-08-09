/* File="Helpers"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Random {
    public static class Helpers {
        private static List<string> randomUsed = new();
        private static System.Random random = new System.Random();

        [Flags]
        public enum Sections {
            Letters = 1,
            Numbers = 2
        }

        public static string GetRandomString(string commonBase, string format = "X4") {
            var result = commonBase;
            var index = random.Next(10000).ToString(format);
            while (true) {
                var temp = $"{result}_{index}";
                if(!randomUsed.Contains(temp)) {
                    result = temp;
                    randomUsed.Add(result);
                    break;
                }
                index = random.Next(01000).ToString(format);
            }
            return result;
        }

        public static string GetRandomString(int length) {
            var val = random.Next(10000);
			var result = new StringBuilder();
            for (int i = 0; i < length; i++) {
                if (i % 2 == 0) {
                    val += random.Next(10000);

				} else {
                    val -= random.Next(10000);
                }
                val = Math.Abs(val);
				var useLetters = val % 2 == 0;
				var index = !useLetters ? random.Next(48, 58) : random.Next(65, 91);
				result.Append((char)index);
            }
            return result.ToString();
		}
	}
}
