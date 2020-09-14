using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace AnalysisSt.TechnicalFunc.TradeAnalysis
{
    public class clsTechStats
    {
        public double Tech_Correlation(double[] x, double[] y)
        {
            return MathNet.Numerics.Statistics.Correlation.Pearson(x, y);
        }

        public Tuple<double, double> Tech_FitLine(double[] x, double[] y)
        {
            Tuple<double, double> p = Fit.Line(x, y);
            return p;
        }
    }
}
