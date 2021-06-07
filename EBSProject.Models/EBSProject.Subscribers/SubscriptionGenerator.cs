using System;
using System.Collections.Generic;
using System.Text;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSProject.Subscribers
{
    public class SubscriptionGenerator
    {

        static Random random = new Random();

        static string[] template = { "StationId", "City", "Temp", "Rain", "Wind", "Direction", "Date" };
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

        public static string getRandomCity()
        {

            return cities[random.Next(cities.Length)];
        }


        public static int getRandomStationId()
        {
            return random.Next(stationIdLimit[0], stationIdLimit[1]);
        }


        public static int getRandomTemp()
        {
            return random.Next(tempLimit[0], tempLimit[1]);
        }


        public static double getRandomRain()
        {
            return Math.Round(random.NextDouble(), 2);
        }


        public static int getRandomWind()
        {
            return random.Next(windLimit[0], windLimit[1]);
        }


        public static string getRandomDirection()
        {
            return direction[random.Next(direction.Length)];
        }


        public static DateTime getRandomDate()
        {
            return DateTime.Now.AddDays(random.Next(daysLimit[0], daysLimit[1])).Date;
        }

        // Generates the subs with random values for the fields
        public static List<Subscription> GenerateSubscriptions(int numberOfSub)
        {

            List<Subscription> subscriptions = new List<Subscription>();
            for (int index = 0; index < numberOfSub; index++)
            {
                Subscription sub = new Subscription();
                sub.IsComplex = false;
                sub.Conditions = new List<Condition>();
                int numberOfConditions = random.Next(1, template.Length - 1);

                    Condition conditionStationId = new Condition()
                    {
                        Field = template[0],
                        Value = getRandomStationId().ToString(),
                        Operator = operators[1][random.Next(3)]
                    };

                    Condition conditionCity = new Condition()
                    {
                        Field = template[1],
                        Value = getRandomCity(),
                        Operator = operators[0][random.Next(2)]
                    };

                    Condition conditionTemp = new Condition()
                    {
                        Field = template[2],
                        Value = getRandomTemp().ToString(),
                        Operator = operators[0][random.Next(2)]
                    };

                    Condition conditionRain = new Condition()
                    {
                        Field = template[3],
                        Value = getRandomRain().ToString(),
                        Operator = operators[0][random.Next(2)]
                    };

                    Condition conditionWind = new Condition()
                    {
                        Field = template[4],
                        Value = getRandomWind().ToString(),
                        Operator = operators[0][random.Next(2)]
                    };

                    Condition conditionDirection = new Condition()
                    {
                        Field = template[5],
                        Value = getRandomDirection(),
                        Operator = operators[0][random.Next(2)]
                    };

                    Condition conditionDate = new Condition()
                    {
                        Field = template[6],
                        Value = getRandomDate().ToString(),
                        Operator = operators[1][random.Next(3)]
                    };

                    List<Condition> possibleConditions = new List<Condition>();
                    possibleConditions.Add(conditionStationId);
                    possibleConditions.Add(conditionCity);
                    possibleConditions.Add(conditionTemp);
                    possibleConditions.Add(conditionRain);
                    possibleConditions.Add(conditionWind);
                    possibleConditions.Add(conditionDirection);
                    possibleConditions.Add(conditionDate);

                while (numberOfConditions != 0)
                {
                   int indexCond = random.Next(possibleConditions.Count);
                    sub.Conditions.Add(possibleConditions[indexCond]);
                    possibleConditions.RemoveAt(indexCond);
                    numberOfConditions--;
                }
              //  Console.WriteLine(sub.ToString());
                subscriptions.Add(sub);
            }
            return subscriptions;
        }

    }
}
