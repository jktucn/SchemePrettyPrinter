// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            // if the element following define is not a cons, than this is the variable definition syntax of define
            // this syntax is virtually same as Regular
            if (!t.getCdr().getCar().isPair())
            {
                Special v = new Regular();
                v.print(t, n, p);
            }
            // function definition syntax of define
            // this syntax is virtually same as If
            else
            {
                Special v = new If();
                v.print(t, n, p);
            }
        }
    }
}


