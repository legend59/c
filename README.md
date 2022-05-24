

# c
## FileIO

 * [AllDirectories - 하위 모든 폴더 조회](./FileIO/AllDirectories/Program.cs)
 * [CopyAllFiles - 폴더 복사](./FileIO/CopyAllFiles/FileIO.cs)

## DS(Data Structure)

 * [List](./DS/List/ListPrac.cs)
 * [Queue](./DS/Queue/MsgQueue.cs)
 * [Queue](./DS/Queue/Program.cs)

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

## 기타
### 애플리케이션 종료

```cs
Environment.Exit(0);

// 또는

Application.Exit();
```