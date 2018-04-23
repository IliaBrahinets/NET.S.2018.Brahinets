using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class Broker
    {
        private Stock stock;

        public string Name { get; set; }

        public Broker(string name, Stock stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.Update += Stock_Replenish;
        }

        private void Stock_Replenish(object sender, StockInfoEventArgs replenishInfo)
        {
            if (replenishInfo.USD > 30)
                Console.WriteLine("Брокер {0} продает доллары;  Курс доллара: {1}", this.Name, replenishInfo.USD);
            else
                Console.WriteLine("Брокер {0} покупает доллары;  Курс доллара: {1}", this.Name, replenishInfo.USD);
        }

        public void StopTrade()
        {
            stock.Update -= Stock_Replenish;
            stock = null;
        }
    }
}
