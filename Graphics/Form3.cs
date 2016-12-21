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
        const int N = 1000;
        public Form3()
        {
            InitializeComponent();

            int A_0 = 10; // амплитуда модулирующего
            float omega_0 = 400f; // частота несущего  
            float delta_omega = 3f; // девиация частоты 
            float phi = 200f; // частота модулирующего

            float[] formula = new float[N];
            formula = harm(phi);
            draw(HarmGraph, formula, "Гармонический модулирующий сигнал");

            float[] carrier = harm(omega_0);
            draw(CarrierGraph, carrier, "Несущий сигнал");

            float[] formula_1 = harm(phi);
            float[] FM_arr = Calculate_FM(A_0, omega_0, delta_omega, phi, formula_1);
            float[] SP_FM_arr = spectrum(FM_arr);
            draw(FMHarmGraph, FM_arr, "ЧМ гармонического сигнала");
            draw(SP_FMHarmGraph, SP_FM_arr, "Спектр ЧМ гармонического сигнала");

            float[] poly_arr = polyHarm();
            float[] SP_harm_arr = spectrum(poly_arr);
            float[] FM_Poly = Calculate_FM(A_0, omega_0, delta_omega, phi, poly_arr);
            float[] SP_PolyFM = spectrum(FM_Poly);
            draw(PolyHarmGraph, poly_arr, "Полигармонический модулирующий сигнал");
            draw(FMPolyGraph, FM_Poly, "ЧМ полигармонического сигнала");
            draw(SP_PolyGraph, SP_harm_arr, "Спектр полигармонического сигнала");
            draw(SP_PolyGraph, SP_PolyFM, "Спектр ЧМ полигармонического сигнала");

            float[] NZ_poly_arr = polyHarmRand(FM_Poly, A_0);
            draw(FM_PolyNoizeGraph, NZ_poly_arr, "ЧМ полигармонического сигнала с помехами");
            draw(Spectre_FMPolyNoiseGraph, spectrum(NZ_poly_arr), "Спектр ЧМ полигармонического сигнала с шумами");


/*
            //float[] FM_points = Calculate_FM(A_0, omega_0, delta_omega, phi);
            GraphPane pane = HarmGraph.GraphPane;
            GraphPane pane_2 = PolyHarmGraph.GraphPane;
            GraphPane pane_3 = PolyHarmWithNoiseGraph.GraphPane;
            GraphPane pane_4 = CarrierGraph.GraphPane;
            GraphPane pane__1 = FMHarmGraph.GraphPane;

            pane.Title.Text = "Гармонический модулирующий сигнал";
            pane_2.Title.Text = "Полигармонический модулирующий сигнал";
            pane_3.Title.Text = "Полигармонический модулирующий сигнал с шумом";
            pane_4.Title.Text = "Несущий сигнал";
            pane__1.Title.Text = "ЧМ гармонического сигнала";

            pane.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();
            pane__1.CurveList.Clear();
            pane.XAxis.Scale.Max = pane_2.XAxis.Scale.Max = pane_3.XAxis.Scale.Max = pane_4.XAxis.Scale.Max = pane__1.XAxis.Scale.Max = 1000;

            float[] carrier_points = harm(40, 150, 0.001f);
            float[] harm_points = harm(4, 50, 0.001f);
            float[] polyharm_points = polyHarm(29, 90, 0.001f, 3, 25, 170, 30);
            float[] polyharmwithnoise_points = polyHarmRand(29, 90, 0.001f, 3, 25, 170, 30, 200);
            float[] fm_harm_points = Calculate_FM(40, 150, 3, 50);

            PointPairList lst = new PointPairList();
            PointPairList lst_2 = new PointPairList();
            PointPairList lst_3 = new PointPairList();
            PointPairList lst_4 = new PointPairList();
            PointPairList lst_5 = new PointPairList();
            for (int i = 0; i != harm_points.Length; ++i)
            {
                lst.Add(i, harm_points[i]);
                lst_2.Add(i, polyharm_points[i]);
                lst_3.Add(i, polyharmwithnoise_points[i]);
                lst_4.Add(i, carrier_points[i]);
                lst_5.Add(i, fm_harm_points[i]);
            }
            pane.AddCurve("", lst, Color.Blue, SymbolType.None);
            pane_2.AddCurve("", lst_2, Color.Blue, SymbolType.None);
            pane_3.AddCurve("", lst_3, Color.Blue, SymbolType.None);
            pane_4.AddCurve("", lst_4, Color.Blue, SymbolType.None);
            pane__1.AddCurve("", lst_5, Color.Blue, SymbolType.None);
            HarmGraph.AxisChange();
            HarmGraph.Invalidate();
            PolyHarmGraph.AxisChange();
            PolyHarmGraph.Invalidate();
            PolyHarmWithNoiseGraph.AxisChange();
            PolyHarmWithNoiseGraph.Invalidate();
            CarrierGraph.AxisChange();
            CarrierGraph.Invalidate();
            FMHarmGraph.AxisChange();
            FMHarmGraph.Invalidate();
 */
        }

        private float[] harm(float phi, int A=1)
        {
            float[] res = new float[N];
            float dt = 0.001f;
            for (int i = 0; i != N; ++i)
            {
                res[i] = A * (float)Math.Cos(phi * i * dt);
            }
            return res;
        }

        private float[] polyHarm()
        {
            float[] res = new float[N];
            int A_0 = 90, A_1 = 25, A_2 = 30;
            float f_0 = 30, f_1 = 5, f_2 = 100;
            float dt = 0.001f;
            for (int i = 0; i != N; ++i)
            {
                res[i] = (float)(Math.Cos(f_0 * i * dt) + Math.Cos(f_1 * i * dt) + Math.Cos(f_2 * i * dt));
            }
            return res;
        }

        private float[] polyHarmRand(float[] points, float A_0)
        {
            Random rnd = new Random();
            float[] res = new float[points.Length];

            for (int i = 0; i != points.Length; ++i)
            {
                res[i] = points[i];
            }
            for (int j = 0; j != res.Length; ++j)
            {
                res[j] += rnd.Next(-(int)A_0, (int)A_0);
            }
            return res;
        }

        private float[] Calculate_FM(int A_0, float omega_0, float delta_omega, float phi, float[] carrier)
        {
            float[] FM_points = new float[N];
            for (int i = 0; i != N; ++i)
            {
                FM_points[i] = A_0 * (float)Math.Cos(omega_0 * i + delta_omega * carrier[i]);
            }
            return FM_points;
        }

        private float[] spectrum(float[] points)
        {
            int N = points.Length;
            float[] spectre = new float[points.Length];
            float[] spectre_Re = new float[points.Length];
            float[] spectre_Im = new float[points.Length];
            float argument;

            for (int k = 0; k != N; ++k)
            {
                for (int n = 0; n != points.Length; ++n)
                {
                    argument = (float)((2 * Math.PI * k * n) / N);
                    spectre_Re[k] += points[n] * (float)Math.Cos(argument);
                    spectre_Im[k] += -points[n] * (float)Math.Sin(argument);
                }
            }

            for (int k = 0; k != spectre.Length; ++k)
            {
                spectre[k] = (float)Math.Sqrt(Math.Pow(spectre_Re[k], 2) + Math.Pow(spectre_Im[k], 2));
            }

            return spectre;
        }

        private double[] toDouble(float[] points)
        {
            double[] res = new double[points.Length];
            for (int i = 0; i != points.Length; ++i)
            {
                res[i] = (double)points[i];
            }
            return res;
        }

        private void draw(ZedGraphControl control, float[] yValues, string Title)
        {
            GraphPane pane = control.GraphPane;
            pane.XAxis.Scale.Max = N;
            pane.Title.Text = Title;
            double[] yValues_double = toDouble(yValues);
            double[] xValues = new double[N];

            for (int i = 0; i != xValues.Length; ++i)
            {
                xValues[i] = i;
            }
            control.GraphPane.CurveList.Clear();
            var curve = control.GraphPane.AddCurve("", xValues, yValues_double.ToArray(), Color.Blue);
            curve.Symbol.Size = 0.1f;
            control.AxisChange();
            control.Invalidate();
        }
    }
}
