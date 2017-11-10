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
namespace enshushoki2
{
    public class MorphologicalAnalyzer
    {
        private string name;
        public string appid = "dj00aiZpPW4zVzlxV2lWUXdXWCZzPWNvbnN1bWVyc2VjcmV0Jng9OTY-";
        public MorphologicalAnalyzer()
        {
            this.name = "MorphologicalAnalyzer";
        }

        public Morpheme[] Analyse(string str) {

            LinkedList<string> resultpos = new LinkedList<string>();
            LinkedList<string> resultsurface = new LinkedList<string>();

            //解析する文字列はURLエンコードする
            String postString = String.Format("appid=dj00aiZpPW4zVzlxV2lWUXdXWCZzPWNvbnN1bWVyc2VjcmV0Jng9OTY-&sentence="+str);
           
            //UTF8でバイト配列にエンコードする
            byte[] postData = Encoding.UTF8.GetBytes(postString);

            //Webリクエストを生成する
            WebRequest webReq = WebRequest.Create("http://jlp.yahooapis.jp/MAService/V1/parse");
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentLength = postData.Length;

            //Postするデータを出力する
            using (Stream writer = webReq.GetRequestStream())
            {
                writer.Write(postData, 0, postData.Length);
            }

            //結果をうけとってDOMオブジェクトにする
            WebResponse webRes = webReq.GetResponse();

            XmlDocument resultXml = new XmlDocument();

            using (StreamReader reader = new StreamReader(webRes.GetResponseStream()))
            {
                resultXml.Load(reader);
            }

            //結果XML中の[word]タグのリストを取得する
            XmlNodeList wordList = resultXml.GetElementsByTagName("word");

            //[word]以下のノードに含まれる内容をmorphemeに記録
            foreach (XmlNode wordNode in wordList)
            {
                foreach (XmlNode resultNode in wordNode.ChildNodes)
                {
                    if (resultNode.Name == "pos")
                    {
                        resultpos.AddLast(resultNode.InnerText);      
                    }
                    else if (resultNode.Name == "surface")
                    {
                        resultsurface.AddLast(resultNode.InnerText);
                    }
                }
            }
            Morpheme[] result = new Morpheme[resultsurface.Count];
            int length = resultsurface.Count;
            for (int i = 0; i < length; i++) {
                Morpheme mor = new Morpheme(resultsurface.First.Value, resultpos.First.Value);
                result[i] = mor;
                resultpos.RemoveFirst();
                resultsurface.RemoveFirst();
            }
                return result;  
        }
    }
}
