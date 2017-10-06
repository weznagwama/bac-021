using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;

namespace BitcoinThing {
    class Program {
        static void Main(string[] args)
        {

            Key privateKey = new Key();
            PubKey publicKey = privateKey.PubKey;
            Console.WriteLine("Our key is {0}",publicKey);
            Console.ReadLine();
        }
    }
}
