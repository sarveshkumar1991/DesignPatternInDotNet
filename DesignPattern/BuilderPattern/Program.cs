using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern
{


    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }


        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append("\n");
            }

            foreach (var e in elements)
                sb.Append(e.ToStringImpl(indent + 1));

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }

    public class HtmlBuilder
    {
        private string _name;

        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string name, string text = "")
        {
            this._name = name;
            root.Name = name;
            if (!string.IsNullOrWhiteSpace(text))
            {
                root.Text = text;
            }
        }

        public void AddChild(string name, string text)
        {
            root.elements.Add(new HtmlElement()
            {
                Name = name,
                Text = text
            });
        }

        public override string ToString()
        {
            return root.ToString();
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            HtmlBuilder builder = new HtmlBuilder("ul");
            builder.AddChild("li", "apple");
            builder.AddChild("li", "lemon");
            Console.WriteLine(builder.ToString());

            Console.WriteLine("element without child");

            HtmlBuilder builder1 = new HtmlBuilder("strong", "This is headline");
            Console.WriteLine(builder1.ToString());


            Console.WriteLine("Hello World!");
        }
    }
}
