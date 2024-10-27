using System.ComponentModel;
using System.Timers;

namespace SmartHome.Services
{
    public class Wettersensor : IWettersensor
    {
        private decimal aussentemperatur;
        private decimal windgeschwindigkeit;
        private bool regen;

        private readonly Random random = new Random();
        public event PropertyChangedEventHandler? PropertyChanged;

        public Wettersensor() {
            aussentemperatur = 20;
            windgeschwindigkeit = 30;
            regen = false;
        }

        public decimal Aussentemperatur
        {
            get => aussentemperatur;
            private set
            {
                if (aussentemperatur != value)
                {
                    aussentemperatur = value;
                    OnPropertyChanged(nameof(Aussentemperatur));
                }
            }
        }

        public decimal Windgeschwindigkeit
        {
            get => windgeschwindigkeit;
            private set
            {
                if (windgeschwindigkeit != value)
                {
                    windgeschwindigkeit = value;
                    OnPropertyChanged(nameof(Windgeschwindigkeit));
                }
            }
        }

        public bool Regen
        {
            get => regen;
            private set
            {
                if (regen != value)
                {
                    regen = value;
                    OnPropertyChanged(nameof(Regen));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Tick()
        {
            Aussentemperatur += (decimal)(random.NextDouble() - 0.5);
            Aussentemperatur = Math.Clamp(Aussentemperatur, -5, 30);

            Windgeschwindigkeit += random.Next(-2, 2);
            Windgeschwindigkeit = Math.Max(0, Math.Min(Windgeschwindigkeit, 150));

            Regen = random.Next(0, 10) == 0;
            Console.WriteLine($"Neue Temperatur: {Aussentemperatur.ToString("F1")}°C, Wind: {Windgeschwindigkeit} km/h, Regen: {Regen}");
        }
    }
}
