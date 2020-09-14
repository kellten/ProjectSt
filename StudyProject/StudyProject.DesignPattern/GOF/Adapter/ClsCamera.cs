using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF.Adapter
{
    class ClsCamera
    {
        PImageProcessor pi_processor;

        public ClsCamera(PImageProcessor pi_processor)
        {
            this.pi_processor = pi_processor;
        }

        public string TakeAPicture(string subject)
        {
            pi_processor.Subject = subject;
            pi_processor.ImageProcessing();
            return pi_processor.Picture;
        }
    }
}
