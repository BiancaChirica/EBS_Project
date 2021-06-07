using System;
using System.Collections.Generic;
using System.Text;
using EBSProject.Models;

namespace publisher
{
    class PublicationsGenerator
    {
       static Random random = new Random();

       static string[] template = { "stationid", "city", "temp", "rain", "wind", "direction", "date" };
        // domains
        static string[] cities = { "Botosani", "Iasi", "Cluj", "Timisoara", "Bucuresti", "Constanta", "Brasov", "Suceava", "Craiova", "Oradea" };
        static string[] direction = { "N", "S", "E", "V", "NE", "NV", "SE", "SV" };
        static List<List<string>> operators = new List<List<string>>() { new List<string>() { "=", "!=" }, new List<string>() { "=", ">", "<" } };

        // Limits
        static int[] tempLimit = { -30, 30 };
        static int[] rainLimit = { 0, 100 };
        static int[] windLimit = { 0, 30 };
        static int[] daysLimit = { 0, 10 };
        static int[] stationIdLimit = { 0, 100 };

        public static string getRandomCity() {

            return cities[random.Next(cities.Length)];
        }


        public static int getRandomStationId() {
            return random.Next(stationIdLimit[0], stationIdLimit[1]); }


        public static int getRandomTemp() {
            return random.Next(tempLimit[0], tempLimit[1]); }


        public static double getRandomRain() {
            return Math.Round(random.NextDouble(), 2);
        }


        public static int getRandomWind() {
            return random.Next(windLimit[0], windLimit[1]);
        }


        public static string getRandomDirection() {
            return direction[random.Next(direction.Length)];
        }


        public static DateTime getRandomDate() {
            return DateTime.Now.AddDays(random.Next(daysLimit[0], daysLimit[1])).Date;
        }

        // Generates the publications with random values for the fields
         public static List<Publication> GeneratePublications(int numberOfPub) {
      
            List<Publication> publications = new List<Publication>();
            for (int index = 0; index < numberOfPub; index++)
            {
                Publication pub = new Publication()
                {
                    StationId = getRandomStationId(),
                    City = getRandomCity(),
                    Temp = getRandomTemp(),
                    Rain = getRandomRain(),
                    Wind = getRandomWind(),
                    Direction = getRandomDirection(),
                    Date = getRandomDate(),
                    PublicationId = $"{index}_{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")}"

                };

            publications.Add(pub);
            }
            return publications;
        
    }
    }
}
