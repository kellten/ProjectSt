using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    class ClsHoLens : ITake
    {
        public void Take() // Take에 약속된 기능을 구체적으로 구현
        {
            TakeWrite("자연스럽다");
        }

        public string TakeWrite(string str)
        {
            return str;
        }

        public string AutoFocus()
        {
            return "사용자의 명령대로 초점을 잡다.";
        }
    }
}
