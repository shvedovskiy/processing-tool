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
using NAudio.Wave;


namespace Graphics
{
    public partial class Form2 : Form
    {
        private int S = 100;
        private int N = 1000;

        private RandomGraph     standart;
        private RandomGraph     my;
        private PolyGraph       notPoly;
        private PolyGraph       poly;
        private RandomGraph     random_trend;
        private JustSpikesGraph pulse;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            standart = new RandomGraph(S, N, false);
            my = new RandomGraph(S, N, true);
            notPoly = new PolyGraph(S, N, false);
            poly = new PolyGraph(S, N, true);
            random_trend = new RandomGraph(S, N, false);
            pulse = new JustSpikesGraph(S, N, 3);

            DrawGraph_Random(S, N);
            DrawGraph_Poly(S, N);
            DrawGraph_Trends(S, N);
            LoadWav();
        }

        private void drawGraph(ZedGraphControl control, PointPairList list, string title, string lineTitle = "", string color = "Blue", string xTitle = "Ось X", string yTitle = "Ось Y", int xMin = 0, int xMax = 1000, int yMin = 0, int yMax = 0, bool isClear = true)
        {
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
            pane.AddCurve(lineTitle, list, Color.FromName(color), SymbolType.None);
            control.AxisChange();
            control.Invalidate();
        }
        private void drawGraph(ZedGraphControl control, double[] x, double[] y, string title, string lineTitle = "", string color = "Blue", string xTitle = "Ось X", string yTitle = "Ось Y", int xMin = 0, int xMax = 1000, int yMin = 0, int yMax = 0, bool isClear = true)
        {
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
            pane.AddCurve(lineTitle, x, y, Color.FromName(color), SymbolType.None);
            control.AxisChange();
            control.Invalidate();
        }

