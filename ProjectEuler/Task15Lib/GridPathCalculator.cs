using System.Numerics;

namespace Task15Lib;

internal class GridPathCalculator
{
    internal static BigInteger CountPathsBigInteger(int rows, int cols)
    {
        var dp = new BigInteger[cols + 1];

        for (var col = 0; col <= cols; col++)
        {
            dp[col] = BigInteger.One;
        }

        for (var row = 1; row <= rows; row++)
        {
            for (var col = 1; col <= cols; col++)
            {
                dp[col] = dp[col] + dp[col - 1];
            }
        }

        return dp[cols];
    }
}