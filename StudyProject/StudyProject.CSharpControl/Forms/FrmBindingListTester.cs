using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace StudyProject.CSharpControl.Forms
{
    public partial class FrmBindingListTester : Form
    {
        BindingList<Class.ClsCar> cars = new BindingList<Class.ClsCar>();
        List<Class.ClsCar> cars2 = new List<Class.ClsCar>();

        public FrmBindingListTester() => InitializeComponent();

        

        private void btnCall_Click(object sender, EventArgs e)
        {
            
            cars.Add(new Class.ClsCar("Ford", "Mustang", 1967));
            cars.Add(new Class.ClsCar("Shelby AC", "Cobra", 1965));
            cars.Add(new Class.ClsCar("Chevrolet", "Corvette Sting Ray", 1965));

            DgvCar.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn makeColumn = new DataGridViewTextBoxColumn();
            makeColumn.DataPropertyName = "Make";
            makeColumn.HeaderText = "The Car's Make";

            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            modelColumn.DataPropertyName = "Model";
            modelColumn.HeaderText = "The Car's Model";

            DataGridViewTextBoxColumn yearColumn = new DataGridViewTextBoxColumn();
            yearColumn.DataPropertyName = "Year";
            yearColumn.HeaderText = "The Car's Year";
            
            DgvCar.Columns.Add(makeColumn);
            DgvCar.Columns.Add(modelColumn);
            DgvCar.Columns.Add(yearColumn);

            DgvCar.DataSource = cars;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cars.Add(new Class.ClsCar("Hundae", "Avantae", 1988));
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            cars2.Add(new Class.ClsCar("Hundae", "Avantae", 1977));

        }

        private void btnCall2_Click(object sender, EventArgs e)
        {
       
            cars2.Add(new Class.ClsCar("Ford", "Mustang", 1967));
            cars2.Add(new Class.ClsCar("Shelby AC", "Cobra", 1965));
            cars2.Add(new Class.ClsCar("Chevrolet", "Corvette Sting Ray", 1965));

            DgvCar.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn makeColumn = new DataGridViewTextBoxColumn();
            makeColumn.DataPropertyName = "Make";
            makeColumn.HeaderText = "The Car's Make";

            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            modelColumn.DataPropertyName = "Model";
            modelColumn.HeaderText = "The Car's Model";

            DataGridViewTextBoxColumn yearColumn = new DataGridViewTextBoxColumn();
            yearColumn.DataPropertyName = "Year";
            yearColumn.HeaderText = "The Car's Year";

            DgvCar.Columns.Add(makeColumn);
            DgvCar.Columns.Add(modelColumn);
            DgvCar.Columns.Add(yearColumn);

            DgvCar.DataSource = cars2;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {

        }
    }
}
