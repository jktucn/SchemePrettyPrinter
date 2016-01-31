author:	Steven T Truong
		Xi Yang

The program is implemented by following the instruction in the project specification, the comments in the skeleton code, and the tips in class presentation.
The following two list represents the parse tree in the program where each exp can be a sub list represented by the same structure.

			   cons
			   /  \
			 exp  cons                     last cons node can be
				  /  \                              cons
				exp  cons                           /  \
					.......                        .   exp
						\                              
						cons                         
						/  \
					  exp   Nil
					  
. exp is given special consideration in parsing and following printing process. In this case, exp is attached to the cdr side and the list does not have Nil.
There are 3 fundermental forms of printing style: Regular, Begin, and If. Other forms are the same or derived from the fundermental forms.
In nested expression, the inner expression is printed by the style that correspong to it, so as the outer expression. Except in the case of quote, where the outermost expression is printed by regular form.
In the form that needs indent, the program can record the position of the open parenthesis, producing a neat output.
Under our test, the program behaved as we would expect it to.

p.s. The mono does not support Console.CursorLeft to retrieve the exact cursor postion, so we have to reimplement the indent. The program's outpout is not as neat as it originally is.