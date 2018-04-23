using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Solution
{
    public class Bank
    {
        private Stock stock;

        public string Name { get; set; }

        public Bank(string name, Stock stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.Update += Stock_Replenish;
        }

        private void Stock_Replenish(object sender, StockUpdateEventArgs replenishInfo)
        {
            if (replenishInfo.Euro > 40)
                Console.WriteLine("Банк {0} продает евро;  Курс евро: {1}", this.Name, replenishInfo.Euro);
            else
                Console.WriteLine("Банк {0} покупает евро;  Курс евро: {1}", this.Name, replenishInfo.Euro);
        }

        public void StopTrade()
        {
            stock.Update -= Stock_Replenish;
            stock = null;
        }
    }
}
