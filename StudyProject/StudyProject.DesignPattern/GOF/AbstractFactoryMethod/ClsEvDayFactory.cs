using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    class ClsEvDayFactory : IMakeCamera // Ev렌즈와 Ev 카메라 생성하는 클래스
    {
        public ITake MakeLens() // 호환성 있는 EvLens를 생성하는 기능 구현
        {
            return new ClsEvLens(); 
        }

        public AbClsCamera MakeCamera() // 호환성 있는 EvCamera를 생성하는 기능 구현
        {
            return new ClsEveCamera();
        }

    }
}
