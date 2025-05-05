
#region Создание задач

//Task t1 = new Task(() => Console.WriteLine("Vasia"));
//t1.Start();

//Task t2 = Task.Factory.StartNew(() => Console.WriteLine("Petya"));

//Task t3 = Task.Run(() => Console.WriteLine("Dima"));

//t1.Wait();
//t2.Wait();
//t3.Wait();

#endregion


#region Синхронное выполнение Task'a

//Task t = new Task(() =>
//{
//    Console.WriteLine("start");
//    Thread.Sleep(1000);
//    Console.WriteLine("end");
//});

//// t.Start();               // async call   !!!!
//t.RunSynchronously();       // sync call    !!!!


//Console.WriteLine("Main end");
//Console.ReadLine();

#endregion


#region Состояние задачи

//Task t = new Task(() =>
//{
//    Console.WriteLine("start");
//    Thread.Sleep(1000);
//    Console.WriteLine("end");
//});

//t.Start();

//Console.WriteLine(t.Id);
//Console.WriteLine(t.Status);
//Console.WriteLine(t.IsCompleted);

//t.Wait();

#endregion


#region Вложенные задачи

//Task t1 = new Task(() =>
//{
//    Console.WriteLine("t1 started");

//    Task t2 = new Task(() =>
//    {
//        Console.WriteLine("t2 started");
//        Thread.Sleep(3000);
//        Console.WriteLine("t2 finished");
//    }, TaskCreationOptions.AttachedToParent);

//    t2.Start();

//    Console.WriteLine("t1 finished");
//});

//t1.Start();
//t1.Wait();

//Console.WriteLine("Main finished");

#endregion


#region Массив задач

//long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

//List<Task> tasks = new List<Task>()
//{
//    new Task(() =>
//    {
//        Thread.Sleep(1000);
//        Console.WriteLine("Task 1000ms finished");
//    }),
//    new Task(() =>
//    {
//        Thread.Sleep(1200);
//        Console.WriteLine("Task 1200ms finished");
//    }),
//    new Task(() =>
//    {
//        Thread.Sleep(2000);
//        Console.WriteLine("Task 2000ms finished");
//    }),
//};

//tasks.ForEach(t => t.Start());

////tasks[0].Wait();
////tasks[1].Wait();
////tasks[2].Wait();

//// Task.WaitAll(tasks);
//Task.WaitAny(tasks.ToArray());

//Console.WriteLine($"Result time = {DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - time}");

//Console.WriteLine("Main finished");

#endregion


#region Возврат результата

//Task<int> t = new Task<int>(() =>
//{
//    Thread.Sleep(1000);
//    return 10;
//});

//t.Start();
//int result = t.Result;          // BLOCKING
//Console.WriteLine(result);

//Console.WriteLine("Main finished");




//int Sum(int a, int b) => a + b;

//Task<int> t = new Task<int>(() => Sum(3, 4));
//t.Start();

//Console.WriteLine(t.Result);            // BLOCKING




//Task<Thread> t = new Task<Thread>(() =>
//{
//    Thread thread = new Thread(() => Console.WriteLine("test"));

//    return thread;
//});

//t.Start();
////
////
//Thread th = t.Result;               // BLOCKING
//th.Start();




//Task<Task<int>> t = new Task<Task<int>>(() =>
//{
//    Task<int> task = new Task<int>(() => 43);

//    task.Start();

//    return task;
//});

//t.Start();
////
////
//Task<int> result = t.Result;            // BLOCKING
//Console.WriteLine(result.Result);

#endregion


#region Цепочка тасков

//Task<int> t = new Task<int>(() =>
//{
//    Task<int> t1 = new Task<int>(() => 5);
//    t1.Start();
//    //
//    //
//    int result = t1.Result;

//    Task<int> t2 = new Task<int>(() => result * result);
//    t2.Start();
//    //
//    //

//    return t2.Result;
//});

//t.Start();
////
////
//Console.WriteLine(t.Result);





//Task t1 = new Task(() => Console.WriteLine("start task"));

//Task t2 = t1.ContinueWith(t => 
//{
//    Console.WriteLine(t.Id);
//    Console.WriteLine(Task.CurrentId);
//});

//t1.Start();
//t2.Wait();



//Task t1 = new Task(() => Console.WriteLine("start task"));

//Task chain = t1
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id))
//    .ContinueWith(t => Console.WriteLine(t.Id));
//t1.Start();
////
////
//chain.Wait();










//using System.Text;

//Photo photo = new Photo("bali.jpg");

//Task<Photo> t1 = new Task<Photo>(() =>
//{
//    lock(photo)
//        photo.Filters.Add("filter_1");

//    return photo;
//});

//Task<Photo> result = t1
//    .ContinueWith(t =>
//    {
//        Photo ph = t.Result;
//        lock (photo)
//            ph.Filters.Add("filter_2");

//        return ph;
//    })
//    .ContinueWith(t =>
//    {
//        Photo ph = t.Result;
//        lock (photo)
//            ph.Filters.Add("filter_3");

//        return ph;
//    });

//t1.Start();
//Photo res = result.Result;
//Console.WriteLine(res);


//class Photo
//{
//    public string Title { get; set; }
//    public List<string> Filters { get; set; } = new List<string>();

//    public Photo(string title) => Title = title;
//    public override string ToString()
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append($"{Title}:\n");
//        Filters.ForEach(f => sb.Append($"{f}, "));

//        return sb.ToString();
//    }
//}

#endregion


// ====================== Parallel ============================

#region Invoke()

//Parallel.Invoke
//(
//    TestPrint,
//    () => Console.WriteLine("test lambda"),
//    () => Sum(3, 4)
//);

//void TestPrint()
//{
//    Thread.Sleep(2000);
//    Console.WriteLine("Test print");
//}

//int Sum(int a, int b)
//{
//    Console.WriteLine($"Sum");
//    return a + b;
//}

//Console.ReadLine();

#endregion


#region For()

//for (int i = 0; i < 10; ++i)
//{
//    int n = i;
//    _ = Task.Run(() => { Console.WriteLine($"{n * n}"); });
//}

//Console.ReadLine();

//Parallel.For(0, 10, n => Console.WriteLine($"{n * n}"));

#endregion


#region ForEach()

ThreadPool.SetMinThreads(10, 2);

long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

List<int> nums = new List<int>() { 3, 5, 6, 1, 8, 4, 9, 10, 1, 5 };

Parallel.ForEach(nums, n =>
{
    Thread.Sleep(100);
    Console.WriteLine(n * n);
});

Console.WriteLine($"Result time = {DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - time}");

#endregion






