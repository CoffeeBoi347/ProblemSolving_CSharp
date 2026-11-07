using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tantya.Utils
{
    public class HoltForecaster 
    {
        private readonly float alpha;
        private readonly float beta;

        private float level;
        private float trend;

        private bool initialized;
        private int observationCount;

        public float Level => level;
        public float Trend => trend;

        public HoltForecaster(float alpha, float beta)
        {
            if (alpha < 0f || alpha > 1f)
                throw new ArgumentOutOfRangeException(
                    nameof(alpha), "Alpha must be between 0 and 1.");

            if (beta < 0f || beta > 1f)
                throw new ArgumentOutOfRangeException(
                    nameof(beta), "Beta must be between 0 and 1.");

            if (alpha + beta > 1f)
                throw new ArgumentException(
                    "Alpha + Beta cannot exceed 1.");

            this.alpha = Mathf.Clamp01(alpha);
            this.beta = Mathf.Clamp01(beta);
        }

        public void AddObservation(float observation)
        {
            observationCount++;
            if (!initialized)
            {
                level = observation;
                trend = 0f;
                initialized = true;
            }
            else
            {
                float previousLevel = level;
                level = alpha * observation + (1 - alpha) * (level + trend); // a * yT + (1-a) * (lT-1 + tT-1)
                trend = beta * (level - previousLevel) + (1 - beta) * trend; // b * (lT - lT-1) + (1 - b) * tT-1
            }
        }

        public void Initialize(float firstDemand, float initTrend)
        {
            level = Mathf.Max(0f, firstDemand);
            trend = initTrend;

            initialized = true;
            observationCount++;
        }

        public float Predict(int stepsAhead)
        {
            if (!initialized || stepsAhead < 1)
                return 0f;

            var forecast = level + stepsAhead * trend; // lT + h * tT
            return Mathf.Max(0f, forecast);
        }

        public void Reset()
        {
            level = 0f;
            trend = 0f;
            initialized = false;
            observationCount = 0;
        }
    }
}