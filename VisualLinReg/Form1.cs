

using ScottPlot;

namespace VisualLinReg
{
    public partial class Form1 : Form
    {
        Gentrator gen = new Gentrator(a: 2.0, b: 1.0, TrueT: 30, FalseT: 20);


        public Form1()
        {
            InitializeComponent();
            var plt = formsPlot1.Plot;
            List<Dictionary<double, double>> result = gen.Gen();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            foreach (var i in result)
            {
                foreach (var j in i)
                {
                    X.Add(j.Key);
                    Y.Add(j.Value);
                    break;
                }
            }
            var markers = formsPlot1.Plot.Add.Markers(X, Y, MarkerShape.FilledCircle, 5);
            formsPlot1.Refresh();

            /*Learning learn = new Learning(result, 100, 0.01);
            for (int i = 0; i < learn.epohs; i++)
            {
                var py = learn.PredskazanieY();
                var grad = learn.GradientW();
                var WeightsNew = learn.weightsNew();
                var line = formsPlot1.Plot.Add.Line(learn.X[0], learn.YPreds[0], learn.X[1], learn.YPreds[1]);
                formsPlot1.Refresh();

            }*/
        }


    }
}
