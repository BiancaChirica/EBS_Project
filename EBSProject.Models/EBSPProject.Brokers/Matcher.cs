using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBSProject.Models;
using EBSProject.Shared;

namespace EBSPProject.Brokers
{
    public class Matcher
    {
        public bool Match(Publication publication, Subscription subscription)
        {
            bool currentComparisonResult = true;
            foreach (var condition in subscription.Conditions)
            {
                switch (condition.Field)
                {
                    case "StationId":
                        currentComparisonResult =
                            Comparators.CompareInt(publication.StationId, int.Parse(condition.Value), condition.Operator);
                        break;
                    case "City":
                        currentComparisonResult =
                            Comparators.CompareString(publication.City, condition.Value, condition.Operator);
                        break;
                    case "Temp":
                        currentComparisonResult =
                            Comparators.CompareInt(publication.Temp, int.Parse(condition.Value), condition.Operator);
                        break;
                    case "Rain":
                        currentComparisonResult =
                            Comparators.CompareDouble(publication.Rain, double.Parse(condition.Value), condition.Operator);
                        break;
                    case "Wind":
                        currentComparisonResult =
                            Comparators.CompareInt(publication.Wind, int.Parse(condition.Value), condition.Operator);
                        break;
                    case "Direction":
                        currentComparisonResult =
                            Comparators.CompareString(publication.Direction, condition.Value, condition.Operator);
                        break;
                    case "Date":
                        currentComparisonResult =
                            Comparators.CompareDate(publication.Date, DateTime.Parse(condition.Value), condition.Operator);
                        break;

                }

                if (currentComparisonResult == false)
                {
                    return false;
                }

            }
            return true;

        }
        public bool MatchComplex(List<Publication> publications, Subscription subscription)
        {
            bool currentComparisonResult = true;
            foreach (var condition in subscription.Conditions)
            {
                switch (condition.Field)
                {
                    case "StationId":
                        currentComparisonResult =
                            publications.All(p => Comparators.CompareInt(p.StationId, int.Parse(condition.Value), condition.Operator));
                        break;
                    case "City":
                        currentComparisonResult =
                            publications.All(p=>Comparators.CompareString(p.City, condition.Value, condition.Operator));
                        break;
                    case "Temp":
                        currentComparisonResult =
                            Comparators.CompareDecimal(Convert.ToDecimal(publications.Average(p=>p.Temp)), decimal.Parse(condition.Value), condition.Operator);
                        break;
                    case "Rain":
                        currentComparisonResult =
                            Comparators.CompareDecimal(Convert.ToDecimal(publications.Average(p => p.Rain)), decimal.Parse(condition.Value), condition.Operator);
                        break;
                    case "Wind":
                        currentComparisonResult =
                            Comparators.CompareDecimal(Convert.ToDecimal(publications.Average(p => p.Wind)), decimal.Parse(condition.Value), condition.Operator);
                        break;
                    case "Direction":
                        currentComparisonResult =
                            publications.All(p => Comparators.CompareString(p.Direction, condition.Value, condition.Operator));
                        break;
                    case "Date":
                        currentComparisonResult =
                            publications.All(p => Comparators.CompareDate(p.Date, DateTime.Parse(condition.Value), condition.Operator));
                        break;

                }

                if (currentComparisonResult == false)
                {
                    return false;
                }

            }
            return true;

        }

    }
}
