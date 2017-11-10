using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PageRank;

namespace PageRankCalculatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> links = new List<List<int>>();
            links.Add(new List<int>{0, 1, 1});
            links.Add(new List<int>{0, 0, 1});
            links.Add(new List<int>{1, 0, 0});
            double d = 0.85;
            int n = 20; // 20回反復計算する（Power法を想定）
            PageRankCalculator pagerank = new PageRankCalculator(d, n);
            List<double> result = pagerank.Calculate(links);
            var str = String.Join(",", result);
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
