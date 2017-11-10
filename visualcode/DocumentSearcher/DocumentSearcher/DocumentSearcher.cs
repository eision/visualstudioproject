using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doc;
using System.Net;
using System.IO;
using System.Xml;
using Vec;

namespace DocumentSearch
{
    /// <summary>
    /// okです
    /// [maenishi]
    /// </summary>
    public class DocumentSearcher
    {
        private List<Document> docs;

        public DocumentSearcher(List<Document> docs) {
            this.docs = docs;
        }

        //形態素解析
        public List<string> Analyse(string str)
        {

            List<string> resultsurface = new List<string>();

            //解析する文字列はURLエンコードする
            String postString = String.Format("appid=dj00aiZpPW4zVzlxV2lWUXdXWCZzPWNvbnN1bWVyc2VjcmV0Jng9OTY-&sentence=" + str);

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

            XmlNode posNode;
            XmlNode surfaceNode;
            //[word]以下のノードに含まれる内容をresultsurfaceに記録
            foreach (XmlNode wordNode in wordList)
            {
                posNode = wordNode["pos"];
                surfaceNode = wordNode["surface"];
                    if ((posNode.InnerText == "名詞") || ((posNode.InnerText == "動詞") || (posNode.InnerText == "形容詞")))
                    {
                        resultsurface.Add(surfaceNode.InnerText);
                    }
            }
            //string[] result = new string[resultsurface.Count];
            //int length = resultsurface.Count;
            //for (int i = 0; i < length; i++)
            //{
                //string part = resultsurface.First.Value;
                //result[i] = part;
                //resultsurface.RemoveFirst();
            //}
            return resultsurface;
        }

        //クエリ作成
        public double[] makequery(List<string> kinds, List<string> textana)
        {
            LinkedList<double> textquery = new LinkedList<double>();
            double num;

            foreach (string e in kinds)
            {
                num = 0;
                for (int i = 0; i < textana.Count; i++)
                {
                    if (textana[i] == e)
                    {
                        num++;
                    }
                }
                textquery.AddLast(num);
                num = 0;
            }
            return textquery.ToArray();
        } 

        // Cosine計算
        public Dictionary<int, double> Cosine(List<string> kensaku)
        {
            Dictionary<int, double> Cosresult = new Dictionary<int, double>(); ;
            int count = 0;
            List<Vector> textquery = new List<Vector>();
            Vector idfquery;
            Vector searchquery;
            double[] df;
            double[] idf;
            double searchnorm;
            double textnorm = 0;
            List<string> textana = new List<string>();
            double naiseki;
            List<List<string>> textanalist = new List<List<string>>();
            List<string> kinds = new List<string>(); 
            List<string> words = new List<string>();
            List<Vector> Lasttextquery = new List<Vector>();

            foreach (Document e in this.docs)
            {
                textana = Analyse(e.Body);
                textanalist.Add(textana);
                count++;
            }

            // 種類の動的配列
            for (int i = 0; i < textanalist.ElementAt(0).Count; i++)
            {
                if (textanalist.ElementAt(0).ElementAt(i).Length > 0)
                {
                    kinds.Add(textanalist.ElementAt(0).ElementAt(i));
                }
            }

            foreach (List<string> str in textanalist)
            {      
                for (int j = 0; j < str.Count; j++)
                {
                    if (!(kinds.Contains(str[j])))
                    {
                        kinds.Add(str[j]);
                    }
                }
            }
            
            count = 0;
            //query作成
            foreach (List<string> str in textanalist)
            {
                textquery.Add(new Vector(makequery(kinds, str)));
                count++;
            }
            searchquery = new Vector(makequery(kinds, kensaku));

            //IDF
            df = new double[kinds.Count];
            idf = new double[kinds.Count];
            for (int i = 0; i < kinds.Count; i++)
            {
                df[i] = 0.0;
                for (int j = 0; j < textquery.Count; j++) {
                    if (textquery.ElementAt(j).GetValue(i+1) > 0)
                    {
                        df[i]++;
                    }
                }
                idf[i] = Math.Log(100 / df[i]);
            }
            idfquery = new Vector(idf);

            //最終計算前query    
            for(int i=0;i<textquery.Count;i++){
                Lasttextquery.Add(textquery.ElementAt(i).Mult(idfquery));
            }
            searchquery = searchquery.Mult(idfquery);

            //Cosine計算(まだ計算できていない)
            searchnorm = searchquery.Norm();
            for (int i = 0; i < Lasttextquery.Count; i++)
            {
                naiseki = searchquery.InnerProduct(Lasttextquery.ElementAt(i));
                textnorm = Lasttextquery.ElementAt(i).Norm();
                Cosresult.Add(i,(naiseki / (searchnorm * textnorm)));
            } 
            return Cosresult;
        }

        public List<Document> Search(string query) { 
            //string query を vector に直す
            List<string> kensaku = new List<string>();
            Dictionary<int, double> searchValues;
            List<Document> searchResult = new List<Document>(); 
            kensaku = Analyse(query);
            //検索queryと this.bodyのCosine計算
            searchValues = Cosine(kensaku);
            //計算値のcosが大きいものから順に並べる
            var vs1 = searchValues.OrderBy((x) => x.Value);
            foreach (var v in vs1)
            {
                searchResult.Add(this.docs[v.Key]);
            }
            //並べたDocumentを返す
            return searchResult;
        }
    }
}
