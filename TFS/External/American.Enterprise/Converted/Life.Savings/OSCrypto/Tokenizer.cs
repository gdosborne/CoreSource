using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCrypto {
    public class Tokenizer {
        private static string _Password = "0sBorn3S0ftw4Re;";
        private static int _IV = 65412;
        public Tokenizer() : this(_Password, _IV) { }
        public Tokenizer(string password) : this(password, _IV) { }
        public Tokenizer(string password, int iv) {
            _Password = password;
            _IV = iv;
        }
        public long GetToken(string original) {
            long result = _IV;
            var data = Encoding.Unicode.GetBytes(_Password + original);
            if (data.Length < 64)
                data = new byte[64 - data.Length].Concat(data).ToArray();
            else if (data.Length > 64)
                data = data.Take(64).ToArray();
            for (int i = 0; i < data.Length; i++)
            {
                result = ApplyRule(result, data[i], i);
            }
            return result;
        }
        private long ApplyRule(long current, byte value, int index) {
            long result = 0;
            if((index % 5) == 0)
                result = current - value;
            else if((index % 3) == 0)
                result = current ^ value;
            else if ((index % 2) == 0)
                result = current * value;
            else
                result = current + value;
            return Math.Abs(result);
        }
    }
}
