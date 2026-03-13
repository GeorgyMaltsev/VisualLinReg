using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualLinReg
{
    internal class Gentrator
    {
        public double a { get; set; }
        public double b { get; set; }
        public int TrueT { get; set; }
        public int FalseT { get; set; }
        public Random rnd1 = new Random();
        public Gentrator(double a, double b, int TrueT, int FalseT)
        {
            this.a = a;
            this.b = b;
            this.TrueT = TrueT;
            this.FalseT = FalseT;
        }
        public List<Dictionary<double, double>> Gen()
        {
            List<Dictionary<double, double>> G = new List<Dictionary<double, double>>();
            double x;
            double y;
            for (int i = 0; i < TrueT; i++)
            {
                Dictionary<double, double> D = new Dictionary<double, double>();
                x = rnd1.NextDouble() + rnd1.Next(0, 20);
                y = a * x + b;
                D.Add(x, y);
                G.Add(D);
            }
            for (int i = 0; i < FalseT; i++)
            {
                Dictionary<double, double> D = new Dictionary<double, double>();
                x = rnd1.NextDouble() + rnd1.Next(0, 20);
                double noise = 4;
                y = a * x + b + (2 * noise * rnd1.NextDouble() - noise);
                D.Add(x, y);
                G.Add(D);
            }
            return G;
        }
    }
}
