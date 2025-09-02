using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUtils
{
    public class WeatherLib
    {
       
        public double GetCloudHeight()
        {
            Console.Write("Geben Sie die Temperatur in °C ein: ");
            if (!double.TryParse(Console.ReadLine(), out double temperature))
            {
                Console.WriteLine("Ungültige Eingabe für Temperatur.");
                return 0;
            }
            Console.Write("Geben Sie die relative Luftfeuchtigkeit in % ein: ");
            if (!double.TryParse(Console.ReadLine(), out double humidity))
            {
                Console.WriteLine("Ungültige Eingabe für Luftfeuchtigkeit.");
                return 0;
            }
            double cloudHeight = (temperature - 2.5) * (100 - humidity) / 100;
            return cloudHeight;
        }
        double GetWindchill;
        double GetHeatIndex;
        double Temperature, Humidity;

        public double GetDewPoint()
        {
            double a = 17.27;
            double b = 237.7;
            double alpha = ((a * Temperature) / (b + Temperature)) + Math.Log(Humidity / 100.0);
            return (b * alpha) / (a - alpha);
        }
        public void Eingabe()
        {
            Console.Write("Temperatur in °C: ");
            if (double.TryParse(Console.ReadLine(), out double temp))
            {
                Temperature = temp;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe für Temperatur.");
                return;
            }
            Console.Write("Luftfeuchtigkeit in %: ");
            if (double.TryParse(Console.ReadLine(), out double hum))
            {
                Humidity = hum;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe für Luftfeuchtigkeit.");
                return;
            }
        }
    }
}
