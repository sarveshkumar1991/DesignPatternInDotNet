using System;

namespace SingletonPattern
{

    public sealed class Employee
    {
        public static int count = 0;
        public static Employee employee = null;

        private Employee()
        {
            count += 1;
            Console.WriteLine($"Instance count:{count}");
        }

        public static Employee GetInstance()
        {
            if (employee == null)
            {
                employee = new Employee();
            }
            return employee;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            Employee a = Employee.GetInstance();
            Employee b = Employee.GetInstance();

            Console.WriteLine("Hello World!");
        }
    }
}
