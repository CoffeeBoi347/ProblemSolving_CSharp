using UnityEngine;

public class Permutation : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(SolvePermutation(18, 15));    
    }

    public int Factorial(int n)
    {
        if (n < 0)
        {
            Debug.LogError("Factorial is not defined for negative numbers.");
            return -1;
        }
        if (n == 0 || n == 1)
        {
            return 1;
        }
        int result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    public long SolvePermutation(int n, int r)
    {
        return Factorial(n) / Factorial(n - r);
    }
}
