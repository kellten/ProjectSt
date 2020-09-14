using System;
using System.Windows.Forms;
using StudyProject.DesignPattern.GOF;
using static StudyProject.DesignPattern.Util.ClsFunc;

namespace StudyProject.DesignPattern.Forms
{
    public partial class FrmFactoryMethod1 : Form
    {
        public FrmFactoryMethod1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            EnemyGenerator[] enemyGenerator = new EnemyGenerator[2];
            enemyGenerator[0] = new PatternAGenerator();
            enemyGenerator[1] = new PatternBGenerator();

            enemyGenerator[0].CreateEnemys();
            enemyGenerator[1].CreateEnemys();

            WriteRcText(rcText, enemyGenerator[0].DisplayEnemys());
            WriteRcText(rcText, enemyGenerator[1].DisplayEnemys());

        }
    }
}
