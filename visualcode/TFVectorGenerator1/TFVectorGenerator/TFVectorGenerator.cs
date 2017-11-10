using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enshu4
{
    /// <summary>
    /// よくできています
    /// [maenishi]
    /// </summary>
    public class TFVectorGenerator
    {
        private string name;
        public TFVectorGenerator() 
        {
            this.name = "TFVectorGenerator1";
        }
        public Dictionary<string, int> Generate(string str) 
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '?', '!' };
            Dictionary<string, int> data = new Dictionary<string, int>();
            string[] words = str.Split(delimiterChars);

            foreach (string s in words)
            {
                string s1 = s.ToLower();
                if (!data.ContainsKey(s1)&& s1.Length > 0)
                {
                    var matchQuery = from word in words
                                     where word.ToLowerInvariant() == s1.ToLowerInvariant()
                                     select word;
                    int wordCount = matchQuery.Count();
                    data.Add(s1, wordCount);
                }
            }
            return data;
        }  
    }
}
