public class Solution
{
    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int jobCount = startTime.Length;
        var dp = new Dictionary<int, int>();
        
        var indexes = Enumerable.Range(0, jobCount);
        var endOrdered = indexes.OrderBy(i => endTime[i]).ToList();
        var startOrdered = indexes.OrderBy(i => startTime[i]).ToList();
        
        int maxProfit = 0;
        int startIndex = 0;
        
        foreach (int e in endOrdered)
        {   
            int start = startTime[e];
            int end = endTime[e];
            
            while (startIndex < jobCount && startTime[startOrdered[startIndex]] < end)
            {
                dp[startTime[startOrdered[startIndex]]] = maxProfit;
                
                startIndex++;
            }

            maxProfit = Math.Max(maxProfit, dp[start] + profit[e]);            
        }
        
        return maxProfit;
    }
}