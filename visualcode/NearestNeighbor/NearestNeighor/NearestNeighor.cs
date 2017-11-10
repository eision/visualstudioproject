using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vec;

namespace NN
{
    public class NearestNeighbor
    {
        private List<Vector> positives;
        private List<Vector> negatives;

        public NearestNeighbor(List<Vector> positives, List<Vector> negatives){
            this.positives = positives;
            this.negatives = negatives;
        }

        //分類メソッド
        public int Classify(Vector data, int k) {

            Vector Subvector;
            double Yulength;
            List<double> posiYulengths = new List<double>();
            List<double> negaYulengths = new List<double>(); 
            List<List<double>> YulengthsList = new List<List<double>>();
            int posicount = 0;
            int negacount = 0;
            bool error = ((this.negatives.Count + this.positives.Count) > k);

            // ユークリッド距離を計り、ソート
            foreach(Vector posi in this.positives){
                Subvector = data.Sub(posi);
                Yulength = Subvector.Norm();
                posiYulengths.Add(Yulength);
            }
            posiYulengths.Sort();
            YulengthsList.Add(posiYulengths);
            foreach (Vector nega in this.negatives)
            {
                Subvector = data.Sub(nega);
                Yulength = Subvector.Norm();
                negaYulengths.Add(Yulength);
            }
            negaYulengths.Sort();
            YulengthsList.Add(negaYulengths);

            //評価
            int ps = 0;
            int ng = 0;
            for (int i = 0; i < k; i++)
            {
                if ((ng + 1) <= this.negatives.Count)
                {
                    if ((ps + 1) <= this.negatives.Count)
                    {
                        if (YulengthsList[1][ng] < YulengthsList[0][ps])
                        {
                            negacount++;
                            ng++;
                        }
                        else
                        {
                            posicount++;
                            ps++;
                        }
                    }
                    else
                    {
                        negacount++;
                        ng++;
                    }
                } else {
                    posicount++;
                    ps++;
                }
            }

            //分類結果
            if (error)
            {
                if (posicount >= negacount)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }

        }
    }
}
