namespace PSD_Csharp.Assignment_05;

public class Exercise5_1
{
    public static int[] merge(int[] xs, int[] ys)
    {
        int sum = (xs.Length + ys.Length);
        int[] newArr = new int[sum];

        for (int i = 0; i < xs.Length; i++)
        {
            newArr[i] = xs[i];
        };

        int n = xs.Length;
        for (int i = 0; n < sum; i++)
        {
            newArr[n++] = ys[i];
        }

        Array.Sort(newArr);
        
        return newArr;
    }
}