using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.CSharpControl.Class
{
    public class ClsCar
    {
        private string _make;
        private string _model;
        private int _year;

        public ClsCar(string make, string model, int year)
        {
            _make = make;
            _model = model;
            _year = year;
        }

        public string Make { get { return _make; }  set { _make = value; } }
        public string Model { get { return _model; } set { _model = value; } }
        public int Year { get { return _year; } set { _year = value; } }

    }
}
