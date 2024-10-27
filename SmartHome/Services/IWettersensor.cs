using System.ComponentModel;

namespace SmartHome.Services
{
    public interface IWettersensor : INotifyPropertyChanged
    {
        decimal Aussentemperatur { get;}
        decimal Windgeschwindigkeit { get; }
        bool Regen { get; }

        void Tick();
    }
}
