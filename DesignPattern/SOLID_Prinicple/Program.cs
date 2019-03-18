using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http.Headers;
namespace SOLID_OpenClosePrinicple
{

    public enum Size
    {
        VerySmall,
        Small,
        Medium,
        Large
    }

    public enum Color
    {
        Red,
        Blue,
        Green
    }


    public class Product
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }

        public Product(string name, Color color, Size size)
        {
            this.Name = name;
            this.Size = size;
            this.Color = color;
        }

    }



    public interface ISpecification<T>
    {
        bool IsSatisfy(T t);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;
        public ColorSpecification(Color color)
        {
            this._color = color;
        }

        public bool IsSatisfy(Product t)
        {
            if (t.Color == _color)
                return true;
            else
                return false;
        }
    }


    public class SizeSpecification : ISpecification<Product>
    {
        private Size _Size;
        public SizeSpecification(Size size)
        {
            this._Size = size;
        }

        public bool IsSatisfy(Product t)
        {
            if (t.Size == _Size)
                return true;
            else
                return false;
        }
    }

    public class SizeAndColorSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first;
        private ISpecification<T> second;
        public SizeAndColorSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
        {
            this.first = spec1;
            this.second = spec2;
        }

        public bool IsSatisfy(T t)
        {
            if (this.first.IsSatisfy(t) && this.second.IsSatisfy(t))
                return true;
            else
                return false;
        }
    }



    public interface IFilter<T>
    {
        IEnumerable<T> FilterProduct(IEnumerable<T> products, ISpecification<T> specification);
    }

    public class ProductFilter : IFilter<Product>
    {
        public IEnumerable<Product> FilterProduct(IEnumerable<Product> products, ISpecification<Product> specification)
        {
            foreach (var item in products)
            {
                if (specification.IsSatisfy(item))
                    yield return item;
            }
        }
    }















    class Program
    {
        static void Main(string[] args)
        {


            var tree = new Product("Tree", Color.Blue, Size.Large);
            var apple = new Product("apple", Color.Green, Size.Small);
            var house = new Product("House", Color.Green, Size.Large);
            Product[] products = { tree, apple, house };



            //ProductFilter productFilter = new ProductFilter();
            //var filterProduct = productFilter.FilterProduct(products, new SizeSpecification(Size.Large));
            //foreach (var item in filterProduct)
            //{
            //    Console.WriteLine($"Product Name:{item.Name}  ----  Size:{item.Size}  ----  Color:{item.Color}  ");

            //}


            ProductFilter productFilter = new ProductFilter();
            var filterProduct = productFilter.FilterProduct(products, new SizeAndColorSpecification<Product>(
                       new ColorSpecification(Color.Green),
                  new SizeSpecification(Size.Large)));


            foreach (var item in filterProduct)
            {
                Console.WriteLine($"Product Name:{item.Name}  ----  Size:{item.Size}  ----  Color:{item.Color}  ");

            }




            //ProductFilter productFilter = new ProductFilter();
            //var filteredProduct = productFilter.FilterBySize(products, Size.Large);
            //foreach (var item in filteredProduct)
            //{
            //    Console.WriteLine($"Product Name:{item.Name}  ----  Size:{item.Size}  ----  Color:{item.Color}  ");
            //}

            Console.Read();
        }
    }












    ///// <summary>
    /////  Filter product by color or size
    ///// </summary>

    //public class ProductFilter
    //{
    //    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    //    {
    //        foreach (var item in products)
    //        {
    //            if (item.Color == color)
    //            {
    //                yield return item;
    //            }
    //        }
    //    }

    //    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    //    {
    //        foreach (var item in products)
    //        {
    //            if (item.Size == size)
    //            {
    //                yield return item;
    //            }
    //        }
    //    }

    //    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size, Color color)
    //    {
    //        foreach (var item in products)
    //        {
    //            if (item.Size == size && item.Color == color)
    //            {
    //                yield return item;
    //            }
    //        }
    //    }

    //}



}

