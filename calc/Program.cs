using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Generate set = new Generate();
            while (true)
            {
                string ex = Console.ReadLine();
                Console.WriteLine(set.run(ex));
            }
        }
    }
}
