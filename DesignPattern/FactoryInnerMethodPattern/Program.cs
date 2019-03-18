using System;

namespace FactoryMethodPattern
{


    // This code does not follow SOLID principle 
    // voliating Open-close principle
    public class Point
    {

        private double _x, _y;
        private Point(double a, double b)
        {
            this._x = a;
            this._y = b;
        }

        public override string ToString()
        {
            return $"{nameof(_x)}:{_x}  and {nameof(_y)}:{_y}  ";
        }

        public static class Factory
        {
            public static Point Origin = new Point(0, 0);

            // factory method for Cartesian Point
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            // factory method for polar Point
            public static Point NewPolarPoint(double rho, double theta)
            {
                double x = rho * Math.Cos(theta);
                double y = rho * Math.Sin(theta);
                return new Point(x, y);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var polarPoint = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(polarPoint.ToString());

            Console.WriteLine(Point.Factory.Origin.ToString());

            Console.WriteLine("Hello World!");
        }
    }
}
