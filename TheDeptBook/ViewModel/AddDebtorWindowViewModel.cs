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
        public AddDebtorWindowViewModel(string title, Debtor debtor)
        {
            Title = title;
            newDebtor = debtor;
        }

        #region Properties

        string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        Debtor newdebtor;

        public Debtor newDebtor
        {
            get { return newdebtor; }
            set { SetProperty(ref newdebtor, value); }
        }

        #endregion

        #region Commands

        ICommand _saveBtnCommand;

        public ICommand SaveBtnCommand
        {
            get
            {
                return _saveBtnCommand ?? (_saveBtnCommand = new DelegateCommand(
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

        public bool IsValid
        {
            get
            {
                bool isValid = !string.IsNullOrWhiteSpace(newDebtor.Name);
                if (double.IsNaN(newDebtor.Value))
                    isValid = false;
                return isValid;

            }
        }

        #endregion
    }

}
