using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.AbstractFactoryMethod
{
    abstract class AbClsCamera
    {
        protected ITake Lens { get; set; }

        public virtual bool TakeAPicture() // 사진을 찍는 가상 메소드
        {
            if (Lens == null)
            {
                return false;
            }

            Lens.Take(); // 렌즈에 상을 맺힌다.

            return true;
        }

        public abstract bool PutInLens(ITake itake);

        public ITake GetOutLens()
        {
            ITake re = Lens;
            Lens = null;
            return re;
        }

        public AbClsCamera()
        {
            Lens = null;
        }
    }
}
