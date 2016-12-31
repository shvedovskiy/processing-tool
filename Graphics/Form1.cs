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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PointPairList list = new PointPairList();
            PointPairList list_2 = new PointPairList();
            PointPairList list_3 = new PointPairList();
            for (double x = 0; x <= 100; x += 0.01)
            {
                list.Add(x, f_1(x));
                list_2.Add(x, f_2(x));
                list_3.Add(x, f_3(x));
            }
            drawGraph(linearControl, list, "y = ax + b", xMin: -1, xMax: 105);
            drawGraph(minusLinearControl, list_2, "y = -ax + b", xMin: -1, xMax: 105, yMin: -1, yMax: 200);
            drawGraph(expControl, list_3, "y = ce^(alpha * x)", xMax: 50, yMin: -1, yMax: 200);


            PointPairList list_4 = new PointPairList();
            for (double x = -100; x <= 100; x += 0.01)
            {
                list_4.Add(x, f_4(x));
            }
            drawGraph(minusExpControl, list_4, "y = ce^(-alpha * x)", xMin: -100, xMax: 200, yMax: 50);


            PointPairList list_5 = new PointPairList();
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
            drawGraph(multiTrendControl, list_5, "Multi Trend", yMax: 500);


            PolyGraph file = new PolyGraph(@"E:\олег\Graphics\Graphics\bin\Release\php.dat");
            drawGraph(fileControl, Graph.create_pair_list(file.points, file.points.Length), "From .dat file", xMax: file.points.Length);
            drawGraph(spectreFileControl, Graph.create_pair_list(PolyGraph.spectrum(file.points, file.N), file.points.Length / 2), "Спектр Фурье", xMax: file.points.Length / 2);
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

        private void drawGraph(ZedGraphControl control, PointPairList list, string title, string xTitle="Ось X", string yTitle="Ось Y", int xMin=0, int xMax=1000, int yMin=0, int yMax=0, bool isClear=true) {
            GraphPane pane = control.GraphPane;
            pane.Title.Text = title;
            pane.XAxis.Title.Text = xTitle;
            pane.YAxis.Title.Text = yTitle;

            pane.XAxis.Scale.Min = xMin;
            pane.XAxis.Scale.Max = xMax;
            pane.YAxis.Scale.Min = yMin;
            if (yMax != 0)
            {
                pane.YAxis.Scale.Max = yMax;
            }

            if (isClear)
            {
                pane.CurveList.Clear();
            }
            pane.AddCurve("", list, Color.Blue, SymbolType.None);
            control.AxisChange();
            control.Invalidate();
        }
    }
}