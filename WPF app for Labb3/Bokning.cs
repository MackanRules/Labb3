using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_app_for_Labb3
{
    internal class Bokning : IBokning{
        public DateTime date { get; set; }
        public string dateOnly { get; set; }
        public string time { get; set; }
        public string name { get; set; }
        public int tableNumber { get; set; }

        public Bokning(DateTime date, string time, string name, int tableNumber)
        {
            this.date = date;
            this.time = time;
            this.name = name;
            this.tableNumber = tableNumber;
            dateOnly = date.ToShortDateString();
        }

    }
}
