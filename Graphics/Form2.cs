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
    public partial class Form2 : Form
    {
        private int S = 100;
        private int N = 1000;

        public Form2()
        {
            InitializeComponent();
            DrawGraph(this.S, this.N);
        }
        private void SInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
                e.Handled = true;
        }
        private void SInputButton_Click(object sender, EventArgs e)
        {
            S = Convert.ToInt32(SInput.Text);
            DrawGraph(S, N);
        }
        private void SpikesInput_Rand_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList newList = new PointPairList();
            Random rand = new Random();
            CurveItem curve = pane.CurveList[0];

            for (int i = 0; i != 5; ++i)
            {
                int var = rand.Next(100, curve.Points.Count + 1);
                curve.Points[var].Y = curve.Points[var].Y * 15;
            }

            for (int i = 0; i != curve.Points.Count; ++i)
            {
                newList.Add(curve.Points[i].X, curve.Points[i].Y);
            }
            curve.Points = newList;

            pane.YAxis.Scale.Min = -S * 10;
            pane.YAxis.Scale.Max = S * 10;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void SpikesDelete_Rand_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList newList = new PointPairList();
            Random rand = new Random();
            CurveItem curve = pane.CurveList[0];

            for (int i = 0; i != curve.Points.Count; ++i)
            {
                if (Math.Abs(curve.Points[i].Y) > S)
                {
                    curve.Points[i].Y = (curve.Points[i - 1].Y + curve.Points[i + 1].Y) / 2;
                }
            }
            for (int i = 0; i != curve.Points.Count; ++i)
            {
                newList.Add(curve.Points[i].X, curve.Points[i].Y);
            }
            curve.Points = newList;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void SpikesInput_Poly_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl6.GraphPane;
            PointPairList newList = new PointPairList();
            Random rand = new Random();
            CurveItem curve = pane.CurveList[0];

            for (int i = 0; i != 5; ++i)
            {
                int var = rand.Next(100, curve.Points.Count + 1);
                curve.Points[var].Y = curve.Points[var].Y * 15;
            }

            for (int i = 0; i != curve.Points.Count; ++i)
            {
                newList.Add(curve.Points[i].X, curve.Points[i].Y);
            }
            curve.Points = newList;

            pane.YAxis.Scale.Min = -S * 10;
            pane.YAxis.Scale.Max = S * 10;
            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void SpikesDelete_Poly_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl6.GraphPane;
            PointPairList newList = new PointPairList();
            Random rand = new Random();
            CurveItem curve = pane.CurveList[0];

            for (int i = 0; i != curve.Points.Count; ++i)
            {
                if (Math.Abs(curve.Points[i].Y) > S)
                {
                    curve.Points[i].Y = (curve.Points[i - 1].Y + curve.Points[i + 1].Y) / 2;
                }
            }
            for (int i = 0; i != curve.Points.Count; ++i)
            {
                newList.Add(curve.Points[i].X, curve.Points[i].Y);
            }
            curve.Points = newList;

            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void DrawGraph(int S, int N)
        {
            // Создание объектов с координатами:
            Graph standart = new Graph(S, N, false, false, false);
            Graph my = new Graph(S, N, true, false, false);
            //double[][] my_more_stat = my.calculate_more_statistics();
            Graph notPoly = new Graph(S, N, true, true, false);
            Graph poly = new Graph(S, N, true, true, true);

            // Отображение статистики:
            avgtextbox.Text = Convert.ToString(standart.stat[0]);
            squaredavgtextbox.Text = Convert.ToString(standart.stat[1]);
            epstextbox.Text = Convert.ToString(standart.stat[2]);
            dispersiontextbox.Text = Convert.ToString(standart.stat[3]);
            errtextbox.Text = Convert.ToString(standart.stat[4]);
            asymtextbox.Text = Convert.ToString(standart.stat[5]);
            exctextbox.Text = Convert.ToString(standart.stat[6]);

            avgtextbox_own.Text = Convert.ToString(my.stat[0]);
            squaredavgtextbox_own.Text = Convert.ToString(my.stat[1]);
            epstextbox_own.Text = Convert.ToString(my.stat[2]);
            dispersiontextbox_own.Text = Convert.ToString(my.stat[3]);
            errtextbox_own.Text = Convert.ToString(my.stat[4]);
            asymtextbox_own.Text = Convert.ToString(my.stat[5]);
            exctextbox_own.Text = Convert.ToString(my.stat[6]);

            s_avgtextbox.Text = Convert.ToString(standart.stationarity[0]);
            s_squaredavgtextbox.Text = Convert.ToString(standart.stationarity[1]);
            s_epstextbox.Text = Convert.ToString(standart.stationarity[2]);
            s_dispersiontextbox.Text = Convert.ToString(standart.stationarity[3]);
            s_errtextbox.Text = Convert.ToString(standart.stationarity[4]);
            s_asymtextbox.Text = Convert.ToString(standart.stationarity[5]);
            s_exctextbox.Text = Convert.ToString(standart.stationarity[6]);

            s_avgtextbox_own.Text = Convert.ToString(my.stationarity[0]);
            s_squaredavgtextbox_own.Text = Convert.ToString(my.stationarity[1]);
            s_epstextbox_own.Text = Convert.ToString(my.stationarity[2]);
            s_dispersiontextbox_own.Text = Convert.ToString(my.stationarity[3]);
            s_errtextbox_own.Text = Convert.ToString(my.stationarity[4]);
            s_asymtextbox_own.Text = Convert.ToString(my.stationarity[5]);
            s_exctextbox_own.Text = Convert.ToString(my.stationarity[6]);
            
            // Отображение графиков:
            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;
            GraphPane pane_5 = zedGraphControl5.GraphPane;
            GraphPane pane_6 = zedGraphControl6.GraphPane;
            GraphPane pane_7 = zedGraphControl7.GraphPane;
            GraphPane pane_8 = zedGraphControl8.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_3.YAxis.Title.Text = pane_4.YAxis.Title.Text = "Ось Y";
            pane_5.XAxis.Title.Text = pane_6.XAxis.Title.Text = pane_7.XAxis.Title.Text = pane_8.XAxis.Title.Text = "Ось X";
            pane_5.YAxis.Title.Text = pane_6.YAxis.Title.Text = pane_7.YAxis.Title.Text = pane_8.YAxis.Title.Text = "Ось Y";

            pane.Title.Text   = "Случайные числа";
            pane_2.Title.Text = "Плотность распределения";
            pane_3.Title.Text = "Автокорелляция";
            pane_4.Title.Text = "Взаимная корелляция (реализации кастомного генератора)";
            pane_5.Title.Text = "Эффект наложения частот";
            pane_6.Title.Text = "Полигармонический процесс";
            pane_7.Title.Text = "Плотность полигармонического процесса";
            pane_8.Title.Text = "Авторреляция полигармонического процесса";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();
            pane_5.CurveList.Clear();
            pane_6.CurveList.Clear();
            pane_7.CurveList.Clear();
            pane_8.CurveList.Clear();

            pane.XAxis.Scale.Min   = pane_2.XAxis.Scale.Min = 0;
            pane.XAxis.Scale.Max   = pane_3.XAxis.Scale.Max = pane_4.XAxis.Scale.Max = pane_5.XAxis.Scale.Max = pane_6.XAxis.Scale.Max = pane_8.XAxis.Scale.Max = N;
            pane.YAxis.Scale.Min   = -S;
            pane.YAxis.Scale.Max   = S;
            pane_2.XAxis.Scale.Max = pane_7.XAxis.Scale.Max = 30;

            //pane.AddCurve("Стандартный", standart.create_pair_list(standart.points, standart.N), Color.Blue, SymbolType.None);
            pane.AddCurve("Кастомный", my.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            
            pane_2.AddCurve("Стандартный генератор", standart.create_pair_list(standart.density, 30), Color.Blue, SymbolType.None);
            pane_2.AddCurve("Кастомный генератор", my.create_pair_list(my.density, 30), Color.Green, SymbolType.None);
            pane_2.Legend.Position = LegendPos.Float;
            pane_2.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            pane_2.Legend.Location.AlignH = AlignH.Right;
            pane_2.Legend.Location.AlignV = AlignV.Bottom;
            pane_2.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f); 
            
            pane_3.AddCurve("", my.create_pair_list(my.calculate_more_statistics()[0], my.N), Color.Red, SymbolType.None);
            
            pane_4.AddCurve("R_XY", my.create_pair_list(my.calculate_more_statistics()[1], my.N), Color.Blue, SymbolType.None);
            pane_4.AddCurve("R_YX", my.create_pair_list(my.calculate_more_statistics()[2], my.N), Color.Black, SymbolType.None);
            pane_4.Legend.Position = LegendPos.Float;
            pane_4.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            pane_4.Legend.Location.AlignH = AlignH.Right;
            pane_4.Legend.Location.AlignV = AlignV.Bottom;
            pane_4.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f); 
            
            /* Плотность в виде гистограмм:
            BarItem curve = pane_3.AddBar("Количество значений Y", null, standart.density, Color.Blue);
            curve.Bar.Fill.Color = Color.YellowGreen;
            curve.Bar.Fill.Type = FillType.Solid;
            curve.Bar.Border.IsVisible = false;

            BarItem curve_2 = pane_4.AddBar("Количество значений Y", null, my.density, Color.Blue);
            curve_2.Bar.Fill.Color = Color.YellowGreen;
            curve_2.Bar.Fill.Type = FillType.Solid;
            curve_2.Bar.Border.IsVisible = false;
            */

            pane_5.AddCurve("", notPoly.create_pair_list(notPoly.points, notPoly.N), Color.Blue, SymbolType.None);
            pane_6.AddCurve("", poly.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            pane_7.AddCurve("", poly.create_pair_list(poly.density, 30), Color.Blue, SymbolType.None);
            pane_8.AddCurve("", poly.create_pair_list(poly.calculate_more_statistics()[0], poly.N), Color.Blue, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
            zedGraphControl5.AxisChange();
            zedGraphControl6.AxisChange();
            zedGraphControl7.AxisChange();
            zedGraphControl8.AxisChange();

            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
            zedGraphControl5.Invalidate();
            zedGraphControl6.Invalidate();
            zedGraphControl7.Invalidate();
            zedGraphControl8.Invalidate();
        }
    }
}
