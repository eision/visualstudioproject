using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastDocumentSearch;
using Doc;
using System.Xml; 

namespace FastDocumentSearcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("kyoto_results_100.xml");

            //Create an XmlNamespaceManager for resolving namespaces.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "urn:samples");

            //Select and display the value of all the ISBN attributes.
            XmlElement xml = doc.DocumentElement;
            XmlNodeList nodes = xml.SelectNodes("//document", nsmgr); //SelectNodesメソッドでXPathを利用できる．
            List<Document> docs = new List<Document>();

            foreach (XmlNode node in nodes)
            {
                string id = node.Attributes[0].Value; //XmlNodeオブジェクトの属性値を取得するには，Attirbutesプロパティを利用する
                string title = node.ChildNodes[0].InnerText;
                string body = node.ChildNodes[1].InnerText;
                docs.Add(new Document(id, title, body)); // 文書群作成
            }
            FastDocumentSearcher ds = new FastDocumentSearcher(docs);

            //　検索
            Console.WriteLine("検索クエリ：");

            string query = Console.ReadLine(); //様々なクエリを試してみよ 余裕があれば，形態素やbi-gramで得られていない単語が入力された際の処理を工夫してみよ．
            List<Document> rankedDocs = ds.Search(query);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(rankedDocs[i].Title);
            }
            Console.ReadKey();
        }
    }
}
