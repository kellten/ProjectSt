using System;
using System.Windows.Forms;
using StudyProject.DesignPattern.GOF.AbstractFactoryMethod;
using static StudyProject.DesignPattern.Util.ClsFunc;

namespace StudyProject.DesignPattern.Forms
{
    public partial class FrmAbstactFactoryMethod1 : Form
    {
        public FrmAbstactFactoryMethod1()
        {
            InitializeComponent();
        }

        IMakeCamera[] factories = new IMakeCamera[2];
        
        public void Tester()
        {
            factories[0] = new ClsEvDayFactory();
            factories[1] = new ClsHoDayFactory();
        }
        // 카메라 호환성 테스트
        private void TestCase(AbClsCamera camera, ITake lens)
        {
            WriteRcText(rcText, "테스트");

            if (camera.PutInLens(lens) == false)
            {
                WriteRcText(rcText, "카메라에 장착이 되지 않았음");
            }
            else
            {
                
            }


            if (camera.TakeAPicture() == false)
            {
                WriteRcText(rcText, "사진이 찍히지 않았습니다.");
            }
        }
        public void Test()
        {
            TestDirect(); // 직접카메라와 렌즈를 생성하여 호환성 테스트
            TestUsingFactory(); // 팩토리를 통해 생성하여 호환성 테스트
            
        }
        private void TestUsingFactory()
        {
            AbClsCamera camera = null;
            ITake lens = null;

            foreach (IMakeCamera factory in factories)
            {
                camera = factory.MakeCamera();
                lens = factory.MakeLens();

                TestCase(camera, lens); // 호환성 테스트
            }
        }

        private void TestDirect()
        {
            AbClsCamera camera = new ClsEveCamera();
            ITake lens = new ClsHoLens();
            TestCase(camera, lens); // 호환성 테스트
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Tester();
            Test();
        }
    }
}
