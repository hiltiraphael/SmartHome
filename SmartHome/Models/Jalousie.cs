using SmartHome.Services;

namespace SmartHome.Models
{
    public class Jalousie : Akteur
    {
        public enum JalousienenStatus
        {
            hochgefahren,
            runtergefahren
        }

        public JalousienenStatus Status { get; private set; }

        public Jalousie(IWettersensor sensor) : base(sensor)
        {
            Status = JalousienenStatus.hochgefahren;
        }

        public override void Trigger(Raum raum)
        {
            var aussentemperatur = _wettersensor.Aussentemperatur;

            if (raum.DeffinierteZimmertemperatur < aussentemperatur && !raum.IsPersonInRaum)
            {
                if (Status != JalousienenStatus.runtergefahren)
                {
                    Status = JalousienenStatus.runtergefahren;
                    LogStatus(nameof(Jalousie), raum.Name, Status.ToString());
                }
            }
            else if (Status != JalousienenStatus.hochgefahren)
            {
                Status = JalousienenStatus.hochgefahren;
                LogStatus(nameof(Jalousie), raum.Name,Status.ToString());
            }
        }
    }
}
