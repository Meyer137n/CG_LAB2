using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading; // Не забудьте добавить эту директиву


namespace CG_LAB2
{
    // Класс для многоугольника
    public class Polygon
    {
        private List<Point3D> points; // Список вершин многоугольника
        private Color color; // Цвет для отрисовки многоугольника

        // Конструктор класса Polygon
        public Polygon(List<Point3D> points, Color color)
        {
            this.points = points; // Инициализируем вершины
            this.color = color; // Инициализируем цвет
        }

        public List<Point3D> getPoints()
        {
            return points;
        }

        public Color getColor()
        {
            return color;
        }

        // Метод для отрисовки каркасного многоугольника
        public void Carcas(Graphics g)
        {
            // Проверяем, достаточно ли точек для формирования многоугольника
            if (points.Count < 3) return;

            // Создаем перо с цветом многоугольника
            using (Pen pen = new Pen(color))
            {
                // Проходим по всем вершинам многоугольника
                for (int i = 0; i < points.Count; i++)
                {
                    Point3D p1 = points[i]; // Текущая вершина
                    Point3D p2 = points[(i + 1) % points.Count]; // Следующая вершина (с циклическим доступом)

                    // Рисуем отрезок между двумя вершинами
                    g.DrawLine(pen, p1.getX(), p1.getY(), p2.getX(), p2.getY());
                }
            }
        }

        // Метод для отрисовки заполненного многоугольника
        public void Draw(Graphics g, float[,] zBuffer, int screenWidth, int screenHeight)
        {
            // Проверяем, достаточно ли точек для формирования многоугольника
            if (points.Count < 3) return;

            // Находим минимальные и максимальные Y координаты
            float minY = float.MaxValue, maxY = float.MinValue;
            foreach (var point in points)
            {
                if (point.getY() < minY) minY = point.getY(); // Обновляем минимальную Y координату
                if (point.getY() > maxY) maxY = point.getY(); // Обновляем максимальную Y координату
            }

            // Для каждой строки выполняем построчный скан
            for (float y = minY; y <= maxY; y++)
            {
                // Пропускаем, если текущая Y координата вне экранных границ
                if (y < 0 || y >= screenHeight) continue;

                // Находим все пересечения многоугольника с текущей линией сканирования
                List<Tuple<int, float>> intersections = new List<Tuple<int, float>>(); // Список пересечений X и Z

                for (int i = 0; i < points.Count; i++)
                {
                    Point3D p1 = points[i]; // Текущая вершина
                    Point3D p2 = points[(i + 1) % points.Count]; // Следующая вершина

                    // Проверяем, пересекает ли отрезок текущую линию сканирования
                    if ((p1.getY() <= y && p2.getY() > y) || (p2.getY() <= y && p1.getY() > y))
                    {
                        // Находим X координату пересечения и интерполируем Z
                        float intersectX = p1.getX() + (y - p1.getY()) * (p2.getX() - p1.getX()) / (p2.getY() - p1.getY());
                        float intersectZ = p1.getZ() + (y - p1.getY()) * (p2.getZ() - p1.getZ()) / (p2.getY() - p1.getY());
                        intersections.Add(new Tuple<int, float>(Convert.ToInt32(intersectX), intersectZ)); // Добавляем пересечение в список
                    }
                }

                // Сортируем пересечения по X
                intersections.Sort((a, b) => a.Item1.CompareTo(b.Item1));
                // Обработка видимых частей многоугольника
                for (int i = 0; i < intersections.Count - 1; i += 2)
                {
                    int xStart = intersections[i].Item1; // Начальная X координата
                    float zStart = intersections[i].Item2; // Z координата для начального X
                    int xEnd = intersections[i + 1].Item1; // Конечная X координата
                    float zEnd = intersections[i + 1].Item2; // Z координата для конечного X

                    // Линейная интерполяция Z между началом и концом
                    for (int x = xStart; x <= xEnd; x++)
                    {
                        // Пропускаем, если X координата вне экранных границ
                        if (x < 0 || x >= screenWidth) continue;

                        // Вычисляем Z для текущего X с использованием интерполяции
                        float z = zStart + (x - xStart) * (zEnd - zStart) / (xEnd - xStart);

                        // Проверяем Z-буфер
                        if (z < zBuffer[Convert.ToInt32(x), Convert.ToInt32(y)])
                        {
                            if (zBuffer[Convert.ToInt32(x), Convert.ToInt32(y)] != float.MaxValue)
                            {
                                Console.WriteLine(" [ X = {0} ][ Y = {1} ][ Z = {2} ]", x, y, zBuffer[Convert.ToInt32(x), Convert.ToInt32(y)]);
                            }
                            // Обновляем Z-буфер и рисуем пиксель с нужным цветом
                            zBuffer[Convert.ToInt32(x), Convert.ToInt32(y)] = z;
                            g.FillRectangle(new SolidBrush(color), x, y, 1, 1); // Рисуем пиксель
                        }
                    }
                    Thread.Sleep(1);
                }
            }
        }
    }
}
