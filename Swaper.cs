using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling1
{
    internal class Swaper
    {
        public static int[,] GetBestPermutation(int[,] matrix)
        {
            int[] indices = new int[matrix.GetLength(0)];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            int[] bestPermutation = (int[])indices.Clone();
            int minDowntime = int.MaxValue;

            do
            {
                int currentDowntime = CalculateDowntime(matrix, indices);
                if (currentDowntime < minDowntime)
                {
                    minDowntime = currentDowntime;
                    bestPermutation = (int[])indices.Clone();
                }
            } while (NextPermutation(indices));

            // Создаем новую матрицу для лучшей перестановки
            int[,] bestMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < bestPermutation.Length; i++)
            {
                bestMatrix[i, 0] = matrix[bestPermutation[i], 0];
                bestMatrix[i, 1] = matrix[bestPermutation[i], 1];
            }

            return bestMatrix;
        }

        static int CalculateDowntime(int[,] matrix, int[] indices)
        {
            int[] x = new int[indices.Length];

            // Вычисляем простои для каждого задания
            for (int i = 0; i < indices.Length; i++)
            {
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += matrix[indices[n], 0]; // Суммируем значения a_i
                }

                int sumDowntime = 0;
                int sumTask1Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += x[m]; // Суммируем предыдущие простои
                    sumTask1Duration += matrix[indices[m], 1]; // Суммируем длительности b_i
                }

                x[i] = Math.Max(0, sumTask1 - sumDowntime - sumTask1Duration); // Вычисляем простои
            }

            // Вычисляем общее время окончания обработки
            int totalTime = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                totalTime += x[i]; // Добавляем простои
                totalTime += matrix[indices[i], 1]; // Добавляем длительности b_i
            }

            return totalTime; // Возвращаем общее время
        }


        static int CalculateDowntimeNx3(int[,] matrix, int[] indices)
        {
            int[] downtime = new int[indices.Length];
            int[] downtimeforc = new int[indices.Length];

            // Вычисляем простои
            for (int i = 0; i < indices.Length; i++)
            {
                int sumTask2 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask2 += matrix[indices[n], 0]; // Суммируем значения a_i
                }

                int sumDowntime = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += downtime[m]; // Суммируем предыдущие простои
                }

                int sumTask2Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumTask2Duration += matrix[indices[m], 1]; // Суммируем длительности b_i
                }

                // Вычисляем downtime для текущего задания
                downtime[i] = Math.Max(0, sumTask2 - sumDowntime - sumTask2Duration);
            }

            // Вычисляем простои для станка C
            for (int i = 0; i < indices.Length; i++)
            {
                // Суммируем downtime от 0 до i
                int sumDowntimeForc = 0;
                for (int m = 0; m <= i; m++)
                {
                    sumDowntimeForc += downtime[m];
                }

                // Суммируем task2 от 0 до i
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += matrix[indices[n], 1]; // сумма b_i
                }

                // Суммируем downtimeforc от 0 до i-1
                int sumDowntimeForc2 = 0;
                for (int n = 0; n < i; n++)
                {
                    sumDowntimeForc2 += downtimeforc[n];
                }

                // Суммируем task2 от 0 до i-1
                int sumTask2Duration = 0;
                for (int n = 0; n < i; n++)
                {
                    sumTask2Duration += matrix[indices[n], 2];
                }

                // Вычисляем downtimeforc[i]
                downtimeforc[i] = Math.Max(0, sumDowntimeForc + sumTask1 - sumDowntimeForc2 - sumTask2Duration);
            }

            // Вычисляем общее время
            int totalTime = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                totalTime += downtimeforc[i];
                totalTime += matrix[indices[i], 2]; // Добавляем длительности c_i
            }

            return totalTime; // Возвращаем общее время
        }


        static bool NextPermutation(int[] arr)
        {
            int n = arr.Length;
            int i = n - 2;

            while (i >= 0 && arr[i] >= arr[i + 1])
                i--;

            if (i < 0)
                return false;

            int j = n - 1;
            while (arr[j] <= arr[i])
                j--;

            Swap(arr, i, j);
            Array.Reverse(arr, i + 1, n - i - 1);
            return true;
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public static int[,] GetBestPermutationNx3(int[,] matrix)
        {
            int[] indices = new int[matrix.GetLength(0)];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            int[] bestPermutation = (int[])indices.Clone();
            int minDowntime = int.MaxValue;

            do
            {
                int currentDowntime = CalculateDowntimeNx3(matrix, indices);
                if (currentDowntime < minDowntime)
                {
                    minDowntime = currentDowntime;
                    bestPermutation = (int[])indices.Clone();
                }
            } while (NextPermutation(indices));

            // Создаем новую матрицу для лучшей перестановки
            int[,] bestMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < bestPermutation.Length; i++)
            {
                bestMatrix[i, 0] = matrix[bestPermutation[i], 0];
                bestMatrix[i, 1] = matrix[bestPermutation[i], 1];
                bestMatrix[i, 2] = matrix[bestPermutation[i], 2];
            }

            return bestMatrix;
        }
    }
}


