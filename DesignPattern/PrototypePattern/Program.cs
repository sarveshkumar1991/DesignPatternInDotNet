using System;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace PrototypePattern
{
    public static class EntensionMethod
    {
        public static T BinaryCopy<T>(this T self)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var binaryFormator = new BinaryFormatter();
                binaryFormator.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copyObject = binaryFormator.Deserialize(stream);
                return (T)copyObject;
            }

        }

        public static T XMLCopy<T>(this T self)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var binaryFormator = new XmlSerializer(typeof(T));
                binaryFormator.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copyObject = binaryFormator.Deserialize(stream);
                return (T)copyObject;
            }

        }

    }





    public class Person
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Person(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
        }

        public Person()
        {

        }

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name} and  {nameof(Address)}:{Address} ";
        }
    }

    public class Address
    {
        public int HouseNumber { get; set; }
        public string StreetAddress { get; set; }

        public Address(int houseNumber, string streetAddress)
        {
            this.HouseNumber = houseNumber;
            this.StreetAddress = streetAddress;
        }
        public Address()
        {

        }

        public override string ToString()
        {
            return $"{nameof(HouseNumber)}:{HouseNumber} , {nameof(StreetAddress)}:{StreetAddress} ";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("amit kumar", new Address(123, "Pola Street"));

            Person person2 = person.XMLCopy();
            person2.Name = "amit";
            person2.Address.HouseNumber = 321;


            Console.WriteLine(person.ToString());
            Console.WriteLine(person2.ToString());

            Console.WriteLine("Hello World!");
        }
    }
}
