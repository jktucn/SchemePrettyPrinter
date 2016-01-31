// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	public Begin() { }

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
            // int indent = Console.CursorLeft - 1;
            pcount++;
            // change line after printing the first element
            t.getCar().print(0);
            // print the following element, until Nil or a non-list at cdr
            while (t.getCdr().isPair())
            {
                t = t.getCdr();
                t.getCar().print(pcount * 4);
            }
            // going to print last element
            pcount--;
            // last element is Nil
            if (t.getCdr().isNull())
            {
                // print ')' at the same indent as '('
                t.getCdr().print(pcount * 4, true);
            }
            // last element is a non-list
            else
            {
                Console.Write(new string(' ', pcount * 4) + ". ");
                t.getCdr().print(0);
                Console.WriteLine(new string(' ', pcount * 4) + ")");
            }
        }
    }
}

