using System;
using ConsoleApp1;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void СircleTriangleSquareCalculatorTest()
        {
            Assert.Throws<ArgumentException>(() => СircleTriangleSquareCalculator.GetShapeSquare());
            Assert.Throws<ArgumentException>(() => СircleTriangleSquareCalculator.GetShapeSquare(1, 2));
            Assert.Throws<ArgumentException>(() => СircleTriangleSquareCalculator.GetShapeSquare(1, 2, 3, 4));
            Assert.AreEqual(6, СircleTriangleSquareCalculator.GetShapeSquare(3, 4, 5));
            Assert.AreEqual(314.16, СircleTriangleSquareCalculator.GetShapeSquare(10).GetPrecision(2));
        }

        [Test]
        public void SquareCalculatorTest()
        {
            Assert.Throws<ArgumentException>(() => SquareCalculator.GetShapeSquare());
            Assert.AreEqual(0, SquareCalculator.GetShapeSquare((1, 10)));
            Assert.AreEqual(314.16, SquareCalculator.GetShapeSquare((1, 1), (1, 11)).GetPrecision(2));
            Assert.AreEqual(7.5, SquareCalculator.GetShapeSquare((1, 2), (4, 5), (6, 2)));
        }
    }
}