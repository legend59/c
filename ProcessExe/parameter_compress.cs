using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "C:\\Path\\To\\ExternalProgram.exe";
        string jsonString = "{\"key1\":\"value1\",\"key2\":\"value2\"}";

        // JSON 문자열 압축
        byte[] compressedBytes = CompressString(jsonString);

        // 압축된 데이터를 Base64로 인코딩
        string encodedData = Convert.ToBase64String(compressedBytes);
        
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = filePath;
        startInfo.Arguments = $"\"{encodedData}\""; // 인코딩된 데이터를 인용 부호로 둘러싸서 전달
        
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("외부 프로세스 실행 중 오류가 발생했습니다: " + ex.Message);
        }
    }

    // 문자열을 압축하는 메서드
    static byte[] CompressString(string input)
    {
        byte[] uncompressedBytes = Encoding.UTF8.GetBytes(input);

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
            {
                deflateStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
            }

            return memoryStream.ToArray();
        }
    }

    // 압축된 데이터를 해제하는 메서드
    static string DecompressBytes(byte[] input)
    {
        using (MemoryStream memoryStream = new MemoryStream(input))
        {
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(deflateStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
