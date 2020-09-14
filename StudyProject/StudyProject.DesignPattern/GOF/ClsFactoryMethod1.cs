using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.DesignPattern.GOF
{
    public enum Enemytype
    {
         Zombie
        ,Slime
    }
    abstract class Enemy
    {
        protected Enemytype type;
        protected string name;
        protected int hp;
        protected int exp;

        public string Name { get { return name; } }
        public int Hp { get { return hp; } }
        public int Exp { get { return Exp; } }
    }

    class Zombie : Enemy
    {
        public Zombie()
        {
            type = Enemytype.Zombie;
            name = "Zombie";
            hp = 100;
            exp = 50;
        }
    }

    class Slime : Enemy
    {
        public Slime()
        {
            type = Enemytype.Slime;
            name = "Slime";
            hp = 200;
            exp = 15;
        }
    }

    abstract class EnemyGenerator
    {
        private List<Enemy> _enemy = new List<Enemy>();
        public EnemyGenerator()
        {
        }

        public List<Enemy> Enemys
        {
            get { return _enemy; }
        }

        public abstract void CreateEnemys(); // Factory Method
        public abstract string DisplayEnemys();
    }

    class PatternAGenerator : EnemyGenerator
    {
        public override void CreateEnemys()
        {
            Enemys.Add(new Zombie());
        }

        public override string DisplayEnemys()
        {
            return Enemys[Enemys.Count - 1].Name;
        }
    }

    class PatternBGenerator : EnemyGenerator
    {
        public override void CreateEnemys()
        {
            Enemys.Add(new Slime());
        }
        public override string DisplayEnemys()
        {
            return Enemys[Enemys.Count - 1].Name;
        }

    }

}
