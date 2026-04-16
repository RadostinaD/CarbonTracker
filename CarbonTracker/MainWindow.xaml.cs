using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarbonTracker.Models;
using CarbonTracker.Services;


namespace CarbonTracker
{
    public partial class MainWindow : Window
    {

        List<CarbonActivityClass> classActivities = new List<CarbonActivityClass>();
        List<CarbonActivityStruct> structActivities = new List<CarbonActivityStruct>();

        ComparisonService comparisonService = new ComparisonService();

        public MainWindow()
        {
            InitializeComponent();

            category_box.ItemsSource = Enum.GetValues(typeof(EmissionSource)); 

            category_box.SelectedIndex = 0; // by default
        }

        private void Button_Click_Add_Activity(object sender, RoutedEventArgs e)
        {
            EmissionSource category = (EmissionSource)category_box.SelectedItem;
            double amount = double.Parse(amount_box.Text);  

            double factor = CarbonCalculator.GetEmissionFactor(category);
            DateTime date = DateTime.Now;

            CarbonActivityClass carbonActivityClass = new CarbonActivityClass(category, amount, factor, date);
            CarbonActivityStruct carbonActivityStruct = new CarbonActivityStruct(category, amount, factor, date);

            classActivities.Add(carbonActivityClass);
            structActivities.Add(carbonActivityStruct);

            activities_grid.ItemsSource = null;
            activities_grid.ItemsSource = classActivities;

            factor_block.Text = factor.ToString();

        }

        private void Button_Click_Test_Copy(object sender, RoutedEventArgs e)
        {
            if(structActivities.Count < 1 || classActivities.Count < 1)
            {
                MessageBox.Show("You need at least one activities to make this test.");
                return;
                 
            }

            int id = 0;

            string result = comparisonService.RunCopyTest(structActivities[id], classActivities[id]);
            results_grid.Text = result;

        }

        private void Button_Click_Test_Method(object sender, RoutedEventArgs e)
        {
            if(structActivities.Count < 1 || classActivities.Count < 1)
            {
                MessageBox.Show("You need at least one saved activity to make this test.");
                return;
            }

            int id = 0;
            string result = comparisonService.RunMethodTest(structActivities[id], classActivities[id]);
            results_grid.Text = result;

          
        }

        private void Button_Click_Test_Large_Data(object sender, RoutedEventArgs e)
        {
            results_grid.Text = comparisonService.RunPerformanceTest();
        }

        // daily amount
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (classActivities == null || classActivities.Count == 0)
            {
                MessageBox.Show("No added activities.",
                                "Daily Emissions Summary",
                                MessageBoxButton.OK,
                                MessageBoxImage.None
                                );
                return;
            }

            int totalRecords = classActivities.Count;
            double totalEmissions = 0;

            CarbonActivityClass topActivity = classActivities[0];

            foreach (var activity in classActivities)
            {
                totalEmissions += activity.TotalEmission;

  
                if (activity.TotalEmission > topActivity.TotalEmission)
                {
                    topActivity = activity;
                }
            }

            double averageEmission = totalEmissions / totalRecords;

            string summaryMessage = $"Total activities: {totalRecords}\n" +
                                    $"Total emissions: {totalEmissions:F2} kg CO2e\n" +
                                    $"Average emission per activity: {averageEmission:F2} kg\n" +
                                    $"Highest emission source: {topActivity.Category} ({topActivity.TotalEmission:F2} kg)\n\n";

            MessageBox.Show(summaryMessage,
                            "Daily Emission Summary",
                            MessageBoxButton.OK,
                            MessageBoxImage.None);

        }
    }
}