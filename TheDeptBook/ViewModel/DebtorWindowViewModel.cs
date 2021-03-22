using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Commands;
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
        
        #region Properties

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

        private double tbxNewDebit_Content;

        public double TbxNewDebit_Content
        {
            get { return tbxNewDebit_Content; }
            set { SetProperty(ref tbxNewDebit_Content, value); }
        }
       
        #endregion

        #region Commands

        ICommand _addDebitCommand;
        public ICommand AddDebit
        {
            get
            {
                return _addDebitCommand ?? (_addDebitCommand = new DelegateCommand(() =>
                {
                    int newId;
                    if (Debits.Any())
                    {
                        newId = Int32.Parse(Debits.Last().ID) + 1;
                    }
                    else
                    {
                        newId = 0;
                    }
                    
                    Debits.Add(new Debit(newId.ToString(), TbxNewDebit_Content));
                    currentIndex = Debits.Count - 1;
                    CurrentDebtor.UpdateBalance();
                }));
            }
        }

        ICommand _removeDebitCommand;

        public ICommand RemoveDebit
        {
            get
            {
                return _removeDebitCommand ?? (_removeDebitCommand = new DelegateCommand(() =>
                {
                    Debits.Remove(CurrentDebit);
                    currentIndex = Debits.Count - 1;
                }));
            }
        }
        #endregion
    }
}
