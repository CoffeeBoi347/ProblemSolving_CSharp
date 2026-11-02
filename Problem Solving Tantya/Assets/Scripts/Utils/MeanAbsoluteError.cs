using UnityEngine;

namespace Tantya.Utils
{
    public class MeanAbsoluteTest
    {
        private float totalAbsoluteError;
        private int count;

        public int Count => count;

        public void AddError(float actual, float predicted)
        {
            float error = Mathf.Abs(actual - predicted);
            totalAbsoluteError += error;
            count++;
        }

        public float GetMeanAbsoluteError()
        {
            if (count == 0)
                return 0f;
            return totalAbsoluteError / count;
        }

        public void Reset()
        {
            totalAbsoluteError = 0f;
            count = 0;
        }
    }
}