using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vec;
using NN;

namespace NearestNeighborTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vector> positives = new List<Vector>();
            List<Vector> negatives = new List<Vector>();
            // Vectorの各要素 0:無料 1:出会い 2:登録 3:参加 4:講義
            positives.Add(new Vector(new double[] { 1.0, 0, 0, 1.0, 1.0 }));
            negatives.Add(new Vector(new double[] { 2.0, 1.0, 1.0, 1.0, 0 }));
            NearestNeighbor nearestNeighbor = new NearestNeighbor(positives, negatives);
            int result = nearestNeighbor.Classify(new Vector(new double[] { 1.0, 1.0, 1.0, 0, 0 }), 1);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
