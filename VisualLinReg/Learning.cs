using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualLinReg
{
    internal class Learning
    {
        public int epohs;
        public double LS;
        public double[,] weights;
        public double[] loss_history;
        public List<Dictionary<double, double>> start;
        public double bias;
        public List<double> X;
        public List<double> Y;
        public List<double> YPreds;
        public double[,] Grads;

        public Learning(List<Dictionary<double, double>> start, int epohs, double LS)
        {
            weights = new double[1, 1];
            this.start = start;
            this.epohs = epohs;
            this.LS = LS;
            foreach (var i in start)
            {
                foreach (var j in i)
                {
                    X.Add(j.Key);
                    Y.Add(j.Value);
                    break;
                }
            }
        }
        public List<Dictionary<double, double>> PredskazanieY()
        {
            List<Dictionary<double, double>> P = new List<Dictionary<double, double>>();
            int stepen = weights.GetUpperBound(0) + 1;
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            foreach (var i in start)
            {
                foreach (var j in i)
                {
                    X.Add(j.Key);

                    break;
                }
            }

            for (int i = 0; i < X.Count; i++)
            {
                Dictionary<double, double> D = new Dictionary<double, double>();
                double x = X[i];
                double y = 0;

                for (int s = 1; s <= stepen; s++)
                {
                    double x_step = 1;
                    for (int j = 1; j <= s; j++)
                    {
                        x_step *= x;
                    }
                    y += x_step * weights[s - 1, 0];
                }
                y += bias;
                D.Add(x, y);
                P.Add(D);
            }
            YPreds.Clear();
            foreach (var i in P)
            {
                foreach (var j in i)
                {
                    YPreds.Add(j.Value);

                    break;
                }

            }
            return P;
        }

        public double[,] GradientW()
        {
            int length = weights.Length;
            var a = PredskazanieY();
            double[,] YPredsmassiv = new double[1, YPreds.Count()];
            int countYP = 0;
            foreach (var ypred in YPreds)
            {
                YPredsmassiv[0, countYP] = ypred;
                countYP++;
            }
            double[,] Ysmassiv = new double[1, Y.Count()];
            int countY = 0;
            foreach (var y in Y)
            {
                YPredsmassiv[0, countY] = y;
                countY++;
            }

            Grads = MatricaXDouble(MatricaXMatrica(T(X), MatricaMinus(YPredsmassiv, Ysmassiv)), 2 / X.Count());

            return Grads;

        }

        public double[,] weightsNew()
        {
            weights = MatricaMinus(weights, GradientW());
            return weights;
        }
        double[,] T(List<double> X)
        {
            double[,] XT = new double[X.Count, 1];
            int count = 0;
            foreach (var x in X)
            {
                XT[count, 0] = x;
                count++;
            }
            return XT;
        }

        double[,] MatricaSum(double[,] X1, double[,] X2)
        {
            double[,] Result = new double[X1.GetUpperBound(0) + 1, X2.GetUpperBound(1) + 1];
            for (int i = 0; i < X1.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < X2.GetUpperBound(1) + 1; j++)
                {
                    Result[i, j] = X1[i, j] + X2[i, j];
                }
            }
            return Result;
        }

        double[,] MatricaMinus(double[,] X1, double[,] X2)
        {
            double[,] Result = new double[X1.GetUpperBound(0) + 1, X2.GetUpperBound(1) + 1];
            for (int i = 0; i < X1.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < X2.GetUpperBound(1) + 1; j++)
                {
                    Result[i, j] = X1[i, j] - X2[i, j];
                }
            }
            return Result;
        }

        double[,] MatricaXDouble(double[,] X1, double a)
        {
            double[,] Result = new double[X1.GetUpperBound(0) + 1, X1.GetUpperBound(1) + 1];
            for (int i = 0; i < X1.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < X1.GetUpperBound(1) + 1; j++)
                {
                    Result[i, j] = X1[i, j] * a;
                }
            }
            return Result;
        }

        double[,] MatricaXMatrica(double[,] X1, double[,] X2)
        {
            double[,] Result = new double[X1.GetUpperBound(0) + 1, X2.GetUpperBound(1) + 1];

            for (int i = 0; i < X1.GetUpperBound(0) + 1; i++)
            {//идём по строкам Х1
                for (int j = 0; j < X1.GetUpperBound(1) + 1; j++)
                {//Идём по столбцам Х2
                    double xij = 0;
                    for (int ix1 = 0; ix1 < X1.GetUpperBound(1) + 1; ix1++)
                    {//идём по элементам строки из Х1
                        for (int ix2 = 0; ix2 < X1.GetUpperBound(0) + 1; ix2++)
                        {//идём по элементам столбца из Х2
                            xij += X1[i, ix1] * X2[j, ix2];
                        }
                    }
                    Result[i, j] = xij;
                }
            }
            return Result;
        }
    }
}
