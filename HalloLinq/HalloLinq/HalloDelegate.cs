using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloLinq
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string msg);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDelete = EinfacheMethode;
            Action meineAction = EinfacheMethode;
            Action meineActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action meineActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meineActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAlsAction = MethodeMitPara;
            Action<string> deleMitParaAlsAno = (string txt) => { Console.WriteLine(txt); };
            Action<string> deleMitParaAlsAno2 = (txt) => Console.WriteLine(txt);
            Action<string> deleMitParaAlsAno3 = x => Console.WriteLine(x);

            CalcDelegate calc = Multi;
            //long rsult = calc.Invoke(4, 6);
            Func<int, int, long> calcAlsFunc = Sum;
            CalcDelegate calcAno = (a, b) => { return a + b; };
            CalcDelegate calcAno2 = (a, b) => a + b;

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long Multi(int a, int b)
        {
            return a * b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string txt)
        {
            Console.WriteLine("Hallo: " + txt);
        }

        public void EinfacheMethode()
        {
            System.Console.WriteLine("Hallo");
        }
    }
}
