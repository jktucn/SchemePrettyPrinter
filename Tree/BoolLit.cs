// BoolLit -- Parse tree node class for representing boolean literals
// using Singleton Pattern

using System;

namespace Tree
{
    public class BoolLit : Node
    {
        private static readonly BoolLit TRUE = new BoolLit(true);
        private static readonly BoolLit FALSE = new BoolLit(false);
        private bool boolVal;
  
        private BoolLit(bool b)
        {
            boolVal = b;
        }

        public static BoolLit getTrue()
        {
            return TRUE;
        }

        public static BoolLit getFalse()
        {
            return FALSE;
        }
  
        public override void print(int n)
        {
            if (n < 0)
            {
                Console.Write(new String(' ', -n - 1));
                if (boolVal)
                    Console.Write("#t");
                else
                    Console.Write("#f");
            }
            else
            {
                Console.Write(new String(' ', n));
                if (boolVal)
                    Console.WriteLine("#t");
                else
                    Console.WriteLine("#f");
            }
        }
        public override bool isBool()
        {
            return true;
        }
    }
}
