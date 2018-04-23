using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class Stock
    {
        public event EventHandler<StockUpdateEventArgs> Update = delegate { };

        public void Market()
        {
            Random rnd = new Random();
            var replenishInfo = new StockUpdateEventArgs();
            replenishInfo.USD = rnd.Next(20, 40);
            replenishInfo.Euro = rnd.Next(30, 50);
            OnReplenish(replenishInfo);          
        }

        protected virtual void OnReplenish(StockUpdateEventArgs args)
        {
            Update(this, args);
        }
    }
}
