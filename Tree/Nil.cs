// Nil -- Parse tree node class for representing the empty list
// using Singleton Pattern

using System;

namespace Tree
{
    public class Nil : Node
    {
        private static readonly Nil INSTANCE = new Nil();
        private Nil() { }

        public static Nil getNil()
        {
            return INSTANCE;
        }
  
        public override void print(int n)
        {
            print(n, false);
        }

        public override void print(int n, bool p)
        {
            if (n < 0)
            {
                Console.Write(new String(' ', -n - 1));
                if (p)
                {
                    Console.Write(")");
                }
                else
                    Console.Write("()");
            }
            else
            {
                Console.Write(new String(' ', n));
                if (p)
                {
                    Console.WriteLine(")");
                }
                else
                    Console.WriteLine("()");
            }
        }
        public override bool isNull()
        {
            return true;
        }
    }
}
