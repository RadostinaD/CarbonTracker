using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Packaging;
using System.Text;
using CarbonTracker.Models;

namespace CarbonTracker.Services
{
    public class ComparisonService
    {
        public string RunCopyTest(CarbonActivityStruct struct1, CarbonActivityClass class1)
        {
            StringBuilder sb = new StringBuilder();

            CarbonActivityStruct struct2 = struct1;
            struct2.Amount = 50;


            sb.AppendLine("=== COPY TEST ===");
            sb.AppendLine("STRUCT:");
            sb.AppendLine("Original struct:");
            sb.AppendLine(struct1.ToString());
            sb.AppendLine("Copied and modified struct:");
            sb.AppendLine(struct2.ToString());
            sb.AppendLine("Result: the original struct does NOT change.");
            sb.AppendLine();

            CarbonActivityClass class2 = class1;
            class2.Amount = 50;

            sb.AppendLine("CLASS:");
            sb.AppendLine("Original class:");
            sb.AppendLine(class1.ToString());
            sb.AppendLine("Copied and modified class:");
            sb.AppendLine(class2.ToString());
            sb.AppendLine("Result: the original class DOES change because both variables reference the same object.");

            return sb.ToString();


        }

        public string RunMethodTest(CarbonActivityStruct carbonActivityStruct, CarbonActivityClass carbonActivityClass)
        {
            StringBuilder sb = new StringBuilder();

            ModifyStruct(carbonActivityStruct);
            ModifyClass(carbonActivityClass);


            sb.AppendLine("=== METHOD TEST ===");
            sb.AppendLine("STRUCT after method call:");
            sb.AppendLine(carbonActivityStruct.ToString());
            sb.AppendLine("Result: the struct remains unchanged outside the method.");
            sb.AppendLine();

            sb.AppendLine("CLASS after method call:");
            sb.AppendLine(carbonActivityClass.ToString());
            sb.AppendLine("Result: the class object is changed outside the method.");

            return sb.ToString();

        }

        public string RunPerformanceTest()
        {
            StringBuilder sb = new StringBuilder();

            int[] testSizes = { 10000, 50000, 100000 };
            DateTime testDate = DateTime.Today;

            sb.AppendLine("=== Performance Test ===\n");

            foreach (int count in testSizes)
            {
                List<CarbonActivityStruct> structList = new List<CarbonActivityStruct>();
                List<CarbonActivityClass> classList = new List<CarbonActivityClass>();

                Stopwatch stopwatch = new Stopwatch();

                // STRUCT TEST
                stopwatch.Start();
                for (int i = 0; i < count; i++)
                {
                    structList.Add(new CarbonActivityStruct(EmissionSource.Electricity, 5, 0.38, testDate));
                }

                double structTotal = CarbonCalculator.CalculateTotalForStructList(structList);
                stopwatch.Stop();
                long structTime = stopwatch.ElapsedMilliseconds;

                stopwatch.Reset();

                // CLASS TEST
                stopwatch.Start();
                for (int i = 0; i < count; i++)
                {
                    classList.Add(new CarbonActivityClass(EmissionSource.Electricity, 5, 0.38, testDate));
                }

                double classTotal = CarbonCalculator.CalculateTotalForClassList(classList);
                stopwatch.Stop();
                long classTime = stopwatch.ElapsedMilliseconds;

                sb.AppendLine($"Number of generated activities: {count}\n");

                sb.AppendLine("STRUCT:");
                sb.AppendLine($"Total emission: {structTotal:F2} kg CO2");
                sb.AppendLine($"Execution time: {structTime} ms\n");

                sb.AppendLine("CLASS:");
                sb.AppendLine($"Total emission: {classTotal:F2} kg CO2");
                sb.AppendLine($"Execution time: {classTime} ms");

                sb.AppendLine("---------------------------------\n");
            }

            return sb.ToString();
        }

        private void ModifyStruct(CarbonActivityStruct activity)
        {
            activity.Amount = 100;
        }

        private void ModifyClass(CarbonActivityClass activity)
        {
            activity.Amount = 100;
        }

    }
}
