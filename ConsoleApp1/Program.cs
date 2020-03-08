using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(СircleTriangleSquareCalculator.GetShapeSquare(84, 112, 140));
            Console.WriteLine(СircleTriangleSquareCalculator.GetShapeSquare(2));

            Console.WriteLine(SquareCalculator.GetShapeSquare((1, 2), (4, 5), (4, 2)));
            Console.WriteLine(SquareCalculator.GetShapeSquare((1, 2), (4, 5), (6, 2)));
            Console.WriteLine(SquareCalculator.GetShapeSquare((1, 2), (4, 2)));
        }
    }

    /// <summary>
    /// Этот класс рассчитывает площадь окружности по радиусу и треугольника по 3-м сторонам.
    /// </summary>
    public class СircleTriangleSquareCalculator
    {
        public static double GetShapeSquare(params double[] edges)
        {
            if (!edges.Any())
            {
                throw new ArgumentException("Список сторон пуст", nameof(edges));
            }
            
            if (edges.Length == 2 || edges.Length > 3)
            {
                throw new ArgumentException("Неправильно передано кол-во сторон", nameof(edges));
            }

            // Предполагается, что передан радиус окружности
            if (edges.Length == 1)
            {
                return edges.First().PowOfTwo() * Math.PI;
            }

            // Проверка на то, что треугольник прямоугольный
            var orderedEdges = edges.OrderByDescending(z => z);
            var hypotenuse = orderedEdges.First();
            var cathetus = orderedEdges.Skip(1);
            if (hypotenuse.PowOfTwo().GetPrecision(2) == cathetus.Sum(z => z.PowOfTwo()).GetPrecision(2))
            {
                Console.WriteLine("Это прямоугольный треугольник");
            }
            
            // Вычисляем по формуле Герона:
            // S = √(p * (p - a)*(p - b)*(p - c)), где
            // p - полупериметр треугольника (a+b+c)/2
            // a,b,c - длины сторон треугольника
            var halfPerimeter = edges.Sum() / 2;
            return Math.Sqrt(halfPerimeter * edges.Aggregate(1d, (a, b) => a * (halfPerimeter - b)));
        }
    }

    /// <summary>
    /// Этот класс рассчитывает площадь окружности по координатам радиуса и любого N-угольника по координатам вершин.
    /// </summary>
    public class SquareCalculator
    {
        public static double GetShapeSquare(params (double x, double y)[] points)
        {
            if (!points.Any())
            {
                throw new ArgumentException("Список координат пуст", nameof(points));
            }

            // Площадь точки == 0
            if (points.Length == 1)
            {
                return 0;
            }

            // Предполагается, что переданы координаты радиуса окружности
            if (points.Length == 2)
            {
                return (points[0].x - points[1].x).PowOfTwo() + (points[0].y - points[1].y).PowOfTwo() * Math.PI;
            }

            points = points.Append(points[0]).ToArray();

            // Проверка на то, что треугольник прямоугольный
            if (points.Length - 1 == 3)
            {
                var orderedEdgesPowOfTwo = points
                    .Take(points.Length - 1)
                    .Select((p, i) => (p.x - points[i + 1].x).PowOfTwo() + (p.y - points[i + 1].y).PowOfTwo())
                    .OrderByDescending(z => z);

                var hypotenuse = orderedEdgesPowOfTwo.First();
                var cathetus = orderedEdgesPowOfTwo.Skip(1);
                if (hypotenuse.GetPrecision(2) == cathetus.Sum().GetPrecision(2))
                {
                    Console.WriteLine("Это прямоугольный треугольник");
                }
            }

            // Вычисляем по формуле Гаусса:
            var pointsSum = points
                .Take(points.Length - 1)
                .Select((p, i) => p.x * points[i + 1].y - points[i + 1].x * p.y);

            return Math.Abs(pointsSum.Sum() / 2);
        }
    }

    public static class MathHelper
    {
        public static double GetPrecision(this double integer, int precision) => Math.Round(integer, precision);
        public static double PowOfTwo(this double integer) => Math.Pow(integer, 2);
    }
}