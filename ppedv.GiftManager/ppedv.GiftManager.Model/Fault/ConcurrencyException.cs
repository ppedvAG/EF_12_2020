using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.GiftManager.Model.Fault
{
    public class ConcurrencyException : Exception
    {
        public Action UserWins { get; set; }
        public Action DbWins { get; set; }

        public ConcurrencyException(string msg) : base(msg)
        {
        }
    }
}
