// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {

	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            // Set is virtually same as Regular
            Special v = new Regular();
            v.print(t, n, p);
        }
    }
}

