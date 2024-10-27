using SmartHome.Services;

namespace SmartHome.Models
{
    public abstract class Akteur
    {
        public readonly IWettersensor _wettersensor;

        protected Akteur(IWettersensor sensor)
        {
            _wettersensor = sensor;
        }

        public abstract void Trigger(Raum raum);

        protected void LogStatus(string steuerungName, string raumName,string status)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{steuerungName} im Raum {raumName} wird {status}");
            Console.ResetColor();
        }
    }
}