        private void SInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
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
            drawGraph(zedGraphControl1, Graph.create_pair_list(my.points, my.N), "Случайные числа", "Кастомный", "Green", xTitle: "t", yTitle: "A", xMax: N, yMax: my.S * 2);
        }
        private void SpikesInput_Rand_Click(object sender, EventArgs e)
        {
            my.calculate_spikes(5);
            drawGraph(zedGraphControl1, Graph.create_pair_list(my.points, my.N), "Случайные числа", "Кастомный", "Green", xTitle: "t", yTitle: "A");
        }
        private void SpikesDelete_Rand_Click(object sender, EventArgs e)
        {
            my.delete_spikes();
            drawGraph(zedGraphControl1, Graph.create_pair_list(my.points, my.N), "Случайные числа", "Кастомный", "Green", xTitle: "t", yTitle: "A");
        }
        private void AntiShiftButton_Rand_Click(object sender, EventArgs e)
        {
            my.unshift();
            drawGraph(zedGraphControl1, Graph.create_pair_list(my.points, my.N), "Случайные числа", "Кастомный", "Green", xTitle: "t", yTitle: "A", xMax: N, yMax: my.S * 2);
        }
        private void DrawGraph_Random(int S, int N)
        {
            //double[][] my_more_stat = this.my.calculate_more_statistics();
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

            drawGraph(zedGraphControl1, Graph.create_pair_list(my.points, my.N), "Случайные числа", "Кастомный", xTitle: "t", yTitle: "A", xMax: N, yMin: -S, yMax: S);
            drawGraph(zedGraphControl2, Graph.create_pair_list(standart.density, 30), "", "Стандартный генератор", "Blue", xTitle: "t", yTitle: "A", xMin: 0, yMin: -S, yMax: S);
            drawGraph(zedGraphControl2, Graph.create_pair_list(my.density, 30), "Плотность распределения", "Кастомный генератор", "Green", xTitle: "x", yTitle: "f(x)", xMin: 0, xMax: 30, isClear: false);
            drawGraph(zedGraphControl3, Graph.create_pair_list(my.calculate_more_statistics()[0], my.N), "Автокорелляция", color: "Red", xMax: N);
            drawGraph(zedGraphControl4, Graph.create_pair_list(my.calculate_more_statistics()[1], my.N), "", "R_XY", color: "Blue", xMax: N);
            drawGraph(zedGraphControl4, Graph.create_pair_list(my.calculate_more_statistics()[2], my.N), "Взаимная корелляция (реализации кастомного генератора)", "R_YX", color: "Black", xMax: N, yMin: -400, isClear: false);
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
            drawGraph(polyharmonicControl, Graph.create_pair_list(poly.points, poly.N), "Полигармонический процесс", "Кастомный", yMax: poly.S * 2);
        }
        private void SpikesInput_Poly_Click(object sender, EventArgs e)
        {
            poly.calculate_spikes(5);
            drawGraph(polyharmonicControl, Graph.create_pair_list(poly.points, poly.N), "Полигармонический процесс", yMin: -poly.S * 2, yMax: poly.S * 2);
        }
        private void SpikesDelete_Poly_Click(object sender, EventArgs e)
        {
            poly.delete_spikes();
            drawGraph(polyharmonicControl, Graph.create_pair_list(poly.points, poly.N), "Полигармонический процесс", yMin: -poly.S * 2, yMax: poly.S * 2);
        }
        private void AntiShiftButton_Poly_Click(object sender, EventArgs e)
        {
            poly.unshift();
            drawGraph(polyharmonicControl, Graph.create_pair_list(poly.points, poly.N), "Полигармонический процесс", "Кастомный");
        }
        private void DrawGraph_Poly(int S, int N)
        {
            drawGraph(harmonicControl, Graph.create_pair_list(notPoly.points, notPoly.N), "Эффект наложения частот", xMax: N, yMin: -notPoly.S * 2, yMax: notPoly.S * 2);
            drawGraph(polyharmonicControl, Graph.create_pair_list(poly.points, poly.N), "Полигармонический процесс", xMax: N, yMin: -200, yMax: 200);
            drawGraph(densityControl, Graph.create_pair_list(poly.density, 30), "Плотность полигармонического процесса", xMax: 30);
            drawGraph(polyharmonicAutocorrelationControl, Graph.create_pair_list(poly.calculate_more_statistics()[0], poly.N), "Авторреляция полигармонического процесса", xMax: N, yMin : -1, yMax: 1);
            drawGraph(harmonicSpectreControl, Graph.create_pair_list(PolyGraph.spectrum(notPoly.points, notPoly.N), notPoly.N / 2), "Спектр Фурье гармонического процесса");
            //poly.calculate_spikes(5);
            //drawGraph(zedGraphControl13, Graph.create_pair_list(PolyGraph.spectrum(poly.points, poly.N), poly.N / 2), "Спектры Фурье полигармонического процесса", lineTitle: "Со спайками", color: "Red");
            //poly.delete_spikes();
            //drawGraph(zedGraphControl13, Graph.create_pair_list(PolyGraph.spectrum(poly.points, poly.N), poly.N / 2), "Спектры Фурье полигармонического процесса", color: "Green", isClear: false);
            drawGraph(polyharmonicSpectreControl, Graph.create_pair_list(PolyGraph.spectrum(poly.points, poly.N), poly.N / 2), "Спектры Фурье полигармонического процесса", color: "Green");
        }


        private void AddTrendButton_Click(object sender, EventArgs e)
        {
            random_trend.add_trend();
            random_trend.shift(10);
            //random_trend.calculate_spikes(5);
            drawGraph(withTrendsControl, Graph.create_pair_list(random_trend.points, random_trend.N), "Добавление тренда, удаление шума", lineTitle: "Кастомный", color: "Green", yMin: -my.S * 10, yMax: my.S * 50);
        }
        private void DeleteTrendButton_Click(object sender, EventArgs e)
        {
            random_trend.delete_trend(10);
            drawGraph(withTrendsControl, Graph.create_pair_list(random_trend.points, random_trend.N), "Добавление тренда, удаление шума", lineTitle: "Кастомный", color: "Green");
        }
        private void DrawGraph_Trends(int S, int N)
        {
            double[] lpw = PolyGraph.lowPassFilter(50, 32, 0.001);
            double[] lpfw = poly.convolution(lpw);

            drawGraph(withTrendsControl, Graph.create_pair_list(standart.points, standart.N), "Добавление тренда, удаление шума", xMax: N, yMin: -120);
            drawGraph(sumControl, Graph.create_pair_list(notPoly.add_randoms(100), notPoly.N), "Сумма реализаций (100)", xMax: N, yMin: -150);
            drawGraph(lpfControl, Graph.create_pair_list(lpfw, lpfw.Length), "Фильтр высоких частот", yMin: -20);
            //double[] conv = pulse.convolution(40, 200);
            //drawGraph(zedGraphControl14, Graph.create_pair_list(conv, conv.Length), "Спектр после применения ФВЧ", xMax: conv.Length);
            drawGraph(zedGraphControl14, Graph.create_pair_list(PolyGraph.spectrum(lpfw, poly.N), lpfw.Length / 2), "Спектр после применения ФВЧ");

            double[] hpw = PolyGraph.highPassFilter(30, 32, 0.001);
            double[] hpfw = poly.convolution(hpw);
            drawGraph(hpfControl, Graph.create_pair_list(hpfw, hpfw.Length), "Фильтр низких частот", yMin: -140);
            drawGraph(zedGraphControl16, Graph.create_pair_list(PolyGraph.spectrum(hpfw, poly.N), hpfw.Length / 2), "Спектр после применения ФНЧ");

            double[] bpw = PolyGraph.bentPassFilter(30, 150, 32, 0.001);
            double[] bpfw = poly.convolution(bpw);
            drawGraph(bpfControl, Graph.create_pair_list(bpfw, bpfw.Length), "Полосовой фильтр", yMin: -70);
            drawGraph(zedGraphControl18, Graph.create_pair_list(PolyGraph.spectrum(bpfw, poly.N), bpfw.Length / 2), "Спектр после применения ПФ");

            double[] bspw = PolyGraph.bentStopFilter(15, 250, 32, 0.001);
            double[] bspfw = poly.convolution(bspw);
            drawGraph(bspfControl, Graph.create_pair_list(bspfw, bspfw.Length), "Режекторный фильтр", yMin: -40);
            drawGraph(zedGraphControl20, Graph.create_pair_list(PolyGraph.spectrum(bspfw, poly.N), bspfw.Length / 2), "Спектр после применения РФ");
        }

        private double[] wavSpectrum(double[] inreal)
        {
            int n = inreal.Length;
            double[] spectre = new double[n / 2];
            double[] outreal = new double[n];
            double[] outimag = new double[n];

            double sumreal;
            double sumimag;
            for (int k = 0; k != n; ++k)
            {
                sumreal = 0;
                sumimag = 0;
                for (int t = 0; t != n; ++t)
                {
                    double angle = (2 * Math.PI * t * k) / n;
                    sumreal += inreal[t] * Math.Cos(angle);
                    sumimag += -inreal[t] * Math.Sin(angle);
                }
                outreal[k] = sumreal;
                outimag[k] = sumimag;
            }

            for (int k = 0; k != n / 2; ++k)
            {
                spectre[k] = Math.Sqrt(Math.Pow(outreal[k], 2) + Math.Pow(outimag[k], 2));
            }
            return spectre;
        }
        private void LoadWav()
        {
            using (WaveFileReader reader = new WaveFileReader("E:\\олег\\Graphics\\Graphics\\1.wav"))
            {
                using (WaveFileWriter writer = new WaveFileWriter("E:\\олег\\Graphics\\Graphics\\test.wav", reader.WaveFormat))
                {
                    byte[] bytesBuffer = new byte[reader.Length];
                    int read = reader.Read(bytesBuffer, 0, bytesBuffer.Length);
                    short[] test = new short[bytesBuffer.Length / sizeof(short)];
                    Buffer.BlockCopy(bytesBuffer, 0, test, 0, bytesBuffer.Length);
                    var floatSamples = new double[read / 2];

                    for (int sampleIndex = 0; sampleIndex < read / 2; sampleIndex++)
                    {
                        var intSampleValue = BitConverter.ToInt16(bytesBuffer, sampleIndex * 2);
                        floatSamples[sampleIndex] = intSampleValue / 32768.0;
                    }

                    double[] X = new double[read];
                    for (int i = 1; i != X.Length; ++i)
                    {
                        X[i] = 11025 / floatSamples.Length * i;
                    }

                    byte[] result = new byte[test.Length * sizeof(short)];
                    Buffer.BlockCopy(test, 0, result, 0, result.Length);

                    double[] spec = wavSpectrum(test.Select(n => (double)n).ToArray());

                    double[] filtered_test = PolyGraph.convolution(test.Select(n => (double)n).ToArray(), PolyGraph.bentPassFilter(2600, 3600, 100, 0.0009));
                    double[] filtered_spec = wavSpectrum(filtered_test.Select(n => (double)n).ToArray());

                    short[] shortTestFilter = filtered_test.Select(n => (short)n).ToArray();
                    writer.WriteSamples(shortTestFilter, 0, shortTestFilter.Length);

                    PointPairList lst = new PointPairList();
                    for (int i = 0; i != test.Length; ++i)
                    {
                        lst.Add(i, test[i]);
                    }

                    drawGraph(wavControl, lst, "Временной ряд файла", color: "Red", xMax: test.Length, yMin: -20000);
                    drawGraph(wavSpectreControl, X, filtered_spec, "Спектр Фурье файла", color: "Red", xMax: 6000, yMax: (int) filtered_spec.Max() + 1);
                }
            }
        }
    } 
}