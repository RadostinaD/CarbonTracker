using System;
using System.Collections.Generic;
using System.Text;
using CarbonTracker.Models;

namespace CarbonTracker.Services
{
    public static class CarbonCalculator
    {
        public static double GetEmissionFactor(EmissionSource category)
        {
            switch (category)
            {
                case EmissionSource.Car: return 0.21;
                case EmissionSource.Bus: return 0.10;
                case EmissionSource.Electricity: return 0.38;
                case EmissionSource.Meat: return 5.00;
                case EmissionSource.Vegetarian: return 2.00;
                default: return 0.00;

            }
        }

        public static double CalculateEmission(double amount, double emissionFactor)
        {
            return Math.Round((amount * emissionFactor), 2);       
        }

        public static double CalculateTotalForStructList(List<CarbonActivityStruct> activities)
        {
            double sum = 0;

            for (int i = 0; i < activities.Count; i++)
            {
                sum += activities[i].TotalEmission;
            }

            return sum;
        }

        public static double CalculateTotalForClassList(List<CarbonActivityClass> activities)
        {
            double sum = 0;

            for (int i = 0; i < activities.Count; i++)
            {
                sum += activities[i].TotalEmission;
            }

            return sum;
        }
    }
}
