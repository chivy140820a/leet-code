using System;
using System.Collections.Generic;
public class Solution
{
    public IList<int> FindSubstring(string s, string[] words)
    {
        var result = new List<int>();

        if (s == null || words == null || words.Length == 0) return result;

        int wordLength = words[0].Length;
        int totalLength = words.Length * wordLength;

        // Tạo dictionary để lưu tần suất từ trong words
        var wordCount = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (!wordCount.ContainsKey(word))
            {
                wordCount[word] = 0;
            }
            wordCount[word]++;
        }

        // Duyệt qua chuỗi với sliding window
        for (int i = 0; i < wordLength; i++)
        {
            int left = i, right = i;
            var currentCount = new Dictionary<string, int>();
            int count = 0; // Số từ hợp lệ hiện tại trong cửa sổ

            while (right + wordLength <= s.Length)
            {
                var word = s.Substring(right, wordLength);
                right += wordLength;

                // Nếu từ nằm trong words
                if (wordCount.ContainsKey(word))
                {
                    if (!currentCount.ContainsKey(word))
                    {
                        currentCount[word] = 0;
                    }
                    currentCount[word]++;

                    if (currentCount[word] <= wordCount[word])
                    {
                        count++;
                    }
                    else
                    {
                        // Nếu tần suất vượt quá, giảm cửa sổ
                        while (currentCount[word] > wordCount[word])
                        {
                            var leftWord = s.Substring(left, wordLength);
                            currentCount[leftWord]--;
                            if (currentCount[leftWord] < wordCount[leftWord])
                            {
                                count--;
                            }
                            left += wordLength;
                        }
                    }

                    // Nếu tất cả các từ hợp lệ, thêm chỉ số bắt đầu
                    if (count == words.Length)
                    {
                        result.Add(left);
                    }
                }
                else
                {
                    // Reset nếu gặp từ không hợp lệ
                    currentCount.Clear();
                    count = 0;
                    left = right;
                }
            }
        }

        return result;
    }
}
