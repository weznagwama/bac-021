using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTest {

    class Cookie {
        string dough;
        string filling;
        string flavour;
        string name;

        public Cookie(string owner) {
            name = owner;
            Console.WriteLine("You just baked a new cookie for {0}!", owner);

        }

        public void AddFlavour(string topping) {
            Console.WriteLine("Your cookie added flavour {0}", topping);
            flavour = topping;
        }

        public void AddDough(string newdough) {
            Console.WriteLine("Your dough added was {0}", newdough);
            dough = newdough;
        }

        public void AddFilling(string newfilling) {
            Console.WriteLine("Your new filling was {0}", newfilling);
            filling = newfilling;
        }

        public void GetCookie() {
            Console.WriteLine("{3} cookie flavour is {0} and new dough is {1} and new filling is {2}",
            flavour, dough, filling, name);
        }
    }

    class Program {
        static void Main(string[] args) {

            string[] names = { "Tristan", "clare", "nick", "alex", "david" };
            string[] flavours = {"choc chip", "blueberyy", "bourbon", "double chocolate", "caramel" };
            string[] doughs = {"white", "premix", "icecream", "cocacola", "ben n jerrys" };
            string[] fillings = { "m and ms", "sprinkles", "snickers", "mars bar", "malteres"};
            int amount = flavours.Length;
            for (int i = 0; i<(flavours.Length-1); i++) {
                Cookie yummies = new Cookie(names[i]);
                yummies.AddFlavour(flavours[i]);
                yummies.AddDough(doughs[i]);
                yummies.AddFilling(fillings[i]);
                Console.WriteLine();
                yummies.GetCookie();
                Console.WriteLine();
            }
            Console.ReadLine();

        }
    }
}
