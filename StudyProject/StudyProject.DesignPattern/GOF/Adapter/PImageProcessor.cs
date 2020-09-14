using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.Adapter
{
    class PImageProcessor
    {
        public string Subject
        {
            get;
            set;
        }

        public string Picture
        {
            get;
            private set;
        }
        public PImageProcessor()
        {
            Subject = string.Empty;
            Picture = string.Empty;
        }

        public void ImageProcessing()
        {
            Picture = Subject.Replace("people", "PEOPLE");
        }

    }
}
