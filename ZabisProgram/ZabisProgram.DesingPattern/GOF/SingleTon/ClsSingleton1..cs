using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabisProgram.DesingPattern.GOF.SingleTon
{
    class ClsSingleton1
    {
        private int a;
        private static ClsSingleton1 singleton;
        private ClsSingleton1()
        {
            a = 0;
        }

        public static ClsSingleton1 GetInstance()
        {
            if (singleton == null)
            {
                singleton = new ClsSingleton1();
                return singleton;
            }

            return singleton;
        }

        internal void SetA(int val)
        {
            this.a = val;
        }

        internal int GetA()
        {
            return a;
        }
    }
}
