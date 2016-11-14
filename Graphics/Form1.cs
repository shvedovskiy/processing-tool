using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawGraph();
        }

        private double f_1(double x)
        {
            double a = 2.5, b = 2;
            return a * x + b;
        }
        private double f_2(double x)
        {
            double a = 3, b = 200;
            return -a * x + b;
        }
        private double f_3(double x)
        {
            double c = 1.5, alpha = 0.3;
            return Math.Pow((c * Math.E), (alpha * x));
        }
        private double f_4(double x)
        {
            double c = 5000, alpha = 0.0098;
            return Math.Pow((c * Math.E), (alpha * (-x)));
        }

        private void DrawGraph()
        {
            PolyGraph file = new PolyGraph(@"php.dat");

            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;
            GraphPane pane_5 = zedGraphControl5.GraphPane;
            GraphPane pane_6 = zedGraphControl6.GraphPane;
            GraphPane pane_7 = zedGraphControl7.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = pane_5.XAxis.Title.Text = pane_6.XAxis.Title.Text = "X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_3.YAxis.Title.Text = pane_4.YAxis.Title.Text = pane_5.YAxis.Title.Text = pane_6.YAxis.Title.Text = "Y";

            pane.Title.Text = "y = ax + b";
            pane_2.Title.Text = "y = -ax + b";
            pane_3.Title.Text = "y = ce^(alpha * x)";
            pane_4.Title.Text = "y = ce^(-alpha * x)";
            pane_5.Title.Text = "Multi Trend";
            pane_6.Title.Text = "From .dat file";
            pane_7.Title.Text = "Спектр Фурье";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();
            pane_5.CurveList.Clear();
            pane_6.CurveList.Clear();
            pane_7.CurveList.Clear();

            PointPairList list = new PointPairList();
            PointPairList list_2 = new PointPairList();
            PointPairList list_3 = new PointPairList();
            PointPairList list_4 = new PointPairList();
            PointPairList list_5 = new PointPairList();

            double xmin = 0;
            double xmax = 100;

            double xmin_limit   = -1;
            double xmax_limit   = 105;
            double x_3max_limit = 50;
            double x_4max_limit = 200;
            double x_4min_limit = -100;
            double ymin_limit   = -1;
            double ymax_limit   = 200;
            double y_4max_limit = 50;

            for (double x = xmin; x <= xmax; x += 0.01)
            {
                list.Add(x, f_1(x));
                list_2.Add(x, f_2(x));
                list_3.Add(x, f_3(x));
            }
            for (double x = -100; x <= xmax; x += 0.01)
            {
                list_4.Add(x, f_4(x));
            }

            for (int i = 0; i != 250; ++i)
            {
                list_5.Add(Convert.ToDouble(i), Convert.ToDouble(i));
            }
            for (int i = 250; i != 500; ++i)
            {
                list_5.Add(Convert.ToDouble(i), 321 * Math.Exp(-0.001 * i));
            }
            for (int i = 500; i != 750; ++i)
            {
                list_5.Add(Convert.ToDouble(i), 119 * Math.Exp(0.001 * i));
            }
            for (int i = 750; i != 1000; ++i)
            {
                list_5.Add(Convert.ToDouble(i), -1 * i + 1002);
            }

            pane.AddCurve("", list, Color.Blue, SymbolType.None);
            pane_2.AddCurve("", list_2, Color.Blue, SymbolType.None);
            pane_3.AddCurve("", list_3, Color.Blue, SymbolType.None);
            pane_4.AddCurve("", list_4, Color.Blue, SymbolType.None); 
            pane_5.AddCurve("", list_5, Color.Blue, SymbolType.None);
            pane_6.AddCurve("", Graph.create_pair_list(file.points, file.points.Length), Color.Red, SymbolType.None);
            pane_7.AddCurve("", Graph.create_pair_list(file.spectrum(), file.points.Length / 2), Color.Red, SymbolType.None);

            pane.XAxis.Scale.Min = pane_2.XAxis.Scale.Min = xmin_limit;
            pane.XAxis.Scale.Max = pane_2.XAxis.Scale.Max = xmax_limit;
            pane_3.XAxis.Scale.Max = x_3max_limit;
            pane_6.XAxis.Scale.Max = file.points.Length;
            pane_7.XAxis.Scale.Max = file.points.Length / 2;
            pane_4.XAxis.Scale.Min = x_4min_limit;
            pane_4.XAxis.Scale.Max = x_4max_limit;

            pane_2.YAxis.Scale.Min = pane_3.YAxis.Scale.Min = ymin_limit;
            pane_2.YAxis.Scale.Max = pane_3.YAxis.Scale.Max = ymax_limit;
            pane_4.YAxis.Scale.Max = y_4max_limit;   

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();
            zedGraphControl6.AxisChange();
            zedGraphControl7.AxisChange();

            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
            zedGraphControl5.Invalidate();
            zedGraphControl6.Invalidate();
            zedGraphControl7.Invalidate();
        }
    }
}