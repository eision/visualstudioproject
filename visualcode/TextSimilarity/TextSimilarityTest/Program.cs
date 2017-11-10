using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextSim;
using Vec;

namespace TextSimilarityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string text1 = "私の名前は、田中です。";
            string text2 = "私の名前は、山田です。";
            TextSimilarity ts = new TextSimilarity(text1, text2);
            double jaccardSim = ts.Jaccard(); // Jaccard係数
            double cosineSim = ts.Cosine(); // コサイン類似度
            Console.WriteLine("Jaccard = {0}",jaccardSim);
            Console.WriteLine("cosine = {0}",cosineSim);
            Console.ReadKey();
        }
    }
}
