using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeCabanaly;
using Vec;
using Doc;
using System.Xml;

namespace FastDocumentSearch
{
    public class FastDocumentSearcher
    {
        private List<Document> docs;
        private LinkedList<string> kinds;
        private List<List<string>> textana;
        private Vector idfquery;
        private Vector[] textlastquery;
        private List<double> textnorm;
        private Vector[] textquerys;

        public FastDocumentSearcher(List<Document> docs) {
            this.docs = docs;
            this.textana = maketextana(); 
            this.kinds = makekinds();
            this.textquerys = maketextquery();
            this.idfquery = makeidfquery();
            this.textlastquery = makevector();
            this.textnorm = makenorm();
        }

        //検索文章の形態素解析
        public List<List<string>> maketextana()
        {
            List<List<string>> textAna = new List<List<string>>();
            MeCabanalyse mecab = new MeCabanalyse();
            List<string> atext = new List<string>();
            foreach (Document e in this.docs)
            {
                atext = mecab.Analyse(e.Body);
                textAna.Add(atext);
            }

            return textAna;
        }

        //種類ベクトル作成
        public LinkedList<string> makekinds()
        {
            LinkedList<string> kinds = new LinkedList<string>();
            // 種類の動的配列
            for (int i = 0; i < textana[0].Count; i++)
            {
                kinds.AddLast(textana[0][i]);
            }

            foreach (List<string> str in textana)
            {
                for (int j = 0; j < str.Count; j++)
                {
                    if (!(kinds.Contains(str[j])))
                    {
                        kinds.AddLast(str[j]);
                    }
                }
            }
            return kinds;
        }

        //検索文章ベクトルをつくりidf作成
        public Vector makeidfquery()
        {
            Vector idfquery;
            double[] df = new double[textquerys[0].Dimension];
            double[] idf = new double[textquerys[0].Dimension];

            //IDF
            for (int i = 0; i < textquerys[0].Dimension; i++)
            {
                for (int j = 0; j < textquerys.Length; j++)
                {
                    if (textquerys[j].GetValue(i) > 0)
                    {
                        df[i]++;
                    }
                }
                idf[i] = Math.Log(10000 / df[i]);
            }
            idfquery = new Vector(idf);

            return idfquery;
        }

        //検索クエリ
        public Vector[] maketextquery()
        {
            int counttq = 0;
            Vector textqueryvector;
            Vector[] textquerys = new Vector[textana.Count];
            foreach (List<string> str in textana)
            {
                textqueryvector = new Vector(makequery(kinds, str));
                textquerys[counttq] = textqueryvector;
                counttq = counttq + 1;
            }

            return textquerys;
        }

        //検索クエリ作成メソッド
        public double[] makequery(LinkedList<string> kinds, List<string> textana)
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

        //idfからtextquery作成
        public Vector[] makevector(){
        //最終計算前query    
            Vector[] textlastquery = null;
            for (int i = 0; i < textquerys.Length; i++)
            {
                textlastquery[i] = textquerys[i].Mult(idfquery);
            }
            return textlastquery;
        }       

        //ノルム計算
        public List<double> makenorm (){
            List<double> textnorm = new List<double>();
            foreach(Vector tx in textquerys){
                textnorm.Add(tx.Norm());
            }
            return textnorm;
        }
        
        // Cosine計算
        public Dictionary<int, double> Cosine(List<string> kensaku)
        {     
            Dictionary<int, double> Cosresult = new Dictionary<int, double>();   
            Vector searchquery;
            double searchnorm;
            double naiseki;

            //query作成
            searchquery = new Vector(makequery(kinds, kensaku));
            // idf 計算
            searchquery = searchquery.Mult(idfquery);
            //Cosine計算(まだ計算できていない)
            searchnorm = searchquery.Norm();
            for (int i = 0; i < textquerys.Length; i++)
            {
                naiseki = searchquery.InnerProduct(textquerys[i]);
                Cosresult.Add(i,(naiseki / (searchnorm * textnorm[i])));
            } 
            return Cosresult;
        }

        public List<Document> Search(string query) { 
            //string query を vector に直す
            List<string> kensaku = new List<string>();
            Dictionary<int, double> searchValues;
            MeCabanalyse mecab = new MeCabanalyse();
            List<Document> searchResult = new List<Document>();
            kensaku = mecab.Analyse(query);
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
