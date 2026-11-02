using System.Collections.Generic;
using UnityEngine;

namespace Tantya.Utils
{
    public class HoltForecastTest : MonoBehaviour
    {
        private HoltForecaster forecaster;

        private const float ALPHA_TEST = 0.7f;
        private const float BETA_TEST = 0.3f;

        public static readonly List<float> ConstantData = new List<float>
        {
            10f, 10f, 10f, 10f, 10f
        };

        public static readonly List<float> IncreasingData = new List<float>
        {
            10f, 12f, 14f, 16f, 18f
        };

        public static readonly List<float> DecreasingData = new List<float>
        {
            10f, 8f, 6f, 4f, 2f
        };

        public static readonly List<float> FluctuatingData = new List<float>
        {
            10f, 15f, 5f, 20f, 0f
        };

        private void Start()
        {
            forecaster = new HoltForecaster(ALPHA_TEST, BETA_TEST);

            TestConstantDemand();
            TestIncreasingDemand();
            TestDecreasingDemand();
            TestFluctuatingDemand();
            TestCustomInitialTrend();
        }

        private void TestConstantDemand()
        {
            RunTest("CONSTANT DEMAND", ConstantData);
        }

        private void TestIncreasingDemand()
        {
            RunTest("INCREASING DEMAND", IncreasingData);
        }

        private void TestDecreasingDemand()
        {
            RunTest("DECREASING DEMAND", DecreasingData);
        }

        private void TestFluctuatingDemand()
        {
            RunTest("FLUCTUATING DEMAND", FluctuatingData);
        }

        private void TestCustomInitialTrend()
        {
            forecaster.Reset();
            forecaster.Initialize(10f, 2f);

            Debug.Log("=== CUSTOM INITIAL TREND ===");
            Debug.Log($"Level: {forecaster.Level:F2}");
            Debug.Log($"Trend: {forecaster.Trend:F2}");
            Debug.Log($"Next forecast: {forecaster.Predict(1):F2}");
        }

        private void RunTest(string testName, List<float> data)
        {
            forecaster.Reset();

            MeanAbsoluteTest mae = new MeanAbsoluteTest();

            Debug.Log($"========== {testName} ==========");

            for (int i = 0; i < data.Count - 1; i++)
            {
                float currentObservation = data[i];
                forecaster.AddObservation(currentObservation);

                // Predict the next observation.
                float predicted = forecaster.Predict(1);

                // Get the next actual observation.
                float actual = data[i + 1];

                // Accumulate the absolute error.
                mae.AddError(actual, predicted);

                Debug.Log(
                    $"Current: {currentObservation:F2} | " +
                    $"Predicted: {predicted:F2} | " +
                    $"Actual Next: {actual:F2} | " +
                    $"MAE So Far: {mae.GetMeanAbsoluteError():F2}"
                );
            }

            Debug.Log($"FINAL MAE for {testName}: {mae.GetMeanAbsoluteError():F2}");
        }
    }
}