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

        private RandomGraph standart;
        private RandomGraph my;
        private PolyGraph   notPoly;
        private PolyGraph   poly;
        private RandomGraph random_trend;
        
        public Form2()
        {
            InitializeComponent();
            standart     = new RandomGraph(S, N, false);
            my           = new RandomGraph(S, N, true);
            notPoly      = new PolyGraph(S, N, false);
            poly         = new PolyGraph(S, N, true);
            random_trend = new RandomGraph(S, N, false);

            DrawGraph_Random(S, N);
            DrawGraph_Poly(S, N);
            DrawGraph_Trends(S, N);
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
            my = new RandomGraph(S, N, true);
            DrawGraph_Random(S, N);
        }
        private void ShiftButton_Rand_Click(object sender, EventArgs e)
        {
            my.shift(10);
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            pane.YAxis.Scale.Max = my.S * 2;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void SpikesInput_Rand_Click(object sender, EventArgs e)
        {
            my.calculate_spikes(5);
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void SpikesDelete_Rand_Click(object sender, EventArgs e)
        {
            my.delete_spikes();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void AntiShiftButton_Rand_Click(object sender, EventArgs e)
        {
            my.unshift();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void DrawGraph_Random(int S, int N)
        {
            //double[][] my_more_stat = this.my.calculate_more_statistics();

            // Отображение статистики:
            avgtextbox.Text        = Convert.ToString(standart.stat[0]);
            squaredavgtextbox.Text = Convert.ToString(standart.stat[1]);
            epstextbox.Text        = Convert.ToString(standart.stat[2]);
            dispersiontextbox.Text = Convert.ToString(standart.stat[3]);
            errtextbox.Text        = Convert.ToString(standart.stat[4]);
            asymtextbox.Text       = Convert.ToString(standart.stat[5]);
            exctextbox.Text        = Convert.ToString(standart.stat[6]);

            avgtextbox_own.Text        = Convert.ToString(my.stat[0]);
            squaredavgtextbox_own.Text = Convert.ToString(my.stat[1]);
            epstextbox_own.Text        = Convert.ToString(my.stat[2]);
            dispersiontextbox_own.Text = Convert.ToString(my.stat[3]);
            errtextbox_own.Text        = Convert.ToString(my.stat[4]);
            asymtextbox_own.Text       = Convert.ToString(my.stat[5]);
            exctextbox_own.Text        = Convert.ToString(my.stat[6]);

            s_avgtextbox.Text        = Convert.ToString(standart.stationarity[0]);
            s_squaredavgtextbox.Text = Convert.ToString(standart.stationarity[1]);
            s_epstextbox.Text        = Convert.ToString(standart.stationarity[2]);
            s_dispersiontextbox.Text = Convert.ToString(standart.stationarity[3]);
            s_errtextbox.Text        = Convert.ToString(standart.stationarity[4]);
            s_asymtextbox.Text       = Convert.ToString(standart.stationarity[5]);
            s_exctextbox.Text        = Convert.ToString(standart.stationarity[6]);

            s_avgtextbox_own.Text        = Convert.ToString(my.stationarity[0]);
            s_squaredavgtextbox_own.Text = Convert.ToString(my.stationarity[1]);
            s_epstextbox_own.Text        = Convert.ToString(my.stationarity[2]);
            s_dispersiontextbox_own.Text = Convert.ToString(my.stationarity[3]);
            s_errtextbox_own.Text        = Convert.ToString(my.stationarity[4]);
            s_asymtextbox_own.Text       = Convert.ToString(my.stationarity[5]);
            s_exctextbox_own.Text        = Convert.ToString(my.stationarity[6]);

            // Отображение графиков:
            GraphPane pane   = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_3.YAxis.Title.Text = pane_4.YAxis.Title.Text = "Ось Y";

            pane.Title.Text   = "Случайные числа";
            pane_2.Title.Text = "Плотность распределения";
            pane_3.Title.Text = "Автокорелляция";
            pane_4.Title.Text = "Взаимная корелляция (реализации кастомного генератора)";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();

            pane.XAxis.Scale.Min = pane_2.XAxis.Scale.Min = 0;
            pane.XAxis.Scale.Max = pane_3.XAxis.Scale.Max = pane_4.XAxis.Scale.Max = N;
            pane.YAxis.Scale.Min = -S;
            pane.YAxis.Scale.Max = S;
            pane_2.XAxis.Scale.Max = 30;

            pane.AddCurve("Кастомный", Graph.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);

            pane_2.AddCurve("Стандартный генератор", Graph.create_pair_list(standart.density, 30), Color.Blue, SymbolType.None);
            pane_2.AddCurve("Кастомный генератор", Graph.create_pair_list(my.density, 30), Color.Green, SymbolType.None);
            pane_2.Legend.Position = LegendPos.Float;
            pane_2.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            pane_2.Legend.Location.AlignH = AlignH.Right;
            pane_2.Legend.Location.AlignV = AlignV.Bottom;
            pane_2.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f);

            pane_3.AddCurve("", Graph.create_pair_list(my.calculate_more_statistics()[0], my.N), Color.Red, SymbolType.None);

            pane_4.AddCurve("R_XY", Graph.create_pair_list(my.calculate_more_statistics()[1], my.N), Color.Blue, SymbolType.None);
            pane_4.AddCurve("R_YX", Graph.create_pair_list(my.calculate_more_statistics()[2], my.N), Color.Black, SymbolType.None);
            pane_4.Legend.Position = LegendPos.Float;
            pane_4.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            pane_4.Legend.Location.AlignH = AlignH.Right;
            pane_4.Legend.Location.AlignV = AlignV.Bottom;
            pane_4.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();

            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            notPoly = new PolyGraph(S, N, false);
            poly = new PolyGraph(S, N, true);
            DrawGraph_Poly(S, N);
        }
        private void ShiftButton_Poly_Click(object sender, EventArgs e)
        {
            poly.shift(10);
            GraphPane pane = zedGraphControl6.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            pane.YAxis.Scale.Max = poly.S * 2;
            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void SpikesInput_Poly_Click(object sender, EventArgs e)
        {
            poly.calculate_spikes(5);
            GraphPane pane = zedGraphControl6.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("", Graph.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void SpikesDelete_Poly_Click(object sender, EventArgs e)
        {
            poly.delete_spikes();
            GraphPane pane = zedGraphControl6.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("", Graph.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void AntiShiftButton_Poly_Click(object sender, EventArgs e)
        {
            poly.unshift();
            GraphPane pane = zedGraphControl6.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            zedGraphControl6.AxisChange();
            zedGraphControl6.Invalidate();
        }
        private void DrawGraph_Poly(int S, int N)
        {
            GraphPane pane   = zedGraphControl5.GraphPane;
            GraphPane pane_2 = zedGraphControl6.GraphPane;
            GraphPane pane_3 = zedGraphControl7.GraphPane;
            GraphPane pane_4 = zedGraphControl8.GraphPane;
            GraphPane pane_5 = zedGraphControl12.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = pane_5.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_4.YAxis.Title.Text = pane_4.YAxis.Title.Text = pane_5.YAxis.Title.Text = "Ось Y";

            pane.Title.Text   = "Эффект наложения частот";
            pane_2.Title.Text = "Полигармонический процесс";
            pane_3.Title.Text = "Плотность полигармонического процесса";
            pane_4.Title.Text = "Авторреляция полигармонического процесса";
            pane_5.Title.Text = "Спектр Фурье гармонического процесса";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();
            pane_5.CurveList.Clear();

            pane.XAxis.Scale.Max = pane_2.XAxis.Scale.Max = pane_4.XAxis.Scale.Max = N;
            pane_3.XAxis.Scale.Max = 30;
            pane_2.YAxis.Scale.Min = -200;
            pane_2.YAxis.Scale.Max = 200;

            pane.AddCurve("", Graph.create_pair_list(notPoly.points, notPoly.N), Color.Blue, SymbolType.None);
            pane_2.AddCurve("", Graph.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            pane_3.AddCurve("", Graph.create_pair_list(poly.density, 30), Color.Blue, SymbolType.None);
            pane_4.AddCurve("", Graph.create_pair_list(poly.calculate_more_statistics()[0], poly.N), Color.Blue, SymbolType.None);
            pane_5.AddCurve("", Graph.create_pair_list(notPoly.spectrum(), notPoly.N / 2), Color.Blue, SymbolType.None);

            zedGraphControl5.AxisChange();
            zedGraphControl6.AxisChange();
            zedGraphControl7.AxisChange();
            zedGraphControl8.AxisChange();
            zedGraphControl12.AxisChange();

            zedGraphControl5.Invalidate();
            zedGraphControl6.Invalidate();
            zedGraphControl7.Invalidate();
            zedGraphControl8.Invalidate();
            zedGraphControl12.Invalidate();
        }

        private void AddTrendButton_Click(object sender, EventArgs e)
        {
            random_trend.add_trend();
            random_trend.shift(10);
            //random_trend.calculate_spikes(5);
            GraphPane pane = zedGraphControl9.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(random_trend.points, random_trend.N), Color.Green, SymbolType.None);
            pane.YAxis.Scale.Min = -my.S * 10;
            pane.YAxis.Scale.Max = my.S * 50;
            zedGraphControl9.AxisChange();
            zedGraphControl9.Invalidate();
        }
        private void DeleteTrendButton_Click(object sender, EventArgs e)
        {
            random_trend.delete_trend(10);
            GraphPane pane = zedGraphControl9.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve("Кастомный", Graph.create_pair_list(random_trend.points, random_trend.N), Color.Green, SymbolType.None);
            zedGraphControl9.AxisChange();
            zedGraphControl9.Invalidate();
        }
        private void DrawGraph_Trends(int S, int N)
        {
            GraphPane pane = zedGraphControl9.GraphPane;
            GraphPane pane_2 = zedGraphControl10.GraphPane;
            GraphPane pane_3 = zedGraphControl11.GraphPane;

            pane.Title.Text = "Добавление тренда, удаление шума";
            pane_2.Title.Text = "Сумма реализаций (10)";
            pane_3.Title.Text = "Сумма реализаций (100)";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();

            pane.XAxis.Scale.Max = N;
            
            pane.AddCurve("", Graph.create_pair_list(this.standart.points, this.standart.N), Color.Blue, SymbolType.None);
            pane_2.AddCurve("", Graph.create_pair_list(this.notPoly.add_randoms(1), this.notPoly.N), Color.Blue, SymbolType.None);
            pane_3.AddCurve("", Graph.create_pair_list(this.notPoly.add_randoms(100), this.notPoly.N), Color.Blue, SymbolType.None);

            zedGraphControl9.AxisChange();
            zedGraphControl10.AxisChange();
            zedGraphControl11.AxisChange();

            zedGraphControl9.AxisChange();
            zedGraphControl10.AxisChange();
            zedGraphControl11.AxisChange();
        }
    } 
}