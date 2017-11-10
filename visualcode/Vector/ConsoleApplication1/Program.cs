using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vec;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector x = new Vector(new double[] { 1.0, 2.0 });
            Console.WriteLine(x.ToString());
            Vector y = new Vector(new double[] { 3.0, 1.0 });
            Console.WriteLine(y.ToString());
            Vector z = x.Add(y); // (4.0, 3.0)
            Console.WriteLine(z.ToString());
            Vector a = new Vector(2);
            Console.WriteLine(a.ToString());
            a = a.Add(x); // (1.0, 2.0)
            Console.WriteLine(a.ToString());
            Vector b = a.ScalarMultiply(2.0);
            Console.WriteLine(b.ToString());
            Console.ReadKey();
        }
    }
}
