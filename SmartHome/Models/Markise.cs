using SmartHome.Services;

namespace SmartHome.Models
{
    public class Markise : Akteur
    {
        public enum MarkisenStatus
        {
            eingefahren,
            ausgefahren
        }

        public MarkisenStatus Status { get; private set; }

        public Markise(IWettersensor sensor) : base( sensor)
        {
            Status = MarkisenStatus.eingefahren;
        }

        public override void Trigger(Raum raum)
        {
            var aussentemperatur = _wettersensor.Aussentemperatur;
            var windgeschwindigkeit = _wettersensor.Windgeschwindigkeit;
            var isRegen = _wettersensor.Regen;

            if (raum.DeffinierteZimmertemperatur < aussentemperatur && windgeschwindigkeit < 30 && !isRegen)
            {
                if (Status != MarkisenStatus.ausgefahren)
                {
                    Status = MarkisenStatus.ausgefahren;
                    LogStatus(nameof(Markise),raum.Name, Status.ToString());
                }
            }
            else if (Status != MarkisenStatus.eingefahren)
            {
                Status = MarkisenStatus.eingefahren;
                LogStatus(nameof(Markise),raum.Name, Status.ToString());
            }
        }
    }
}
