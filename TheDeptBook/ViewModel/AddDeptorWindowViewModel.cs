using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook.Model;

namespace TheDeptBook.ViewModel
{
    class AddDebtorWindowViewModel : BindableBase
    {

        public AddDebtorWindowViewModel( Debtor debtor)
        {
            newDebtor = debtor;
        }

        #region Properties

       
        Debtor newdebtor;

        public Debtor newDebtor
        {
            get { return newdebtor; }
            set { SetProperty(ref newdebtor, value); }
        }

        public bool IsValid
        {
            get
            {
                bool isValid = true;

                if (string.IsNullOrWhiteSpace(newDebtor.Name))
                {
                    isValid = false;
                }

                if (double.IsNaN(newDebtor.Value))
                {
                    isValid = false;
                }

                return isValid;
            }
        }

        #endregion

        #region Commands

        ICommand _addBtnCommand;
        public ICommand AddBtnCommand
        {
            get
            {
                return _addBtnCommand ?? (_addBtnCommand = new DelegateCommand(
                        SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                    .ObservesProperty(() => newDebtor.Name)
                    .ObservesProperty(() => newDebtor.Value));
            }
        }

        private void SaveBtnCommand_Execute()
        {
            // Intet behøves herinde
        }

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        #endregion
    }

}