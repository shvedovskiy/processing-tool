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
    public partial class Form3 : Form
    {
        private int N = 1000;

        public Form3()
        {
            InitializeComponent();
            DrawGraph();
        }

        public double f_1(double k)
        {
            double A_0 = 100;
            double f_0 = 51;
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        public double f_2(double k)
        {
            double A_0 = 15;
            double f_0 = 5;
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        public double f_3(double k)
        {
            double A_0 = 20;
            double f_0 = 150;
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        public double f(double k)
        {
            return f_1(k) + f_2(k) + f_3(k);
        }

        private void DrawGraph()
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = "Ось Y";

            pane.Title.Text = "Эффект наложения частот";
            pane_2.Title.Text = "Полигармонический процесс";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();

            PointPairList list = new PointPairList();
            PointPairList list_2 = new PointPairList();

            for (double k = 0; k != this.N; k++)
            {
                list.Add(k, f_1(k));
                list_2.Add(k, f(k));
            }

            pane.AddCurve("", list, Color.Blue, SymbolType.None);
            pane_2.AddCurve("", list_2, Color.Blue, SymbolType.None);

            pane.XAxis.Scale.Max = pane_2.XAxis.Scale.Max = this.N;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
        }
    }
}
