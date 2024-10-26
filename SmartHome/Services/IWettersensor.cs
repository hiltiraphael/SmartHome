using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public interface IWettersensor
    {
        decimal GetAussentemperatur();

        int GetWindgeschwindikeit();

        bool IsRegen();

        void Tick();
    }
}
