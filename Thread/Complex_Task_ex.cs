/*
위의 예제에서는 병렬로 여러 웹 페이지의 내용을 가져오고, 각 페이지에서 단어의 출현 빈도를 계산합니다. urls 리스트에 웹 페이지의 URL을 나열하고, GetWebContentAsync() 메서드를 사용하여 각 페이지의 내용을 가져옵니다.

Task.Run()을 사용하여 병렬로 실행되는 작업을 만들고, CalculateWordFrequencies() 메서드를 사용하여 각 페이지의 내용에서 단어 출현 빈도를 계산합니다. 계산된 빈도는 ConcurrentDictionary<string, int>에 저장되어 각 단어의 출현 횟수를 유지합니다.

Task.WhenAll()을 사용하여 모든 작업이 완료될 때까지 대기하고, 결과를 출력합니다. wordFrequencies 딕셔너리에 저장된 단어 출현 빈도를 순회하여 출력합니다.

이 예제는 비동기 작업을 병렬로 실행하고, 결과를 조합하면서 동시성을 다루는 방법을 보여줍니다. ConcurrentDictionary를 사용하여 여러 작업이 동시에 결과를 업데이트할 수 있도록 합니다.

더 복잡한 예제에서는 작업의 취소, 진행률 보고, 에러 처리 등을 추가로 다룰 수 있습니다. 비동기 작업을 처리하는 다양한 기능과 개념을 익히기 위해서는 공식 문서와 관련 자료를 참고하는 것이 좋습니다.
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        List<string> urls = new List<string>
        {
            "https://www.example.com",
            "https://www.example.net",
            "https://www.example.org"
        };

        ConcurrentDictionary<string, int> wordFrequencies = new ConcurrentDictionary<string, int>();

        List<Task> tasks = new List<Task>();
        foreach (string url in urls)
        {
            tasks.Add(Task.Run(async () =>
            {
                string content = await GetWebContentAsync(url);
                Dictionary<string, int> frequencies = CalculateWordFrequencies(content);
                foreach (var pair in frequencies)
                {
                    wordFrequencies.AddOrUpdate(pair.Key, pair.Value, (_, currentCount) => currentCount + pair.Value);
                }
            }));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine("단어 출현 빈도:");
        foreach (var pair in wordFrequencies)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }

    static async Task<string> GetWebContentAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            Console.WriteLine($"웹 페이지 내용을 가져오는 중: {url}");
            string content = await client.GetStringAsync(url);
            return content;
        }
    }

    static Dictionary<string, int> CalculateWordFrequencies(string content)
    {
        Dictionary<string, int> frequencies = new Dictionary<string, int>();
        string[] words = content.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string word in words)
        {
            if (frequencies.ContainsKey(word))
            {
                frequencies[word]++;
            }
            else
            {
                frequencies[word] = 1;
            }
        }

        return frequencies;
    }
}
