
using SmartHome.Models;
using SmartHome.Services;
using System.Timers;

public class Program
{
    private static System.Timers.Timer _timer;

    private static IWettersensor _wettersensor;

    static void Main()
    {
        #region Initialization
        _wettersensor = new Wettersensor();
        Console.Clear();
        #endregion

        #region Timer
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Start();
        #endregion


        var kueche = new Raum(20, false, RaumTyp.Küche, _wettersensor);
        kueche.AddHeinzungsventil();
        kueche.AddJalousienensteuerung();

        var wc = new Raum(19, false, RaumTyp.Bad, _wettersensor, "WC");
        wc.AddHeinzungsventil();

        var bad = new Raum(21, false, RaumTyp.Bad, _wettersensor);
        bad.AddHeinzungsventil();

        var wohnzimmer = new Raum(22.5m, true, RaumTyp.Wohnzimmer, _wettersensor);
        wohnzimmer.AddJalousienensteuerung();
        wohnzimmer.AddHeinzungsventil();

        var schlafzimmer = new Raum(20, false, RaumTyp.Schlafzimmer, _wettersensor);
        schlafzimmer.AddHeinzungsventil();
        schlafzimmer.AddJalousienensteuerung();

        var gaesteSchlafzimmer = new Raum(20, false, RaumTyp.Schlafzimmer, _wettersensor, "Gäste-Schlafzimmer");
        gaesteSchlafzimmer.AddHeinzungsventil();
        gaesteSchlafzimmer.AddJalousienensteuerung();

        var wintergarten = new Raum(23, false, RaumTyp.Wintergarten, _wettersensor);
        wintergarten.AddMarkisensteuerung();
        wintergarten.AddJalousienensteuerung();

        var garage = new Raum(15, false, RaumTyp.Garage, _wettersensor);

        Console.ReadLine(); 
    }

    private static void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        _wettersensor.Tick();
    }
}