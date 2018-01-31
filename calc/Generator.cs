
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace calc
{
    public class Generate
    {
        Character add = new Adding();
        Character sub = new Subtract();
        Character mul = new Multiply();
        Character div = new Divide();
        Character num = new Number();
        public Generate()
        {
            add.Set(sub, num);
            sub.Set(mul, num);
            mul.Set(div, num);
            num.Set(add, num);
            div.Set(null, num);
        }
        public float run(string text)
        {
            return num.split(text);
        }
    }
}