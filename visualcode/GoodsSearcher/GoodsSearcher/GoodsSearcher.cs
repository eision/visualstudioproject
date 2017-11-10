using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Searcher
{
    /// <summary>
    /// よくできています
    /// 提出する際には、実行時のファイルがあるプロジェクトを
    /// 「スタートアップ　プロジェクトに設定」しておいてください
    /// [maenishi]
    /// </summary>
    public class GoodsSearcher
    {
        public string query;
        public GoodsSearcher(string query){
            this.query = query;
        }

        public Dictionary<string, int> Search(int num) { 
            LinkedList<string> resultname = new LinkedList<string>();
            LinkedList<string> resultprice = new LinkedList<string>();

            //解析する文字列はURLエンコードする
            String postString = String.Format("applicationId=1099193874621330177&format=xml&keyword=ノートパソコン&hits=10");
           
            //UTF8でバイト配列にエンコードする
            byte[] postData = Encoding.UTF8.GetBytes(postString);

            //Webリクエストを生成する
            WebRequest webReq = WebRequest.Create("https://app.rakuten.co.jp/services/api/IchibaItem/Search/20170706?");
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
            XmlNodeList wordList = resultXml.GetElementsByTagName("Items");

            //[word]以下のノードに含まれる内容をmorphemeに記録
            foreach (XmlNode wordNodes in wordList)
            {   
                foreach (XmlNode wordNode in wordNodes)
                {
                    foreach (XmlNode resultNode in wordNode.ChildNodes)
                    {

                        if (resultNode.Name == "itemName")
                        {
                            resultname.AddLast(resultNode.InnerText);
                        }
                        else if (resultNode.Name == "itemPrice")
                        {
                            resultprice.AddLast(resultNode.InnerText);
                        }
                    }
                }
            }
            Dictionary<string, int> search = new Dictionary<string, int>();
            int length = resultname.Count;
            for (int i = 0; i < length; i++) {
                search.Add(resultname.First.Value, Int32.Parse(resultprice.First.Value));
                resultname.RemoveFirst();
                resultprice.RemoveFirst();
            }
                return search;  
        }

        public string Query
        {
            set { this.query = Query; }
            get { return this.query; }
        }

    }
}
