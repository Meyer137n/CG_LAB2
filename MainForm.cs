using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CG_LAB2
{
    public partial class MainForm : Form
    {
        private float[,] proection;
        private int cenX;
        private int cenY;
        private Graphics _graphics;
        private Random _random = new Random();
        private List<float[,]> polygons;

        public MainForm() => InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cenX = Size.Width / 2 - 700;
            cenY = Size.Height / 2 - 250;

            // Настройка матрицы проекции для изометрического проецирования
            float[,] p =
            {
                { 1, 0, 0, 0},
                { 0, -1, 0, 0},
                { -(float)(Math.Cos(Math.PI/4))/2, (float)(Math.Cos(Math.PI/4))/2, 0, 0},
                { cenX, cenY, 0, 1}
            };
            proection = p;

            GeneratePolygons();
            DrawPolygons();
        }

        // Генерация случайных полигонов (от 3 до 6 вершин) с постепенным смещением вправо
        private void GeneratePolygons()
        {
            polygons = new List<float[,]>();
            float shiftX = 0;  // Переменная для смещения по оси X

            for (int i = 0; i < 6; i++) // Генерируем 5 полигонов
            {
                int verticesCount = _random.Next(3, 7); // Случайное количество рёбер от 3 до 6
                float[,] polygon = new float[verticesCount, 4];

                // Определение вершин на окружности с учётом смещения
                for (int j = 0; j < verticesCount; j++)
                {
                    double angle = 2 * Math.PI * j / verticesCount;
                    polygon[j, 0] = (float)(50 * Math.Cos(angle)) + shiftX;  // x-координата со смещением
                    polygon[j, 1] = (float)(50 * Math.Sin(angle));           // y-координата
                    polygon[j, 2] = _random.Next(-20, 20);                   // z-координата (случайная высота)
                    polygon[j, 3] = 1;                                      // однородная координата
                }
                polygons.Add(polygon);

                // Увеличиваем смещение для следующей фигуры
                shiftX += 70;  // Измените это значение, чтобы контролировать размер смещения
            }
        }

        // Умножение матриц
        private float[,] Mult(float[,] X, float[,] Y)
        {
            float[,] result = new float[X.GetLength(0), Y.GetLength(1)];
            for (int i = 0; i < X.GetLength(0); i++)
                for (int j = 0; j < Y.GetLength(1); j++)
                    for (int k = 0; k < Y.GetLength(0); k++)
                        result[i, j] += X[i, k] * Y[k, j];
            return result;
        }

        // Отрисовка сгенерированных полигонов
        private void DrawPolygons()
        {
            _graphics = CreateGraphics();

            foreach (var polygon in polygons)
            {
                float[,] projectedPolygon = Mult(polygon, proection);
                PointF[] points = new PointF[polygon.GetLength(0)];

                for (int i = 0; i < polygon.GetLength(0); i++)
                {
                    points[i] = new PointF(projectedPolygon[i, 0], projectedPolygon[i, 1]);
                }

                // Нарисовать полигон в 2D после проекции
                _graphics.DrawPolygon(Pens.Blue, points);
            }
        }

        // Сброс на стандартные позиции и вызов отрисовки
        private void buttonDeffaultPosition_Click(object sender, EventArgs e)
        {
            GeneratePolygons();
            DrawPolygons();
        }
    }
}
