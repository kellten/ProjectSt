using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    class ClsHoCamera:AbClsCamera
    {
        public override bool PutInLens(ITake itake)
        {
            ClsHoLens evLens = itake as ClsHoLens; // 호환성 있는 Ho렌즈 형식으로 참조 연상

            if (evLens == null)
            {
                return false; /// 호환성 없는 렌즈일때
            }

            Lens = itake; // 호환성 있는 렌즈일때 장착

            return true;
        }

        public override bool TakeAPicture()
        {
            ClsHoLens evLens = Lens as ClsHoLens;

            if (evLens == null)
            {
                return false;
            }

            evLens.AutoFocus();

            return base.TakeAPicture();

        }
    }
}
