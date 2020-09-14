using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyProject.DesignPattern.Util;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    class ClsEvLens : ITake
    {
        public void Take() // Take에 약속된 기능을 구체적으로 구현
        {
            TakeWrite("부드럽다");
        }

        public string TakeWrite(string str)
        {
            return str;
        }

        public string AutoFocus()
        {
            return "자동 초점 조절됩니다.";
        }

    }
}
