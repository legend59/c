

# c
## FileIO

 * [AllDirectories - ���� ��� ���� ��ȸ](./FileIO/AllDirectories/Program.cs)
 * [CopyAllFiles - ���� ����](./FileIO/CopyAllFiles/FileIO.cs)

## DS(Data Structure)

 * [List](./DS/List/ListPrac.cs)
 * [Queue](./DS/Queue/MsgQueue.cs)
 * [Queue](./DS/Queue/Program.cs)

### ����
```cs
List <string> li = new List<string>();

// #1
li.Sort();  // ���� ��������

// #2 delegate
li.Sort(delegate (string x, stringy) {
    return y.CompareTo(x);  // ��������
});

// #3 ���ٽ�
li.Sort((string x, string y) => x.CompareTo(y));
```

### Map
| ���� | ���� |
|----|----|
| SortedDictionary | Key�� �����Ͽ� ���� |
| ConcurrentDictionary | Thread-safe�ϰ� ��� |

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
 * [����ũ : MS - httpclient](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http.httpclient)

### Server
 * [MyServer](./HTTP/http_server/MyServer.cs)
 * [Program](./HTTP/http_server/Program.cs)
 * [����ũ : MS - http](https://docs.microsoft.com/ko-kr/dotnet/api/system.net.http)

## JSON

 * [JSON to File](./JSON/json_to_file.cs)

## ��Ÿ
### ���ø����̼� ����

```cs
Environment.Exit(0);

// �Ǵ�

Application.Exit();
```