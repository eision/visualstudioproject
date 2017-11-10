using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searcher;

namespace researchTest
{
    class researchTest
    {
        static void Main(string[] args)
        {
            string query = "ノートパソコン"; //検索クエリ
            int num = 10; // 検索件数
            int count = 1;
            GoodsSearcher searcher = new GoodsSearcher(query);
            Dictionary<string, int> result = searcher.Search(num);
            Console.WriteLine(result);
            foreach (KeyValuePair<string, int> pair in result)
            {
                Console.WriteLine(string.Format("{0}\n商品名：{1}\n値段:{2}\n", count, pair.Key, pair.Value));
                count++;
            }
            Console.ReadKey();
        }
    }
}
