using System;

namespace WeatherUtils
{
    public class WeatherLib
    {

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }


        public void Eingabe()
        {
            Console.Write("Temperatur in °C: ");
            if (double.TryParse(Console.ReadLine(), out var t))
            {
                Temperature = t;
                Console.Write("Luftfeuchtigkeit in %: ");
                if (double.TryParse(Console.ReadLine(), out var rh))
                {
                    Humidity = rh;
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe für Luftfeuchtigkeit.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe für Temperatur.");
            }
        }


        public double GetDewPoint()
        {

            var rh = Math.Clamp(Humidity, 0.1, 100.0);

            const double a = 17.27;
            const double b = 237.7;
            double gamma = (a * Temperature) / (b + Temperature) + Math.Log(rh / 100.0);
            return (b * gamma) / (a - gamma);
        }


        public double GetWindChill(double windSpeedMs)
        {
            double t = Temperature;
            double vKmh = Math.Max(0.0, windSpeedMs) * 3.6;


            if (t <= 10.0 && vKmh > 4.8)
            {
                double v016 = Math.Pow(vKmh, 0.16);
                double wci = 13.12 + 0.6215 * t - 11.37 * v016 + 0.3965 * t * v016;
                return wci;
            }


            return t;
        }


        public double GetHeatIndex()
        {
            double rh = Math.Clamp(Humidity, 0.0, 100.0);
            double tC = Temperature;


            double tF = tC * 9.0 / 5.0 + 32.0;


            double hiF =
                -42.379 +
                2.04901523 * tF +
                10.14333127 * rh -
                0.22475541 * tF * rh -
                0.00683783 * tF * tF -
                0.05481717 * rh * rh +
                0.00122874 * tF * tF * rh +
                0.00085282 * tF * rh * rh -
                0.00000199 * tF * tF * rh * rh;


            if (rh < 13 && tF >= 80 && tF <= 112)
            {
                hiF -= ((13 - rh) / 4.0) * Math.Sqrt((17 - Math.Abs(tF - 95)) / 17.0);
            }
            else if (rh > 85 && tF >= 80 && tF <= 87)
            {
                hiF += ((rh - 85) / 10.0) * ((87 - tF) / 5.0);
            }


            double hiC = (hiF - 32.0) * 5.0 / 9.0;


            return hiC;
        }

        public double ErmittleWolkenhoehe()
        {
            double td = GetDewPoint();
            double spread = Math.Max(0.0, Temperature - td);
            return 125.0 * spread; // Meter
        }
    }
}