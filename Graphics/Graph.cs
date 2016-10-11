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
    public class Graph
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

        public int S;                // интервал значений по Y
        public int N;                // интервал значений по X (количество точек)
        private bool isOwn;          // флаг определения вида рандомайзера
        private bool isF;            // флаг для Form3
        private bool isPoly;         // флаг определения полигармонического процесса
        public double[] points;      // заполняется в Graph() -> create_random_array()
        public double[] stat;        // заполняется в Graph() -> calculate_statistics()
        public double[] density;     // заполняется в Graph() -> calculate_density() 
        public double[] stationarity;// заполняется в Graph() -> calculate_stability()
        public double[] spikes;      // заполняется в Graph() -> calculate_spikes()

        public Graph(int S, int N, bool isOwn, bool isF, bool isPoly)
        {
            this.S            = S;
            this.N            = N;
            this.isOwn        = isOwn;
            this.isF          = isF;
            this.isPoly       = isPoly;
            this.points       = create_points();
            this.stat         = calculate_statistics();
            this.density      = calculate_density();
            this.stationarity = calculate_stability();
        }
        private double        _f(double k, double A_0, double f_0)
        {
            double delta_t = 0.001;
            return A_0 * Math.Sin(2 * Math.PI * f_0 * k * delta_t);
        }
        private double        f(double k)
        {
            return _f(k, 100, 51) + _f(k, 15, 5) + _f(k, 20, 250);
        }
        private double[]      create_points() // значения функций (общая для рандомных и гармонических функций) 
        {
            double[] points = new double[this.N];

            // Гармонический график:
            if (this.isF) 
            {
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
            }
            // Рандомный график:
            else 
            {
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
                    points[i] = ((rand_arr[i] - ymin) / (ymax - ymin) - 0.5) * (2 * S);
            }
            return points;
        }
        private double[]      calculate_statistics() // 7 статистических метрик
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
        private double[]      calculate_statistics(double[] points)
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
        public  double[][]    calculate_more_statistics() // aka static; автокорреляция, взаимная корреляция 
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

            Graph that = new Graph(this.S, this.N, true, false, false);

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
        private double[]      calculate_density() // плотность распределения
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
        private double[]      calculate_stability() // стационарность 

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
        private double[]      calculate_spikes(int m, int k) // неправдоподобные значения 
        {
            double[] spikes = new double[m];
            int[] arr = new int[m];
            Random rand = new Random();
            for (int i = 0; i != arr.Length; ++i)
            {
                arr[i] = rand.Next();
            }
            for (int i = 0; i != spikes.Length; ++i)
            {
                spikes[i] = this.points[arr[i]] * k * (this.points.Max() - this.points.Min());
            }
            return spikes;
        }
        public  PointPairList create_pair_list(double[] arr, int size)
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