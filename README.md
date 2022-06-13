

# c
## FileIO

 * [AllDirectories - 하위 모든 폴더 조회](./FileIO/AllDirectories/Program.cs)
 * [CopyAllFiles - 폴더 복사](./FileIO/CopyAllFiles/FileIO.cs)

## DS(Data Structure)

 * [List](./DS/List/ListPrac.cs)
 * [Queue](./DS/Queue/MsgQueue.cs)
 * [Queue](./DS/Queue/Program.cs)

### Map
```cs
Dictionary <K, V> m = new Dictionary <K, V>();

m.Add(key, value);

foreach(KeyValuePair<K, V> items in m) {
    items.key...
    iteems.value...
}
```

|분류|설명|
|----|----|
| SortedDictionary | Key로 정렬하여 저장 |
| ConcurrentDictionary | Thread-safe하게 사용 |

### 정렬
```cs
List <string> li = new List<string>();

// #1
li.Sort();  // 정렬 오름차순

// #2 delegate
li.Sort(delegate (string x, stringy) {
    return y.CompareTo(x);  // 내림차순
});

// #3 람다식
li.Sort((string x, string y) => x.CompareTo(y));
```

### Map
| 구분 | 내용 |
|----|----|
| SortedDictionary | Key로 정렬하여 저장 |
| ConcurrentDictionary | Thread-safe하게 사용 |

## Thread

 * [add_in_thread](./Thread/add_in_thread.cs)
 * [ThreadSample](./Thread/ThreadSample.cs)

### Mutex
 
 * [MutexSample.cs](./Mutex/MutexSample.cs)

## ProcessExe

 * [ProcessExe](./ProcessExe/ProcessExe.cs)

## Socket Comm.

 * [Date Client](./Socket/Date/DateClient.cs)
 * [Date Server](./Socket/Date/DateServer.cs)
 * [File Client](./Socket/FileSend/SocketClient.cs)
 * [File Server](./Socket/FileSend/SocketServer.cs)

## HTTP Server/Client

### Client
 * [DateClient](./HTTP/http_client/DateClient.cs)
 * [FileClient](./HTTP/http_client/FileClient.cs)
 * [Program](./HTTP/http_client/Program.cs)
 * [참고링크 : MS - httpclient](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http.httpclient)

### Server
 * [MyServer](./HTTP/http_server/MyServer.cs)
 * [Program](./HTTP/http_server/Program.cs)
 * [참고링크 : MS - http](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http)

## JSON

 * [JSON to File](./JSON/json_to_file.cs)
 * [https://devstarsj.github.io/development/2016/06/11/CSharp.NewtonJSON/]

## 기타
### 애플리케이션 종료

```cs
Environment.Exit(0);

// 또는

Application.Exit();
```

### 시간 차이 계산

```cs
string strTime1 = "20220613143610";
string strTime2 = "20220613143720";
DateTime dt1 = DateTime.ParseExact(strTime1, "yyyyMMddHHmmss", null);
DateTime dt2 = DateTime.ParseExact(strTime2, "yyyyMMddHHmmss", null);
TimeSpan ts = dt2 - dt1;
Console.WriteLine(ts.TotalSeconds); // Sec DIfference
```

### 출력

```cs
int a = 14;

// 10진수 4자리
Console.WriteLine(string.Format("0:D4}", a));

// 16진수
Console.WriteLine(string.Format("{0:X2} {1:x2}", a, a));

double b = 12.345678;
// 소수점
Console.WriteLine(string.Format("0:f3}", b));

```

### 스트링 자르기

#### 위치로 자르기
```cs
String strTest = "My book | Your pen | His desk";

Console.WriteLine(strTest.Substring(10)); // 10자리부터 끝까지
// Your pen | His desk
Console.WriteLine(strTest.Substring(10, 8)); // 10자리에서 8자리 이후까지
// Your pen

#### Delimiter 사용

```cs
String strTest = "My book | Your pen | His desk";

strings[] words = strTest.Split(new[] { " | "}, StringSplitOptions.None);

foreach(var item in words)
    Console.WriteLine(item);

// My book
// Your pen
// His desk
```

### string 처리

#### string -> Byte Array

```cs
string strTEst = "ABCD123";
byte[] byteTest = System.Text.Encoding.UTF8.GetBytes(strTest);
foreach (var b in byteTest)
    Console.Write(b + " ");

// 출력
65 66 67 68 49 50 51
```

#### Byte Array -> string

```cs
string strTest2 = System.Text.Encoding.UTG.GetString(byteTest);
Console.WriteLine(strTest2);

// 출력
ABCD123
```
