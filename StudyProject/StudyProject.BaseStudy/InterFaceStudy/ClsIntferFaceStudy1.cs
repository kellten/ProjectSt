using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.BaseStudy.InterFaceStudy
{
    class ClsIntferFaceStudy1
    {
    }

    interface Exam
    {
        string Exam_method();
    }
    class Test : Exam
    {
        public string Exam_method()
        {
            return "Test Method입니다.";
        }
    }
    class Test1 : Exam
    {
        public string Exam_method()
        {
            return "Test Method1입니다.";
        }
    }
    class Test2 : Exam
    {
        public string Exam_method()
        {
            return "Test Method2입니다.";
        }
    }
}
