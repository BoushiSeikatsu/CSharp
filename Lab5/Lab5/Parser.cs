using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Parser
    {
        private Scanner scanner;
        private Token token;
        private List<string> Rules = new List<string>();
        public Parser(Scanner scanner)
        {
            this.Error = false;
            this.scanner = scanner;
            this.token = scanner.NextToken();
        }
        public bool Error { get; set; }
        public void doParse()
        {
            E();
            if(Error)
            {
                Console.WriteLine("ERROR");
            }
            else
            {
                foreach(var rule in Rules)
                {
                    Console.Write(rule + " ");
                }
            }
        }
        private void Expect(TokenType expectedSymbol)
        {
            if (token.Type == expectedSymbol) this.token = scanner.NextToken();
            else Error = true;
        }
        private void E()
        {
            Rules.Add("1");
            T();
            E1();
        }
        private void E1()
        {
            if (token.Type == TokenType.PLUS)
            {
                Rules.Add("2");
                Expect(TokenType.PLUS);
                T();
                E1();
            }
            else if(token.Type == TokenType.MINUS)
            {
                Rules.Add("3");
                Expect(TokenType.MINUS);
                T();
                E1();
            }
            else 
            {
                Rules.Add("4");
            }
            /*else
            {
                Error = true;
            }*/
        }
        private void T()
        {
            Rules.Add("5");
            F();
            T1();
        }
        private void T1()
        {
            if (token.Type == TokenType.MUL)
            {
                Expect(TokenType.MUL);
                Rules.Add("6");
                F();
                T1();
            }
            else if (token.Type == TokenType.DIV)
            {
                Expect(TokenType.DIV);
                Rules.Add("7");
                F();
                T1();
            }
            else
            {
                Rules.Add("8");
            }
            /*else
            {
                Error = true;
            }*/
        }
        private void F()
        {
            if(token.Type == TokenType.LeftPAR)
            {
                Rules.Add("9");
                Expect(TokenType.LeftPAR);
                E();
                Expect(TokenType.RightPAR);
            }
            else if(token.Type == TokenType.NUMBER)
            {
                Rules.Add("10");
                Expect(TokenType.NUMBER);
            }
            else
            {
                Error = true;
            }
        }
    }
}
