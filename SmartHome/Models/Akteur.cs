using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Akteur(AkteurTyp akteurTyp)
    {
        public AkteurTyp AkteurTyp { get; set; } = akteurTyp;

        public void Trigger(string action)
        {
            Console.WriteLine($"{AkteurTyp.ToString()} führt Aktion {action} aus.");
        }
    }
}
