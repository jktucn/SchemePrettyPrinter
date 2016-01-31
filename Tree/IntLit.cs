// IntLit -- Parse tree node class for representing integer literals

using System;

namespace Tree
{
    public class IntLit : Node
    {
        private int intVal;

        public IntLit(int i)
        {
            intVal = i;
        }

        public override void print(int n)
        {
            if (n < 0)
            {
                Console.Write(new String(' ', -n - 1));
                Console.Write(intVal);
            }
            else
            {
                Console.Write(new String(' ', n));
                Console.WriteLine(intVal);
            }
        }
        public override bool isNumber()
        {
            return true;
        }
    }
}
