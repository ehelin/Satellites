using System;
using Microsoft.Owin.Hosting;
using Shared.threading;

namespace Planet
{
    public class Planet : ThreadBase
    {
        private string url = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }

        public Planet(int pX, int pY, string pUrl)
        {
            url = pUrl;
            X = pX;
            Y = pY;
        }

        public override void DoWork()
        {
            Console.WriteLine("Planet is starting...");

            while (!_shouldStop)
            {
                using (WebApp.Start<PlanetStartup>(url))
                {
                    Console.WriteLine("Planet running on {0}...", url);
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Planet stopping...");
        }
    }
}
