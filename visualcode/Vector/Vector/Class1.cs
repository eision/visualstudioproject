using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vec
{
    public class Vector
    {
        private double[] vector;
        private int dimension;

        //ベクトル作成
        public Vector(double[] vector) {
            this.vector = vector;    
        }

        //指定された次元数のゼロベクトルを作成する
        public Vector(int dimension) {
            double[] zero = new double[dimension];
            for (int i = 0; i < dimension; i++) {
                zero[i] = 0.0;
            }
            this.vector = zero;
        }

        //指定された次元のベクトルの値を取り出す
        public double GetValue(int dimension) {
            return vector[dimension + 1];
        }

        // ベクトルを文字列に直す
        public string ToString() {
            string[] stringArray = Array.ConvertAll(this.vector, delegate(double value)
            {
                return value.ToString();
            });
            string csvString = string.Join(",", stringArray); 
            return csvString;
        }

        // ベクトル同士を加算する(vector ver)
        public Vector Add(Vector other) {
            double[] adds = new double[this.vector.Length];
            for (int i = 0; i < this.vector.Length; i++) {
                adds[i] = this.vector[i] + other.vector[i]; 
            }
            return new Vector(adds);
        }

        // ベクトル同士を加算する(double ver)
        public Vector Add(double[] other)
        {
            double[] adds = new double[this.vector.Length];
            for (int i = 0; i < this.vector.Length; i++)
            {
                adds[i] = this.vector[i] + other[i];
            }
            return new Vector(adds);
        }

        // ベクトル同士を減算する(vector ver)
        public Vector Sub(Vector other)
        {
            double[] subs = new double[this.vector.Length];
            for (int i = 0; i < this.vector.Length; i++)
            {
                subs[i] = this.vector[i] - other.vector[i];
            }
            return new Vector(subs);
        }

        // ベクトル同士を減算する(double ver)
        public Vector Sub(double[] other)
        {
            double[] subs = new double[this.vector.Length];
            for (int i = 0; i < this.vector.Length; i++)
            {
                subs[i] = this.vector[i] - other[i];
            }
            return new Vector(subs);
        }

        // ベクトルにスカラ値をかける
        public Vector ScalarMultiply(double d)
        {
            double[] multi = new double[this.vector.Length];
            for (int i = 0; i < this.vector.Length; i++)
            {
                multi[i] = this.vector[i] * d;
            }
            return new Vector(multi);
        }

        // ベクトル同士の内積を計算する
        public double InnerProduct(Vector other)
        {
            double inner = 0.0;
            for (int i = 0; i < this.vector.Length; i++)
            {
                inner += this.vector[i] * other.vector[i];
            }
            return inner;
        }
        
        // ノルムを返す
        public double Norm() 
        {
            double norm = 0;
            for (int i = 0; i<this.vector.Length; i++) {
                norm += this.vector[i] * this.vector[i];
            }
                return Math.Sqrt(norm);
        }

        // Dimension プロパティ
        public int Dimension
        {
            set { this.Dimension = this.vector.Length; }
            get { return this.dimension; }
        }

        // double[] Elements プロパティ
        public double[] Elements
        {
            set { this.Elements = this.vector; }
            get { return this.Elements; }
        }
    }
}
