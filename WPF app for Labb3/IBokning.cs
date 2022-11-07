using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_app_for_Labb3
{
    internal interface IBokning
    {
        DateTime date { get; set; }
        string dateOnly { get; set; }
        string time { get; set; }
        string name { get; set; }
        int tableNumber { get; set; }

    }
}
