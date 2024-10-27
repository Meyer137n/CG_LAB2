
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CG_LAB2
{
    public partial class Form1 : Form
    {
        private List<Polygon> polygons = new List<Polygon>(); // Список многоугольников для отрисовки
        private float[,] zBuffer; // Z-буфер для хранения глубины
        private int screenWidth, screenHeight; // Ширина и высота экрана
        private Random random = new Random(); // Генератор случайных чисел
        string filePath;

        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
            screenWidth = this.ClientSize.Width; // Получаем ширину клиентской области
            screenHeight = this.ClientSize.Height; // Получаем высоту клиентской области
            zBuffer = new float[screenWidth, screenHeight]; // Инициализация Z-буфера
        }

        // Метод для выбора файла и возврата его пути
        private string SelectFile(string filter = "Все файлы (*.*)|*.*")
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter; // Устанавливаем фильтр для типов файлов
                openFileDialog.Title = "Выберите файл"; // Заголовок диалогового окна

                // Открываем диалог выбора файла и проверяем, был ли выбран файл
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName; // Возвращаем полный путь к выбранному файлу
                }
            }
            return null; // Если файл не был выбран, возвращаем null
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

                        vertices.Add(new Point3D(x + screenWidth/2, y + screenHeight/2, z)); // Добавляем вершину с учетом центра
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

                float z = random.Next(0, 60); // Генерация случайной z-координаты

                // Генерация вершин многоугольника
                for (int j = 0; j < numVertices; j++)
                {
                    double angle = 2 * Math.PI * j / numVertices; // Вычисление угла
                    x = (int)(50 * Math.Cos(angle)) + shiftX;  // x-координата со смещением
                    y = (int)(50 * Math.Sin(angle)) + shiftY;  // y-координата

                    vertices.Add(new Point3D(x + screenWidth/2, y + screenHeight/2, z)); // Добавляем вершину с учетом центра
                }
                shiftX += random.Next(-100, 100); // Случайное смещение по X
                shiftY += random.Next(-100, 100); // Случайное смещение по Y

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
        private void openButton_Click(object sender, EventArgs e)
        {
            ClearDrawingArea(); // Очищаем область рисования перед новой отрисовкой
            filePath = SelectFile("Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"); // Открываем диалог выбора файла

            // Проверяем, был ли файл выбран
            if (!string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show($"Выбранный файл: {filePath}"); // Выводим путь к выбранному файлу
            }
            else
            {
                MessageBox.Show("Файл не был выбран."); // Сообщение, если файл не выбран
            }           
        }

        // Обработчик события нажатия кнопки
        private void loadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(filePath);
            ClearDrawingArea(); // Очищаем область рисования перед новой отрисовкой
            LoadPolygonsFromFile(filePath); // Считываем многоугольники из файла
            DrawPolygons(); // Обновляем форму, вызывая перерисовку
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
                    DrawColorSquares();
                }
            }
        }

        // Метод, вызываемый для отрисовки видимых граней
        public void RenderAlgorithm()
        {
            using (Graphics g = this.CreateGraphics())
            {
                ResetZBuffer(); // Сброс Z-буфера перед отрисовкой
                Console.WriteLine("\n\n Новая обработка");
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

        // Метод для отрисовки маленьких квадратов и подписей вершин многоугольников
        private void DrawColorSquares()
        {
            using (Graphics g = this.CreateGraphics())
            {
                int squareSize = 20; // Размер квадрата 
                int offsetX = 30; // Смещение по X для подписей
                int offsetY = 150; // Смещение по Y для подписей
                int spacing = 20; // Отступ между квадратами

                for (int i = 0; i < polygons.Count; i++)
                {
                    // Рисуем квадрат с цветом многоугольника
                    g.FillRectangle(new SolidBrush(polygons[i].getColor()), offsetX, offsetY + (i * (squareSize + spacing)), squareSize, squareSize);
                    List<Point3D> points = polygons[i].getPoints(); // Список точек текущего многоугольника

                    for (int k = 0; k < points.Count; k++)
                    {
                        // Подписываем квадрат (например, номер вершины)
                        g.DrawString($"Многоугольник {i + 1}    Z = {points[k].getZ()}", this.Font, Brushes.Black, offsetX + squareSize + 2, offsetY + (i * (squareSize + spacing)));
                        offsetY += 20;
                    }
                }
            }
        }
    }
}
