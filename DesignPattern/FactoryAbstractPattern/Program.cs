using System;
using System.Collections.Generic;

namespace FactoryAbstractPattern
{

    public interface IHotDrink
    {
        void Consume();
    }


    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Tea was nice");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee was nice");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink PrepareDrink(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink PrepareDrink(int amount)
        {
            Console.WriteLine($"preapring tea with {amount} of milk");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink PrepareDrink(int amount)
        {
            Console.WriteLine($"preapring coffee with {amount} of milk");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        // // COMMENT code is voilate Open-close principle  so we will use another approch using refection


        // // now using enum to give option to user
        //public enum AvaliableDrink
        //{
        //    Tea, Coffee
        //}

        //private Dictionary<AvaliableDrink, IHotDrinkFactory> factories =
        //new Dictionary<AvaliableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvaliableDrink avaliableDrink in Enum.GetValues(typeof(AvaliableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //        Type.GetType("FactoryAbstractPattern." +
        //            Enum.GetName(typeof(AvaliableDrink), avaliableDrink) +
        //        "Factory")
        //            );
        //        factories.Add(avaliableDrink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvaliableDrink drink, int amount)
        //{
        //    return factories[drink].PrepareDrink(amount);
        //}



        // // now using reflection
        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(new Tuple<string, IHotDrinkFactory>(t.Name.Replace("Factory", ""),
                    (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }


        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Select you drink");
            for (int i = 0; i < factories.Count; i++)
            {
                var tuple = factories[i];
                Console.WriteLine($"{i} : {tuple.Item1}");
            }
            Console.WriteLine("Press enter to exist");

            while (true)
            {
                int option = int.Parse(Console.ReadLine());
                Console.WriteLine("Select your amount: 100ml, 200ml 500ml");
                int amount = int.Parse(Console.ReadLine());
                return factories[option].Item2.PrepareDrink(amount);
            }

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // // approch 1 use
            //HotDrinkMachine hotDrinkMachine = new HotDrinkMachine();
            //var coffee = hotDrinkMachine.MakeDrink(HotDrinkMachine.AvaliableDrink.Coffee, 500);
            //coffee.Consume();



            // // approach 2
            HotDrinkMachine hotDrinkMachine = new HotDrinkMachine();
            IHotDrink hotDrink = hotDrinkMachine.MakeDrink();
            hotDrink.Consume();

            Console.WriteLine("Hello World!");
        }
    }
}
