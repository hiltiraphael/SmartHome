using SmartHome.Services;
using System.ComponentModel;

namespace SmartHome.Models
{
    public class Raum
    {
        public string Name { get; set; }

        public decimal DeffinierteZimmertemperatur { get; set; }

        public bool IsPersonInRaum { get; set; }

        public RaumTyp RaumTyp { get; set; }

        public ICollection<Akteur> Aktoren { get; } = [];

        readonly IWettersensor _wettersensor;
        
        public Raum() { }

        public Raum(decimal deffinierteZimmertemperatur, bool isPersonInRaum, RaumTyp raumTyp, IWettersensor wettersensor, string? name = null)
        {
            Name = name ?? raumTyp.ToString();
            DeffinierteZimmertemperatur = deffinierteZimmertemperatur;
            IsPersonInRaum = isPersonInRaum;
            RaumTyp = raumTyp;
            _wettersensor = wettersensor;
            _wettersensor.PropertyChanged += Wettersensor_PropertyChanged;
        }

        private void Wettersensor_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            foreach(var akteur in Aktoren)
            {
                akteur.Trigger(this);
            }
        }

        public void AddHeinzungsventil()
        {
            if (this.RaumTyp is RaumTyp.Wintergarten || this.RaumTyp is RaumTyp.Garage)
                return;

            this.Aktoren.Add(new Heizungsventil(_wettersensor));
            Console.WriteLine($"Heinzungsventil wurde zu {this.Name} hinzugefügt.");
        }

        public void AddJalousienensteuerung()
        {
            if (this.RaumTyp is RaumTyp.Bad || this.RaumTyp is RaumTyp.Garage)
                return;

            this.Aktoren.Add(new Jalousie(_wettersensor));
            Console.WriteLine($"Jalousienensteuerung wurde zu {this.Name} hinzugefügt.");
        }

        public void AddMarkisensteuerung()
        {
            if (this.RaumTyp is not RaumTyp.Wintergarten)
                return;

            this.Aktoren.Add(new Markise(_wettersensor));
            Console.WriteLine($"Markisensteuerung wurde zu {this.Name} hinzugefügt.");
        }
    }
}
