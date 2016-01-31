// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf = new char[BUFSIZE];

        public Scanner(TextReader i) { In = i; }

        // auxiliary method
        private bool isWhiteSpace(int ch)
        {
            return ((ch >= 9 && ch <= 13) || ch == 32);
        }
        private bool isInitial(int ch)
        {
            return (isLetter(ch) || isSpecialInitial(ch));
        }
        private bool isLetter(int ch)
        {
            return ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'));
        }
        private bool isSpecialInitial(int ch)
        {
            return ( ch == '!' || ch == '$' || ch == '%' || ch == '&' || ch == '*' || ch == '/' || ch == ':' || 
                ch == '<' || ch == '=' || ch == '>' || ch == '?' || ch == '^' || ch == '_' || ch == '~');
        }
        private bool isSubsequent(int ch)
        {
            return (isInitial(ch) || isDigit(ch) || isSpecialSubsequent(ch));
        }
        private bool isDigit(int ch)
        {
            return (ch >= '0' && ch <= '9');
        }
        private bool isSpecialSubsequent(int ch)
        {
            return (ch == '+' || ch == '-' || ch == '.' || ch == '@');
        }
        private bool isPeculiarIdentifier(int ch)
        {
            return (ch == '+' || ch == '-');
        }

        public Token getNextToken()
        {
            int ch;

            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.
                ch = In.Read();
   
                // skip white space
                while (isWhiteSpace(ch))
                {
                    ch = In.Read();
                }
                // skip comments
                while (ch == ';')
                {
                    ch = In.Read();
                    while (ch != 10 && ch != -1)
                    {
                        ch = In.Read();
                    }
                    // skip the white space after a comment
                    while (isWhiteSpace(ch))
                    {
                        ch = In.Read();
                    }
                }

                if (ch == -1)
                {
                    //Console.Error.WriteLine("reach the end of file");
                    return null;
                }

                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);

                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    // scan a string into the buffer variable buf
                    int j = 0;
                    ch = In.Read();
                    while (ch != '"')
                    {
                        if (ch == -1)
                        {
                            Console.Error.WriteLine("Unexpected EOF following \"");
                            return null;
                        }
                        buf[j++] = (char)ch;
                        ch = In.Read();
                    }
                    return new StringToken(new String(buf, 0, j));
                }


                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = ch - '0';
                    // scan the number and convert it to an integer

                    // make sure that the character following the integer
                    // is not removed from the input stream
                    ch = In.Peek();
                    while (ch >= '0' && ch <= '9')
                    {

                        In.Read();
                        i = i * 10 + ch - '0';
                        ch = In.Peek();
                    }
                    return new IntToken(i);
                }

                // Identifiers
                // Peculiar Identifier
                else if (isPeculiarIdentifier(ch))
                {
                    return new IdentToken(new String((char)ch, 1));
                }
                // Normal Identifier
                else if (isInitial(ch))
                {
                    // scan an identifier into the buffer

                    // make sure that the character following the integer
                    // is not removed from the input stream
                    int j = 0;
                    buf[j++] = (char)ch;
                    ch = In.Peek();
                    while (isSubsequent(ch))
                    {
                        In.Read();
                        buf[j++] = (char)ch;
                        ch = In.Peek();
                    }
                    return new IdentToken(new String(buf, 0, j));
                }

                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '"
                                            + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        }
    }

}

