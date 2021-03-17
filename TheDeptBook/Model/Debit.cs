using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeptBook.Model
{
    public class Debit
    {
        private string id;
        private DateTime dateTime;
        private double debitValue;

        public Debit() { }

        public Debit(string i, double val)
        {
            id = i;
            dateTime = DateTime.Now;
            debitValue = val;
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

        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
            }
        }

        public double DebitValue
        {
            get { return debitValue; }
            set { debitValue = value; }
        }
    }
}
