// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {

	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            // Let is virtually same as Begin
            Special v = new Begin();
            v.print(t, n, p);
        }
    }
}


