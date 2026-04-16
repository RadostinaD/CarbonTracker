using System;
using System.Collections.Generic;
using System.Text;
using CarbonTracker.Services;

namespace CarbonTracker.Models
{

    public enum EmissionSource
    {
        Car,
        Bus, 
        Electricity, 
        Meat, 
        Vegetarian
    }

    public struct CarbonActivityStruct
    {
        public EmissionSource category;
        public double amount;
        public double emissionFactor;
        public DateTime date;

        public EmissionSource Category
        {
            get { return category; }
            set { category = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public double EmissionFactor
        {
            get { return emissionFactor; }
            set { emissionFactor = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public double TotalEmission
        {
            get { return CarbonCalculator.CalculateEmission(amount, emissionFactor); }
        }

        public CarbonActivityStruct(EmissionSource category, double amount, double emissionFactor, DateTime date)
        {
            this.category = category;
            this.amount = amount;
            this.emissionFactor = emissionFactor;
            this.date = date;

        }

        public override string ToString()
        {
            return $"{category} | {amount:F2} | {emissionFactor:F2} | {TotalEmission:F2} kg CO2 | {date.ToShortDateString()}";
        }

    }
}
