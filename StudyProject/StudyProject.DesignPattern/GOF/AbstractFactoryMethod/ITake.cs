using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    interface ITake // 렌즈의 기능에 대한 추상적 약속
    {
        void Take(); // 상을 맺히는 기능에 대한 약속
    }
}
