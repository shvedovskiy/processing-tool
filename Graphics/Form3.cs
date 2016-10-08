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
        private void DrawGraph()
        {
            Graph_1 notPoly = new Graph_1(this.N, false);
            Graph_1 poly = new Graph_1(this.N, true);

            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_3.YAxis.Title.Text = pane_4.YAxis.Title.Text = "Ось Y";

            pane.Title.Text = "Эффект наложения частот";
            pane_2.Title.Text = "Полигармонический процесс";
            pane_3.Title.Text = "Плотность полигармонического процесса";
            pane_4.Title.Text = "Авторреляция полигармонического процесса";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();

            PointPairList list = new PointPairList();
            PointPairList list_2 = new PointPairList();
            PointPairList list_3 = new PointPairList();
            PointPairList list_4 = new PointPairList();

            pane.AddCurve("", notPoly.create_pair_list(notPoly.points, notPoly.N), Color.Blue, SymbolType.None);
            pane_2.AddCurve("", poly.create_pair_list(poly.points, poly.N), Color.Blue, SymbolType.None);
            pane_3.AddCurve("", poly.create_pair_list(poly.density, 30), Color.Blue, SymbolType.None);
            pane_4.AddCurve("", poly.create_pair_list(poly.autocorrelation, poly.N), Color.Blue, SymbolType.None);

            pane.XAxis.Scale.Max = pane_2.XAxis.Scale.Max = pane_4.XAxis.Scale.Max= this.N;
            pane_3.XAxis.Scale.Max = 30;

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();

            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
        }
    }

    public class Graph_1
    {
        public  int      N;
        private int      S; // для плотности (зависит от макс. A_0, не рекомендуется его менять)
        private bool     isPoly;
        public  double[] points;
        private double   avg;
        public  double[] density;
        public  double[] autocorrelation;

        public Graph_1(int N, bool isPoly) {
            this.N               = N;
            this.S               = 100;
            this.isPoly          = isPoly;
            this.points          = create_points();
            this.avg             = calculate_avg();
            this.density         = calculate_density();
            this.autocorrelation = calculate_autocorrelation();
        }
        private double       _f(double k, double A_0, double f_0)
        {
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        private double       f(double k)
        {
            return _f(k, 100, 51) + _f(k, 15, 5) + _f(k, 20, 150);
        }
        private double[]     create_points()
        {
            double[] res = new double[this.N];
            if (this.isPoly == true)
            {
                for (int k = 0; k != this.N; ++k)
                {
                    res[k] = f(Convert.ToDouble(k));
                }
            }
            else
            {
                for (int k = 0; k != this.N; ++k)
                {
                    res[k] = _f(Convert.ToDouble(k), 100, 51);
                }
            }
            return res;
        }
        private double       calculate_avg()
        {
            double avg = 0.0;
            for (int i = 0; i != this.points.Length; ++i)
            {
                avg += this.points[i];
            }
            return avg / this.points.Length;
        }
        private double[]     calculate_density()
        {
            double[] y_counts = new double[30];   // кол-во значений рандомайзера в интервалах
            int y_length = 2 * this.S + 1;  // длина области значений по Y
            int[] yarr = new int[y_length]; // массив всех значений по Y
            // Создание всех значений по Y:
            int y = -this.S;
            for (int i = 0; i != yarr.Length; i++)
            {
                yarr[i] = y;
                y++;
            }
            int m = y_length % 30;                        // Y бьем на 30 отрезков
            int n = Convert.ToInt32((y_length - m) / 30); // по сколько эл-тов минимальные интервалы
            int[][] arr = new int[30][];                  // Все 30 интервалов
            int min = 0, max = n;                         // Для сквозной итерации по yarr
            // Разбиение Y на интервалы:
            for (int i = 0; i != 30; ++i)
            {
                arr[i] = new int[n];
                for (int j = min, k = 0; j != max; ++j)
                {
                    arr[i][k] = yarr[j];
                    k++;
                }
                if (m > 0) // жадный метод
                {
                    Array.Resize(ref arr[i], arr[i].Length + 1);
                    arr[i][arr[i].Length - 1] = yarr[max];
                    m--;
                    min = max + 1;
                    max = max + n + 1;
                    continue;
                }
                min = max;
                max = max + n;
            }
            // Подсчет количества значений рандомайзера в интервалах:
            for (int i = 0; i != arr.Length; i++)
            {
                for (int j = 0; j != this.points.Length; j++)
                {
                    if (Array.IndexOf(arr[i], Convert.ToInt32(Math.Round(points[j], 0))) != -1)
                    {
                        y_counts[i]++;
                    }
                }
            }

            return y_counts;
        }
        private double[]     calculate_autocorrelation()
        {
            double[] res = new double[this.N];
            for (int L = 0; L != this.N - 1; ++L) {
                double enumenator = 0.0, denominator = 0.0;
                for (int k = 0; k != this.N - L; ++k) {
                    enumenator += ((this.points[k] - this.avg) * (this.points[k + L] - this.avg));
                }
                for (int k = 0; k != this.N; ++k) {
                    denominator += Math.Pow((this.points[k] - this.avg), 2.0);
                }
                res[L] = enumenator / denominator;
            }
            return res;
        } 
        public PointPairList create_pair_list(double[] arr, int size)
        {
            PointPairList list = new PointPairList();
            for (int x = 0; x != size; ++x)
            {
                list.Add(Convert.ToDouble(x), arr[x]);
            }
            return list;
        }
    }
}
