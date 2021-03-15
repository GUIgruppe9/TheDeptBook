using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDeptBook.Model
{
    public class Debtor
    {
        private string id;
        private string name;
        private double debtValue;

        public Debtor() { }

        public Debtor(string dId, string dName, double dValue)
        {
            id = dId;
            name = dName;
            debtValue = dValue;
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
                name = value;
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
                debtValue = value;
            }
        }
    }
}
