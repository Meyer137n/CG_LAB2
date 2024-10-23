using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CG_LAB2
{
    public partial class Form1 : Form
    {
        private List<Polygon> polygons = new List<Polygon>();
        private float[,] zBuffer; // Z-буфер для хранения глубины
        private int screenWidth, screenHeight;
        private Random random = new Random(); // Генератор случайных чисел

        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
            screenWidth = this.ClientSize.Width;
            screenHeight = this.ClientSize.Height;
            zBuffer = new float[screenWidth, screenHeight]; // Инициализация Z-буфера
        }

        // Обработчик события нажатия кнопки
        private void drawButton_Click(object sender, EventArgs e)
        {
            GenerateRandomPolygons(); // Генерация случайных многоугольников
            Invalidate(); // Обновляем форму, вызывая перерисовку
        }


        // Обработчик события нажатия кнопки
        private void loadButton_Click(object sender, EventArgs e)
        {
            LoadPolygonsFromFile("V://polygons.txt"); // Считываем многоугольники из файла
            Invalidate(); // Обновляем форму, вызывая перерисовку
        }

        // Чтение многоугольников из файла
        private void LoadPolygonsFromFile(string filePath)
        {
            polygons.Clear(); // Очищаем список многоугольников
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                List<Point3D> vertices = new List<Point3D>();
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        if (vertices.Count > 0)
                        {
                            // Генерация случайного цвета для многоугольника
                            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                            polygons.Add(new Polygon(vertices, color)); // Добавляем многоугольник в список
                            vertices = new List<Point3D>(); // Очищаем для следующего многоугольника
                        }
                    }
                    else
                    {
                        // Парсим строку в координаты x, y, z
                        string[] parts = line.Split(' ');
                        int x = int.Parse(parts[0]);
                        int y = int.Parse(parts[1]);
                        int z = int.Parse(parts[2]);

                        vertices.Add(new Point3D(x+300, y+400, z)); // Добавляем вершину
                    }
                }

                // Добавляем последний многоугольник, если он не был добавлен
                if (vertices.Count > 0)
                {
                    Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    polygons.Add(new Polygon(vertices, color));
                }

                ResetZBuffer(); // Сбрасываем Z-буфер
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        // Генерация 5-6 случайных многоугольников с 3-6 вершинами
        private void GenerateRandomPolygons()
        {
            polygons.Clear(); // Очищаем список многоугольников
            float shiftX = 0;
            float x, y;

            int numPolygons = random.Next(5, 7); // Генерируем от 5 до 6 многоугольников
            for (int i = 0; i < numPolygons; i++)
            {
                int numVertices = random.Next(3, 7); // Генерируем от 3 до 6 вершин для каждого многоугольника
                List<Point3D> vertices = new List<Point3D>();

                float z = random.Next(-20, 20); // z-координата (случайная высота)

                // Генерируем вершины многоугольника
                for (int j = 0; j < numVertices; j++)
                {
                    double angle = 2 * Math.PI * j / numVertices;
                    x = (int)(50 * Math.Cos(angle)) + shiftX;  // x-координата со смещением
                    y = (int)(50 * Math.Sin(angle));           // y-координата

                    vertices.Add(new Point3D(x + 400, y + 400, z));
                }
                shiftX += 50;

                // Генерируем случайный цвет для многоугольника
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                // Добавляем сгенерированный многоугольник в список
                polygons.Add(new Polygon(vertices, color));
            }

            ResetZBuffer(); // Сбрасываем Z-буфер
        }

        // Инициализация Z-буфера
        private void ResetZBuffer()
        {
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    zBuffer[x, y] = float.MaxValue; // "Бесконечная" глубина
                }
            }
        }

        // Метод отрисовки формы
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            ResetZBuffer(); // Обнуляем Z-буфер перед каждой отрисовкой

            foreach (var polygon in polygons)
            {
                polygon.Draw(g, zBuffer, screenWidth, screenHeight); // Рисуем многоугольник
            }
        }
    }
}
