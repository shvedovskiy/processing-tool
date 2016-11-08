using System;
using System.IO;
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
    public class Graph
    {
        public int S;                // интервал значений по Y
        public int N;                // интервал значений по X (количество точек)
        public double[] points;      // заполняется в create_random_array()
        public double[] stat;        // заполняется в calculate_statistics()
        public double[] density;     // заполняется в calculate_density() 
        public double[] stationarity;// заполняется в calculate_stability()

        public Graph(int S, int N)
        {
            this.S = S;
            this.N = N;
        }
        public Graph(String filename)
        {
            Directory.GetFiles(@"C:\Users\root\Documents\Visual Studio 2013\Projects\Graphics\Graphics\bin\Release"); 

            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int pos = 0;
                int len = (int)b.BaseStream.Length;
                this.N = len;
                this.points = new double[len];

                while (pos != len)
                {
                    this.points[pos] = b.ReadInt32();
                    pos += sizeof(int);
                }
                if (this.points.Max() > Math.Abs(this.points.Min())) {
                    this.S = Convert.ToInt32(this.points.Max()) + 1;
                } else {
                    this.S = Convert.ToInt32(Math.Abs(this.points.Min())) + 1;
                }
            }
        }      // чтение из .dat-файла

        protected double[]        calculate_statistics()  // 7 статистических метрик
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
        protected static double[] calculate_statistics(double[] points)
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

        protected double[] calculate_density()     // плотность распределения
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
        protected double[] calculate_stability()   // стационарность 

        {
            double[] stability = new double[7];    // Массив усредненных статистик интервалов
            double[][] substats = new double[50][]; // Массив статистик по интервалам
            double[][] arr = new double[50][]; // Разбиение значений рандомайзера на интервалы
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

        public void         calculate_spikes(int m) // неправдоподобные значения, m -- их кол-во
        {
            // для получения статистики по shift, но без спаек
            shift(10);
            this.stat = calculate_statistics();
            unshift();

            Random rand = new Random();
            for (int i = 0; i != m; ++i)
            {
                int val = rand.Next(0, this.N);
                this.points[val] = this.points[val] * 10 * (this.points.Max() - this.points.Min());
            }
            shift(10);
        }
        public static Graph calculate_spikes(Graph graph, int m) {
            Graph spiked = new Graph(graph.S, graph.N);
            spiked.points = new double[graph.N];
            spiked.stat = calculate_statistics(shift(graph, 10));

            for (int i = 0; i != graph.N; ++i)
            {
                spiked.points[i] = graph.points[i];
            }

            Random rand = new Random();
            for (int i = 0; i != m; ++i) 
            {
                int val = rand.Next(0, graph.N);
                spiked.points[val] = spiked.points[val] * 10 * (spiked.points.Max() - spiked.points.Min());
            }
            spiked.points = shift(spiked, 10);
            return spiked;
        }

        public void            delete_spikes()         // удаление неправдоподобных значений (несамостоятельная, только после calculate_spikes())
        {
            unshift();
            for (int i = 0; i != this.N - 1; ++i)
            {
                if ((Math.Abs(this.points[i]) > this.S) && (Math.Abs(this.points[i + 1]) <= this.S))
                {
                    this.points[i] = (this.points[i - 1] + this.points[i + 1]) / 2; // удаляем спайки
                }
            }
        }
        public static double[] delete_spikes(Graph graph)
        {
            double[] unspiked = new double[graph.N];
            double[] unshifted = unshift(graph);

            for (int i = 0; i != graph.N - 1; ++i)
            {
                if ((Math.Abs(unshifted[i]) > graph.S) && (Math.Abs(unshifted[i + 1]) <= graph.S))
                {
                    unspiked[i] = (unshifted[i - 1] + unshifted[i + 1]) / 2; // удаляем спайки
                    continue;
                }
                unspiked[i] = unshifted[i];
            }
            unspiked[graph.N - 1] = unshifted[graph.N - 1];
            return unspiked;
        }

        private void              shift(int n)
        {
            for (int i = 0; i != this.N; ++i)
            {
                this.points[i] += (n * this.S);
            }
        }
        public static double[] shift(Graph graph, int n)
        {
            double[] shifted = new double[graph.N];
            for (int i = 0; i != graph.N; ++i)
            {
                shifted[i] = graph.points[i] + (n * graph.S);
            }
            return shifted;
        }

        private void              unshift()
        {
            for (int i = 0; i != this.N; ++i)
            {
                this.points[i] -= this.stat[0];
            }
        }
        public static double[] unshift(Graph graph)
        {
            double[] unshifted = new double[graph.N];
            for (int i = 0; i != graph.N; ++i)
            {
                unshifted[i] = graph.points[i] - graph.stat[0];
            }
            return unshifted;
        }

        public static PointPairList create_pair_list(double[] arr, int size)
        {
            PointPairList list = new PointPairList();
            for (int x = 0; x != size; ++x)
            {
                list.Add(Convert.ToDouble(x), arr[x]);
            }
            return list;
        }
        public static PointPairList create_pair_list(String filename)
        {
            PointPairList list = new PointPairList();
            Directory.GetFiles(@"C:\Users\root\Documents\Visual Studio 2013\Projects\Graphics\Graphics\bin\Release"); 

            using (BinaryReader b = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int pos = 0;
                int len = (int)b.BaseStream.Length;
                while (pos != len)
                {
                    list.Add(Convert.ToDouble(pos), Convert.ToDouble(b.ReadInt32()));
                    pos += sizeof(int);
                }
            }
            return list;
        }
    }

    public class RandomGraph : Graph
    {
        private class MyOwnRandom
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

        private bool isOwn; // флаг определения вида рандомайзера

        public RandomGraph(int S, int N, bool isOwn) : base(S, N)
        {
            this.isOwn = isOwn;
            this.points = create_points();
            this.stat = calculate_statistics();
            this.density = calculate_density();
            this.stationarity = calculate_stability();
        }
        private double[]   create_points()
        {
            double[] points = new double[this.N];
            double[] rand_arr = new double[this.N];

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
                points[i] = ((rand_arr[i] - ymin) / (ymax - ymin) - 0.5) * (2 * this.S);

            return points;
        }
        public  double[][] calculate_more_statistics() // aka static; автокорреляция, взаимная корреляция 
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

            RandomGraph that = new RandomGraph(this.S, this.N, true);
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
        public  void       add_trend()
        {
            TrendGraph trend = new TrendGraph(this.S, this.N);
            for (int i = 0; i != this.N; ++i)
            {
                this.points[i] += trend.points[i];
            }
        }
        public  void       delete_trend(int w)
        { // TODO
            /*double sum;
            double[] slides = new double[(N - w) - w];
            int i = 0;
            for (int m = w; m != this.N - w; ++m)
            {
                sum = 0;
                for (int k = 0; k != w; ++k)
                {
                    sum += this.points[k];
                }
                slides[i] = sum / w;
                i++;
            }
            return slides;*/
            double sum;
            double[] slides = new double[this.N]; 
            for (int i = 0; i != this.N; ++i)
            {
                sum = 0.0;
                for (int j = i - (w - 1) / 2; j != i + (w - 1) / 2; ++j)
                {
                    sum += this.points[j];
                }
                slides[i] = sum / w;
            }
            for (int i = 0; i != this.N; ++i)
            {
                this.points[i] = slides[i];
            }
        }
    }

    public class TrendGraph : Graph
    {
        public TrendGraph(int S, int N) : base(S, N)
        {
            this.points = create_points();
        }
        private double[] create_points() {
            double[] points = new double[this.N];
            for (int i = 0; i != 250; ++i)
            {
                points[i] = Convert.ToDouble(i);
            }
            for (int i = 250; i != 500; ++i)
            {
                points[i] = 321 * Math.Exp(-0.001 * i);
            }
            for (int i = 500; i != 750; ++i)
            {
                points[i] = 119 * Math.Exp(0.001 * i);
            }
            for (int i = 750; i != this.N; ++i)
            {
                points[i] = Convert.ToDouble(-1 * i + 1002);
            }
            return points;
        }
    }

    public class PolyGraph : Graph
    {
        private bool isPoly; // флаг определения полигармонического процесса

        public PolyGraph(int S, int N, bool isPoly) : base(S, N)
        {
            this.isPoly = isPoly;
            this.points = create_points();
            this.stat = this.calculate_statistics();
            this.density = this.calculate_density();
            this.stationarity = this.calculate_stability();
        }
        private double[]   create_points()
        {
            double[] points = new double[this.N];

            if (this.isPoly == true)
            {
                for (int k = 0; k != this.N; ++k)
                {
                    points[k] = f(Convert.ToDouble(k));
                }
            }
            else
            {
                for (int k = 0; k != this.N; ++k)
                {
                    points[k] = _f(Convert.ToDouble(k), 100, 51);
                }
            }
            return points;
        }
        public  double[][] calculate_more_statistics() // aka static; автокорреляция, взаимная корреляция 
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

            PolyGraph that = new PolyGraph(this.S, this.N, false);
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
        private double     _f(double k, double A_0, double f_0)
        {
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        private double     f(double k)
        {
            return _f(k, 100, 51) + _f(k, 15, 5) + _f(k, 20, 250);
        }
        public double[] add_randoms(int n)
        {
            double[] summary = new double[this.N];
            double[] harmonics = this.points;

            RandomGraph rand = new RandomGraph(this.S, this.N, false);
            double[] randoms = rand.points;
            
            int cnt = 0;
            while (cnt != n)
            {
                PolyGraph tmp = new PolyGraph(this.S / 5, this.N, false);
                RandomGraph r_tmp = new RandomGraph(this.S, this.N, false);
                for (int i = 0; i != this.N; ++i)
                {
                    harmonics[i] += tmp.points[i];
                    randoms[i] += r_tmp.points[i];
                }
                cnt++;
            }
            for (int i = 0; i != this.N; ++i)
            {
                summary[i] = (harmonics[i] + randoms[i]) / n;
            }
            return summary;
        }
    }
}
