using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Vec;

namespace TextSim
{
    public class TextSimilarity
    {
        private string text1;
        private string text2;

        //コンストラクタ作成
        public TextSimilarity(string text1, string text2) {
            this.text1 = text1;
            this.text2 = text2;
        }

        //形態素解析
        public string[] Analyse(string str)
        {

            LinkedList<string> resultsurface = new LinkedList<string>();

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

            //[word]以下のノードに含まれる内容をresultsurfaceに記録
            foreach (XmlNode wordNode in wordList)
            {
                foreach (XmlNode resultNode in wordNode.ChildNodes)
                {
                    if (resultNode.Name == "surface")
                    {
                        resultsurface.AddLast(resultNode.InnerText);
                    }
                }
            }

            string[] result = new string[resultsurface.Count];
            int length = resultsurface.Count;
            for (int i = 0; i < length; i++)
            {
                string part = resultsurface.First.Value;
                result[i] = part;
                resultsurface.RemoveFirst();
            }
            return result;
        }

        //queryメソッド
        public double[] makequery(LinkedList<string> kinds, string[] textana)
        {
            LinkedList<double> textquery = new LinkedList<double>();
            foreach (string e in kinds)
            {
               if (textana.Contains(e))
                {
                    textquery.AddLast(1.0);
                }
                else
                {
                    textquery.AddLast(0.0);
                } 
            }
            return textquery.ToArray();
        } 

        public double Jaccard() {
            double Jacresult;
            string[] text1ana = Analyse(this.text1);
            string[] text2ana = Analyse(this.text2);
            double and = 0.0;
            double or = 0.0;
            bool find = false;
            //言葉の一致率
            for (int j = 0; j < text1ana.Length; j++) {
                for (int i = 0; i < text2ana.Length; i++) { 
                    if(text1ana[j] == text2ana[i]){
                        and++;
                        break;
                    }
                }
            }

            //言葉の種類
            for (int j = 0; j < text1ana.Length; j++)
            {
                for (int i = 0; i < text2ana.Length; i++)
                {
                    if (text1ana[j] == text2ana[i])
                    {
                        find = true;
                    }
                }
                if (!(find)) {
                    or++;
                }
                find = false;
            }
            or += text2ana.Length;
            Jacresult = and / or;
                return Jacresult;
        }

        public double Cosine()
        {
            double Cosresult;
            string[] text1ana = Analyse(this.text1);
            string[] text2ana = Analyse(this.text2);
            LinkedList<string> kinds = new LinkedList<string>();
            double naiseki;
            double text1norm;
            double text2norm;

            // 種類の動的配列
            for (int i = 0; i < text1ana.Length; i++) {
                kinds.AddLast(text1ana[i]);
            }

            for (int j = 0; j < text2ana.Length; j++) {
                if (!(kinds.Contains(text2ana[j]))) {
                    kinds.AddLast(text2ana[j]);
                }
            }

            //query作成
            Vector text1query = new Vector(makequery(kinds, text1ana));
            Vector text2query = new Vector(makequery(kinds, text2ana));

            //計算
            naiseki = text1query.InnerProduct(text2query);
            text1norm = text1query.Norm();
            text2norm = text2query.Norm();
            Cosresult = naiseki / (text1norm * text2norm);
            return Cosresult;
        }
    }
}
