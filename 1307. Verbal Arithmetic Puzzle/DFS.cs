public class Solution
{
    private string[] words;

    private string result;

    private ISet<char> leadingLetters = new HashSet<char>();

    public bool IsSolvable(string[] words, string result)
    {
        this.words = words;
        this.result = result;

        var set = new HashSet<char>(result);
        int maxWordLength = 0;

        for (int i = 0; i < words.Length; i++)
        {
            var word = words[i];

            if (word.Length > 1)
            {
                this.leadingLetters.Add(word[0]);
            }

            if (word.Length > result.Length)
            {
                return false;
            }

            maxWordLength = Math.Max(maxWordLength, word.Length);

            for (int j = 0; j < word.Length; j++)
            {
                set.Add(word[j]);
            }
        }

        if (set.Count > 10)
        {
            return false;
        }

        if (result.Length - maxWordLength > 1)
        {
            return false;
        }

        var map = Enumerable.Repeat(-1, 128).ToArray();
        var digits = new bool[10];

        bool isSolvable = this.Dfs(map, digits, 0, 0, 0);

        return isSolvable;
    }

    private bool Dfs(int[] map, bool[] digits, int m, int n, int sum)
    {
        if (n >= this.result.Length)
        {
            return sum == 0 && map[this.result[0]] != 0;
        }

        if (m >= this.words.Length)
        {
            char r = this.result[this.result.Length - n - 1];
            int d = sum % 10;

            if (map[r] == d)
            {
                return this.Dfs(map, digits, 0, n + 1, sum / 10);
            }
            else if (map[r] == -1)
            {
                if (digits[d])
                {
                    return false;
                }

                map[r] = d;
                digits[d] = true;

                bool isSolvable = this.Dfs(map, digits, 0, n + 1, sum / 10);
                
                if (isSolvable)
                {
                    return true;
                }

                map[r] = -1;
                digits[d] = false;

                return false;
            }
            else
            {
                return false;
            }
        }

        string word = this.words[m];

        if (n >= word.Length)
        {
            return this.Dfs(map, digits, m + 1, n, sum);
        }

        char c = word[word.Length - n - 1];

        if (map[c] != -1)
        {
            return this.Dfs(map, digits, m + 1, n, sum + map[c]);
        }

	int start = this.leadingLetters.Contains(c) ? 1 : 0;

        for (int i = start; i < 10; i++)
        {
            if (digits[i])
            {
                continue;
            }

            map[c] = i;
            digits[i] = true;

            if (this.Dfs(map, digits, m + 1, n, sum + i))
            {
                return true;
            }
            
            map[c] = -1;
            digits[i] = false;
        }

        return false;
    }
}