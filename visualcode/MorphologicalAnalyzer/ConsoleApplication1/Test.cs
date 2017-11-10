using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Morphemename;
using enshushoki2;

namespace MorphologicalAnalyzerTest
{

    /// <summary>
    /// よくできています。
    /// 
    /// [fukuchi]
    /// </summary>

    class test
    {
        public string appid = "dj00aiZpPW4zVzlxV2lWUXdXWCZzPWNvbnN1bWVyc2VjcmV0Jng9OTY-";
        static void Main(string[] args)
        {
            string str = "今日はいい天気です";
            MorphologicalAnalyzer analyser = new MorphologicalAnalyzer();
            Morpheme[] morphemes = analyser.Analyse(str);
            foreach (Morpheme morph in morphemes)
            {
                Console.WriteLine(morph.Tostring());
            }
            Console.ReadKey();
            ////解析する文字列はURLエンコードする
            //String postString = String.Format("appid=dj00aiZpPW4zVzlxV2lWUXdXWCZzPWNvbnN1bWVyc2VjcmV0Jng9OTY-&sentence=今日はいい天気です");

            ////UTF8でバイト配列にエンコードする
            //byte[] postData = Encoding.UTF8.GetBytes(postString);

            ////Webリクエストを生成する
            //WebRequest webReq = WebRequest.Create("http://jlp.yahooapis.jp/MAService/V1/parse");
            //webReq.Method = "POST";
            //webReq.ContentType = "application/x-www-form-urlencoded";
            //webReq.ContentLength = postData.Length;

            ////Postするデータを出力する
            //using (Stream writer = webReq.GetRequestStream())
            //{
            //    writer.Write(postData, 0, postData.Length);
            //}

            ////結果をうけとってDOMオブジェクトにする
            //WebResponse webRes = webReq.GetResponse();

            //XmlDocument resultXml = new XmlDocument();

            //using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
            //{
            //    resultXml.Load(reader);
            //}

            ////結果XML中の[word]タグのリストを取得する
            //XmlNodeList wordList = resultXml.GetElementsByTagName("word");

            ////[word]以下のノードに含まれる内容をコンソールに出力する
            //foreach (XmlNode wordNode in wordList)
            //{
            //    foreach (XmlNode resultNode in wordNode.ChildNodes)
            //    {
            //        if (resultNode.Name == "pos" || resultNode.Name == "surface")   
            //        Console.WriteLine("node_name:{0} value:{1}", resultNode.Name, resultNode.InnerText);
            //    }   
            //}
            //        Console.ReadKey();
        }
    }
}
