using SmartHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class Raum(string name, decimal deffinierteZimmertemperatur, bool isPersonInRaum, RaumTyp raumTyp, IWettersensor wettersensor)
    {
        public string Name { get; set; } = name;

        public decimal DeffinierteZimmertemperatur { get; set; } = deffinierteZimmertemperatur;

        public bool IsPersonInRaum { get; set; } = isPersonInRaum;

        public RaumTyp RaumTyp { get; set; } = raumTyp;

        public ICollection<Akteur> Aktoren { get; } = [];
        

        public void ZimmertemperaturPruefen()
        {
            var aussenTemperatur = wettersensor.GetAussentemperatur();
            var windgeschwindigkeit = wettersensor.GetWindgeschwindikeit();
            var isRegen = wettersensor.IsRegen();

            if (aussenTemperatur < DeffinierteZimmertemperatur)
            {
                var heizungsventil = Aktoren.Where(a => a.AkteurTyp == AkteurTyp.Heizungsventil).FirstOrDefault();

                heizungsventil?.Trigger("öffnen");
            }

            if (aussenTemperatur > DeffinierteZimmertemperatur)
            {
                var markiesensteuerung = Aktoren.Where(a => a.AkteurTyp == AkteurTyp.Markisensteuerung).FirstOrDefault();
                var jalousienensteuerung = Aktoren.Where(a => a.AkteurTyp == AkteurTyp.Jalousienensteuerung).FirstOrDefault();

                if (windgeschwindigkeit < 30 && isRegen is false)
                {
                    markiesensteuerung?.Trigger("ausfahren");
                }

                if(this.IsPersonInRaum is false)
                {
                    jalousienensteuerung?.Trigger("runterfahren");
                }
            }
        }

        public void AddAkteur(Akteur akteur)
        {
            switch (akteur.AkteurTyp)
            {
                case AkteurTyp.Heizungsventil:
                    AddHeinzungsventil(akteur); 
                    break;
                case AkteurTyp.Jalousienensteuerung:
                    AddJalousienensteuerung(akteur);
                    break;
                case AkteurTyp.Markisensteuerung:
                    AddMarkisensteuerung(akteur);
                    break;
            }
        }
        
        private void AddHeinzungsventil(Akteur akteur)
        {
            if (this.RaumTyp is RaumTyp.Wintergarten || this.RaumTyp is RaumTyp.Garage)
                return;

            this.Aktoren.Add(akteur);
            Console.WriteLine($"Heinzungsventil wurde zu {this.Name} hinzugefügt.");
        }

        private void AddJalousienensteuerung(Akteur akteur)
        {
            if (this.RaumTyp is RaumTyp.Bad || this.RaumTyp is RaumTyp.Garage)
                return;

            this.Aktoren.Add(akteur);
            Console.WriteLine($"Jalousienensteuerung wurde zu {this.Name} hinzugefügt.");
        }

        private void AddMarkisensteuerung(Akteur akteur)
        {
            if (this.RaumTyp is RaumTyp.Wintergarten)
                return;

            this.Aktoren.Add(akteur);
            Console.WriteLine($"Markisensteuerung wurde zu {this.Name} hinzugefügt.");
        }
    }
}
