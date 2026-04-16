using CarbonTracker.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarbonTracker.Models
{
    public class CarbonActivityClass
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

        public CarbonActivityClass(EmissionSource category, double amount, double emissionFactor, DateTime date)
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

