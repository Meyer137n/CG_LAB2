using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CG_LAB2
{
    public partial class Form1 : Form
    {
        private List<Polygon> polygons = new List<Polygon>(); // Список многоугольников для отрисовки
        private float[,] zBuffer; // Z-буфер для хранения глубины
        private int screenWidth, screenHeight; // Ширина и высота экрана
        private Random random = new Random(); // Генератор случайных чисел
        private int cenX; // Центр по X
        private int cenY; // Центр по Y

        public Form1()
        {
            cenX = Size.Width; // Устанавливаем центр по X на ширину формы
            cenY = Size.Height; // Устанавливаем центр по Y на высоту формы
            InitializeComponent(); // Инициализация компонентов формы
            screenWidth = this.ClientSize.Width; // Получаем ширину клиентской области
            screenHeight = this.ClientSize.Height; // Получаем высоту клиентской области
            zBuffer = new float[screenWidth, screenHeight]; // Инициализация Z-буфера
        }

        // Метод для чтения многоугольников из файла
        private void LoadPolygonsFromFile(string filePath)
        {
            polygons.Clear(); // Очищаем список многоугольников
            try
            {
                string[] lines = File.ReadAllLines(filePath); // Читаем все строки из файла
                List<Point3D> vertices = new List<Point3D>(); // Список для хранения вершин многоугольника
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) // Проверяем, является ли строка пустой
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

                        vertices.Add(new Point3D(x + cenX, y + cenY, z)); // Добавляем вершину с учетом центра
                    }
                }

                // Добавляем последний многоугольник, если он не был добавлен
                if (vertices.Count > 0)
                {
                    Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    polygons.Add(new Polygon(vertices, color)); // Добавляем последний многоугольник
                }

                ResetZBuffer(); // Сбрасываем Z-буфер
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}"); // Сообщаем об ошибке при чтении
            }
        }

        // Метод для генерации 5-6 случайных многоугольников с 3-6 вершинами
        private void GenerateRandomPolygons()
        {
            polygons.Clear(); // Очищаем список многоугольников
            float shiftX = 0; // Смещение по X
            float shiftY = 0; // Смещение по Y
            float x, y;

            int numPolygons = random.Next(5, 7); // Генерируем от 5 до 6 многоугольников
            for (int i = 0; i < numPolygons; i++)
            {
                int numVertices = random.Next(3, 7); // Генерируем от 3 до 6 вершин для каждого многоугольника
                List<Point3D> vertices = new List<Point3D>(); // Список вершин многоугольника

                float z = random.Next(-20, 20); // Генерация случайной z-координаты

                // Генерация вершин многоугольника
                for (int j = 0; j < numVertices; j++)
                {
                    double angle = 2 * Math.PI * j / numVertices; // Вычисление угла
                    x = (int)(50 * Math.Cos(angle)) + shiftX;  // x-координата со смещением
                    y = (int)(50 * Math.Sin(angle)) + shiftY;  // y-координата

                    vertices.Add(new Point3D(x + cenX, y + cenY, z)); // Добавляем вершину с учетом центра
                }
                shiftX += random.Next(-50, 100); // Случайное смещение по X
                shiftY += random.Next(-50, 100); // Случайное смещение по Y

                // Генерация случайного цвета для многоугольника
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                // Добавляем сгенерированный многоугольник в список
                polygons.Add(new Polygon(vertices, color));
            }

            ResetZBuffer(); // Сбрасываем Z-буфер
        }

        // Метод для инициализации Z-буфера
        private void ResetZBuffer()
        {
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    zBuffer[x, y] = float.MaxValue; // Устанавливаем "бесконечную" глубину
                }
            }
        }

        // Обработчик события нажатия кнопки для рисования
        private void drawButton_Click(object sender, EventArgs e)
        {
            ClearDrawingArea(); // Очищаем область рисования перед новой отрисовкой
            GenerateRandomPolygons(); // Генерация случайных многоугольников
            DrawPolygons();
        }

        // Обработчик события нажатия кнопки для загрузки многоугольников
        private void loadButton_Click(object sender, EventArgs e)
        {
            ClearDrawingArea(); // Очищаем область рисования перед новой отрисовкой
            LoadPolygonsFromFile("V://polygons.txt"); // Считываем многоугольники из файла
            DrawPolygons();
        }

        // Обработчик события нажатия кнопки для запуска алгоритма
        private void algorithmButton_Click(object sender, EventArgs e)
        {
            RenderAlgorithm(); // Генерация случайных многоугольников
           
        }

        // Метод для отрисовки всех многоугольников
        private void DrawPolygons()
        {
            using (Graphics g = this.CreateGraphics())
            {
                ResetZBuffer(); // Сброс Z-буфера перед отрисовкой

                // Рисуем каркас каждого многоугольника
                foreach (var polygon in polygons)
                {
                    polygon.Carcas(g);
                }
            }
        }

        // Метод, вызываемый для отрисовки видимых граней
        public void RenderAlgorithm()
        {
            using (Graphics g = this.CreateGraphics())
            {
                ResetZBuffer(); // Сброс Z-буфера перед отрисовкой

                // Отрисовка каждого многоугольника
                foreach (var polygon in polygons)
                {
                    polygon.Draw(g, zBuffer, screenWidth, screenHeight);
                }
            }
        }

        // Метод для очистки области рисования
        private void ClearDrawingArea()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(this.BackColor); // Очищаем холст, устанавливая его цвет
            }
        }

    }
}
