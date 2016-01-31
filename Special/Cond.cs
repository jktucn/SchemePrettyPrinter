// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        {
            // Cond is virtually same as Begin
            Special v = new Begin();
            v.print(t, n, p);
        }
    }
}


