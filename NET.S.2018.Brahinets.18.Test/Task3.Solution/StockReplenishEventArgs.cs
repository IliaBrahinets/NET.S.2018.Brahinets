﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class StockInfoEventArgs:EventArgs
    { 
        public int USD { get; set; }
        public int Euro { get; set; }

        public StockInfoEventArgs(int usd, int euro)
        {
            USD = usd;
            Euro = euro;
        }

        public StockInfoEventArgs()
        {

        }

    }
}
