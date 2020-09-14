using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apparelbase.MVVMBase;

namespace WpfCustomControlLibrary1
{
    public class MainViewModel:ObservableObject
    {
        private int testNumber;
        public int TestNumber
        {
            get { return this.testNumber; }
            set
            {
                if (this.testNumber != value)
                {
                    this.testNumber = value;
                    this.RaisePropertyChanged("TestNumber");
                }
            }
        }

        #region PlusCommand()
        private System.Windows.Input.ICommand plusCommand;
        public System.Windows.Input.ICommand PlusCommand
        {
            get { return (this.plusCommand) ?? (this.plusCommand = new DelegateCommand(Plus, CanPlus)); }
        }

        private bool CanPlus()
        {
            if (true)
                return true;
            else
                return false;
        }

        private void Plus()
        {
            this.TestNumber++;
        }

        #endregion

        #region MinusCommand()
        private System.Windows.Input.ICommand minusCommand;
        public System.Windows.Input.ICommand MinusCommand
        {
            get { return (this.MinusCommand) ?? (this.minusCommand = new DelegateCommand(Minus, CanMinus)); }
        }

        private bool CanMinus()
        {
            if (true)
                return true;
            else
                return false;
        }

        private void Minus()
        {
            this.TestNumber++;
        }
        #endregion
    }
}
