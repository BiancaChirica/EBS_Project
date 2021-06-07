using System;
using System.Collections.Generic;
using System.Text;

namespace EBSPProject.Brokers
{
    public  static class Comparators
    {
        public static bool CompareString(string fieldValue,string subscriptionValue,string operatorUsed)
        {
            if (operatorUsed == "=")
            {
                return fieldValue == subscriptionValue;
            }
            else
            {
                return fieldValue != subscriptionValue;
            }
        }

        public static bool CompareInt(int fieldValue, int subscriptionValue, string operatorUsed)
        {
            switch (operatorUsed)
            {
                case "=":
                    return fieldValue == subscriptionValue;
                case "!=":
                    return fieldValue != subscriptionValue;
                case ">=":
                    return fieldValue >= subscriptionValue;
                case ">":
                    return fieldValue > subscriptionValue;
                case "<=":
                    return fieldValue <= subscriptionValue;
                case "<":
                    return fieldValue < subscriptionValue;
                default:
                    return false;

            }
        }

        public static bool CompareDouble(double fieldValue, double subscriptionValue, string operatorUsed)
        {
            switch (operatorUsed)
            {
                case "=":
                    return fieldValue == subscriptionValue;
                case "!=":
                    return fieldValue != subscriptionValue;
                case ">=":
                    return fieldValue >= subscriptionValue;
                case ">":
                    return fieldValue > subscriptionValue;
                case "<=":
                    return fieldValue <= subscriptionValue;
                case "<":
                    return fieldValue < subscriptionValue;
                default:
                    return false;

            }
        }

        public static bool CompareDate(DateTime fieldValue, DateTime subscriptionValue, string operatorUsed)
        {
            switch (operatorUsed)
            {
                case "=":
                    return fieldValue == subscriptionValue;
                case "!=":
                    return fieldValue != subscriptionValue;
                case ">=":
                    return fieldValue >= subscriptionValue;
                case ">":
                    return fieldValue > subscriptionValue;
                case "<=":
                    return fieldValue <= subscriptionValue;
                case "<":
                    return fieldValue < subscriptionValue;
                default:
                    return false;

            }
        }
        public static bool CompareDecimal(decimal fieldValue, decimal subscriptionValue, string operatorUsed)
        {
            switch (operatorUsed)
            {
                case "=":
                    return fieldValue == subscriptionValue;
                case "!=":
                    return fieldValue != subscriptionValue;
                case ">=":
                    return fieldValue >= subscriptionValue;
                case ">":
                    return fieldValue > subscriptionValue;
                case "<=":
                    return fieldValue <= subscriptionValue;
                case "<":
                    return fieldValue < subscriptionValue;
                default:
                    return false;

            }
        }
    }
}
