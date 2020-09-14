using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    interface IMakeCamera // 호환성 있는 렌즈와 카메라를 생성하는 기능에 대한 약속
    {
        ITake MakeLens();
        AbClsCamera MakeCamera();
    }
}
