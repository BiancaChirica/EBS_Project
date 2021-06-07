using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSProject.Subscribers
{
    public class SubscriptionGenerator
    {
        private static Dictionary<string, int> getRatio(int subscriptionCount, List<int> percentage, List<string> template)
        {
            Dictionary<string, int> ratio = new Dictionary<string, int>();
            for (int index = 0; index < percentage.Count; index++)
            {

                if (percentage[index] != 0)
                {
                    ratio.Add(template[index], (int)((percentage[index] * 1.0 / 100 * subscriptionCount)));
                }
            }

            return ratio;
        }

        private static Dictionary<string, int> recalculateRatio(Dictionary<string, int> repetitionListForFields, string fieldToAdd)
        {
            Dictionary<string, int> recalculatedList = repetitionListForFields;
            recalculatedList[fieldToAdd] = recalculatedList[fieldToAdd] - 1;
            if (recalculatedList[fieldToAdd] == 0)
                recalculatedList.Remove(fieldToAdd);
            return recalculatedList;
        }

        private static Condition getCondition(
            string fieldToAdd,
            Dictionary<string, List<string>> operators,
            List<string> cities,
            int tempMinim,
            int tempMaxim,
            int rainMinim,
            int rainMaxim,
            int windMinim,
            int windMaxim,
            int daysRange,
            int stationidMinim,
            int stationidMaxim,
            List<string> directionList)
        {
            Random random = new Random();
            Condition condition = new Condition();
            condition.Field = fieldToAdd;
            condition.Operator = operators[fieldToAdd][random.Next(operators[fieldToAdd].Count)];

            switch (fieldToAdd)
            {
                case "stationid":
                    condition.Value = random.Next(stationidMinim, stationidMaxim).ToString();
                    break;
                case "city":
                    condition.Value = cities[random.Next(cities.Count)];
                    break;
                case "temp":
                    condition.Value = random.Next(tempMinim, tempMaxim).ToString();
                    break;
                case "rain":
                    condition.Value = random.Next(rainMinim, rainMaxim).ToString();
                    break;
                case "wind":
                    condition.Value = random.Next(windMinim, windMaxim).ToString();
                    break;
                case "direction":
                    condition.Value = directionList[random.Next(directionList.Count)];
                    break;
                case "date":
                    condition.Value = DateTime.Today.AddDays(random.Next(0, daysRange)).ToString("dd-MMM-yy");
                    break;
                default:
                    condition.Value = "0";
                    break;
            }

            return condition;
        }

        public static List<Subscription> GenerateSimpleSubscription(
            int countSub,
            bool isComplex,
            int tempMinim,
            int tempMaxim,
            int rainMinim,
            int rainMaxim,
            int windMinim,
            int windMaxim,
            int daysRange,
            int stationidMinim,
            int stationidMaxim,
            int percentageForEqualForCity,
            Dictionary<string, List<string>> operators,
            List<string> template,
            List<int> percentage,
            List<string> cities,
            List<string> directionList)
        {
            var random = new Random();
            List<Subscription> subscriptionList = new List<Subscription>();
            for (int i = 0; i < countSub; i++)
            {
                Subscription sub = new Subscription();
                sub.IsComplex = isComplex;
                sub.SubscriberTopic = "";
                Condition condition = new Condition();
                sub.Conditions = new List<Condition>();
                subscriptionList.Add(sub);
            }

            Dictionary<string, int> repetitionListForFields = getRatio(countSub, percentage, template);
            List<int> subsWithCity = new List<int>();

            while (repetitionListForFields.Count != 0)
            {
                for (int subIndex = 0; subIndex < countSub; subIndex++)
                {

                    List<string> listOfAvailableFields = new List<string>(repetitionListForFields.Keys);

                    for (int index = 0; index < subscriptionList[subIndex].Conditions.Count; index++)
                        if (listOfAvailableFields.Contains(subscriptionList[subIndex].Conditions[index].Field))
                            listOfAvailableFields.Remove(subscriptionList[subIndex].Conditions[index].Field);

                    if (listOfAvailableFields.Count != 0)
                    {
                        string fieldToAdd = listOfAvailableFields[random.Next(listOfAvailableFields.Count)];

                        subscriptionList[subIndex].Conditions.Add(getCondition(fieldToAdd, operators, cities, tempMinim, tempMaxim, rainMinim, rainMaxim, windMinim, windMaxim, daysRange, stationidMinim, stationidMaxim, directionList));
                        repetitionListForFields = recalculateRatio(repetitionListForFields, fieldToAdd);

                        if (fieldToAdd == "city")
                        {
                            subsWithCity.Add(subIndex);
                        }

                        if (repetitionListForFields.Count == 0)
                            break;
                    }
                }

            }

            subsWithCity = subsWithCity.OrderBy(x => Guid.NewGuid()).ToList();
            int numberOfEqualRepetitionForCity = (int)((percentageForEqualForCity * 1.0 / 100 * subsWithCity.Count));
            for (int i = 0; i < numberOfEqualRepetitionForCity; i++)
            {
                foreach (Condition cond in subscriptionList[subsWithCity[i]].Conditions)
                {
                    if (cond.Field == "city")
                    {
                        cond.Operator = "=";
                    }
                }
            }

            return subscriptionList;
        }


        public static List<Subscription> GenerateSubscriptions(int numberOfSub)
        {
            Random random = new Random();
            int tempMinim = -30;
            int tempMaxim = 30;
            int rainMinim = 0;
            int rainMaxim = 100;
            int windMinim = 0;
            int windMaxim = 30;
            int daysRange = 10;
            int stationidMinim = 0;
            int stationidMaxim = 100;
            int percentageForEqualForCity = 50;

            Dictionary<string, List<string>> operators = new Dictionary<string, List<string>> {
            { "stationid", new List<string>() { "=", "!=" } },
            { "city", new List<string>() { "=", "!=" } },
            { "temp", new List<string>() { "=", ">", "<" } },
            { "rain", new List<string>() { "=", ">", "<" } },
            { "wind", new List<string>() { "=", ">", "<" } },
            { "direction", new List<string>() { "=", "!=" } },
            { "date", new List<string>() { "=", "!=" }
            }
        };
            List<string> template = new List<string>
        {
            "stationid", "city", "temp", "rain", "wind", "direction", "date"
        };

            List<int> percentage = new List<int>
        {
            0, 100, 0, 80, 90, 10, 10
        };

            List<string> cities = new List<string>
        {
            "Botosani", "Iasi", "Cluj", "Timisoara", "Bucuresti", "Constanta", "Brasov", "Suceava", "Craiova", "Oradea"
        };

            List<string> directionList = new List<string>
        {
            "N", "S", "E", "V", "NE", "NV", "SE", "SV"
        };
           
         

            List<Subscription> subscriptions = GenerateSimpleSubscription(
                numberOfSub,
               Convert.ToBoolean(random.Next(2)),
                tempMinim,
                tempMaxim,
                rainMinim,
                rainMaxim,
                windMinim,
                windMaxim,
                daysRange,
                stationidMinim,
                stationidMaxim,
                percentageForEqualForCity,
                operators,
                template,
                percentage,
                cities,
                directionList
                );
            
            return subscriptions;
        }
    }
}
