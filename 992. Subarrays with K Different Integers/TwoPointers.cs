public class Solution
{
    public int SubarraysWithKDistinct(int[] nums, int k)
    {
        int result = 0;

        var counts = new Dictionary<int, int>();
        int left = 0;
        int right = 0;
        int d = 0;

        while (right < nums.Length)
        {
            int n = nums[right];

            if (counts.ContainsKey(n))
            {
                counts[n]++;
            }
            else
            {
                counts[n] = 1;
            }

            if (counts.Count == k)
            {
                result += 1;

                while (left <= right)
                {
                    int m = nums[left];
                    
                    if (counts[m] == 1)
                    {
                        break;
                    }
                    else
                    {
                        counts[m]--;
                    }

                    left++;
                    d++;
                }

                result += d;
            }
            else if (counts.Count > k)
            {
                result += 1;
                d = 0;

                while (counts.Count > k)
                {
                    int m = nums[left];
                    
                    if (counts[m] == 1)
                    {
                        counts.Remove(m);
                    }
                    else
                    {
                        counts[m]--;
                    }

                    left++;
                }

                while (left <= right)
                {
                    int m = nums[left];
                    
                    if (counts[m] == 1)
                    {
                        break;
                    }
                    else
                    {
                        counts[m]--;
                    }

                    left++;
                    d++;
                }

                result += d;
            }

            right++;
        }

        return result;
    }
}
