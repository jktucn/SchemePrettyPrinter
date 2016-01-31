// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {

        public Regular() {   }

        public override void print(Node t, int n, bool p)
        {
            // print leading spaces and '('
            if (n < 0)
            {
                n = -(n + 1);
            }
            Console.Write(new string(' ', n) + "(");
            // counting the number of unclosed '('
            pcount++;
            // print the first element after '(', with no leading space no change line 
            t.getCar().print(-1);
            // print the following element, until Nil or a non-list at cdr
            while (t.getCdr().isPair())
            {
                t = t.getCdr();
                // print a space before print an element
                Console.Write(" ");
                t.getCar().print(-1);
            }
            // going to print Nil or a non-list at cdr, change the number of unclosed '('
            pcount--;
            // last element is Nil
            if (t.getCdr().isNull())
            {
                // regular expression is not finished when pcount > 0
                if (pcount > 0)
                {
                    t.getCdr().print(-1, true);
                    // if n > 0, there is indent, so change line after the sub-expression
                    if (n > 0)
                    {
                        Console.WriteLine();
                    }
                }
                // regular expression is finished, change line after the print
                else
                {
                    t.getCdr().print(0, true);
                }
            }
            // last elemet is a non-list at cdr
            else
            {
                Console.Write(" . ");
                t.getCdr().print(-1);
                Console.Write(")");
                if (pcount == 0 || n > 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}


