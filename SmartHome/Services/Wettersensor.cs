using System.Timers;

namespace SmartHome.Services
{

    public class Wettersensor : IWettersensor
    {
        private static decimal aussentemperatur;
        private static int windgeschwindigkeit;
        private static bool regen;
        private Random random;
        private System.Timers.Timer timer;

        public Wettersensor()
        {
            aussentemperatur = 15m; 
            windgeschwindigkeit = 10; 
            regen = false;
            random = new Random();

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Tick();
            Console.WriteLine($"Neue Temperatur: {aussentemperatur}°C, Wind: {windgeschwindigkeit} km/h, Regen: {regen}");
        }

        public decimal GetAussentemperatur()
        {
            return aussentemperatur;
        }

        public int GetWindgeschwindikeit()
        {
            return windgeschwindigkeit;
        }

        public bool IsRegen()
        {
            return regen;
        }

        public void Tick()
        {
            decimal temperaturAenderung = (decimal)(random.NextDouble() * 1 - 0.5);
            aussentemperatur += temperaturAenderung;

            if (aussentemperatur < -5)
            {
                aussentemperatur = -5;
            }
            else if (aussentemperatur > 30)
            {
                aussentemperatur = 30;
            }

            int windAenderung = random.Next(-2, 3);
            windgeschwindigkeit += windAenderung;

            if (windgeschwindigkeit < 0)
            {
                windgeschwindigkeit = 0;
            }
            else if (windgeschwindigkeit > 150)
            {
                windgeschwindigkeit = 150;
            }

            if (random.Next(0, 10) == 0)
            {
                regen = !regen;
            }
        }
    }
}
