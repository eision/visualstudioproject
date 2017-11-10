using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Morphemename;
using MeCabMorphologicalAna;

namespace MeCabtest
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "今日はいい天気です";
            MeCabMorphologicalAnalyzer analyser = new MeCabMorphologicalAnalyzer();
            List<Morpheme> result = analyser.Analyse(str);
            foreach (Morpheme word in result)
            {
                Console.WriteLine(word.Tostring());
            }
            Console.ReadKey();
        }
    }
}
