using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TheDeptBook.Model;

namespace TheDeptBook.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Debtor> debtors;

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>
            {
                new Debtor("1", "Marcus", 0.1),
                new Debtor("2", "Mikkel", 500),
                new Debtor("3", "Anders", 44)
            };
            debtors.First().Debits.Add(new Debit("0", 500));
            debtors.First().UpdateBalance();
        }

        /// Properties
        public ObservableCollection<Debtor> Debtors
        {
            get { return debtors; }
            set { SetProperty(ref debtors, value); }
        }

        private Debtor currentDebtor = null;

        public Debtor CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                SetProperty(ref currentDebtor, value);
            }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        // Commands
        ICommand _addDebtorCommand;

        public ICommand AddDebtor
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(() =>
                {
                    var newDebtor = new Debtor();
                    var addvm = new AddDebtorWindowViewModel("Add new debtor", newDebtor);
                    var adddeptor = new AddDebtorWindow()
                    {
                        DataContext = addvm,
                        Owner = App.Current.MainWindow
                    };
                    adddeptor.Show();

                    if (adddeptor.ShowDialog() == true)
                    {
                        Debtors.Add(newDebtor);
                        
                    }
                })); 
                //return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(() =>
                //{
                //    Debtors.Add(new Debtor());
                //    CurrentIndex = Debtors.Count - 1;
                //}));
            }
        }

        ICommand _debtorClickedCommand;

        public ICommand DebtorClicked
        {
            get
            {
                return _debtorClickedCommand ?? (_debtorClickedCommand = new DelegateCommand(() =>
                {
                    var vm = new DebtorWindowViewModel(CurrentDebtor);
                    var debtorWindow = new DebtorWindow()
                    {
                        DataContext = vm,
                        Owner = App.Current.MainWindow
                    };
                    debtorWindow.Show();
                }));
            }
        }
    }
}
