
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace calc
{


    abstract class Character
    {
        protected Character first;
        protected Character next;
        abstract public float split(string expression);
        public void Set(Character next, Character first)
        {
            this.first = first;
            this.next = next;
        }
    }

    abstract class Operator : Character
    {
        protected char charac;
        abstract public float execute(float expression1, float expression2);
        public override float split(string expression)
        {
            for(int i = expression.Length - 1; i >= 0; i--)
            {
                if(expression[i] == charac)
                {
                    string[] result = two(expression, i);
                    float a = first.split(result[0]);
                    float b = first.split(result[1]);
                    return execute(a, b);
                }
            }
            if (next != null) return next.split(expression);
            else return 0;
            throw new NotImplementedException();
        }
        private string[] two(string expression, int place)
        {
            string[] strings = { "", "" };
            for(int i = 0; i < place; i++)
            {
                strings[0] += expression[i];
            }
            for (int i = place + 1; i < expression.Length; i++)
            {
                strings[1] += expression[i];
            }
            return strings;
        }
    }
    class Adding : Operator
    {
        public Adding()
        {
            charac = '+';
        }
        public override float execute(float expression1, float expression2)
        {
            return expression1 + expression2;
        }
    }
    class Subtract : Operator
    {
        public Subtract()
        {
            charac = '-';
        }
        public override float execute(float expression1, float expression2)
        {
            return expression1 - expression2;
        }
    }
    class Multiply : Operator
    {
        public Multiply()
        {
            charac = '*';
        }
        public override float execute(float expression1, float expression2)
        {
            return expression1 * expression2;
        }
    }
    class Divide : Operator
    {
        public Divide()
        {
            charac = '/';
        }
        public override float execute(float expression1, float expression2)
        {
            return expression1 / expression2;
        }
    }
    class Number : Character
    {
        private char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',' };
        private List<char> numb = new List<char>();
        
        public override float split(string expression)
        {
            foreach (char i in numbers)
            {
                numb.Add(i);
            }
            foreach (char i in expression)
            {
                if (!(numb.Contains(i))) return next.split(expression);
            }
            return float.Parse(expression);
        }
    }

    class Brackets : Character
    {
        public override float split(string expression)
        {
            int numberOfBrackets = 0;
            int[] isIt = new int[expression.Length];
            for (int i = 0; i < expression.Length; i++)
            {
                isIt[i] = 0;
            }
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(') isIt[i] = 1;
                if (expression[i] == ')') isIt[i] = 2;
            }
            int j = 0;
            List<string> pieces = new List<string>();
            List<string> result = new List<string>();
            List<char> charaList = new List<char>();
            bool first = true;
            for (int i = 0; i < expression.Length; i++)
            {
                if (i == expression.Length - 1 && expression[i] != '(' && expression[i] != ')')
                {
                    if (first)
                    {
                        pieces.Add(expression[i].ToString());
                        first = false;
                    }
                    else
                    {
                        pieces[j] += expression[i];
                    }
                }
                else if (expression[i] == ')')
                {
                    j++;
                    if (i != expression.Length-1) charaList.Add(expression[i + 1]);
                    i++;
                    first = true;
                }
                else if (expression[i] != '(' && expression[i+1] != '(' && expression[i] != ')')
                {
                    if (first)
                    {
                        pieces.Add(expression[i].ToString());
                        first = false;
                    }
                    else
                    {
                        pieces[j] += expression[i];
                    }
                }
                else if (expression[i + 1] == '(')
                {
                    j++;
                    charaList.Add(expression[i]);
                    i++;
                    first = true;
                }
            }
            foreach (var piece in pieces)
            {
                result.Add(next.split(piece).ToString());
            }

            string outer = "";
            for (int i = 0; i < result.Count; i++)
            {
                outer += result[i];
                if (i != result.Count - 1) outer += charaList[i];
            }
            return next.split(outer);
        }
    }

}