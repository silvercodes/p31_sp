
// 1. Асинхронный метод
// Возврат
//      Task
//      Task<T>
//      void            // :-(
//      ValueTask
//      ValueTask<T>
//      IAsyncEnumerable<T>
//      IAsyncEnumerator<T>

// 2.
// async / await

// 3. ...Async() {}


//async Task MethodAsync()
//{
//    Console.WriteLine("Before call");

//    //Task t = new Task(() => Thread.Sleep(1000));
//    //t.Start();
//    // >>>> EQUALS <<<<<
//    Task t = Task.Delay(1000);

//    Console.WriteLine("After call");

//    // t.Wait();                   // BLOCKING

//    await t;

//    Console.WriteLine("After Wait()");
//}

//Task t = MethodAsync();

//Console.WriteLine("End Main");

//Console.ReadLine();








//Console.WriteLine("one");

////Task t = new Task(() => Thread.Sleep(3000));
////t.Start();
//// >>>> EQUALS <<<<<
//Task t = Task.Delay(3000);

//Console.WriteLine("two");





//async Task RenderAsync()
//{
//    await Task.Delay(3000);
//    Console.WriteLine("test");
//}

//Task t = RenderAsync();
////
////
////
////
//t.Wait();
//await t;






//async Task<int> SquareAsync(int n)
//{
//    await Task.Delay(1000);

//    return n * n;
//}

//Task<int> t = SquareAsync(11);

////
////

//int res = await t;
//Console.WriteLine(res);







//async Task RenderAsync(string message)
//{
//    Task t = Task.Delay(1000); // new Task(() => Thread.Sleep(1000)).Start();
//    await t;
//    Console.WriteLine(message);
//}

//Task t1 = RenderAsync("Vasia");
//Task t2 = RenderAsync("Petya");
//Task t3 = RenderAsync("Dima");

//// Task t = Task.WhenAll(t1, t2, t3);
//Task t = Task.WhenAny(t1, t2, t3);

//await t;







//async Task<bool> ValidateAsync(string str)
//{
//    await Task.Delay(1000);

//    if (str.Length < 5)
//        throw new ArgumentException("Invlaid string");

//    return true;
//}

//try
//{
//    await ValidateAsync("vasiaaaa");
//    await ValidateAsync("vas");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);

//}






// ============================================

//Task DoWork()
//{
//    Console.WriteLine("Begin");

//    Task t = new Task(() => Thread.Sleep(2000));
//    Task res = t.ContinueWith(t =>
//    {
//        Console.WriteLine("End");
//    });

//    t.Start();

//    return res;
//}

//Task t = DoWork();
//
//
//

//t.Wait();



//async Task DoWorkAsync()
//{
//    Console.WriteLine("Begin");
//    await Task.Delay(2000);
//    Console.WriteLine("End");
//}
//Task t = DoWorkAsync();
////
////
////
//await t;




// ============================================

//Task<string> ReadFile()
//{
//    Task<string> t = File.ReadAllTextAsync("data.txt");

//    return t.ContinueWith(t =>
//    {
//        if (t.IsFaulted)
//            throw t.Exception;

//        return t.Result.ToUpper();
//    });
//}

//Task<string> t = ReadFile();
////
////
//string res = t.Result;
//Console.WriteLine(res);



//async Task<string> ReadFileAsync()
//{
//    string content = await File.ReadAllTextAsync("data.txt");

//    return content.ToUpper();
//}

//Console.WriteLine(await ReadFileAsync());





// ============================================


async Task<string> GetPageAsync(string url)
{
    using HttpClient client = new HttpClient();

    string content = await client.GetStringAsync(url);

    return content;
}

Console.WriteLine(await GetPageAsync(@"https://google.com"));
