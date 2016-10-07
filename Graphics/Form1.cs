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
            DrawGraph();
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
        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane_1 = zedGraphControl1.GraphPane;
            GraphPane pane_2 = zedGraphControl2.GraphPane;
            GraphPane pane_3 = zedGraphControl3.GraphPane;
            GraphPane pane_4 = zedGraphControl4.GraphPane;


            // Изменим тест надписи по оси X, Y
            pane_1.XAxis.Title.Text = "Ось X";
            pane_1.YAxis.Title.Text = "Ось Y";
            pane_2.XAxis.Title.Text = "Ось X";
            pane_2.YAxis.Title.Text = "Ось Y";
            pane_3.XAxis.Title.Text = "Ось X";
            pane_3.YAxis.Title.Text = "Ось Y";
            pane_4.XAxis.Title.Text = "Ось X";
            pane_4.YAxis.Title.Text = "Ось Y";

            // Изменим текст заголовка графика
            pane_1.Title.Text = "y = ax + b";
            pane_2.Title.Text = "y = -ax + b";
            pane_3.Title.Text = "y = ce^(alpha * x)";
            pane_4.Title.Text = "y = ce^(-alpha * x)";


            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane_1.CurveList.Clear();
            pane_2.CurveList.Clear();
            pane_3.CurveList.Clear();
            pane_4.CurveList.Clear();

            // Создадим список точек
            PointPairList list_1 = new PointPairList();
            PointPairList list_2 = new PointPairList();
            PointPairList list_3 = new PointPairList();
            PointPairList list_4 = new PointPairList();

            double xmin = 0;
            double xmax = 100;

            double xmin_limit = -1;
            double xmax_limit = 105;
            double x_3max_limit = 50;
            double x_4max_limit = 200;
            double x_4min_limit = -100;

            double ymin_limit = -1;
            double ymax_limit = 200;
            double y_4max_limit = 50;

            // Заполняем список точек
            for (double x = xmin; x <= xmax; x += 0.01)
            {
                list_1.Add(x, f_1(x));
                list_2.Add(x, f_2(x));
                list_3.Add(x, f_3(x));
            }
            for (double x = -100; x <= xmax; x += 0.01)
            {
                list_4.Add(x, f_4(x));
            }

            // Создадим кривую
            pane_1.AddCurve("", list_1, Color.Blue, SymbolType.None);
            pane_2.AddCurve("", list_2, Color.Blue, SymbolType.None);
            pane_3.AddCurve("", list_3, Color.Blue, SymbolType.None);
            pane_4.AddCurve("", list_4, Color.Blue, SymbolType.None);

            // Устанавливаем интересующий нас интервал по оси X
            pane_1.XAxis.Scale.Min = xmin_limit;
            pane_1.XAxis.Scale.Max = xmax_limit;
            pane_2.XAxis.Scale.Min = xmin_limit;
            pane_2.XAxis.Scale.Max = xmax_limit;
            pane_3.XAxis.Scale.Max = x_3max_limit;
            pane_4.XAxis.Scale.Min = x_4min_limit;
            pane_4.XAxis.Scale.Max = x_4max_limit;

            pane_2.YAxis.Scale.Min = ymin_limit;
            pane_2.YAxis.Scale.Max = ymax_limit;
            pane_3.YAxis.Scale.Min = ymin_limit;
            pane_3.YAxis.Scale.Max = ymax_limit;
            pane_4.YAxis.Scale.Max = y_4max_limit;   

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();
            zedGraphControl4.Invalidate();
        }
    }
}
