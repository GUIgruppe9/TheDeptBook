using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using TheDeptBook.Model;

namespace TheDeptBook.ViewModel
{
    public class DebtorWindowViewModel : BindableBase
    {
        public DebtorWindowViewModel(Debtor debtor)
        {
            CurrentDebtor = debtor;
            debits = CurrentDebtor.Debits;
        }

        public DebtorWindowViewModel()
        {
            Debits = new ObservableCollection<Debit>
            {
                new Debit("1", 500)
            };
            //CurrentDebtor = Application.Current.MainWindow

        }

        Debtor currentDebtor;
        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set { SetProperty(ref currentDebtor, value); }
        }

        private ObservableCollection<Debit> debits;
        public ObservableCollection<Debit> Debits
        {
            get { return debits; }
            set { SetProperty(ref debits, value); }
        }

        private Debit currentDebit = null;
        public Debit CurrentDebit
        {
            get { return currentDebit; }
            set { SetProperty(ref currentDebit, value); }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }

    }
}
