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
            DrawGraph(S, N);
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
        private void DrawGraph(int S, int N)
        {
            //создание объектов с координатами
            Graph standart = new Graph(S, N, false);
            Graph my = new Graph(S, N, true);
            //double[][] my_more_stat = my.calculate_more_statistics();

            //отображение статистики
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

            s_avgtextbox.Text = Convert.ToString(standart.stability[0]);
            s_squaredavgtextbox.Text = Convert.ToString(standart.stability[1]);
            s_epstextbox.Text = Convert.ToString(standart.stability[2]);
            s_dispersiontextbox.Text = Convert.ToString(standart.stability[3]);
            s_errtextbox.Text = Convert.ToString(standart.stability[4]);
            s_asymtextbox.Text = Convert.ToString(standart.stability[5]);
            s_exctextbox.Text = Convert.ToString(standart.stability[6]);

            s_avgtextbox_own.Text = Convert.ToString(my.stability[0]);
            s_squaredavgtextbox_own.Text = Convert.ToString(my.stability[1]);
            s_epstextbox_own.Text = Convert.ToString(my.stability[2]);
            s_dispersiontextbox_own.Text = Convert.ToString(my.stability[3]);
            s_errtextbox_own.Text = Convert.ToString(my.stability[4]);
            s_asymtextbox_own.Text = Convert.ToString(my.stability[5]);
            s_exctextbox_own.Text = Convert.ToString(my.stability[6]);
            
            //отображение графиков:
            GraphPane pane = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;

            pane.XAxis.Title.Text = pane_2.XAxis.Title.Text = pane_3.XAxis.Title.Text = pane_4.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = pane_2.YAxis.Title.Text = pane_3.YAxis.Title.Text = pane_4.YAxis.Title.Text = "Ось Y";

            pane.Title.Text = "Случайные числа";
            pane_2.Title.Text = "Плотность распределения";
            pane_3.Title.Text = "Автокорелляция";
            pane_4.Title.Text = "Взаимная корелляция (реализации кастомного генератора)";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();

            pane.XAxis.Scale.Min = pane_2.XAxis.Scale.Min = 0;
            pane.XAxis.Scale.Max = pane_3.XAxis.Scale.Max = pane_4.XAxis.Scale.Max = this.N;
            pane.YAxis.Scale.Min = -this.S;
            pane.YAxis.Scale.Max = this.S;
            pane_2.XAxis.Scale.Max = 30;

            pane.AddCurve("Стандартный", standart.create_pair_list(standart.points, standart.N), Color.Blue, SymbolType.None);
            pane.AddCurve("Кастомный", my.create_pair_list(my.points, my.N), Color.Green, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
           
            pane_2.AddCurve("Стандартный генератор", standart.create_pair_list(standart.density, 30), Color.Blue, SymbolType.None);
            pane_2.AddCurve("Кастомный генератор", my.create_pair_list(my.density, 30), Color.Green, SymbolType.None);
            // Указываем, что расположение легенды мы будет задавать в виде координат левого верхнего угла
            pane_2.Legend.Position = LegendPos.Float;
            // Координаты будут отсчитываться в системе координат окна графика
            pane_2.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            // Задаем выравнивание, относительно которого мы будем задавать координат
            pane_2.Legend.Location.AlignH = AlignH.Right;
            pane_2.Legend.Location.AlignV = AlignV.Bottom;
            // Задаем координаты легенды, вычитаем 0.02f, чтобы был небольшой зазор между осями и легендой
            pane_2.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f); 
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();

            pane_3.AddCurve("", my.create_pair_list(my.calculate_more_statistics()[0], my.N), Color.Red, SymbolType.None);
            zedGraphControl3.AxisChange();
            zedGraphControl3.Invalidate();

            pane_4.AddCurve("R_XY", my.create_pair_list(my.calculate_more_statistics()[1], my.N), Color.Blue, SymbolType.None);
            pane_4.AddCurve("R_YX", my.create_pair_list(my.calculate_more_statistics()[2], my.N), Color.Black, SymbolType.None);
            pane_4.Legend.Position = LegendPos.Float;
            pane_4.Legend.Location.CoordinateFrame = CoordType.ChartFraction;
            pane_4.Legend.Location.AlignH = AlignH.Right;
            pane_4.Legend.Location.AlignV = AlignV.Bottom;
            pane_4.Legend.Location.TopLeft = new PointF(1.0f - 0.02f, 1.0f - 0.02f); 
            zedGraphControl4.AxisChange();
            zedGraphControl4.Invalidate();

            /*
            BarItem curve = pane_3.AddBar("Количество значений Y", null, standart.density, Color.Blue);
            curve.Bar.Fill.Color = Color.YellowGreen;
            curve.Bar.Fill.Type = FillType.Solid;
            curve.Bar.Border.IsVisible = false;
            zedGraphControl3.AxisChange();
            zedGraphControl3.Invalidate();

            BarItem curve_2 = pane_4.AddBar("Количество значений Y", null, my.density, Color.Blue);
            curve_2.Bar.Fill.Color = Color.YellowGreen;
            curve_2.Bar.Fill.Type = FillType.Solid;
            curve_2.Bar.Border.IsVisible = false;
            zedGraphControl4.AxisChange();
            zedGraphControl4.Invalidate();*/
        }
    }

    public class Graph
    {
        class MyOwnRandom
        {
            private const long _a = 6364136223846793005;
            private const long _c = 1442695040888963407;
            private const long _m = 4294967296;
            private long _last;

            public MyOwnRandom()
            {
                _last = (long)Math.Pow(DateTime.Now.TimeOfDay.TotalMilliseconds, 11.0 / 7.0);
            }
            public MyOwnRandom(long seed)
            {
                _last = seed;
            }
            public long Next()
            {
                _last = ((_a * _last) + _c) % _m;
                return _last;
            }
            public long Next(long max)
            {
                return Next() % max;
            }
        }

        public int      S;        // интервал значений по Y
        public int      N;        // интервал значений по X (количество точек)
        public bool     isOwn;    // флаг определения вида рандомайзера
        public double[] points;   // заполняется в Graph() -> create_random_array()
        public double[] stat;     // заполняется в Graph() -> calculate_statistics()
        public double[] density;  // заполняется в Graph() -> calculate_density() 
        public double[] stability;// заполняется в Graph() -> calculate_stability()

        public Graph(int S, int N, bool isOwn)
        {
            this.S         = S;
            this.N         = N;
            this.isOwn     = isOwn;
            this.points    = create_random_array();
            this.stat      = calculate_statistics();
            this.density   = calculate_density();
            this.stability = calculate_stability();
        }
        public double[]      create_random_array()
        {
            double[] rand_arr = new double[this.N];
            double[] points = new double[this.N];

            if (this.isOwn == false)
            {
                Random rand = new Random();
                for (int i = 0; i != this.N; ++i)
                    rand_arr[i] = Convert.ToDouble(rand.Next());
            }
            else
            {
                MyOwnRandom rand = new MyOwnRandom();
                for (int i = 0; i != this.N; ++i)
                    rand_arr[i] = Convert.ToDouble(rand.Next());
            }

            double ymax = rand_arr.Max();
            double ymin = rand_arr.Min();

            // Скорректировать значения в массивах, сформировать список координат:
            for (int i = 0; i != this.N; ++i)
                points[i] = ((rand_arr[i] - ymin) / (ymax - ymin) - 0.5) * (2 * S);

            return points;
        }
        public double[]      calculate_statistics()
        {
            int len = this.points.Length;
            double[] res = new double[7];

            // Среднее значение и средний квадрат:
            double avg = 0.0, squared_avg = 0.0;
            for (int i = 0; i != len; ++i)
            {
                avg += this.points[i];
                squared_avg += Math.Pow(this.points[i], 2.0);
            }
            avg /= len; res[0] = avg;
            squared_avg /= len; res[1] = squared_avg;

            // Среднеквадратическая ошибка:
            double eps = Math.Sqrt(squared_avg);
            res[2] = eps;

            // Дисперсия:
            double dispersion = 0.0;
            for (int i = 0; i != len; ++i)
                dispersion += Math.Pow((this.points[i] - avg), 2.0);
            dispersion /= len; res[3] = dispersion;

            // Средняя ошибка:
            double err = Math.Sqrt(dispersion);
            res[4] = err;

            // Асимметрия и эксцесс:
            double asym = 0.0, exc = 0.0;
            for (int i = 0; i != len; ++i)
            {
                asym += Math.Pow((this.points[i] - avg), 3.0);
                exc += Math.Pow((this.points[i] - avg), 4.0);
            }
            asym /= len;
            exc /= len;
            res[5] = asym / Math.Pow(err, 3.0);
            res[6] = exc / Math.Pow(err, 4.0) - 3.0;

            return res;
        }
        public double[]      calculate_statistics(double[] points)
        {
            int len = points.Length;
            double[] res = new double[7];

            // Среднее значение и средний квадрат:
            double avg = 0.0, squared_avg = 0.0;
            for (int i = 0; i != len; ++i)
            {
                avg += points[i];
                squared_avg += Math.Pow(points[i], 2.0);
            }
            avg /= len; res[0] = avg;
            squared_avg /= len; res[1] = squared_avg;

            // Среднеквадратическая ошибка:
            double eps = Math.Sqrt(squared_avg);
            res[2] = eps;

            // Дисперсия:
            double dispersion = 0.0;
            for (int i = 0; i != len; ++i)
                dispersion += Math.Pow((points[i] - avg), 2.0);
            dispersion /= len; res[3] = dispersion;

            // Средняя ошибка:
            double err = Math.Sqrt(dispersion);
            res[4] = err;

            // Асимметрия и эксцесс:
            double asym = 0.0, exc = 0.0;
            for (int i = 0; i != len; ++i)
            {
                asym += Math.Pow((points[i] - avg), 3.0);
                exc += Math.Pow((points[i] - avg), 4.0);
            }
            asym /= len;
            exc /= len;
            res[5] = asym / Math.Pow(err, 3.0);
            res[6] = exc / Math.Pow(err, 4.0) - 3.0;

            return res;
        }
        public double[][]    calculate_more_statistics() // aka static
        {
            double[][] res = new double[3][];
            res[0] = new double[this.N];
            res[1] = new double[this.N];
            res[2] = new double[this.N];

            for (int L = 0; L != this.N - 1; ++L)
            {
                double enumerator = 0.0, denominator = 0.0;
                for (int k = 0; k != this.N - L; ++k)
                {
                    enumerator += ((this.points[k] - this.stat[0]) * (this.points[k + L] - this.stat[0]));
                }
                for (int k = 0; k != this.N; ++k)
                {
                    denominator += Math.Pow((this.points[k] - this.stat[0]), 2.0);
                }
                res[0][L] = enumerator / denominator;
            }

            Graph that = new Graph(this.S, this.N, true);

            for (int L = 0; L != this.N; ++L)
            {
                double sum = 0.0;
                for (int k = 0; k != this.N - L; ++k)
                {
                    sum += ((this.points[k] - this.stat[0]) * (that.points[k + L] - that.stat[0]));
                }
                res[1][L] = sum / this.N;
            }

            for (int L = 0; L != this.N; ++L)
            {
                double sum = 0.0;
                for (int k = 0; k != this.N - L; ++k)
                {
                    sum += ((that.points[k] - that.stat[0]) * (this.points[k + L] - this.stat[0]));
                }
                res[2][L] = sum / this.N;
            }

            return res;
        }
        public double[]      calculate_density()
        {
            double[] y_counts = new double[30];   // кол-во значений рандомайзера в интервалах
            int y_length = 2 * this.S + 1;  // длина области значений по Y
            int[] yarr = new int[y_length]; // массив всех значений по Y
            // Создание всех значений по Y:
            int y = -S;
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
        public double[]      calculate_stability()
        {
            double[]   stability = new double[7];    // Массив усредненных статистик интервалов
            double[][] substats  = new double[50][]; // Массив статистик по интервалам
            double[][] arr       = new double[50][]; // Разбиение значений рандомайзера на интервалы
            int m = this.N % 50; // 0
            int n = Convert.ToInt32((this.N - m) / 50); // длина интервала для N = 1000 - 20

            int I = 0, J = 0, K = 0;
            arr[0] = new double[n];
            while (I != this.N)
            {
                if (K == n)
                {
                    K = 0;
                    J++;
                    arr[J] = new double[n];
                }
                arr[J][K] = this.points[I];
                I++;
                K++;
            }
            // Вычисление статистик для каждого интервала:
            for (int index = 0; index != arr.Length; ++index)
            {
                double[] substat = calculate_statistics(arr[index]); // 7 эл-тов
                substats[index] = new double[substat.Length]; 
                substats[index] = substat;
            }
            
            // Проверка на стационарность -- попарное сравнение значений каждой статистики:
            int eps = 5;
            for (int index = 0; index != 7; ++index)
            {
                for (int u = 0; u != substats.Length - 1; ++u)
                {
                    if (Math.Abs(substats[u][index] - substats[u + 1][index]) > eps)
                    {
                        // TODO
                    }
                }
                
            }

            // Взятие среднего от статистик по интервалам:
            for (int index = 0; index != 7; ++index) // извлечение n-ой статистики из всех интервалов
            {
                double[] tmp = new double[substats.Length]; // 50 эл-тов
                for (int sub = 0; sub != substats.Length; ++sub)
                {
                    tmp[sub] = substats[sub][index];
                }
                double sum = 0.0;
                for (int u = 0; u != tmp.Length; ++u)
                {
                    sum += tmp[u];
                }
                stability[index] = sum / tmp.Length;
            }

            return stability;
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
