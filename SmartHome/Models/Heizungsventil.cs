using SmartHome.Services;

namespace SmartHome.Models
{
    public class Heizungsventil : Akteur
    {
        public enum HeizungsventilStatus
        {
            geschlossen,
            geöffnet
        }

        public HeizungsventilStatus Status { get; private set; }

        public Heizungsventil(IWettersensor sensor) : base(sensor)
        {
            Status = HeizungsventilStatus.geschlossen;
        }

        public override void Trigger(Raum raum)
        {
            var aussentemperatur = _wettersensor.Aussentemperatur;

            if (raum.DeffinierteZimmertemperatur > aussentemperatur)
            {
                if (Status != HeizungsventilStatus.geöffnet)
                {
                    Status = HeizungsventilStatus.geöffnet;
                    LogStatus(nameof(Heizungsventil),raum.Name, Status.ToString());
                }
            }
            else if (Status != HeizungsventilStatus.geschlossen)
            {
                Status = HeizungsventilStatus.geschlossen;
                LogStatus(nameof(Heizungsventil),raum.Name, Status.ToString());
            }
        }
    }
}