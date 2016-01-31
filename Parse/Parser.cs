// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp R
//    R    -> rest
//         |  . exp )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
        private Scanner scanner;
        public Parser(Scanner s) { scanner = s; }

        // parseExp() parses the next token
        public Node parseExp()
        {
            return parseExp(scanner.getNextToken());
        }


        private Node parseExp(Token t)
        {
            if (t == null)
            {
                //Console.Error.WriteLine("Warning null token!!!");
                return null;
            }
            else if (t.getType() == TokenType.LPAREN)
            {
                return parseRest();
            }
            else if (t.getType() == TokenType.FALSE)
            {
                return BoolLit.getFalse();
            }
            else if (t.getType() == TokenType.TRUE)
            {
                return BoolLit.getTrue();
            }
            else if (t.getType() == TokenType.QUOTE)
            {
                // quote token does not have a name, so give it name "quote"
                //               cons
                //               /  \
                //            quote cons
                //                  /  \
                //                exp  Nil
                return new Cons(new Ident("quote"), new Cons(parseExp(), Nil.getNil()));
            }
            else if (t.getType() == TokenType.INT)
            {
                return new IntLit(t.getIntVal());
            }
            else if (t.getType() == TokenType.STRING)
            {
                return new StringLit(t.getStringVal());
            }
            else if (t.getType() == TokenType.IDENT)
            {
                return new Ident(t.getName());
            }
            else if (t.getType() == TokenType.DOT)
            {
                Console.Error.WriteLine("Warning illegal DOT token");
                return parseExp();
            }
            else if (t.getType() == TokenType.RPAREN)
            {
                Console.Error.WriteLine("Warning illegal RPAREN token");
                return parseExp();
            }
            else
            {
                Console.Error.WriteLine("Warning illegal token");
                return parseExp();
            }
        }

        public Node parseRest()
        {
            return parseRest(scanner.getNextToken());
        }

        private Node parseRest(Token t)
        {
            if (t == null)
            {
                //Console.Error.WriteLine("Warning null token!!!");
                return null;
            }
            // list expression ened with RPAREN, return Nil to end the list
            else if (t.getType() == TokenType.RPAREN)
            {
                return Nil.getNil();
            }
            else if (t.getType() == TokenType.DOT)
            {
                Console.Error.WriteLine("Warning illegal DOT token");
                return parseRest();
            }
            //           cons
            //           /  \
            //         exp  cons                     last cons node can be
            //              /  \                              cons
            //            exp  cons                           /  \
            //                .......                        .   exp
            //                    \                             
            //                    cons                         
            //                    /  \
            //                  exp   Nil    
            Node a = parseExp(t);
            Node b = parseR();
            return new Cons(a, b);
        }

        public Node parseR()
        {
            return parseR(scanner.getNextToken());
        }
        
        private Node parseR(Token t)
        {
            if (t.getType() == TokenType.DOT)
            {
                Node a = parseExp();
                Token lookahead = scanner.getNextToken();
                if (lookahead.getType() == TokenType.RPAREN)
                {
                    return a;
                }
                else
                {
                    Console.Error.WriteLine("Warning more than one exp token after dot");
                    // discard exps between '. exp' and ')'
                    do
                    {
                        lookahead = scanner.getNextToken();
                    } while (lookahead.getType() != TokenType.RPAREN || lookahead == null);
                    if (lookahead == null)
                    {
                        return null;
                    }
                    else
                    {
                        return a;
                    }
                }
            }
            return parseRest(t);
        }
    }
}

