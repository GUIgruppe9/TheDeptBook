using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        // Properties
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
    }
}
