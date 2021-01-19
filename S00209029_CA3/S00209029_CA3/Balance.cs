using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S00209029_CA3
{
    //Class implementing an added player-betting functionality.
    class Balance
    {
        public int Funds  { get; set; }

        public Balance(int startingFunds)
        {
            Funds = startingFunds;
        }

        public void AddFunds(int funds)
        {
            Funds += funds;
        }

        public void SubtractFunds(int funds)
        {
            Funds -= funds;
        }

        public override string ToString()
        {
            return ("Balance: " + Funds + ".");
        }
    }
}
