using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using TheDeptBook.ViewModel;


namespace TheDeptBook.Model
{
    public class Debtor : BindableBase
    {
        private string id;
        private string name;
        private double debtValue;
        public ObservableCollection<Debit> Debits;

        public Debtor() { }

        public Debtor(string dId, string dName, double dValue)
        {
            id = dId;
            name = dName;
            //debtValue = dValue;
            Debits = new ObservableCollection<Debit>();
            Debits.Add(new Debit(0.ToString(), dValue));
            UpdateBalance();
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public double Value
        {
            get
            {
                return debtValue;
            }
            set
            {
                SetProperty(ref debtValue, value);
            }
        }

        public void UpdateBalance()
        {
            Value = 0;
            foreach (var debit in Debits)
            {
                Value += debit.DebitValue;
            }
        }
    }
}
