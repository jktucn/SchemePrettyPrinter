// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {

	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            // print leading spaces and '
            if (n < 0)
            {
                n = -(n + 1);
            }
            Console.Write(new string(' ', n) + "'");
            // if quote a list
            if (t.getCdr().getCar().isPair())
            {
                // this while is to print following quote as ' to get the sample output
                while (t.getCdr().getCar().getCar().isSymbol() 
                    && ((Ident) t.getCdr().getCar().getCar()).getName().Equals("quote"))
                {
                    Console.Write("'");
                    t = t.getCdr().getCar();
                }
                // using the Regular form to print quoted list
                Special v = new Regular();
                v.print(t.getCdr().getCar(), 0, false);
                // if the caller asks for change line
                if (n > 0)
                {
                    Console.WriteLine();
                }
            }
            // if quote a single element
            else
            {
                t.getCdr().getCar().print(0);
            }
        }
    }
}

