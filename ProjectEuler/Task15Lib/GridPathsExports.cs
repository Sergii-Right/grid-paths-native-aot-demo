using System.Runtime.InteropServices;
using System.Text;

namespace Task15Lib;

public static class GridPathsExports
{
    [UnmanagedCallersOnly(EntryPoint = "GetRectGridPathCount")]
    public static ulong GetGridPathCount(int rows, int cols)
    {
        if (rows < 0 || cols < 0)
            return 0;

        var res = GridPathCalculator.CountPathsBigInteger(rows, cols);

        if (res > ulong.MaxValue)
            throw new OverflowException();

        return (ulong)res;
    }

    [UnmanagedCallersOnly(EntryPoint = "GetRectGridPathCountText")]
    public static int GetRectGridPathCountText(int rows, int cols, IntPtr buffer, int bufferSize)
    {
        if (rows < 0 || cols < 0)
            return -1;

        if (buffer == IntPtr.Zero || bufferSize <= 0)
            return -2;

        string result;

        try
        {
            result = GridPathCalculator.CountPathsBigInteger(rows, cols).ToString();
        }
        catch
        {
            return -3;
        }

        byte[] bytes = Encoding.ASCII.GetBytes(result);
        int requiredSize = bytes.Length + 1;

        if (bufferSize < requiredSize)
            return requiredSize;

        Marshal.Copy(bytes, 0, buffer, bytes.Length);
        Marshal.WriteByte(buffer, bytes.Length, 0);

        return bytes.Length;
    }
}