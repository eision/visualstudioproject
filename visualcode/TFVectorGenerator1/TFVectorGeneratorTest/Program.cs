using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enshu4;

namespace TFVectorGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "To be, or not to be";
            TFVectorGenerator generator = new TFVectorGenerator();
            Dictionary<string, int> result = generator.Generate(str);
            Console.WriteLine(result);
            foreach (KeyValuePair<string, int> pair in result)
            {
                Console.WriteLine(string.Format("{0},{1}", pair.Key, pair.Value));
            }
            Console.ReadKey();
        }
    }
}
