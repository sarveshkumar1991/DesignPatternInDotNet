using System;
using System.Drawing;
using System.Dynamic;
namespace SOLID_LiskovSubstitutionPrinciple
{


    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override string ToString()
        {
            return $" width:{this.Width} and height:{this.Height}";
        }

    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Height = base.Width = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Height = base.Width = value;
            }
        }


    }




    public class LiskovSubProgram
    {

        public static int Area(Rectangle rectangle) => rectangle.Height * rectangle.Width;


        static void Main(string[] args)
        {

            Rectangle rectangle = new Rectangle(3, 6);
            int rectangleArea = Area(rectangle);
            Console.WriteLine($"Area of {nameof(rectangle)} is {rectangleArea} of {rectangle.ToString()}");


            Rectangle square = new Square();
            square.Width = 4;
            int squareArea = Area(square);
            Console.WriteLine($"Area of {nameof(square)} is {squareArea} of {square.ToString()}");



        }
    }
}