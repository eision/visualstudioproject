using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRank
{
    public class PageRankCalculator
    {
        double d;
        int n;

        public PageRankCalculator(double d, int n) {
            this.d = d;
            this.n = n;
        }

        public List<double> Calculate(List<List<int>> links)
        {
            List<List<double>> newlinksA = new List<List<double>>();
            List<double> newlinkA = new List<double>();
            List<double> res = new List<double>();
            double a = 1 - d;
            int linksno = links[0].Count;
            List<double> linksulist = new List<double>();
            int linksuu = 0;

            //何個リンクがあるか
            foreach (List<int> link in links)
            {
                foreach (int linkcontain in link)
                {
                    if (linkcontain > 0)
                    {
                        linksuu++;
                    }
                }
                linksulist.Add(linksuu);
                linksuu = 0;
            }

            //行列 A＝（(1-d)/N * 1） + d * B
            //int linktateno = links.Count;
            //int l = 0;
            //foreach(List<int> link in links){
                //foreach (int linkcontain in link) {
                    //double linkno = linkcontain / linksulist[l] * d;
                    //newlinkA.Add(linkno + (a / linksno));
                 //}
                //l++;
                //newlinksA.Add(newlinkA);
                //newlinkA = new List<double>();
            //}

            //行列 A＝（(1-d)/N * 1） + d * B
            int linktateno = links.Count;
            int l = 0;
            foreach(List<int> link in links){
            foreach (int linkcontain in link) {
            double linkno = linkcontain / linksulist[l] * d;
            newlinkA.Add(linkno);
            }
            l++;
            newlinksA.Add(newlinkA);
            newlinkA = new List<double>();
            }

            //転置行列
            List<List<double>> newlinksA1 = new List<List<double>>();
            List<double> newlinkA1 = new List<double>();
            for (int i = 0; i < linksno; i++)
            {
                for (int j = 0; j < linktateno; j++)
                {
                    newlinkA1.Add(newlinksA[j][i]);
                }
                newlinksA1.Add(newlinkA1);
                newlinkA1 = new List<double>();
            }



            //p n = A^n * p0
            List<double> p0 = new List<double>();
            List<double> p = new List<double>();
            List<double> newlinkP = new List<double>();
            List<List<double>> newlinksP = new List<List<double>>();
            List<double> newlinkN = new List<double>();
            List<List<double>> newlinksN = new List<List<double>>();

            for (int i = 0; i < linktateno; i++)
            {
                for (int j = 0; j < linksno; j++)
                {
                    newlinkP.Add(0.0);
                    newlinkN.Add(0.0);
                }
                newlinksP.Add(newlinkP);
                newlinkP = new List<double>();
                newlinksN.Add(newlinkN);
                newlinkN = new List<double>();
            }

            //p0初期化
            for (int i = 0; i < linksno; i++)
            {
                p0.Add(1.0 / linksno);
            }
            for (int i = 0; i < linksno; i++)
            {
                p.Add(1.0 / linksno);
            }

            newlinksN = newlinksA1;
            //for (int k = 0; k < n; k++)
            //{
                
                //for (int i = 0; i < linktateno; i++)
                //{
                    //for(int t=0;t<linksno;t++){
                        //newlinksP[i][t] = 0.0;
                        //for (int j = 0; j < linksno; j++)
                        //{
                            //newlinksP[i][t] += newlinksN[i][j] * newlinksA1[j][t];
                        //}
                    //}
                //}
                //newlinksN = newlinksP;

            //}


            for (int k = 0; k < n; k++)
            {

                for (int i = 0; i < linktateno; i++)
                {
                    p[i] = 0.0;
                    for (int j = 0; j < linksno; j++)
                    {
                        p[i] += newlinksN[i][j] * p0[j] + a * (1.0 / linksno) / 3;
                    }
                }
                p0 = p;
            }
            //for (int i = 0; i < linktateno; i++)
            //{
               // resno = 0.0;
                //for(int j = 0; j < linksno; j++){
                    //resno += newlinksN[i][j] * p0[j];
                //}
                //res.Add(resno);
            //}
            res = p0;
            return res;
        }
    }
}
