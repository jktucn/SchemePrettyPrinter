// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;

        public StringLit(string s)
        {
            stringVal = s;
        }

        public override void print(int n)
        {
            if (n < 0)
            {
                Console.Write(new String(' ', -n - 1));
                Console.Write("\"" + stringVal + "\"");
            }
            else
            {
                Console.Write(new String(' ', n));
                Console.WriteLine("\"" + stringVal + "\"");
            }
        }
        public override bool isString()
        {
            return true;
        }
    }
}

