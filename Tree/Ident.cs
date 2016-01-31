// Ident -- Parse tree node class for representing identifiers

using System;

namespace Tree
{
    public class Ident : Node
    {
        private string name;

        public Ident(string n)
        {
            name = n.ToLower();
        }

        public override void print(int n)
        {
            if (n < 0)
            {
                Console.Write(new String(' ', -n - 1));
                Console.Write(name);
            }
            else
            {
                Console.Write(new String(' ', n));
                Console.WriteLine(name);
            }
        }
        public override bool isSymbol()
        {
            return true;
        }
        public String getName()
        {
            return name;
        }
    }
}

