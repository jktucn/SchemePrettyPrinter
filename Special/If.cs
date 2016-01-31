// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
        public If() { }

        public override void print(Node t, int n, bool p)
        {
            // print leading spaces and '('
            if (n < 0)
            {
                n = -(n + 1);
            }
            Console.Write(new string(' ', n) + "(");
            // record the current indent
            // common out due to CursorLeft is not supported in mono
            //int indent = Console.CursorLeft - 1;
            pcount++;
            // first element is parinted with no leading space, no change line
            t.getCar().print(-1);
            // if 'if' is the only element (if) or (if . x)
            if(!t.getCdr().isPair())
            {
                pcount--;
                if (t.getCdr().isNull())
                {
                    t.getCdr().print(0, true);
                }
                else
                {
                    Console.Write(" . ");
                    t.getCdr().print(0);
                }
                return;
            }
            // second element is printed with 1 leading space, change line
            else
            {
                t = t.getCdr();
                t.getCar().print(1);
            }
            // following element is printed with further indent, change line
            while (t.getCdr().isPair())
            {
                t = t.getCdr();
                t.getCar().print(pcount * 4);
            }
            // print last element and ')'
            pcount--;
            if (t.getCdr().isNull())
            {
                // ')' is printed with indent, change line
                t.getCdr().print(pcount * 4, true);
            }
            else
            {
                Console.Write(new string(' ', pcount * 4) + ". ");
                t.getCdr().print(0);
                Console.WriteLine(new string(' ', pcount * 4) + ")");
            }


        }
    }
}

